using System;
using System.Collections.Generic;
using UnityEngine;
namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Templates/枢元Upgrade",
        fileName = "枢元Upgrade")]
    public class 枢元Template : ScriptableObject {
        public string Name;
        public (Sprite,Sprite) Sprites;
    }
}