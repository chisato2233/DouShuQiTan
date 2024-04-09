using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChouJiang : MonoBehaviour
{
    float t = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (ExploreSystem.IsNewExplo)
        {
            AudioManager.Instance.PlaySfx("Tiger");
            AudioManager.Instance.PlayMusic("battle");
            for (int i = 0; i < 4; i++)
                transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("choujiang", true);
            transform.GetChild(4).GetChild(0).GetComponent<Button>().enabled = false;
            var JuQing = GameObject.Find("JuQing");
            if(JuQing!=null)
                JuQing.SetActive(true);
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
            AudioManager.Instance.PlayMusic("battle");
            var JuQing = GameObject.Find("JuQing");
            if (JuQing != null)
                JuQing.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t > 2)
        {
            for(int i=0;i<4;i++)
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Animator>().SetBool("choujiang", false);
        }
        if (t > 4.6)
            transform.GetChild(4).GetChild(0).GetComponent<Button>().enabled = true;
        
    }
}
