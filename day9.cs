// https://adventofcode.com/2018/day/9

        class Data9
        {
            LinkedListNode<int> Selected;

            LinkedList<int> Circle;

            public int Insert(int marble)
            {
                if (marble == 0)
                {
                    if (Circle == null)
                    {
                        Circle = new LinkedList<int>();
                    }
                    Selected = Circle.AddLast(0);
                }
                else if (marble % 23 != 0)
                {
                    LinkedListNode<int> next = (Selected.Next != null) ? Selected.Next : Circle.First;
                    Selected = Circle.AddAfter(next, marble);//.Insert(Selected, marble);
                }
                else // special case
                {
                    for (int i = 0; i < 7; ++i)
                    {
                        Selected = (Selected.Previous != null) ? Selected.Previous : Circle.Last;
                    }
                    LinkedListNode<int> old = Selected;
                    int score = marble + old.Value;
                    Selected = (Selected.Next != null) ? Selected.Next : Circle.First;
                    Circle.Remove(old);
                    return score;
                }
                return 0;
            }
        }

        static long Day9(int players, int marbles)
        {
            Data9 data = new Data9();
            long[] scoreList = new long[players];
            for (int m = 0; m <= marbles; ++m)
            {
                int score = data.Insert(m);
                int playerIndex = (m % players);
                scoreList[playerIndex] += score;
            }
            return scoreList.Max();
        }
