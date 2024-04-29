using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class GameTurnSystem : SystemBase {
        public UnityEvent OnTurnSystemStart = new UnityEvent();
        public UnityEvent OnTurnSystemEnd = new UnityEvent();

        [Header("State")]
        private List<TurnHandler> TurnQueue = new List<TurnHandler>();
        public int CurrentTurn = 0;


        public void AddTurnHandler(TurnHandler Handler) {
            TurnQueue.Add(Handler);
        }

        public void RemoveTurnHandler(TurnHandler Handler) {
            TurnQueue.Remove(Handler);
        }


        public override void Initialize(GameObject player, List<GameObject> enemies) {
            base.Initialize(player, enemies);
            Init();
        }


        public void Init() {
            AddTurnHandler(Player.GetComponent<TurnHandler>());
            foreach (var enemy in Enemies) {
                AddTurnHandler(enemy.GetComponent<TurnHandler>());
            }
        }



        public IEnumerator MainTurnRotine() {
            while (true) {
                CurrentTurn++;
                OnTurnSystemStart?.Invoke();
                
                foreach (var turnHandler in TurnQueue) {
                    yield return turnHandler.TurnRoutine();
                }

                OnTurnSystemEnd?.Invoke();
            }
        }

    }
}
