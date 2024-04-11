using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class GameTurnSystem : SystemBase {
        public UnityEvent OnTurnStart;
        public UnityEvent OnTurnEnd;
        public int CurrentTurn = 0;

        public IEnumerator MainTurnRotine() {
            CurrentTurn++;
            OnTurnStart?.Invoke();
            yield return Player.StaticTurnRoutine();
            foreach (var enemy in Enemies) {
                yield return enemy.StaticTurnRoutine();
            }
            OnTurnEnd?.Invoke();
        }

    }
}
