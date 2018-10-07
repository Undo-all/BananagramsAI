using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading;
using System.Diagnostics;

using Priority_Queue; // C'mon dude, don't have underscores in namespaces.

namespace BananagramsAI {
    class Program {
        static string GenerateRegexStartingAt(string test, int start) {
            char anchor = test[start];

            int next = -1;
            for (int i = start + 1; i < test.Length; ++i) {
                if (test[i] != '_') {
                    next = i;
                    break;
                } else if (test[i] == '-') {
                    next = i + 1;
                }
            }

            if (next == -1) {
                return String.Format("{0}.*$", anchor);
            }

            int leeway = next - start - 1;
            string endEarly = String.Format("(.{{0,{0}}}$)", leeway - 1);
            string fromNext = GenerateRegexStartingAt(test, next);
            string goOn = String.Format("(.{{{0}}}({1}))", leeway, fromNext);
            return String.Format("{0}({1}|{2})", anchor, goOn, endEarly);
        }

        public static void Main(string[] args) {
            var query = from line in File.ReadLines(@"C:\Users\undoall\source\repos\BananagramsAI\BananagramsAI\words.txt")
                        where line.All(c => "abcdefghijklmnopqrstuvwxyz".Contains(c))
                        select line;
            List<string> words = query.ToList();

            float Heuristic(SearchNode node) {
                return node.State.Bank.Size;
            }

            /*
            PlayerState AStarSearch(PlayerState start) {
                SimplePriorityQueue<SearchNode> queue = new SimplePriorityQueue<SearchNode>();
                Dictionary<SearchNode, SearchNode> cameFrom = new Dictionary<SearchNode, SearchNode>();

                queue.Enqueue(new SearchNode(start, words.Where(w => w.Any(c => bank.HasLetter(c))).ToList(), 0), 21);
                for (; ; ) {
                    if (queue.Count >= 500000) {
                        return null;
                    }

                    SearchNode node = queue.Dequeue();

                    Console.Clear();
                    node.State.Grid.Display();
                    Console.WriteLine("Got node with bank size " + node.State.Bank.Size + " and " + node.Words.Count + " possible words");

                    node.ExpandChildren();
                    foreach (SearchNode child in node.Children) {
                        if (child.State.Bank.Size == 0) {
                            return child.State;
                        }

                        int movesToReach = node.MovesToReach + 1;
                        if (!queue.Contains(child) || movesToReach < child.MovesToReach) {
                            cameFrom[child] = node;
                            child.MovesToReach = movesToReach;
                            float priority = child.MovesToReach*0 + Heuristic(child);
                            if (!queue.Contains(child)) {
                                queue.Enqueue(child, priority);
                            } else {
                                queue.UpdatePriority(child, priority);
                            }
                        }
                    }
                }
            }
            */

            /*
            PlayerState BestFirstSearch(PlayerState start) {
                SimplePriorityQueue<SearchNode> queue = new SimplePriorityQueue<SearchNode>();

                queue.Enqueue(new SearchNode(start, words.Where(w => w.Any(c => bank.HasLetter(c))).ToList(), 0), 21);
                for (; ; ) {
                    if (queue.Count >= 500000) {
                        return null;
                    }

                    SearchNode node = queue.Dequeue();

                    Console.Clear();
                    node.State.Grid.Display();
                    Console.WriteLine("Got node with bank size " + node.State.Bank.Size + " and " + node.Words.Count + " possible words");

                    foreach (SearchNode child in node.FindChildren()) {
                        if (child.State.Bank.Size == 0) {
                            return child.State;
                        }

                        queue.Enqueue(child, Heuristic(child));
                    }
                }
            }
            */

            Random rng = new Random();
            int[] temp = Enumerable.Repeat(1, 26).ToArray();

            Bank bank = new Bank(temp);
            Grid grid = new Grid();

            grid.PlaceWordAt("placed", 0, 0, false);
            grid.PlaceWordAt("loft", 1, 0, true);
            grid.PlaceWordAt("far", 1, 2, false);
            grid.Display();

            PlayerState state = new PlayerState(bank, grid);

            Stopwatch sw = new Stopwatch();

            sw.Start();

            for (int i = 0; i < 10; ++i) {
                List<Placement> placements = state.FindPlacements(words);
                //Console.WriteLine(placements[rng.Next(0, placements.Count)].Word);
            }

            sw.Stop();

            Console.WriteLine("Time elapsed: {0}", sw.Elapsed);
            
            /*
            Console.Write("Bank: ");
            for (char c = 'a'; c <= 'z'; ++c) {
                for (int i = 0; i < bank.letters[c - 'a']; ++i) {
                    Console.Write(c);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Searching for solution...");
            PlayerState solution = BestFirstSearch(new PlayerState(bank, grid));
            Console.WriteLine("Solution Found!");
            solution.Grid.Display();
            */

            /*
            PlayerState state = new PlayerState(bank, grid);
            List<Placement> placements;
            for (int i = 0; i < 20; ++i) {
                Console.WriteLine();
                placements = state.FindPlacements(wordsBlock);
                Console.Write("From " + placements.Count + " possible moves, placing: ");
                Placement placement = placements[rng.Next(0, placements.Count)];


                Console.WriteLine(placement.word);
                state.PlaceWord(placement);
                state.Grid.Display();
                for (char c = 'a'; c <= 'z'; ++c) {
                    if (state.Bank.HasLetter(c)) {
                        Console.Write(c);
                    }
                }
                Console.WriteLine();
            }
            */
        }
    }
}
