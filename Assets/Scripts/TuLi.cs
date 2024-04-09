using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuLi : MonoBehaviour
{
    private bool IsTanChu=false;
    
    public void Click()
    {
        if(IsTanChu)
        {
            IsTanChu = false;
            GetComponent<Animator>().SetBool("IsTanChu",false);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsTanChu", true);
            IsTanChu = true;        
        }
    }
}
