using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class PlacedWord {
        string word;
        WordPlacement placement;

        PlacedWord(string word, WordPlacement placement) {
            this.word = word;
            this.placement = placement;
        }

        public char this[int index] {
            get {
                return word[index];
            }
        }
    }
}
