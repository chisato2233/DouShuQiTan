using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.Rendering;
using UnityEngine;

namespace DouShuQiTan {
    [CreateAssetMenu(fileName = "枢元Library",menuName = "Game/Libraries/枢元")]
    public class 枢元Library :ScriptableObject {
        public List<枢元Template> 枢元Lists = new List<枢元Template>();
        
        private Dictionary<string, 枢元Template> Dict = new Dictionary<string, 枢元Template>();
        
        void Awake() {
            foreach (var cardTemplate in 枢元Lists) {
                Dict[cardTemplate.Name] = cardTemplate;
            }
        }

        public (Sprite, Sprite) Find(string name) {
            return Dict[name].Sprites;
        }
    }

    
}
