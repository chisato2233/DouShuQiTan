using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_枢元Flower : MonoBehaviour {

  





    public void Get枢元(GameObject 枢元) {
        ChangeToClone(枢元.GetComponent<Runtime枢元>());
    }


    private void ChangeToClone(Runtime枢元 Template) {
        transform.Find("底部").GetComponent<Image>().overrideSprite =
            Template.transform.Find("底部").GetComponent<Image>().sprite;
        transform.Find("顶部").GetComponent<Image>().overrideSprite =
            Template.transform.Find("顶部").GetComponent<Image>().sprite;

        transform.Find("Grade").GetComponent<TextMeshProUGUI>().text =
            Template.transform.Find("Grade").GetComponent<TextMeshProUGUI>().text;

        foreach (var image in GetComponentsInChildren<Image>()) {
            var c = image.color;
            c.a = 0.5f;
            image.color = c;
        }

    }

    void Update() {
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //    GetComponent<RectTransform>(),
        //    Input.mousePosition,
        //    Camera.main, out var Pos);
        //GetComponent<RectTransform>().anchoredPosition = Pos;


        Vector3 mousePos = Input.mousePosition;
        // 由于鼠标坐标是以屏幕坐标给出的，我们需要将其转换为UI用的世界坐标
        mousePos.z = transform.position.z - Camera.main.transform.position.z; // 设置Z坐标为UI元素的Z坐标
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonUp(0)) {
            GameIntro.GameSystem.UserOperationSystem.Release枢元();
        }
    }
}
