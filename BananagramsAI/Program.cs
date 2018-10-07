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
            var query = from line in File.ReadLines(@"C:\Users\undoall\source\repos\BananagramsAI\BananagramsAI\words.txt")
                        where line.All(c => "abcdefghijklmnopqrstuvwxyz".Contains(c))
                        select line;
            List<String> words = query.ToList();

            Random rng = new Random();
            int[] temp = new int[26];
            for (int i = 0; i < 21; ++i) {
                temp[rng.Next(0, 26)] += 1;
            }


            Bank bank = new Bank(temp);

            Grid grid = new Grid();

            PlayerState state = new PlayerState(bank, grid);
            List<Placement> placements;
            for (int i = 0; i < 20; ++i) {
                Console.WriteLine();
                placements = state.FindPlacements(words);
                Console.Write("From " + placements.Count + " possible moves, placing: ");

                int maxLength = placements.Max(w => w.word.Length);
                var longs = placements.Where(w => w.word.Length == maxLength).ToList();
                Placement placement = longs[rng.Next(0, longs.Count)];


                Console.WriteLine(placement.word);
                state.PlaceWord(placement);
                state.Grid.Display();
                for (char c = 'a'; c <= 'z'; ++c) {
                    if (state.Bank.HasLetter(c)) {
                        Console.Write(c);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
