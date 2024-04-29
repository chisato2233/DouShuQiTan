using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

namespace DouShuQiTan {
    [CreateAssetMenu(fileName = "EnemyLibrary",menuName = "Game/Libraries/Enemy")]
    public class EnemyLibrary :ScriptableObject {
        public List<EnemyTemplate> Enemies;


    }

    
}
