
using System;
using System.Collections.Generic;
using UnityEngine;
namespace DouShuQiTan {
    [Serializable]
    [CreateAssetMenu(
    menuName = "Game/Libraries",
    fileName = "枢元Upgrade")]
    public class 枢元UpgradeLibrary : ScriptableObject {
        public List<枢元UpgradeTemplate> 枢元Upgrades;
        // Add More Fields Here
    }
}