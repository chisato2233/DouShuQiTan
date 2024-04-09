using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BeadPackage : MonoBehaviour
{
    public Transform scroll;
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
                    = "<sprite=" + (GameData.ExtraGrade[i] + 1).ToString() + ">";

                if (GameData.OnceUpGrade[i] != 0)
                    bead.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text
                        = "ÿ�����������" + GameData.OnceUpGrade[i].ToString() + "��";
                else
                    bead.GetChild(2).GetChild(0).GetComponent<TextMeshPro>().text
                        = "�޶�������";

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
                        = "�޶�������";
            }
        }
        if (GameData.ExtraRandom.Count>0)
            scroll.GetChild(16).GetChild(0).GetComponent<TextMeshPro>().text
                = "*��Ԫǰ�����������������Եĸ��ʣ�" + (20+GameData.ExtraRandom[0]*2).ToString() + "%";
        else
            scroll.GetChild(16).GetChild(0).GetComponent<TextMeshPro>().text
                                = "*��Ԫǰ�����������������Եĸ��ʣ�"  + "20%";

        if(GameData.ShuXingGaiLv.Count>0)
        for(int i=0;i<4;i++)
        {
            scroll.GetChild(17 + i).GetChild(1).GetComponent<TextMeshPro>().text
                = "�����������Եĸ��ʣ�" + (GameData.ShuXingGaiLv[0][i+1]).ToString()+"%";

        }
        else
        {
            for(int i=0;i<4;i++)
            {
                scroll.GetChild(17 + i).GetChild(1).GetComponent<TextMeshPro>().text
                = "�����������Եĸ��ʣ�20%";
            }
        }
        if (GameData.TuUp == 1)
        {
            scroll.GetChild(17).GetChild(2).GetComponent<TextMeshPro>().text = "��ǿ��";
            scroll.GetChild(17).GetChild(3).GetComponent<TextMeshPro>().text
                = "��������ͬʱ����3�Ż�֮��Ԫʱ����ɫ�ظ���֮��Ԫ���ȼ�֮��*(1+0.25*n)����Ѫ��";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if(GameData.ShuiUp==1)
        {
            scroll.GetChild(18).GetChild(2).GetComponent<TextMeshPro>().text = "��ǿ��";
            scroll.GetChild(18).GetChild(3).GetComponent<TextMeshPro>().text
                = "������ÿ��ˮ֮��Ԫ������з�������ɵȼ�*3(+0.25*n)���˺�";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if (GameData.HuoUp==1)
        {
            scroll.GetChild(19).GetChild(2).GetComponent<TextMeshPro>().text = "��ǿ��";
            scroll.GetChild(19).GetChild(3).GetComponent<TextMeshPro>().text
                = "ÿ��һ�Ż�֮��Ԫ��ѵ�������У��������Եз��˺�����25(+2.5%*n)%";
            scroll.GetChild(21).gameObject.SetActive(true);
        }
        if (GameData.FengUp==1)
        {
            scroll.GetChild(20).GetChild(2).GetComponent<TextMeshPro>().text = "��ǿ��";
            scroll.GetChild(20).GetChild(3).GetComponent<TextMeshPro>().text
                = "ÿ�����ŷ�֮��Ԫ�������ϣ����n����Ԫ�ȼ�����2��������������Ԫ�ȼ�����1��";
            scroll.GetChild(21).gameObject.SetActive(true);
        }




    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
