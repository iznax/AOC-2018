// https://adventofcode.com/2018/day/1

// Input is a sequence of +/- integers on separate lines
// Returns the final frequency from list of change values
int Day1a(string[] lines)
{
  int freq = 0;
  foreach (string num in lines)
  {
    freq += int.Parse(num);
  }
  return freq;
}

// Returns the first intermediate frequency that occurs twice
int Day1b(string[] lines)
{
  HashSet<int> history = new HashSet<int>();
  
  int freq = 0;
  int index = 0;
  int MaxTries = 32*1000*1000;
  
  while (history.Add(freq) && index<MaxTries)
  {
    freq += int.Parse(lines[index++ % lines.Length]);
  }
  
  if (index < MaxTries)
  {
    return freq;
  }
  
  throw new Exception("ERROR: No duplicate frequency found after many tries?");
}
