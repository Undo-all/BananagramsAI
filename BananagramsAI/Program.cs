using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BananagramsAI {
    class Program {
        static string GenerateRegexStartingAt(string test, int start) {
            char anchor = test[start];

            int next = -1;
            for (int i = start + 1; i < test.Length; ++i) {
                if (test[i] != '_') {
                    next = i;
                    break;
                } else if (test[i] == '-') {
                    next = i + 1;
                }
            }

            if (next == -1) {
                return String.Format("{0}.*$", anchor);
            }

            int leeway = next - start - 1;
            string endEarly = String.Format("(.{{0,{0}}}$)", leeway - 1);
            string fromNext = GenerateRegexStartingAt(test, next);
            string goOn = String.Format("(.{{{0}}}({1}))", leeway, fromNext);
            return String.Format("{0}({1}|{2})", anchor, goOn, endEarly);
        }

        public static void Main(string[] args) {
            var query = from line in File.ReadLines(@"\Users\undoall\source\repos\BananagramsAI\BananagramsAI\words.txt")
                        where line.All(c => "abcdefghijklmnopqrstuvwxyz".Contains(c))
                        select line;
            List<String> words = query.ToList();

            Bank bank = new Bank(Enumerable.Repeat(1, 26).ToArray());

            Grid grid = new Grid();

            grid.PlaceWordAt("test", 0, 0, false);
            grid.PlaceWordAt("tell", 0, 0, true);
            grid.PlaceWordAt("ball", -3, 3, false);

            PlayerState test = new PlayerState(bank, grid);
            List<Grid> moves = test.FindMoves(words);

            foreach (Grid move in moves) move.Display();
        }
    }
}
