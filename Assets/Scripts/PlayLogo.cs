using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayLogo : MonoBehaviour
{
    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        //�Զ�����2����ת
        transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
        sceneName = "Title";
        transitionManager.LoadScene(sceneName, "SlowFade", 7f);
    }

}
