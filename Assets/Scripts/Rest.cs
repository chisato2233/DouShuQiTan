using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    public Node node;
    public GameObject package;
    public ExploreSystem explore;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        if ((info.normalizedTime > 0.95f) && info.IsName("HeiPing"))
        {
            gameObject.SetActive(false);
        }
    }
    public void Cure()
    {
        var Explore = GameObject.Find("ExploreSystem");
        if (Explore != null)
            explore = Explore.GetComponent<ExploreSystem>();
        if ((int)(GameData.HP + GameData.MaxHp * 0.25f) <= GameData.MaxHp)
            GameData.HP = (int)(GameData.HP + GameData.MaxHp * 0.25f);
        else
            GameData.HP = GameData.MaxHp;
        explore.UpdateHP();
        GetComponent<Animator>().Play("HeiPing");
        node.save();
    }
    public void Forget()
    {
        package.SetActive(true);
        package.GetComponent<CardPackage>().rest = this;
        package.GetComponent<CardPackage>().IsForgetRest = true;
        
    }
    
}
