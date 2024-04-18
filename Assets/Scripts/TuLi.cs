using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TuLi : MonoBehaviour {
    private bool IsTanChu=false;
    public Vector2 PopUpPosition;
    public Vector2 StartPosition;
    [SerializeField] private float PopUpTime = 0.01f;
    [SerializeField]private float ShowTime = 3.0f;

    void Awake() {
    }

    void Start() {
        StartPosition = GetComponent<RectTransform>().anchoredPosition;
        PopUpPosition = StartPosition;
        PopUpPosition.x = -270;
    }
    public void PopUp() {
        Debug.Log("On Pop Up" );
        GetComponent<RectTransform>().DOAnchorPos(PopUpPosition, PopUpTime);
    }

    public void PopDown() {
        GetComponent<RectTransform>().DOAnchorPos(StartPosition, PopUpTime);
    }


    public IEnumerator AutoShow() {
        PopUp();
        yield return new WaitForSeconds(ShowTime);
        PopDown();
    }
}
