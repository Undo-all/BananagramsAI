using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Gaddag {
        public bool isEndOfWord;
        public Gaddag[] children;

        public Gaddag(bool isEndOfWord) {
            children = new Gaddag[27];
            this.isEndOfWord = isEndOfWord;
        }
        
        public IEnumerable<string> FindAllWords() {
            if (isEndOfWord)
                yield return "";

            for (int i = 0; i < 27; ++i) {
                if (children[i] == null) continue;

                char c = (char)(i + 'a');
                var child = children[i].FindAllWords();
                foreach (string word in child) {
                    yield return (c + word);
                } 
            }
        }

        void InsertSplitAt(string word, int split) {
            Gaddag iter = this;

            void InsertLetter(char letter) {
                int index = letter - 'a';

                if (iter.children[index] == null) {
                    iter.children[index] = new Gaddag(false);
                }

                iter = iter.children[index];
            }

            for (int i = 0; i <= split; ++i) {
                int index = split - i;
                InsertLetter(word[index]);
            }

            InsertLetter('{'); // Represents a line-reversal. Equal to 'z' + 1
            
            for (int i = split + 1; i < word.Length; ++i) {
                InsertLetter(word[i]);
            }

            iter.isEndOfWord = true;
        }

        public void InsertWord(string word) {
            for (int i = 0; i < word.Length; ++i) {
                InsertSplitAt(word, i);
            }
        }
    }
}
