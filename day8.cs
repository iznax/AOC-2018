// https://adventofcode.com/2018/day/8

        class Node8
        {
            public List<Node8> Kids;
            public List<int> Meta;

            public void Read(List<int> data)
            {
                Kids = new List<Node8>();
                Meta = new List<int>();

                if (data.Count >= 2)
                {
                    int kids = data[0];
                    int meta = data[1];
                    data.RemoveAt(0);
                    data.RemoveAt(0);
                    for (int i = 0; i < kids; ++i)
                    {
                        Node8 child = new Node8();
                        Kids.Add(child);
                        child.Read(data);
                    }
                    for (int i = 0; i < meta; ++i)
                    {
                        Meta.Add(data[0]);
                        data.RemoveAt(0);
                    }
                }
                else
                {
                    throw new Exception("!");
                }
            }

            public int SumMetadata()
            {
                int total = Meta.Sum();
                foreach (Node8 child in Kids)
                {
                    total += child.SumMetadata();
                }
                return total;
            }

            public int Value()
            {
                if (Kids.Count == 0)
                {
                    return Meta.Sum();
                }
                else
                {
                    int sum = 0;
                    foreach (int index in Meta)
                    {
                        if (index >= 1 && index <= Kids.Count)
                        {
                            sum += Kids[index-1].Value();
                        }
                    }
                    return sum;
                }
            }
        }

        static int Day8a(string line)
        {
            string[] parts = line.Split(' ');
            List<int> data = new List<int>();
            foreach (string item in parts)
            {
                data.Add(int.Parse(item));
            }

            List<Node8> root = new List<Node8>();

            int sum = 0;

            while (data.Count > 0)
            {
                Node8 node = new Node8();
                node.Read(data);
                root.Add(node);
                sum += node.SumMetadata();
            }

            return sum;
        }

        static int Day8b(string line)
        {
            string[] parts = line.Split(' ');
            List<int> data = new List<int>();
            foreach (string item in parts)
            {
                data.Add(int.Parse(item));
            }

            Node8 root = null;

            while (data.Count > 0)
            {
                Node8 node = new Node8();
                node.Read(data);
                if (root == null)
                {
                    root = node;
                }
                else
                {
                    throw new Exception("multiple roots?");
                }
            }

            return root.Value();
        }
