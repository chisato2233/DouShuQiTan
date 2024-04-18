using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;
using DG.Tweening;
using System.Xml;
using DouShuQiTan;

public class startScene : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    //是否暂停探索
    public int StopExplo=0;

    public GameObject Menu;
    
    public  GameObject TanWindowPanel;


    //string Filepath = "D:" + "/card.txt";
    string Filepath;
    string Filepath_1;

    // Start is called before the first frame update
    void Awake() {
        Filepath = Application.dataPath + "/card.txt";
        Filepath_1 = Application.dataPath + "/bead.txt";
        GameData.Init();
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();

        StopExplo = PlayerPrefs.GetInt("StopExplo",0);
        var continueGameButton = Menu.transform.Find("Continue").gameObject;
        if (StopExplo == 0) {
            continueGameButton.GetComponentInChildren<startButton>().Disable();
        }
    }
    public void StartGame() {
        if(StopExplo==0) EnterNewGame();
        else TanWindowPanel.SetActive(true);
    }
    public void EnterNewGame() {
        ExploreSystem.IsNewExplo = true;
        if (PlayerPrefs.HasKey("nowLayer"))
        {
            PlayerPrefs.DeleteKey("nowLayer");
        }
        ExploreSystem.nowLayer = 1;
        GameData.Reset();
        sceneName = GameSceneName.MapScene;
        transitionManager.LoadScene(sceneName, "SlowFade", 0f);
    }

    public void ContinueGame() {
        ExploreSystem.IsNewExplo = false;
        if (PlayerPrefs.HasKey("nowLayer")) {
            ExploreSystem.nowLayer = PlayerPrefs.GetInt("nowLayer");
        }
        
        else ExploreSystem.nowLayer = 1;

        LoadFile();
        LoadFile_1();
        GameData.CardData.LoadCard();
        GameData.Load();


        sceneName = GameSceneName.MapScene; ;
        transitionManager.LoadScene(sceneName, "SlowFade", 0f);
    }



    void LoadFile() {
        //读档
        GameData.HoldCard.Clear();
        GameData.IsUpGrade.Clear();
        GameData.HasZhenXing.Clear();
        GameData.ZhenXing.Clear();
        if (File.Exists(Filepath))
        {
            string[] cardsFile = File.ReadAllLines(Filepath);
            if(cardsFile.Length>0)
            {
                for (int i = 0; i < cardsFile.Length; i++)
                {
                    string[] card = cardsFile[i].Split("\t");
                    int num;
                    Int32.TryParse(card[0], out num);
                    GameData.HoldCard.Add(num);

                    int IsUpgrade;
                    Int32.TryParse(card[1], out IsUpgrade);
                    if (IsUpgrade == 1)
                        GameData.IsUpGrade.Add(true);
                    else
                        GameData.IsUpGrade.Add(false);

                    int HasZhenXing;
                    Int32.TryParse(card[2], out HasZhenXing);
                    if (HasZhenXing == 1)
                        GameData.HasZhenXing.Add(true);
                    else
                        GameData.HasZhenXing.Add(false);

                    List<int[]> zhenxing = new List<int[]> { };
                    for (int j = 3; j < card.Length; j++)
                    {
                        string[] posString = card[j].Split('\'');
                        int[] pos = new int[2];
                        Int32.TryParse(posString[0], out pos[0]);
                        Int32.TryParse(posString[1], out pos[1]);
                        zhenxing.Add(pos);
                    }
                    GameData.ZhenXing.Add(zhenxing);
                }
            }
            
        }
        
    }
    void LoadFile_1()
    {
        //读档
        GameData.ExtraGrade.Clear();
        GameData.OnceUpGrade.Clear();
        GameData.ExtraRandom.Clear();
        GameData.ShuXingGaiLv.Clear();
        GameData.TuUp = 0;
        GameData.ShuiUp = 0;
        GameData.HuoUp = 0;
        GameData.FengUp = 0;
        if (File.Exists(Filepath_1))
        {
            string[] beadsFile = File.ReadAllLines(Filepath_1);
            if(beadsFile.Length>0)
            {
                for (int i = 0; i < beadsFile.Length - 3; i++)
                {
                    var bead = beadsFile[i].Split('\t');

                    int extragrade;
                    Int32.TryParse(bead[0], out extragrade);
                    GameData.ExtraGrade.Add(extragrade);

                    int onceup;
                    Int32.TryParse(bead[1], out onceup);
                    GameData.OnceUpGrade.Add(onceup);
                }

                int extraRandom;
                Int32.TryParse(beadsFile[beadsFile.Length - 3], out extraRandom);
                for (int i = 0; i < 16; i++)
                {
                    GameData.ExtraRandom.Add(extraRandom);
                }


                string[] ShuxingsFile = beadsFile[beadsFile.Length - 2].Split('\t');
                int[] Shuxings = new int[ShuxingsFile.Length];
                for (int i = 0; i < ShuxingsFile.Length; i++)
                {
                    int shuxing;
                    Int32.TryParse(ShuxingsFile[i], out shuxing);
                    Shuxings[i] = shuxing;
                }
                for (int i = 0; i < 16; i++)
                {
                    GameData.ShuXingGaiLv.Add(Shuxings);
                }

                string[] ShuXingUp = beadsFile[beadsFile.Length - 1].Split('\t');

                Int32.TryParse(ShuXingUp[0], out GameData.TuUp);
                Int32.TryParse(ShuXingUp[1], out GameData.ShuiUp);
                Int32.TryParse(ShuXingUp[2], out GameData.HuoUp);
                Int32.TryParse(ShuXingUp[3], out GameData.FengUp);

            }


        }
        
    }
    public void ExitGame() {
        Application.Quit();
    }
    public void Enter(GameObject Button) {
        AudioManager.Instance.PlaySfx("PlaceMouse");
        TextMeshProUGUI tmpText = Button.GetComponentInChildren<TextMeshProUGUI>();
        DOTween.To(
            () => tmpText.fontSize, 
            x => tmpText.fontSize = x, 
            90, 
            0.25f
        ).SetEase(Ease.OutQuad);


        Button.transform.GetChild(0).gameObject.SetActive(true);
        var num = Menu.transform.childCount;
        for(int i=0;i<Menu.transform.childCount;i++) {
            var b = Menu.transform.GetChild(i).gameObject;
            if (b != Button) {
                TextMeshProUGUI otherTmpText = b.GetComponentInChildren<TextMeshProUGUI>();
                DOTween.To(() => otherTmpText.fontSize, x => otherTmpText.fontSize = x, 50, 0.25f).SetEase(Ease.OutQuad);
            }
        }
    }
    public void Exit(GameObject Button) {
        Button.transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < Menu.transform.childCount; i++) {
            TextMeshProUGUI tmpText = Menu.transform.GetChild(i).GetComponentInChildren<TextMeshProUGUI>();
            DOTween.To(() => tmpText.fontSize, x => tmpText.fontSize = x, 70, 0.25f).SetEase(Ease.OutQuad);
        }
    }
    public void playClickSFX()
    {
        AudioManager.Instance.PlaySfx("icon");
    }
}
