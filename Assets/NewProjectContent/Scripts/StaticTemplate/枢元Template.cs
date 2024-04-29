using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
        menuName = "Game/Templates/枢元",
        fileName = "枢元")]
    public class 枢元Template : ScriptableObject {
        public string Name;
        public Sprite Top;
        public Sprite Bottom;
    }
}