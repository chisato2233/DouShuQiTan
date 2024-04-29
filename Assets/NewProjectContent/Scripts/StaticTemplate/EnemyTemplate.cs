using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using UnityEngine;

namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Templates/Enemy",
        fileName = "Enemy",
        order = 0)]
    public class EnemyTemplate:ScriptableObject {
        public string Name;
        
        public GameObject RuntimeEnemy => Resources.Load<GameObject>($"Prefabs/Enemy/{Name}");
        public Sprite UiIcon => Resources.Load<Sprite>($"Art/Enemy/{Name}头");
    }
}
