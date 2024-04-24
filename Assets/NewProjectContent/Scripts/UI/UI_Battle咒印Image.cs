using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Battle咒印Image : MonoBehaviour {
    public 咒印Library 咒印Library;

    void Start() {
        string name = GameData.Selected咒印Name;
        var 咒印 = 咒印Library.咒印List.Find(x => x.Name == name);

        GetComponent<Image>().overrideSprite = Resources.Load<Sprite>($"Art/咒印/{name}");
        transform.Find("弹窗").Find("Describe").GetComponent<TextMeshProUGUI>().text = 咒印.Describes[0];
    }
}
