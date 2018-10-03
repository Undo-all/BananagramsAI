using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    struct Hook {
        public char character;
        public WordPlacement placement;

        public Hook(char character, WordPlacement placement) {
            this.character = character;
            this.placement = placement;
        }
    }
}
