using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BananagramsAI {
    class PlayerState {
        Bank bank;
        Grid grid;

        public PlayerState(Bank bank, Grid grid) {
            this.bank = bank;
            this.grid = grid;
        }

        public void PlaceWordAt(string word, int x, int y, bool vertical) {
            grid.PlaceWordAt(word, x, y, vertical);
            bank.TakeWord(word);
        }

        public List<Grid> FindMoves(List<string> words) {
            List<Grid> moves = new List<Grid>();

            for (int i = grid.TopRowIndex; i <= grid.BottomRowIndex; ++i) {
                if (grid.IsEmptyLine(i, false)) continue;

                Dictionary<int, Regex> regexes = grid.GenerateLineRegexes(i, false);
                foreach (KeyValuePair<int, Regex> pair in regexes) {
                    Regex regex = pair.Value;
                    var placable = words.Where(w => regex.IsMatch(w));
                    foreach (string word in placable) {
                        Bank after = new Bank(bank);

                        Match test = regex.Match(word);
                        if (!test.Success) continue;
                        
                        CaptureCollection placed = test.Groups[1].Captures;

                        if (placed.Count == 0) continue;

                        Capture prefix = placed[0];
                        int xPosition = pair.Key - prefix.Length;

                        bool isLegal = true;
                        foreach (Capture capture in placed) {
                            if (!after.TryTakeWord(capture.Value)) {
                                isLegal = false;
                                break;
                            }
                        }

                        if (isLegal) {
                            Grid newGrid = new Grid(grid);
                            newGrid.PlaceWordAt(word, xPosition, i, false);
                            moves.Add(newGrid);
                        }
                    }
                }
            }

            return moves;
        }
    }
}
