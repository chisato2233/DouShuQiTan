using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerIntro : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    public GameObject Package;
    //public static bool IsWin=false;    
    //�浵λ��
    //string Filepath = "D:" + "/card.txt";
    string Filepath;
    string Filepath_1;
    private void Awake()
    {
        Filepath = Application.dataPath + "/card.txt";
        Filepath_1 = Application.dataPath + "/bead.txt";
    }
    void Start()
    {
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click1()
    {
        Debug.Log(((CardType)1).ToString());
    }
    //����
    public void title()
    {
        transitionManager.LoadScene(sceneName, "SlowFade", 0f);

    }

    //����������
    public void Return()
    {
        save();
        save_1();
        PlayerPrefs.SetFloat("HP", GameData.HP);

        sceneName = "start";
        transitionManager.LoadScene(sceneName, "SlowFade", 0f);
        
    }
    void save()
    {
        //�浵
        if (!File.Exists(Filepath))
        {
            FileStream fileStream = new FileStream(Filepath, FileMode.OpenOrCreate);
            fileStream.Close();
        }
        //����ļ�
        FileStream fs = File.Open(Filepath, FileMode.OpenOrCreate, FileAccess.Write);
        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);
        fs.Close();

        if (GameData.HoldCard.Count>0)
        {
            string[] cardsFile = new string[GameData.HoldCard.Count];
            for (int i = 0; i < GameData.HoldCard.Count; i++)
            {
                //����
                string card = GameData.HoldCard[i].ToString();
                //�Ƿ�����
                card = card + '\t' + (GameData.IsUpGrade[i] == true ? 1.ToString() : 0.ToString());
                //�Ƿ�������
                card = card + '\t' + (GameData.HasZhenXing[i] == true ? 1.ToString() : 0.ToString());

                var zhenXing = GameData.ZhenXing[i];
                for (int j = 0; j < zhenXing.Count; j++)
                {
                    for (int x = 0; x < 2; x++)
                        if (x == 0)
                            card = card + '\t' + zhenXing[j][x].ToString();
                        else
                            card = card + '\'' + zhenXing[j][x].ToString();
                }
                cardsFile[i] = card;
            }
            File.WriteAllLines(Filepath, cardsFile);
        }
        
    }
    void save_1()
    {
        //�浵
        if (!File.Exists(Filepath_1))
        {
            FileStream fileStream = new FileStream(Filepath_1, FileMode.OpenOrCreate);
            fileStream.Close();
        }
        //����ļ�
        FileStream fs = File.Open(Filepath_1, FileMode.OpenOrCreate, FileAccess.Write);
        fs.Seek(0, SeekOrigin.Begin);
        fs.SetLength(0);
        fs.Close();

        if (GameData.ExtraGrade.Count>0)
        {
            
            string[] beadsFile = new string[GameData.ExtraGrade.Count + 3];
            string bead = "";
            for (int i = 0; i < GameData.ExtraGrade.Count; i++)
            {
                //��ʼ�ȼ�
                bead = GameData.ExtraGrade[i].ToString();
                //ÿ�ζ�������
                bead = bead + '\t' + GameData.OnceUpGrade[i].ToString();

                beadsFile[i] = bead;
            }
            //ǰ���ν��������Եĸ���
            bead = GameData.ExtraRandom[0].ToString();
            beadsFile[GameData.ExtraGrade.Count] = bead;
            //�����Ը���
            bead = GameData.ShuXingGaiLv[0][0].ToString() + '\t' + GameData.ShuXingGaiLv[0][1].ToString()
                + '\t' + GameData.ShuXingGaiLv[0][2].ToString() + '\t' + GameData.ShuXingGaiLv[0][3].ToString()
                + '\t' + GameData.ShuXingGaiLv[0][4].ToString();
            beadsFile[GameData.ExtraGrade.Count + 1] = bead;
            //�������Ƿ�ǿ��
            bead = GameData.TuUp.ToString() + '\t' + GameData.ShuiUp.ToString() + '\t'
                + GameData.HuoUp.ToString() + '\t' + GameData.FengUp.ToString();
            beadsFile[GameData.ExtraGrade.Count + 2] = bead;
            File.WriteAllLines(Filepath_1, beadsFile);
        }
    }
    //�����ؿ�ʼ���棬�رյ���
    public void DontExit()
    {
        gameObject.SetActive(false);
    }

    //������������ƿ�ʹ���icon
    public void EnterButton()
    {
        gameObject.GetComponent<Animator>().SetBool("IsEnter", true);

    }
    public void ExitButton()
    {
        gameObject.GetComponent<Animator>().SetBool("IsEnter", false);

    }
    public void ClickReturn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ClickPackage()
    {
        Package.SetActive(true);
    }
    
    public void ClosePackage()
    {
        Package.SetActive(false);
    }
    public void PlayJuQing()
    {
        var JuQing = GameObject.Find("JuQing");
        if (JuQing != null)
            JuQing.GetComponent<Animator>().Play("Appear");
    }
}