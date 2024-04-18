using System.Collections;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEngine;

public class PlayLogo : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        //自动播放2秒跳转
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
        sceneName = GameSceneName.TitleScene;
        transitionManager.LoadScene(sceneName, "SlowFade", 7f);
    }

}
