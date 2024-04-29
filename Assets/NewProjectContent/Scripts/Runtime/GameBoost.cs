using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
   
    public class GameBoost : MonoBehaviour {

        [Header("Configuration")]
        public List<GameObject> Enemies = new List<GameObject>();
        public GameObject Player = null;


        [Header("Systems")]
        public GameTurnSystem GameTurnSystem;
        public DamageSystem DamageSystem;
        public UserOperationSystem UserOperationSystem;
        public CardPoolSystem CardPoolSystem;


        [Header("Event")] 
        public UnityEvent GameStart;
        public UnityEvent GameStop;

        void Awake() {
            GameIntro.__GlobalGameBoostInstance = this;
            GameStart.AddListener(()=>Debug.Log("GameStart"));
            GameTurnSystem.OnTurnSystemStart.AddListener(()=>Debug.Log("System Turn Start"));
            GameTurnSystem.OnTurnSystemEnd.AddListener(()=>Debug.Log("System Turn End"));
        }

        void Start() {
            
            InitRuntimeGame();
            StartRuntimeGame();
        }

        void OnDestroy() {
            StopRunTimeGame();
        }


        void InitRuntimeGame() {
            GameTurnSystem.Initialize(Player, Enemies);
            DamageSystem.Initialize(Player, Enemies);
            UserOperationSystem.Initialize(Player, Enemies);
        }

        void StartRuntimeGame() {
            
            GameStart?.Invoke();

            StartCoroutine(GameTurnSystem.MainTurnRotine());
        }

        void StopRunTimeGame() {
            StopCoroutine(GameTurnSystem.MainTurnRotine());
            GameStop?.Invoke();
        }

    }
}
