using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BananagramsAI {
    class PlayerState {
        public Bank Bank { get; private set;  }
        public Grid Grid { get; private set;  }

        public PlayerState(Bank bank, Grid grid) {
            this.Bank = bank;
            this.Grid = grid;
        }

        public PlayerState(PlayerState state) {
            this.Bank = new Bank(state.Bank);
            this.Grid = new Grid(state.Grid);
        }

        /*
        public void PlaceWord(string word, int x, int y, bool vertical) {
            Grid.PlaceWordAt(word, x, y, vertical);
            Bank.TakeWord(word);
        }*/

        public void PlaceWord(Placement placement) {
            Grid.PlaceWordAt(placement.word, placement.x, placement.y, placement.vertical);
            Bank = placement.bankAfter;
        }

        /*
        public List<Placement> FindPlacements(List<string> words) {
            List<Placement> placements = new List<Placement>();

            if (Grid.IsEmpty()) {
                foreach (string word in words.Where(w => Bank.IsAvailableWord(w))) {
                    Bank after = new Bank(Bank);
                    after.TakeWord(word);
                    placements.Add(new Placement(word, 0, 0, false, after));
                    placements.Add(new Placement(word, 0, 0, true, new Bank(after)));
                }

                return placements;
            }

            void FindPlacementsOriented(bool vertical) {
                int start = (vertical ? Grid.LeftmostColumnIndex : Grid.TopRowIndex);
                int end = (vertical ? Grid.RightmostColumnIndex : Grid.BottomRowIndex);

                for (int i = start; i <= end; ++i) {
                    if (Grid.IsEmptyLine(i, vertical)) continue;

                    Dictionary<int, Regex> regexes = Grid.GenerateLineRegexes(i, vertical);
                    foreach (KeyValuePair<int, Regex> pair in regexes) {
                        Regex regex = pair.Value;
                        //Console.WriteLine(regex.ToString());
                        var placable = words.Where(w => w.Length > 1 && regex.IsMatch(w));
                        foreach (string word in placable) {
                            Bank after = new Bank(Bank);

                            Match test = regex.Match(word);
                            if (!test.Success) continue;
                            
                            CaptureCollection placed = test.Groups[1].Captures;

                            if (placed.Count == 0 || (placed[0].Length == 0 && placed[1].Length == 0)) continue;

                            Capture prefix = placed[0];
                            int position = pair.Key - prefix.Length;

                            bool isLegal = true;
                            foreach (Capture capture in placed) {
                                if (!after.TryTakeWord(capture.Value)) {
                                    isLegal = false;
                                    break;
                                }
                            }

                            if (isLegal) {
                                if (vertical) {
                                    placements.Add(new Placement(word, i, position, vertical, after));
                                } else {
                                    placements.Add(new Placement(word, position, i, vertical, after));
                                }
                            }
                        }
                    }
                }
            }

            FindPlacementsOriented(false);
            FindPlacementsOriented(true);

            return placements;
        }
        */

        public List<Placement> FindPlacements(List<string> words) {
            List<Placement> placements = new List<Placement>();

            if (Grid.IsEmpty()) {
                foreach (string word in words.Where(w => Bank.IsAvailableWord(w))) {
                    Bank after = new Bank(Bank);
                    after.TakeWord(word);
                    placements.Add(new Placement(word, 0, 0, false, after));
                    placements.Add(new Placement(word, 0, 0, true, new Bank(after)));
                }

                return placements;
            }

            void FindPlacementsOriented(bool vertical) {
                int start = (vertical ? Grid.LeftmostColumnIndex : Grid.TopRowIndex);
                int end = (vertical ? Grid.RightmostColumnIndex : Grid.BottomRowIndex);

                for (int i = start; i <= end; ++i) {
                    if (Grid.IsEmptyLine(i, vertical)) continue;

                    Dictionary<int, PlacementRequirements> requirements = Grid.GenerateLineRegexes(i, vertical);
                    foreach (KeyValuePair<int, PlacementRequirements> pair in requirements) {
                        PlacementRequirements required = pair.Value;
                        //Console.WriteLine(regex.ToString());
                        var placable = words.Where(w => w.Length > 1);
                        foreach (string word in placable) {
                            Bank after = new Bank(Bank);

                            Regex regex = required.regex;
                            Match match = regex.Match(word);

                            if (!match.Success) continue;
                            int back = match.Index;
                            if (back > required.backLeeway) continue;
                            
                            CaptureCollection placed = match.Groups[1].Captures;
                            if (back == 0 && placed[0].Length == 0) continue;
                            
                            int position = pair.Key - back;

                            bool isLegal = true;

                            if (!after.TryTakeWord(word.Substring(0, back))) isLegal = false;

                            foreach (Capture capture in placed) {
                                if (!after.TryTakeWord(capture.Value)) {
                                    isLegal = false;
                                    break;
                                }
                            }

                            if (isLegal) {
                                if (vertical) {
                                    placements.Add(new Placement(word, i, position, vertical, after));
                                } else {
                                    placements.Add(new Placement(word, position, i, vertical, after));
                                }
                            }
                        }
                    }
                }
            }

            FindPlacementsOriented(false);
            FindPlacementsOriented(true);

            return placements;
        }
    }
}
