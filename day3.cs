// https://adventofcode.com/2018/day/3

  // Returns the number of square inches covered by 2 or more blocks
  static int Day3a(string[] lines)
  {
    HashSet<int> first = new HashSet<int>();
    HashSet<int> results = new HashSet<int>();
    string pattern = @"#(?<id>\d*) \@ (?<x>\d*),(?<y>\d*): (?<w>\d*)x(?<h>\d*)";
    Regex rx = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    foreach (string text in lines)
    {
      Match m = rx.Match(text);
      if (m.Success)
      {
        int x = int.Parse(m.Groups["x"].Value);
        int y = int.Parse(m.Groups["y"].Value);
        int w = int.Parse(m.Groups["w"].Value);
        int h = int.Parse(m.Groups["h"].Value);
        
        for (int iy = 0; iy < h; ++iy)
        {
          for (int ix = 0; ix < w; ++ix)
          {
            int key = (x + ix) + (y + iy) * 1000;
            if (!first.Add(key)) // second or more?
            {
              results.Add(key);
            }
          }
        }
      }
    }
    return results.Count;
  }

  struct Rectangle
  {
    public string id;
    public int x0, y0, x1, y1;

    public bool Overlaps(Rectangle other)
    {
      return (x0 <= other.x1) && (y0 <= other.y1) && (x1 >= other.x0) && (y1 >= other.y0);
    }
   };

  // Returns ID of first box that does not overlap any other boxes
  static string Day3b(string[] lines)
  {
    List<Rectangle> boxes = new List<Rectangle>();
    HashSet<string> all = new HashSet<string>();
    string pattern = @"#(?<id>\d*) \@ (?<x>\d*),(?<y>\d*): (?<w>\d*)x(?<h>\d*)";
    Regex rx = new System.Text.RegularExpressions.Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
    foreach (string text in lines)
    {
      Match m = rx.Match(text);
      if (m.Success)
      {
        Rectangle box = new Rectangle();
        box.id = m.Groups["id"].Value;
        box.x0 = int.Parse(m.Groups["x"].Value);
        box.y0 = int.Parse(m.Groups["y"].Value);
        box.x1 = box.x0 + int.Parse(m.Groups["w"].Value) - 1;
        box.y1 = box.y0 + int.Parse(m.Groups["h"].Value) - 1;
        all.Add(box.id);
        foreach (Rectangle other in boxes)
        {
          if (box.Overlaps(other))
          {
            all.Remove(box.id);
            all.Remove(other.id);
          }
        }
        boxes.Add(box);
      }
    }
    if (all.Count != 0)
    {
      return all.First();
    }
    return null;
  }
