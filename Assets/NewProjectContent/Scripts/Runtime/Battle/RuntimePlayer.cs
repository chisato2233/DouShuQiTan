using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader.Log;
using Unity.VisualScripting;
using UnityEngine;

namespace DouShuQiTan {
    public class RuntimePlayer : TurnHandler{
        void Start() {
            OnTurnStart.AddListener(()=>Debug.Log("Player Turn Start"));
            OnTurnEnd.AddListener(()=>Debug.Log("Player End Turn"));
        }



        


        protected override IEnumerator OnTurn() {
            yield return WaitUserOperator();
            yield return WaitCardOperator();

            Debug.Log("Player On Turn Over");
        }
        IEnumerator WaitUserOperator() {
            Debug.Log($"Wait User Operator {GameIntro.GameSystem.UserOperationSystem.IsUserEndTurn}");
            yield return new WaitUntil(() => GameIntro.GameSystem.UserOperationSystem.IsUserEndTurn);

        }

        IEnumerator WaitCardOperator() {
            yield return GameIntro.GameSystem.CardPoolSystem.HandleAllCards();
        }
    }
}
