using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    struct Point {
        public int x;
        public int y;
        
        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public static Point operator*(int scalar, Point p) {
            return new Point(p.x * scalar, p.y * scalar);
        }

        public static Point operator+(Point p1, Point p2) {
            return new Point(p1.x + p2.x, p1.y + p2.y);
        }

        public static Point operator-(Point p1, Point p2) {
            return new Point(p1.x - p2.x, p1.y - p2.y);
        }
    }
}
