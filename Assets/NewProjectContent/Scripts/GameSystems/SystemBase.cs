using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    public class SystemBase : MonoBehaviour {
        protected CharacterObject Player;
        protected List<CharacterObject> Enemies;

        public virtual void Initialize(CharacterObject player, List<CharacterObject> enemies) {
            Player = player;
            Enemies = enemies;
        }
    }
}
