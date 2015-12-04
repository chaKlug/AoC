using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Day3Part2
{
    internal class ProgramDay3Part2
    {
        private static void Main()
        {
            var path = "D:\\Test\\day3.txt";
            var allines = File.ReadAllText(path);

            //var sb = new StringBuilder();
            //using (var sr = new StreamReader("C:\\Users\\Igor\\Documents\\day3.txt"))
            //{
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        sb.AppendLine(line);
            //    }
            //}
            //var allines = sb.ToString();
            //allines = allines.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

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

            //var visitedHouses = santa.Track.Union(roboSanta.Track).ToList();

            var visitedHouses = santa.Track.Concat(roboSanta.Track).Distinct().ToList();

            //// Draw path track
            //var bitmap = new Bitmap(200, 200, PixelFormat.Format32bppArgb);

            //var minX = Math.Abs(santa.Track.Min(p => p.X));
            //var minY = Math.Abs(santa.Track.Min(p => p.Y));
            //var listOfX = santa.Track.Select(p => p.X + minX).ToList();
            //var listOfY = santa.Track.Select(p => p.Y + minY).ToList();
            
            //for (var i = 0; i < listOfX.Count; i++)
            //{
            //    bitmap.SetPixel(listOfX[i], listOfY[i], Color.Black);
            //}

            //minX = Math.Abs(roboSanta.Track.Min(p => p.X));
            //minY = Math.Abs(roboSanta.Track.Min(p => p.Y));
            //listOfX = roboSanta.Track.Select(p => p.X + minX).ToList();
            //listOfY = roboSanta.Track.Select(p => p.Y + minY).ToList();

            //for (var i = 0; i < listOfX.Count; i++)
            //{
            //    bitmap.SetPixel(listOfX[i], listOfY[i], Color.Red);
            //}

            //bitmap.Save("C:\\Users\\Igor\\Documents\\img.jpg", ImageFormat.Png);
            
            Console.WriteLine("Together they visited " + (visitedHouses.Count - 1));
            Console.ReadLine();
        }
    }

    internal class Position : IEquatable<Position>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public bool Equals(Position other)
        {
            if (other == null) return false;

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
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
