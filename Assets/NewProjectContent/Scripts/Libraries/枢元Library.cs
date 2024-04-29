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
        }

        public Sprite FindTop(string name) {
            if (Dict.TryGetValue(name, out var value)) {
                return value.Top;
            }
            
            return (Dict[name] = FindImpl(name)).Top;
        }

        public Sprite FindBottom(string name) {
            if (Dict.TryGetValue(name, out var value)) {
                return value.Bottom;
            }

            return (Dict[name] = FindImpl(name)).Bottom;
        }


        private 枢元Template FindImpl(string name) {
            return 枢元Lists.Find(x => x.Name == name);
        }
    }

    
}
