// https://adventofcode.com/2018/day/10

        class Node10
        {
            public int PositionX;
            public int PositionY;
            public int VelocityX;
            public int VelocityY;

            public void Parse(string line)
            {
                string[] parts = line.Split('<', '>', ',');
                PositionX = int.Parse(parts[1]);
                PositionY = int.Parse(parts[2]);
                VelocityX = int.Parse(parts[4]);
                VelocityY = int.Parse(parts[5]);
            }

            public KeyValuePair<int, int> Get(int time)
            {
                int x = PositionX + VelocityX * time;
                int y = PositionY + VelocityY * time;
                return new KeyValuePair<int, int>(x,y);
            }
        };

        class View10
        {
            int X0 = 0;
            int Y0 = 0;
            int X1 = 0;
            int Y1 = 0;

            public int Width { get { return X1 - X0; } }
            public int Height { get { return Y1 - Y0; } }

            List<string> screen = new List<string>();

            public void Reset()
            {
                X0 = int.MaxValue;
                Y0 = int.MaxValue;
                X1 = int.MinValue;
                Y1 = int.MinValue;
                all.Clear();
                screen.Clear();
            }

            List<KeyValuePair<int, int>> all = new List<KeyValuePair<int, int>>();

            public void Insert(KeyValuePair<int, int> p)
            {
                int x = p.Key;
                int y = p.Value;
                X0 = Math.Min(x, X0);
                Y0 = Math.Min(y, Y0);
                X1 = Math.Max(x, X1);
                Y1 = Math.Max(y, Y1);
                all.Add(p);
            }

            void Resolve()
            {
                foreach (KeyValuePair<int, int> it in all)
                {
                    int x = it.Key-X0;
                    int y = it.Value-Y0;
                    Plot(x, y);
                }
            }

            void Plot(int x, int y)
            {
                while (y >= screen.Count)
                {
                    screen.Add("");
                }
                string line = screen[y];
                while (x >= line.Length)
                {
                    line += ".";
                }
                screen[y] = line.Remove(x, 1).Insert(x,"#");
            }

            public void Show()
            {
                screen.Clear();
                Resolve();
                for (int i = 0; i < screen.Count; ++i)
                {
                    System.Console.WriteLine(screen[i]);
                }
            }
        }

        static int Day10(string[] lines)
        {
            int total = 0;
            List<Node10> nodes = new List<Node10>();
            foreach (string it in lines)
            {
                Node10 node = new Node10();
                node.Parse(it);
                nodes.Add(node);
                // ASSUME: the message appears near zero (based on data)
                // Estimate time it takes for signals to reach the origin (0,0)
                int estX = (node.VelocityX != 0) ? -node.PositionX / node.VelocityX : 0;
                int estY = (node.VelocityY != 0) ? -node.PositionY / node.VelocityY : 0;
                total += (estX + estY)/ 2;
            }

            // Start before estimate and display any results that seem promising
            int avgTime = total / nodes.Count;
            int baseTime = avgTime - 10;

            int bestTime = 0;
            int bestHeight = 100;

            for (int t = 0; t < 20; ++t)
            {
                int time = baseTime + t;
                View10 screen = new View10();
                screen.Reset();
                foreach (Node10 n in nodes)
                {
                    KeyValuePair<int, int> p = n.Get(time);
                    screen.Insert(p);
                }
                System.Console.WriteLine("Time={0} width={1} height={2}", time, screen.Width, screen.Height);
                // ASSUME: lettering has a small height based on example text
                if (screen.Width < 250 && screen.Height < 16)
                {
                    screen.Show();
                }
                if (screen.Height < bestHeight)
                {
                    bestTime = time;
                    bestHeight = screen.Height;
                }
            }
            return bestTime;
        }
