// https://adventofcode.com/2018/day/11

        class Grid11
        {
            int[,] sum1 = new int[300, 300];

            public void Insert(int i, int j, int value)
            {
                sum1[i, j] = value;
            }

            public int SumXY(int ii, int jj, int xsize, int ysize)
            {
                int total = 0;
                int i1 = ii + xsize;
                int j1 = jj + ysize;
                for (int i = ii; i < i1; ++i)
                {
                    for (int j = jj; j < j1; ++j)
                    {
                        total += sum1[i, j];
                    }
                }
                return total;
            }

            public int FindBiggest(int i, int j, out int bestSize)
            {
                int total = sum1[i, j];
                int best = total;
                bestSize = 1;
                int length = sum1.GetLength(0);
                int max = Math.Min(length - i, length - j);
                for (int sz = 2; sz <= max; ++sz)
                {
                    int last = sz - 1;
                    for (int diag = 0; diag < last; ++diag)
                    {
                        total += sum1[i+last, j+diag];
                        total += sum1[i+diag, j+last];
                    }
                    total += sum1[i+last, j+last];

                    if (total > best)
                    {
                        best = total;
                        bestSize = sz;
                    }
                }
                return best;
            }

            public int Sum(int ii, int jj, int size)
            {
                if (size == 1)
                {
                    return sum1[ii, jj];
                }
                if (size == 3)
                {
                    int a = sum1[ii, jj] + sum1[ii + 1, jj] + sum1[ii + 2, jj];
                    int b = sum1[ii, jj + 1] + sum1[ii + 1, jj + 1] + sum1[ii + 2, jj + 1];
                    int c = sum1[ii, jj + 2] + sum1[ii + 1, jj + 2] + sum1[ii + 2, jj + 2];
                    return a + b + c;
                }
                throw new Exception("Not supported.");
            }
        };

        static Grid11 Day11grid(int serialNumber)
        {
            const int size = 300;
            Grid11 g = new Grid11();
            for (int x = 1; x <= size; ++x)
            {
                for (int y = 1; y <= size; ++y)
                {
                    int rackId = x + 10;
                    int big = (rackId * y + serialNumber) * rackId;
                    int h = (big / 100) % 10;
                    int power = h - 5;
                    g.Insert(x - 1, y - 1, power);
                }
            }
            return g;
        }

        static string Day11a(int serialNumber)
        {
            const int size = 300;
            Grid11 g = Day11grid(serialNumber);

            int bx = 0;
            int by = 0;
            int best = 0;

            for (int x = 1; x <= size-2; ++x)
            {
                for (int y = 1; y <= size-2; ++y)
                {
                    int i = x - 1;
                    int j = y - 1;
                    int sum = g.Sum(i, j, 3);
                    if (sum > best)
                    {
                        best = sum;
                        bx = x;
                        by = y;
                    }
                }
            }
            return string.Format("{0},{1}", bx, by);
        }

        static string Day11b(int serialNumber)
        {
            const int size = 300;
            Grid11 g = Day11grid(serialNumber);

            int bx = 0;
            int by = 0;
            int best = 0;
            int bsize = 0;

            for (int x = 1; x <= size; ++x)
            {
                for (int y = 1; y <= size; ++y)
                {
                    int i = x - 1;
                    int j = y - 1;
                    int max = Math.Min(size-i, size-j);

                    int sz = 0;
                    int big = g.FindBiggest(i, j, out sz);
                    if (big > best)
                    {
                        bx = i+1;
                        by = j+1;
                        best = big;
                        bsize = sz;
                    }
                }
            }
            return string.Format("{0},{1},{2}", bx, by, bsize);
        }
