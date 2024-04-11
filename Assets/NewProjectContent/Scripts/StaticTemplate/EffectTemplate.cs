using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    [CreateAssetMenu(
        menuName = "Game/Templates",
        fileName = "Effect",
        order = 1)]
    public class EffectTemplate:ScriptableObject {
        public int Id;
        public string Name;
        public string Type;
        public string Description;
    }
}
