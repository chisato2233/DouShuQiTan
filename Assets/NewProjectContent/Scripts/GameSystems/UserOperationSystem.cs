using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace DouShuQiTan {
    public class UserOperationSystem:SystemBase {

        public UI_枢元Flower Flower;
        public GameObject Owner枢元 = null;


        [Header("Event")]
        public UnityEvent<GameObject> OnSetOwner枢元 = new UnityEvent<GameObject>();
        public UnityEvent<GameObject> OnRelease枢元 =new UnityEvent<GameObject>();



        public event Action<Runtime枢元, UI_枢元Reciver> OnPlace枢元To; 
        public event Action<Runtime枢元, UI_枢元Reciver> OnRemove枢元From;

        public UnityEvent<UI_枢元Reciver, UI_枢元Reciver, Runtime枢元> OnMove枢元 =
            new UnityEvent<UI_枢元Reciver, UI_枢元Reciver, Runtime枢元>();


        public bool IsUserEndTurn { get; private set; } = false;

        public override void Initialize(GameObject player, List<GameObject> enemies) {
            base.Initialize(player, enemies);
            player.GetComponent<RuntimePlayer>().OnTurnEnd.AddListener(() => IsUserEndTurn = false);
        }

        public void SetOwner枢元(GameObject 枢元) {
            Flower.gameObject.SetActive(true);
            Owner枢元 = 枢元;
            OnSetOwner枢元?.Invoke(枢元);
            Flower.Get枢元(枢元);
        }

        public void Release枢元() {
            OnRelease枢元?.Invoke(Owner枢元);
            Owner枢元 = null;
            Flower.gameObject.SetActive(false);
        }



        public void EndPlayerTurn() {
            IsUserEndTurn = true;
        }                                               

    }
}
