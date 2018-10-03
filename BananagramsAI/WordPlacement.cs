using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    struct WordPlacement {
        public enum Orientation {
            Vertical,
            Horizontal,
        }
        
        public WordPlacement(Point coordinates, Orientation orientation) {
            this.coordinates = coordinates;
            this.orientation = orientation;
        }

        public Orientation orientation;
        public Point coordinates;
    }
}
