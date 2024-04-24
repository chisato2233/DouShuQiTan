using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManBing : MonoBehaviour {
    
    void Start() {
        GetComponent<Image>().DOFade(0, 2f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
    }
}
