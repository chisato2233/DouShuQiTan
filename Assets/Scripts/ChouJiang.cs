using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChouJiang : MonoBehaviour
{
    float t = 0;

    public string PlayerChoose = "";
    private UI_MapStartButton button;

    private void Awake() {
        if (ExploreSystem.IsNewExplo) {
            AudioManager.Instance.PlaySfx("Tiger");
            AudioManager.Instance.PlayMusic("battle");
            for (int i = 0; i < 4; i++)
                transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("choujiang", true);
            button = transform.GetChild(5).GetComponentInChildren<UI_MapStartButton>();


            
            var JuQing = GameObject.Find("JuQing");
            if(JuQing!=null)
                JuQing.SetActive(true);
        }
        else {
            transform.parent.gameObject.SetActive(false);
            AudioManager.Instance.PlayMusic("battle");
            var JuQing = GameObject.Find("JuQing");
            if (JuQing != null)
                JuQing.SetActive(false);
        }
    }

    void Start() {
        StartCoroutine(WaitRollAnimation());
    }

    private IEnumerator WaitRollAnimation() {
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 4; i++)
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("choujiang", false);
        yield return new WaitForSeconds(2.6f);
        for (int i = 0; i < 4; i++)
            transform.GetChild(i).GetComponentInChildren<UI_咒印Toogle>().Enable();
        yield return WaitPlayerChoose();
    }

    private IEnumerator WaitPlayerChoose() {
        yield return new WaitUntil(() => PlayerChoose !="");
        for (int i = 0; i < 4; i++)
            transform.GetChild(i).GetComponentInChildren<UI_咒印Toogle>().Disable();
        GameData.Selected咒印Name = PlayerChoose;
        button.Enable();
    }
}
