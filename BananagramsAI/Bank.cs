using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Bank {
        private Dictionary<char, int> letters;

        public Bank() {
            letters = new Dictionary<char, int>();
        }

        public Bank(Dictionary<char, int> letters) {
            this.letters = letters;
        }

        public bool IsAvailible(string word) {
            return word.All(c => letters[c] != 0);
        }

        public void TakeWord(string word) {
            foreach (char c in word) {
                letters[c] -= 1; 
            }
        }
    }
}
