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
  int MaxTries = 29500123;
  
  while (history.Add(freq) && index<MaxTries)
  {
    freq += int.Parse(lines[index++ % lines.Length]);
  }
  
  if (index < MaxTries)
  {
    return freq;
  }
  
  Console.WriteLine("ERROR: No duplicate found after many tries?");
  return -1;
}
