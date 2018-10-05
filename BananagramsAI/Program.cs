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
            var query = from line in File.ReadLines(@"\Users\undoall\Downloads\words.txt")
                        where line.All(c => "abcdefghijklmnopqrstuvwxyz".Contains(c))
                        select line;
            List<String> words = query.ToList();

            /*foreach (string word in words.Take(2500)) {
                Bank bank = new Bank(word.Select(c => (int)c).ToArray());
                Console.WriteLine(bank.CalculateValue(words));
            }*/

            List<string> regexes = new List<string>();
            string test = "r_i_____k";

            string gross = GenerateRegexStartingAt(test, 0);
            Console.WriteLine(gross);
            Regex regex = new Regex("^.*" + gross, RegexOptions.Compiled);
            foreach (string match in words.Where(w => regex.IsMatch(w))) {
                Console.WriteLine(match);
            }

            SortedList<int, char> thing = new SortedList<int, char>();
            thing.Add(7, 'a');
            thing.Add(3, 'b');
            Console.WriteLine(thing.Values[0]); 
        }
    }
}
