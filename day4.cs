// https://adventofcode.com/2018/day/4

  static Dictionary<int, int[]> Day4(string[] lines)
	{
		List<string> sorted = new List<string>(lines);
		sorted.Sort();
		int Guard = -1;
		int skip = "[####-##-## 00:00] ".Length;
		int fallTime = 0;
		Dictionary<int, int[]> slots = new Dictionary<int, int[]>();
		foreach (string item in sorted)
		{
			string sub = item.Substring(skip);
			if (sub.StartsWith("Guard"))
			{
				string[] parts = sub.Split(' ');
				Guard = int.Parse(parts[1].Substring(1));
				if (!slots.ContainsKey(Guard))
				{
					slots[Guard] = new int[60];
				}
				fallTime = -1;
			}
			else if (sub.StartsWith("falls"))
			{
				fallTime = int.Parse(item.Substring(15, 2));
			}
			else if (sub.StartsWith("wakes"))
			{
				if (fallTime >= 0)
				{
					int wakeTime = int.Parse(item.Substring(15, 2));
					int[] data = slots[Guard];
					for (int i = fallTime; i < wakeTime; ++i)
					{
						data[i] += 1;
					}
					slots[Guard] = data;
					fallTime = -1;
				}
				else
				{
					throw new Exception("Guard is not asleep?");
				}
			}
			else
			{
				throw new Exception("Unknown event = " + item);
			}
		}
		return slots;
	}

// Choose sleepy guard by total minutes
	static int Day4a(string[] lines)
	{
		Dictionary<int, int[]> slots = Day4(lines);

		int best = -1;
		int total = 0;
		foreach (int g in slots.Keys)
		{
			int[] data = slots[g];
			int count = data.Sum();
			if (count > total)
			{
				best = g;
				total = count;
			}
		}

		int most = 0;
		int minute = -1;
		if (best != -1)
		{
			int[] data = slots[best];
			for (int i = 0; i < data.Length; ++i)
			{
				if (data[i] > most)
				{
					most = data[i];
					minute = i;
				}
			}
		}

		return best * minute; // encode results
	}

// Choose sleepy guard by most minutes
	static int Day4b(string[] lines)
	{
		Dictionary<int, int[]> slots = Day4(lines);

		int best = -1;
		int most = 0;
		int minute = -1;

		foreach (int g in slots.Keys)
		{
			int[] data = slots[g];
			for (int i = 0; i < data.Length; ++i)
			{
				if (data[i] > most)
				{
					best = g;
					most = data[i];
					minute = i;
				}
			}
		}

		return best * minute; // encode results
	}
