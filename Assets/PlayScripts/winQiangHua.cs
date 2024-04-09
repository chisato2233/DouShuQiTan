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

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (Num)
        {
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
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���4����Ԫ�ĳ�ʼ�ȼ�����1��";
                break;
            case 1:
                this.transform.GetChild(6).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���2����Ԫ�ĳ�ʼ�ȼ�����2��";
                break;
            case 2:
                this.transform.GetChild(6).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���1����Ԫ�ĳ�ʼ�ȼ�����3��";
                break;
            case 3:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ǿ����֮��Ԫ������Ч��";
                break;
            case 4:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���4����Ԫ����Ϸ��ÿ�����������1��";
                break;
            case 5:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���2����Ԫ����Ϸ��ÿ�����������2��";
                break;
            case 6:
                this.transform.GetChild(7).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ʹ���1����Ԫ����Ϸ��ÿ�����������3��";
                break;
            case 7:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�ƾ�";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "��������Ϊ��֮��Ԫ�ĸ���";
                break;
            case 8:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�ƾ�";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "��������Ϊˮ֮��Ԫ�ĸ���";
                break;
            case 9:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�ƾ�";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "��������Ϊ��֮��Ԫ�ĸ���";
                break;
            case 10:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�ƾ�";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "��������Ϊ��֮��Ԫ�ĸ���";
                break;
            case 11:
                this.transform.GetChild(3).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "�ƾ�";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "��������Ϊ��֮��Ԫ�ĸ���";
                break;
            case 12:
                this.transform.GetChild(5).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "����ǰ����������Ԫ���������ĸ���";
                break;
            case 13:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ǿ����֮��Ԫ������Ч��";
                break;
            case 14:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ǿ��ˮ֮��Ԫ������Ч��";
                break;
            case 15:
                this.transform.GetChild(4).gameObject.SetActive(true);
                this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
                this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ǿ����֮��Ԫ������Ч��";
                break;
            //case 16:
            //    this.transform.GetChild(4).gameObject.SetActive(true);
            //    this.transform.GetChild(0).GetComponent<TextMeshPro>().text = "����";
            //    this.transform.GetChild(1).GetComponent<TextMeshPro>().text = "ǿ����֮��Ԫ������Ч��";
            //    break;
        }
    }
}
