using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DouShuQiTan {
    [CreateAssetMenu(menuName = "Game/Templates/MapNode",fileName = "NewMapNode")]
    public class MapNodeTemplate : ScriptableObject {
        public string Name;
        public string Type;
        public GameObject RuntimeNode => Resources.Load<GameObject>($"Prefabs/MapNodes/{Name}");
    }
}
