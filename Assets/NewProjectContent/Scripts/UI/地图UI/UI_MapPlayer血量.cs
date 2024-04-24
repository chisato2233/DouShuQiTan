using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_MapPlayer血量 : MonoBehaviour {
    void Start() {
        GetComponent<Image>().fillAmount = GameIntro.playerState.CurrentHp / GameIntro.playerState.MaxHp;
        GetComponentInChildren<TextMeshProUGUI>().text =  GameIntro.playerState.CurrentHp + "/" + GameIntro.playerState.MaxHp;
    }
 
}
