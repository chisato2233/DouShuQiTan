using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Node : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public int abscissa;
    public int ordinate;
    public int mark;
    public int branch;
    public ExploreSystem explore;
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    //public static bool IsWin=false;

    // Start is called before the first frame update
    void Start()
    {
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
        sceneName = "SampleScene";
    }
    private void Awake()
    {
           transform.GetChild(0).gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {

    }
   
    public void InitNode(int _abscissa ,int _ordinate,int _mark,int _branch,ExploreSystem _explore)
    {
        abscissa = _abscissa;
        ordinate = _ordinate;
        mark = _mark;
        branch = _branch;
        explore = _explore;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(GetComponent<Animator>()!=null)
        {
            var animator = GetComponent<Animator>();
            animator.SetBool("IsSelect", false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Animator>() != null)
        {
            var animator = GetComponent<Animator>();
            animator.SetBool("IsSelect", true);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlaySfx("clickExplore");
        if(abscissa!=explore.layerNumber-1)
            transform.GetChild(1).gameObject.SetActive(true);
        if (mark == 1 || mark == 4 || mark == 5)
        {
            if (abscissa == 0)
                GameData.IsOne = true;
            if (mark == 1)
            {
                
                if (branch == 1)
                {
                    Feng();
                }
                else if (branch == 2)
                {
                    Yun();
                }
            }
            else if (mark == 4)
            {
                Elite();
            }
            else if (mark == 5)
            {
                Boss();
            }
            transitionManager.LoadScene(sceneName, "Fade", 0f);

        }
        else if(mark==2)
        {
            var canvas=GameObject.Find("Canvas");
            if(canvas!=null)
            {
                var rest = canvas.transform.GetChild(1).gameObject;
                rest.GetComponent<Rest>().node = this;
                rest.SetActive(true);
                rest.GetComponent<Animator>().Play("appear");
            }
        }
        else if(mark==3)
        {
            if(branch==1||branch==2||branch==3)
            {
                if (branch == 1)
                {
                    Feng();
                }
                else if (branch == 2)
                {
                    Yun();
                }

                else if (branch == 3)
                {
                    Elite();
                }
                transitionManager.LoadScene(sceneName, "Fade", 0f);

            }
            else if(branch==4)
            {
                //增益
                var canvas = GameObject.Find("Canvas");
                if (canvas != null)
                {
                    var good = canvas.transform.GetChild(2).gameObject;
                    good.GetComponent<Events>().node = this;
                    good.SetActive(true);
                    good.GetComponent<Animator>().Play("eventAppear");
                }
            }
            else if(branch==5)
            {
                //减益
                var canvas = GameObject.Find("Canvas");
                if (canvas != null)
                {
                    var good = canvas.transform.GetChild(3).gameObject;
                    good.GetComponent<Events>().node = this;
                    good.SetActive(true);
                    good.GetComponent<Animator>().Play("eventAppear");
                }
            }
            else if(branch==6)
            {
                //中立
                var canvas = GameObject.Find("Canvas");
                if (canvas != null)
                {
                    var good = canvas.transform.GetChild(4).gameObject;
                    good.GetComponent<Events>().node = this;
                    good.SetActive(true);
                    good.GetComponent<Animator>().Play("eventAppear");
                }
            }
            else if(branch==7)
            {
                //挑战
                var canvas = GameObject.Find("Canvas");
                if (canvas != null)
                {
                    var good = canvas.transform.GetChild(5).gameObject;
                    good.GetComponent<Events>().node = this;
                    good.SetActive(true);
                    good.GetComponent<Animator>().Play("eventAppear");
                }
            }

        }

    }
    public void save()
    {
        //战斗界面在战斗奖励时再存档
        node _node = explore.Nodes[abscissa][ordinate];
        _node.state = 3;
        explore.Nodes[abscissa][ordinate] = _node;
        for(int i=0;i<explore.Nodes[abscissa].Count;i++)
        {
            if(i!=ordinate)
            {
                node node_1 = explore.Nodes[abscissa][i];
                node_1.state = 1;
                explore.Nodes[abscissa][i] = node_1;
            }
        }
        explore.save();
        ExploreSystem.nowLayer++;
        PlayerPrefs.SetInt("nowLayer", ExploreSystem.nowLayer);
        explore.IsNewLayer = true;
    }
    void Feng()
    {
        GameData.Enemy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        GameData.abscissa = abscissa;
        GameData.ordinate = ordinate;
        UpdateNanDu();

        //for (int i = 0; i < 3; i++)
        //{
        //    if (i == 0)
        if (abscissa == 0)
            GameData.Enemy[0]++;
        else if (abscissa == 1 || abscissa == 2)
            GameData.Enemy[Random.Range(0, 2)]++;
        else
        GameData.Enemy[UnityEngine.Random.Range(0, 3)]++;
        //    if (i > 0 && UnityEngine.Random.Range(1, 3) < 2)
        //        GameData.Enemy[UnityEngine.Random.Range(0, 3)]++;

        //}
    }
    void Yun()
    {
        GameData.Enemy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        GameData.abscissa = abscissa;
        GameData.ordinate = ordinate;
        UpdateNanDu();

        //for (int i = 0; i < 3; i++)
        //{
        //    if (i == 0)
        if (abscissa == 0)
            GameData.Enemy[3]++;
        else if (abscissa == 1 || abscissa == 2)
            GameData.Enemy[Random.Range(3, 5)]++;
        else
            GameData.Enemy[UnityEngine.Random.Range(3, 6)]++;
        //    if (i > 0 && UnityEngine.Random.Range(1, 3) < 2)
        //        GameData.Enemy[UnityEngine.Random.Range(3, 6)]++;

        //}
    }
    public void Elite()
    {
        GameData.Enemy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        GameData.abscissa = abscissa;
        GameData.ordinate = ordinate;

        UpdateNanDu();
        GameData.Enemy[6]++;

    }
    void Boss()
    {
        //Boss无难度之分
        GameData.Enemy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        GameData.abscissa = abscissa;
        GameData.ordinate = ordinate;

        GameData.Enemy[7]++;


    }
    void UpdateNanDu()
    {
        if (abscissa >= 0 && abscissa <= explore.elite[0])
            GameData.NanDu = 1;
        //else if (abscissa > explore.elite[0] && abscissa <= explore.elite[1])
        //    GameData.NanDu = 2;
        else if (abscissa > explore.elite[0] && abscissa < explore.layerNumber-1)
            GameData.NanDu = 2;
    }
    
}
