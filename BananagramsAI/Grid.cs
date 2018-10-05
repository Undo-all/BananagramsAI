using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Grid {
        Dictionary<int, SortedList<int, char>> rows = new Dictionary<int, SortedList<int, char>>();
        Dictionary<int, SortedList<int, char>> columns = new Dictionary<int, SortedList<int, char>>();        
        
        string GenerateLineRegexFrom(int lineIndex, bool column, int start) {
            SortedList<int, char> line;
            if (column) {
                line = columns[lineIndex];
            } else {
                line = rows[lineIndex];
            }

            char anchor = line.Values[start];

            if (start == line.Count - 1) {
                return anchor + ".*$";
            }

            int leeway = line.Keys[start + 1] - line.Keys[start] - 1;
            string endEarly = String.Format(".{{0,{0}}}$", leeway - 1);
            string fromNext = GenerateLineRegexFrom(line, start + 1);
            string goOn = String.Format(".{{{0}}}({1})", leeway, fromNext);
            return String.Format("{0}(({1})({2}))", anchor, goOn, endEarly);
        }

        string GenerateLineRegexes(SortedList<int, char> line, int start, bool row) {
             

            // We start from the largest index.
            for (int i = line.Count - 1; i >= 0; --i) {
                line.Values[i] 
            }
            return "";
        }  
    }
}
