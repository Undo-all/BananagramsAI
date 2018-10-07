using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Priority_Queue;

namespace BananagramsAI {
    class SearchNode {
        public List<string> Words { get;  }
        public PlayerState State { get; }
        public int MovesToReach { get; set; }
        //public SimplePriorityQueue<SearchNode> Children { get; private set;  } = new SimplePriorityQueue<SearchNode>();

        public SearchNode(PlayerState state, List<string> words, int movesToReach) {
            this.State = state;
            this.Words = words;
            this.MovesToReach = movesToReach;
        }

        public IEnumerable<SearchNode> FindChildren() {
            List<Placement> placements = State.FindPlacements(Words);
            foreach (Placement placement in placements) {
                PlayerState next = new PlayerState(State);
                next.PlaceWord(placement);
                List<string> possibleWords = Words.Where(w => w.Any(c => next.Bank.HasLetter(c))).ToList();
                yield return new SearchNode(next, possibleWords, MovesToReach + 1);
            }
        }
    }
}
