using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour
{
    public int num;
    public startScene start;
    // Start is called before the first frame update
    private void Awake()
    {
        //start = transform.parent.GetComponent<startScene>();
        start = transform.GetComponentInParent<startScene>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enter()
    {
        //IsEnter = true;
        start.Enter(transform.parent.gameObject);
    }
    public void Exit()
    {
        //IsEnter = true;
        start.Exit(transform.parent.gameObject);
    }
}
