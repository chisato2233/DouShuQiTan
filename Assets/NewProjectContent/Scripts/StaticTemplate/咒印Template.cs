using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan{
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Templates/咒印",
        fileName = "咒印")]
    public class 咒印Template:ScriptableObject {
        public string Name;
        public int Type = 0;
        public List<string> Describes;
        
        public Sprite Icon => Resources.Load<Sprite>($"咒印/{Name}");
    }
}
