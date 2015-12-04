using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Day4Part1
{
    internal class ProgramDay4Part1
    {
        private static void Main()
        {
            const string secret = "ckczppom";
            const string pattern = @"^00000"; // replace with @"^000000" for part two  

            using (var md5 = MD5.Create())
            {
                for (var i = 1; i < 1000000000; i++)
                {
                    var tmpSecret = string.Concat(new[] {secret, i.ToString()});
                    using (var s = GenerateStreamFromString(tmpSecret))
                    {
                        var key = BitConverter.ToString(md5.ComputeHash(s)).Replace("-", string.Empty);
                        var r = new Regex(pattern, RegexOptions.IgnoreCase);
                        var m = r.Match(key);
                        if (!m.Success) continue;
                        Console.WriteLine("Result: " + i);
                        Console.ReadLine();
                        return;
                    }
                }
            }
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
