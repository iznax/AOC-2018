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
    // Todo!
  }
