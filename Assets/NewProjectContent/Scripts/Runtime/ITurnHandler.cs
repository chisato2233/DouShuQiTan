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
        public UnityEvent OnTurnStart = new UnityEvent();
        public UnityEvent OnTurnEnd = new UnityEvent();

        public List<TurnHandler> ChildTurnHandler = new List<TurnHandler>();

        void Awake() {

            foreach (var turn in ChildTurnHandler)
                OnTurnStart.AddListener(()=>turn.OnTurnStart?.Invoke());

        }
        public IEnumerator TurnRoutine() {
            OnTurnStart.Invoke();
            
            yield return OnTurn();

            foreach (var turn in ChildTurnHandler) 
                yield return turn.TurnRoutine();
            
            OnTurnEnd.Invoke();
        }

        protected virtual IEnumerator OnTurn() {
            yield return null;
        }
    }
}
