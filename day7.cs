// https://adventofcode.com/2018/day/7

        class Data7
        {
            public List<char> All;
            public Dictionary<char, HashSet<char>> Graph;

            public Data7(string[] lines)
            {
                Graph = new Dictionary<char, HashSet<char>>();

                All = new List<char>();

                foreach (string item in lines)
                {
                    char parent = item[5];
                    char child = item[36];
                    if (parent != child)
                    {
                        HashSet<char> family = null;
                        if (Graph.ContainsKey(child))
                        {
                            family = Graph[child];
                        }
                        else
                        {
                            family = new HashSet<char>();
                            Graph[child] = family;
                        }
                        family.Add(parent); // dependency
                    }
                    if (!All.Contains(parent)) All.Add(parent);
                    if (!All.Contains(child)) All.Add(child);
                }

                All.Sort();
            }

            public List<char> GetReady()
            {
                List<char> ready = new List<char>();
                for (int i = 0; i < All.Count; ++i)
                {
                    char ch = All[i];
                    // If no dependencies then character is ready to process?
                    if (!Graph.ContainsKey(ch) || Graph[ch].Count == 0)
                    {
                        ready.Add(ch);
                    }
                }
                return ready;
            }

            HashSet<char> Actives = new HashSet<char>();

            public void RemoveDepends(char dead)
            {
                foreach (HashSet<char> list in Graph.Values)
                {
                    list.Remove(dead);
                }
            }

            public void Activate(char ch)
            {
                All.Remove(ch);
                Actives.Add(ch);
            }

            public void Finish(char ch)
            {
                if (Actives.Contains(ch))
                {
                    Actives.Remove(ch);
                    RemoveDepends(ch);
                }
            }
        };

        static string Day7a(string[] lines)
        {
            Data7 data = new Data7(lines);

            string result = "";

            for (int i = 0; i < data.All.Count;)
            {
                char ch = data.All[i];
                // If no dependencies then process character?
                if (!data.Graph.ContainsKey(ch) || data.Graph[ch].Count == 0)
                {
                    data.All.Remove(ch);
                    data.RemoveDepends(ch);
                    result += ch;
                    i = 0;
                    continue;
                }
                ++i;
            }

            return result;
        }

        class Elf7
        {
            public char Character;
            public int Delay = 0;
            public bool Idle { get { return Delay <= 0; } }
        }

        static bool IsBusy(Elf7[] elves)
        {
            foreach (Elf7 elf in elves)
            {
                if (!elf.Idle)
                {
                    return true;
                }
            }
            return false;
        }

        static int Day7b(string[] lines, int numElves, int baseTime)
        {
            Data7 data = new Data7(lines);

            List<char> work = data.All;
            Elf7[] elves = new Elf7[numElves];
            for (int i = 0; i < elves.Length; ++i)
            {
                elves[i] = new Elf7();
            }

            int ticks = 0;

            while (work.Count != 0 || IsBusy(elves))
            {
                List<char> ready = data.GetReady();
                if (ready.Count != 0)
                {
                    foreach (Elf7 elf in elves)
                    {
                        if (elf.Idle && ready.Count != 0)
                        {
                            char ch = ready[0];
                            elf.Character = ch;
                            elf.Delay = baseTime + (ch - 'A' + 1);
                            ready.RemoveAt(0);
                            data.Activate(ch);
                        }
                    }
                }
                foreach (Elf7 elf in elves)
                {
                    if (!elf.Idle)
                    {
                        elf.Delay -= 1;

                        if (elf.Delay == 0)
                        {
                            data.Finish(elf.Character);
                        }
                    }
                }
                ++ticks;
            }

            return ticks;
        }
