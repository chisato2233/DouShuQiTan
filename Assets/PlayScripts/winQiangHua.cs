using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class winQiangHua : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int Num;
    public GameObject ZiDi;
    private GameStart m_game;

    public void OnInit(GameStart game)  
    {
        m_game = game;
    }

    public void OnPointerClick(PointerEventData eventData) {
        switch (Num) {
            case 0:
                List<int> suiji = new List<int>();
                while (suiji.Count < 4)
                {
                    int i = UnityEngine.Random.Range(0, 16);
                    if (!suiji.Contains(i))
                    {
                        suiji.Add(i);
                    }
                }
                for(int i = 0; i < suiji.Count; i++)
                {
                    m_game.NotUseBead[suiji[i]].extraGrade += 1;
                }
                break;
            case 1:
                List<int> suiji1 = new List<int>();
                while (suiji1.Count < 2)
                {
                    int i = UnityEngine.Random.Range(0, 16);
                    if (!suiji1.Contains(i))
                    {
                        suiji1.Add(i);
                    }
                }
                for (int i = 0; i < suiji1.Count; i++)
                {
                    m_game.NotUseBead[suiji1[i]].extraGrade += 2;
                }
                break;

            //case 2:
            //    List<int> suiji2 = new List<int>();
            //    while (suiji2.Count < 2)
            //    {
            //        int i = UnityEngine.Random.Range(0, 16);
            //        if (!suiji2.Contains(i))
            //        {
            //            suiji2.Add(i);
            //        }
            //    }
            //    for (int i = 0; i < suiji2.Count; i++)
            //    {
            //        m_game.NotUseBead[suiji2[i]].extraGrade += 3;
            //    }
            //    break;
            case 2:
                int j = UnityEngine.Random.Range (0, 16);
                m_game.NotUseBead[j].extraGrade += 3;
                break;
            case 4:
                List<int> suiji4 = new List<int>();
                while (suiji4.Count < 4)
                {
                    int i = UnityEngine.Random.Range(0, 16);
                    if (!suiji4.Contains(i))
                    {
                        suiji4.Add(i);
                    }
                }
                for (int i = 0; i < suiji4.Count; i++)
                {
                    m_game.NotUseBead[suiji4[i]].onceUpGrade += 1;
                }
                break;
            case 5:
                List<int> suiji5 = new List<int>();
                while (suiji5.Count < 2)
                {
                    int i = UnityEngine.Random.Range(0, 16);
                    if (!suiji5.Contains(i))
                    {
                        suiji5.Add(i);
                    }
                }
                for (int i = 0; i < suiji5.Count; i++)
                {
                    m_game.NotUseBead[suiji5[i]].onceUpGrade += 2;
                }
                break;
            case 6:
                int k = UnityEngine.Random.Range(0, 16);
                m_game.NotUseBead[k].onceUpGrade += 3;
                break;
            case 7:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].ShuXingGaiLv[3] += 4;
                    m_game.NotUseBead[i].ShuXingGaiLv[0] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[1] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[2] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[4] -= 1;
                }
                break;
            case 8:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].ShuXingGaiLv[2] += 4;
                    m_game.NotUseBead[i].ShuXingGaiLv[0] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[1] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[3] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[4] -= 1;
                }
                break;
            case 9:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].ShuXingGaiLv[1] += 4;
                    m_game.NotUseBead[i].ShuXingGaiLv[0] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[2] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[3] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[4] -= 1;
                }
                break;
            case 10:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].ShuXingGaiLv[4] += 4;
                    m_game.NotUseBead[i].ShuXingGaiLv[0] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[2] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[3] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[1] -= 1;
                }
                break;
            case 11:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].ShuXingGaiLv[0] += 4;
                    m_game.NotUseBead[i].ShuXingGaiLv[4] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[2] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[3] -= 1;
                    m_game.NotUseBead[i].ShuXingGaiLv[1] -= 1;
                }
                break;
            case 12:
                for(int i = 0; i < 16; i++)
                {
                    m_game.NotUseBead[i].extraRandom++;
                }
                break;
            case 13:
                m_game.HuoUp++;
                break;
            case 14:
                m_game.ShuiUp++;
                break;
            case 15:
                m_game.TuUp++;
                break;
            case 3:
                m_game.FengUp++;
                break;
        }
        m_game.winChooseCard = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ZiDi.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ZiDi.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ZiDi.SetActive(false);
        this.transform.GetChild(3).gameObject.SetActive(false);
        this.transform.GetChild(4).gameObject.SetActive(false);
        this.transform.GetChild(5).gameObject.SetActive(false);
        this.transform.GetChild(6).gameObject.SetActive(false);
        this.transform.GetChild(7).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (Num)
        {
            case 0:
                this.transform.GetChild(6).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "筑基";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机4颗枢元的初始等级提升1级";
                break;
            case 1:
                this.transform.GetChild(6).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "筑基";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机2颗枢元的初始等级提升2级";
                break;
            case 2:
                this.transform.GetChild(6).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "筑基";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机1颗枢元的初始等级提升3级";
                break;
            case 3:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "参悟";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "强化风之枢元的属性效果";
                break;
            case 4:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "飞升";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机4颗枢元在游戏中每次升级额外加1级";
                break;
            case 5:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "飞升";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机2颗枢元在游戏中每次升级额外加2级";
                break;
            case 6:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "飞升";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "使随机1颗枢元在游戏中每次升级额外加3级";
                break;
            case 7:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "掌局";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升进化为火之枢元的概率";
                break;
            case 8:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "掌局";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升进化为水之枢元的概率";
                break;
            case 9:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "掌局";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升进化为土之枢元的概率";
                break;
            case 10:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "掌局";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升进化为风之枢元的概率";
                break;
            case 11:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "掌局";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升进化为空之枢元的概率";
                break;
            case 12:
                this.transform.GetChild(5).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "气运";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "提升前三次升级枢元发生进化的概率";
                break;
            case 13:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "参悟";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "强化火之枢元的属性效果";
                break;
            case 14:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "参悟";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "强化水之枢元的属性效果";
                break;
            case 15:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "参悟";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "强化土之枢元的属性效果";
                break;
            //case 16:
            //    this.transform.GetChild(4).gameObject.SetActive(true);
            //    this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "参悟";
            //    this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "强化风之枢元的属性效果";
            //    break;
        }
    }
}
