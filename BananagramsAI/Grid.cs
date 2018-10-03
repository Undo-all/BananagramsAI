using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class Grid {
        private Dictionary<Point, char> tiles;

        public Grid() {
            tiles = new Dictionary<Point, char>();
        }

        public List<Hook> FindHooks() {
            List<Hook> hooks = new List<Hook>();

            foreach (KeyValuePair<Point, char> tile in tiles) {
                Point coordinates = tile.Key;

                if (!tiles.ContainsKey(coordinates - new Point(1, 0)) && !tiles.ContainsKey(coordinates + new Point(1, 0))) {
                    hooks.Add(new Hook(tiles[coordinates], new WordPlacement(coordinates, WordPlacement.Orientation.Horizontal)));
                } else if (!tiles.ContainsKey(coordinates - new Point(0, 1)) && !tiles.ContainsKey(coordinates + new Point(0, 1))) {
                    hooks.Add(new Hook(tiles[coordinates], new WordPlacement(coordinates, WordPlacement.Orientation.Vertical)));
                }
            }

            return hooks;
        }

        public void PlaceWord(string word, int charIndex, WordPlacement placement) {
            Point direction;
            if (placement.orientation == WordPlacement.Orientation.Horizontal) {
                direction = new Point(1, 0);
            } else {
                direction = new Point(0, 1);
            }

            Point start = placement.coordinates - charIndex * direction;

            for (int i = 0; i < word.Length; ++i) {
                tiles[start + i * direction] = word[i];
            }
        }
    }
}
