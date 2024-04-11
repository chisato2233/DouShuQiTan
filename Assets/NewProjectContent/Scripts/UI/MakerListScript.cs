using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakerListScript : MonoBehaviour {
    public void OnOpen() {
        gameObject.SetActive(true);
        GetComponent<Image>().DOFade(168 / 255f, 0.5f);
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>()?.DOFade(1, 0.5f);
        }
    }

    public void OnClose() {
        GetComponent<Image>().DOFade(0, 0.5f).OnComplete(() => gameObject.SetActive(false));
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<TextMeshProUGUI>()?.DOFade(0, 0.5f);
        }
    }
}
