using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    struct Placement {
        public string word;
        public int x;
        public int y;
        public bool vertical;
        public Bank bankAfter;

        public Placement(string word, int x, int y, bool vertical, Bank bankAfter) {
            this.word = word;
            this.x = x;
            this.y = y;
            this.vertical = vertical;
            this.bankAfter = bankAfter;
        }
    }
}
