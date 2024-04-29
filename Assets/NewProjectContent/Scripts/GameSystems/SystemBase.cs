using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    public class SystemBase : MonoBehaviour {
        protected GameObject Player;
        protected List<GameObject> Enemies;

        public virtual void Initialize(GameObject player, List<GameObject> enemies) {
            Player = player;
            Enemies = enemies;
        }
    }
}
