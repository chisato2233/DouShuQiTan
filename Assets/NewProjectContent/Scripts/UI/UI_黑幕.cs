using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_黑幕 : MonoBehaviour {
    void Start() {
        GetComponent<Image>().DOFade(0, 2f);
    }
}
