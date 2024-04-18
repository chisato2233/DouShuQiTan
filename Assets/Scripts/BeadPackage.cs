using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public static class MyFunc{
    public static string GetText(int i) {
        string digit = i.ToString();
        string res = "";
        foreach (var c in digit) {
            res += $"<sprite={c}> ";
        }

        return res;
    }
}

public class BeadPackage : MonoBehaviour
{
    public Transform scroll;

    public ChouJiang 抽奖System;
    // Start is called before the first frame update
    private void Awake()
    {
        scroll = transform.GetChild(1);

        if (GameData.ExtraGrade.Count > 0)
        {
            for (int i = 0; i < GameData.ExtraGrade.Count; i++)
            {

                var bead = scroll.GetChild(i);
                bead.GetChild(1).GetComponent<TextMeshPro>().text
                    = MyFunc.GetText(GameData.ExtraGrade[i]+1);

                if (GameData.OnceUpGrade[i] != 0)
                    bead.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text
                        = "每次升级额外加" + GameData.OnceUpGrade[i].ToString() + "级";
                else
                    bead.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text
                        = "无额外升级";

            }

        }
            
        else
        {
            for(int i=0;i<16;i++)
            {
                var bead = scroll.GetChild(i);
                bead.GetChild(1).GetComponent<TextMeshPro>().text
                    = "<sprite=1>";

                bead.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text
                        = "无额外升级";
            }
        }
        if (GameData.ExtraRandom.Count>0)
            scroll.GetChild(16).GetChild(0).GetComponent<TextMeshPro>().text
                = "*枢元前三次升级进化出属性的概率：" + (20+GameData.ExtraRandom[0]*2).ToString() + "%";
        else
            scroll.GetChild(16).GetChild(0).GetComponent<TextMeshPro>().text
                                = "*枢元前三次升级进化出属性的概率："  + "20%";

        if(GameData.ShuXingGaiLv.Count>0)
            for(int i=0;i<4;i++)
            {
                scroll.GetChild(17 + i).GetChild(1).GetComponent<TextMeshPro>().text
                    = "进化出该属性的概率：" + (GameData.ShuXingGaiLv[0][i+1]).ToString()+"%";

            }
        else {
            for(int i=0;i<4;i++)
            {
                scroll.GetChild(17 + i).GetChild(1).GetComponent<TextMeshPro>().text
                = "进化出该属性的概率：20%";
            }
        }
        if (GameData.TuUp == 1)
        {
            scroll.GetChild(17).GetChild(2).GetComponent<TextMeshPro>().text = "已强化";
            scroll.GetChild(17).GetChild(3).GetComponent<TextMeshPro>().text
                = "当棋盘上同时出现3颗火之枢元时，角色回复土之枢元【等级之和*(1+0.25*n)】的血量";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if(GameData.ShuiUp==1)
        {
            scroll.GetChild(18).GetChild(2).GetComponent<TextMeshPro>().text = "已强化";
            scroll.GetChild(18).GetChild(3).GetComponent<TextMeshPro>().text
                = "棋盘上每颗水之枢元对随机敌方单体造成等级*3(+0.25*n)的伤害";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if (GameData.HuoUp==1)
        {
            scroll.GetChild(19).GetChild(2).GetComponent<TextMeshPro>().text = "已强化";
            scroll.GetChild(19).GetChild(3).GetComponent<TextMeshPro>().text
                = "每有一颗火之枢元在训诫咒术中，该咒术对敌方伤害增加25(+2.5%*n)%";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if (GameData.FengUp==1)
        {
            scroll.GetChild(20).GetChild(2).GetComponent<TextMeshPro>().text = "已强化";
            scroll.GetChild(20).GetChild(3).GetComponent<TextMeshPro>().text
                = "每有两颗风之枢元在棋盘上，随机n颗枢元等级增加2级，其余所有枢元等级增加1级";
            scroll.GetChild(21).gameObject.SetActive(true);
        }




    }

    private void SelectShow咒印(int index) {
        switch (index) {
            case 0:
                Find咒印展示("Shui");
                break;
            case 1:
                Find咒印展示("Feng");
                break;
            case 2:
                Find咒印展示("Huo");
                break;
            case 3:
                Find咒印展示("Tu");
                break;
        }
    }

    private void Find咒印展示(string name) {
        foreach (Transform trans in transform) {
            if (trans.name == "GameObject") {
                foreach (Transform tran in trans) {
                    if (tran.name == name) {
                        tran.gameObject.SetActive(true);
                        goto end;
                    }
                }

            }
        }

        end: return;
    }

    void Start() {
        SelectShow咒印(GameData.咒印index);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
