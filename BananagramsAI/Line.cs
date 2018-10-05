using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Line {
        List<char> behind = new List<char>();
        List<char> ahead = new List<char>();

        public this[int index] {
            get {
                List<char> line;
                if (index >= 0) {
                    line = ahead;
                    index = -index + 1;
                } else {
                    line = behind;
                }

                
            }
        }
        
        public bool IsEmptyAt(int index) {
            List<char> line;
            if (index >= 0) {
                line = ahead;
                index = -index + 1;
            } else {
                line = behind;
            }

            return (index >= line.Count) || (line[index] == '\0');
        }
    }
}
