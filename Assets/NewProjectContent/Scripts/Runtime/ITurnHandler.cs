using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class TurnHandler : MonoBehaviour {
        UnityEvent OnTurnStart;
        UnityEvent OnTurnEnd;
        private List<TurnHandler> otherTurnHandlers;

        void Awake() {

            foreach (var turn in GetComponents<TurnHandler>())
                OnTurnStart.AddListener(()=>turn.OnTurnStart?.Invoke());

            foreach (var turn in GetComponentsInChildren<TurnHandler>())
                OnTurnStart.AddListener(() => turn.OnTurnEnd?.Invoke());
        }
        public IEnumerator StaticTurnRoutine() {
            OnTurnStart.Invoke();
            
            yield return OnTurn();

            foreach (var turn in GetComponents<TurnHandler>()) 
                yield return turn.OnTurn();
            
            foreach (var turn in GetComponentsInChildren<TurnHandler>())
                yield return turn.OnTurn();
            
            OnTurnEnd.Invoke();
        }

        protected virtual IEnumerator OnTurn() {
            yield return null;
        }
    }
}
