int Day1a(string[] lines)
{
  int freq = 0;
  foreach (string num in lines)
  {
    freq += int.Parse(num);
  }
  return freq;
}

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
