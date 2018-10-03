using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Program {
        public static void Main(string[] args) {
            List<string> words = new List<string>();

            using (StreamReader stream = new StreamReader(@"C:\Users\undoall\Downloads\words.txt")) {
                string line;
                while ((line = stream.ReadLine()) != null) {
                    if (line[0] != '#' && line.All(c => Char.IsLetter(c))) {
                        words.Add(line);
                    }
                }
            }

            

            words.Where(w => !w.Contains("arjklm")).ToList().ForEach(w => Console.WriteLine(w));    
        }
    }
}
