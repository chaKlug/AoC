using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day3Part2
{
    internal class ProgramDay3Part2
    {
        private static void Main()
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
            var roboSanta = new RoboSanta();

            var whoMove = true; // if treue - Santa, if false then Robosanta 

            foreach (var c in allines)
            {
                var person = whoMove ? (IMove) santa : roboSanta;

                Position nextPosition = null;
                switch (c)
                {
                    case '>':
                        nextPosition = person.MoveRight(person.GetLastPosition());
                        break;
                    case '<':
                        nextPosition = person.MoveLeft(person.GetLastPosition());
                        break;
                    case '^':
                        nextPosition = person.MoveUp(person.GetLastPosition());
                        break;
                    case 'v':
                        nextPosition = person.MoveDown(person.GetLastPosition());
                        break;
                }
                person.AddPosition(nextPosition);

                whoMove = !whoMove;
            }

            var Crosses = 0;
            foreach (var position in santa.Track)
            {
                if (roboSanta.Track.Any(p => p.X == position.X & p.Y == position.Y))
                {
                    roboSanta.Track.RemoveAll(p => p.X == position.X & p.Y == position.Y);
                    Crosses++;
                }
            }

            Console.WriteLine("Santa bring presents to " + santa.Track.Count + " houses. Crosses " + santa.Crosses);
            Console.WriteLine("Robo-Santa bring presents to " + (roboSanta.Track.Count - 1) + " houses. Crosses " + roboSanta.Crosses);
            Console.WriteLine("Together they visited " + (santa.Track.Count + roboSanta.Track.Count - 1));
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

    internal interface IMove
    {
        Position MoveRight(Position position);
        Position MoveLeft(Position position);
        Position MoveUp(Position position);
        Position MoveDown(Position position);
        Position GetLastPosition();
        void AddPosition(Position position);
    }

    internal class FairyTaleCgaracter : IMove
    {
        public List<Position> Track { get; set; }
        public int Crosses;

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

        public Position GetLastPosition()
        {
            return Track.Last();
        }

        public void AddPosition(Position position)
        {
            if (Track.Any(p => p.X == position.X & p.Y == position.Y))
            {
                Track.RemoveAll(p => p.X == position.X & p.Y == position.Y);
                Crosses++;
            }

            Track.Add(position);
        }
    }

    internal class Santa : FairyTaleCgaracter
    {
        public Santa()
        {
            Track = new List<Position> { new Position(0, 0) };
        }
    }

    internal class RoboSanta : FairyTaleCgaracter
    {
        public RoboSanta()
        {
            Track = new List<Position> { new Position(0, 0) };
        }
    }
}
