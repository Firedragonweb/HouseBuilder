using System.Text;

namespace HouseBuilder 
{
    public class Program
    {
        private const string HORIZONTAL = "─";
        private const string VERTICAL = "│";
        private const string LOWER_CORNER_LEFT = "└";
        private const string LOWER_CORNER_RIGHT = "┘";
        private const string TEE_HORIZONTAL_LOWER = "┴";
        private const string TEE_HORIZONTAL_UPPER = "┬";
        private const string TEE_VERTICAL_LEFT = "├";
        private const string TEE_VERTICAL_RIGHT = "┤";
        private const string CROSS = "┼";
        private const string WINDOW = "■";
        private const string DOOR = "▐";
        public static void Main(string[] args)
        {
            Console.WriteLine("Drawing house...");
            DrawHouse(4,20);
        }

        private static void DrawHouse(int stories, int width)
        {
            if (width % 2 != 0)
                width++;
            if (width < 4)
                width = 4;
            int spaceCount = width / 2 - 1 ; //BUGFIX: roof was wrong
            while (spaceCount >= 0)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < spaceCount; i++)
                    sb.Append(" ");
                sb.Append("/");
                for (int i = 0; i < (width/2 - spaceCount)*2 -2; i++)
                    sb.Append(" ");
                sb.Append("\\");
                for (int i = 0; i < spaceCount; i++)
                    sb.Append(" ");
                Console.WriteLine(sb.ToString());
                spaceCount--;
            }

            List<Tuple<int, int>> rList = new List<Tuple<int, int>>();
            for (int i = stories -1 ; i >= 0; i--)
            {
                Random random = Random.Shared;
                int n_r = random.Next(0, (width - 2) / 3 + 1);
                for (int ii = 0; ii < n_r; ii++)
                {
                    label:
                    int rr = random.Next(2, width - 2);
                    if(rList.Where(x=>x.Item1 == i).Where(x=>x.Item2 == rr).Concat(rList.Where(x=>x.Item1 == i && Math.Abs(x.Item2 - rr) <= 1)).Count() != 0)
                        goto label; //Too difficult to write loop, so using goto just this once.
                    rList.Add(new Tuple<int, int>(i, rr));
                    
                }
            }
            if (stories > 1)
            {
                Console.Write(TEE_VERTICAL_LEFT);
                int i = -1;
                for (int ii = 0; ii < width - 2; ii++)
                {
                    if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                    {
                        Console.Write(TEE_HORIZONTAL_LOWER);
                    }
                    if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                    {
                        Console.Write(CROSS);
                    }
                    if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                    {
                        Console.Write(TEE_HORIZONTAL_UPPER);
                    }
                    if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                    {
                        Console.Write(HORIZONTAL);
                    }

                }
                Console.WriteLine(TEE_VERTICAL_RIGHT);
            }

            for (int i = 0; i < stories; i++)
            {
                Random random = new Random();
                string spaces = "";
                bool d = false;
                bool w = false; //BUGFIX: Windows too close to each other
                for (int ii = 0; ii < width - 2; ii++)
                {
                    if (rList.Any(x => x.Item1 == i && x.Item2 == ii + 1))
                    {
                        spaces += VERTICAL;
                        w = false;
                        continue;
                    }
                    if (i == stories - 1) 
                    {
                        if (random.NextDouble() < 0.5 && !d)//BUGFIX: House had no door. If still no door, adjust number.
                        {
                            spaces += DOOR;
                            d = true;
                        }
                        else
                            spaces += " ";
                    }
                    else
                    {
                        if (random.NextDouble() < 0.25 &&!w)
                        {
                            spaces += WINDOW;
                            w = true;
                        }
                        else
                        {
                            spaces += " ";
                            w = false;
                        }
                    }
                }

                Console.WriteLine($"{VERTICAL}{spaces}{VERTICAL}");
                if (i == stories - 1)
                {
                    Console.Write(LOWER_CORNER_LEFT);
                    for (int ii = 0; ii < width - 2; ii++)
                    {
                        if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(TEE_HORIZONTAL_LOWER);
                        }
                        if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(CROSS);
                        }
                        if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(TEE_HORIZONTAL_UPPER);
                        }
                        if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(HORIZONTAL);
                        }

                    }
                    Console.WriteLine(LOWER_CORNER_RIGHT);
                }
                if (i < stories - 1)
                {
                    Console.Write(TEE_VERTICAL_LEFT);
                    for (int ii = 0; ii < width - 2; ii++)
                    {
                        if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(TEE_HORIZONTAL_LOWER);
                        }
                        if (rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(CROSS);
                        }
                        if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(TEE_HORIZONTAL_UPPER);
                        }
                        if (!rList.Contains(new Tuple<int, int>(i, ii + 1)) && !rList.Contains(new Tuple<int, int>(i + 1, ii + 1)))
                        {
                            Console.Write(HORIZONTAL);
                        }
                    }
                    Console.WriteLine(TEE_VERTICAL_RIGHT);
                }
            }
        }
    }
}