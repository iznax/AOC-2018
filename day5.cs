// https://adventofcode.com/2018/day/5

  static string Day5_Reduce(string line)
  {
    string work = line;
    int pos = 0;
    while (pos+1 < work.Length)
    {
      // XOR with 0x20 to flip case
      // 'A' = 0x41
      // 'a' = 0x61
      if (work[pos] == (work[pos+1]^0x20))
      {
        work = work.Remove(pos, 2);
        pos = Math.Max(pos - 1, 0);
      }
      else
      {
        pos += 1;
      }
    }
    return work;
  }

  static int Day5a(string line)
  {
    string work = Day5_Reduce(line);
    return work.Length;
  }

  static int Day5b(string line)
  {
    string work = Day5_Reduce(line);
    int best = work.Length;
    for (int i = 0; i < 26; ++i)
    {
      string ch1 = ""+(char)('A'+i);
      string ch2 = ""+(char)('a'+i);
      string temp = work.Replace(ch1,string.Empty).Replace(ch2,string.Empty);
      string small = Day5_Reduce(temp);
      if (small.Length < best)
      {
        best = small.Length;
      }
    }
    return best;
  }
