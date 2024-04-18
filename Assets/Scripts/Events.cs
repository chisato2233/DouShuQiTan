using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEngine;

public class Events : MonoBehaviour
{
    public Node node;
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;
    public GameObject package;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if ((info.normalizedTime > 0.95f) && info.IsName("eventDisappear"))
        {
            gameObject.SetActive(false);
        }
    }
    public void closeMaker()
    {
        GetComponent<Animator>().Play("eventDisappear");
    }
    public void Curse()
    {
        GetComponent<Animator>().Play("eventDisappear");
        node.save();
    }
    public void Exit()
    {
        GetComponent<Animator>().Play("eventDisappear");
        node.save();
    }
    public void Change()
    {
        package.SetActive(true);
        package.GetComponent<CardPackage>().events = this;
        package.GetComponent<CardPackage>().Changed();
    }
    public void Challenge()
    {
        node.Elite();
        //战斗奖励不同
        GameData.IsChallenge = true;
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
        sceneName = GameSceneName.GameScene;
        if(transitionManager!=null)
            transitionManager.LoadScene(sceneName, "Fade", 0f);
        
        GetComponent<Animator>().Play("eventDisappear");
    }
    public void UpGrade()
    {
        package.SetActive(true);
        package.GetComponent<CardPackage>().IsUpGrade = true;
        package.GetComponent<CardPackage>().events = this;
    }
    public void Forget()
    {
        package.SetActive(true);
        package.GetComponent<CardPackage>().events = this;
        package.GetComponent<CardPackage>().IsForgetEvent = true;
    }

}
