// https://adventofcode.com/2018/day/2

  // Returns checksum of ID's with 2x * 3x of duplicate characters
  static int Day2a(string[] lines)
  {
    int twos = 0;
    int threes = 0;
    foreach (string id in lines)
    {
      int[] letters = new int[26];
      for (int i = 0; i < id.Length; ++i)
      {
        int bit = id[i] - 'a';
        if (bit >= 0 && bit <= 25)
        {
          letters[bit] += 1;
        }
        else
        {
          throw new Exception("Invalid letter [" + id[i] + "] in line=" + id);
        }
      }
      int b2 = 0;
      int b3 = 0;
      for (int i = 0; i < letters.Length; ++i)
      {
        if (letters[i] == 2) b2 = 1;
        if (letters[i] == 3) b3 = 1;
      }
      twos += b2;
      threes += b3;
    }
    return twos * threes;
  }

  // Returns the index of first different character or (-1) if none
  static int Diff(string id1, string id2)
  {
    if (id1.Length == id2.Length)
    {
#if false
      for (int i = 0; i < id1.Length; ++i)
      {
        if (id1[i] != id2[i])
        {
          return i;
        }
      }
#else
        return id1.TakeWhile((ch,i) => ch == id2[i]).Count();
#endif
    }
    return -1;
  }

  // Searches for two ID strings with a single character difference
  // Returns the ID without the mis-matched character or null
  static string Day2b(string[] lines)
  {
    for (int i = 0; i < lines.Length-1; ++i)
    {
      string id1 = lines[i];
      for (int j = i+1; j < lines.Length; ++j)
      {
        int pos = Diff(lines[i], lines[j]);
        if (pos >= 0)
        {
          if (lines[i].Substring(pos + 1) == lines[j].Substring(pos + 1))
          {
            return lines[i].Remove(pos, 1);
          }
        }
      }
    }
    return null;
  }
