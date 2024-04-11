using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {

    public class GameBoost : MonoBehaviour {
        [Header("Configuration")]


        [Header("Systems")]
        [SerializeField] 
        GameTurnSystem gameTurnSystem;
        [SerializeField]
        DamageSystem damageSystem;

        [Header("Runtime")]
        [SerializeField]
        List<CharacterObject> Enemies;
        [SerializeField]
        CharacterObject Player;


        void Start() {

        }


        void InitRuntimeGame() {
            gameTurnSystem.Initialize(Player, Enemies);
            damageSystem.Initialize(Player, Enemies);

            StartCoroutine(gameTurnSystem.MainTurnRotine());
        }

        void StopRunTimeGame() {
            StopCoroutine(gameTurnSystem.MainTurnRotine());
        }

    }
}
