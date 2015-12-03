using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Day3Part1
{
    internal class ProgramDay3Part1
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            using (var sr = new StreamReader("D:\\Test\\day3part1.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            var allines = sb.ToString();
            allines = allines.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

            var santa = new Santa();

            foreach (var c in allines)
            {
                Position nextPosition = null;
                switch (c)
                {
                    case '>':
                        nextPosition = santa.MoveRight(santa.Track.Last());
                        break;
                    case '<':
                        nextPosition = santa.MoveLeft(santa.Track.Last());
                        break;
                    case '^':
                        nextPosition = santa.MoveUp(santa.Track.Last());
                        break;
                    case 'v':
                        nextPosition = santa.MoveDown(santa.Track.Last());
                        break;
                }
                santa.AddPosition(nextPosition);
            }
            
            Console.WriteLine(santa.Track.Count);
            Console.WriteLine(santa.Crosses);
            Console.ReadLine();
        }
    }

    internal class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Santa
    {
        //TO:DO
        public List<Position> Track { get; set; }
        public int Crosses;

        public Santa()
        {
            Track = new List<Position> {new Position(0, 0)};
        }

        public Position MoveRight(Position position)
        {
            var nextPosition = new Position(position.X + 1, position.Y);
            return nextPosition;
        }

        public Position MoveLeft(Position position)
        {
            var nextPosition = new Position(position.X - 1, position.Y);
            return nextPosition;
        }

        public Position MoveUp(Position position)
        {
            var nextPosition = new Position(position.X, position.Y + 1);
            return nextPosition;
        }

        public Position MoveDown(Position position)
        {
            var nextPosition = new Position(position.X, position.Y - 1);
            return nextPosition;
        }

        public void AddPosition(Position position)
        {
            if (Track.Any(p => p.X == position.X & p.Y == position.Y))
            {
                if (Track.Count == 1)
                {
                    Debugger.Break();
                }
                
                Track.RemoveAll(p => p.X == position.X & p.Y == position.Y);
                Crosses++;
            }

            Track.Add(position);
        }
    }
}
