using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananagramsAI {
    class GameState {
        Grid grid;
        Bank bank;
        List<String> words;

        public GameState(List<String> words) {
            grid = new Grid();
            bank = new Bank();
            this.words = words;
        }

        public List<GameState> FindPromisingMoves(int count) {
            List<GameState> moves = new List<GameState>();
            
            for 
        }
    }
}
