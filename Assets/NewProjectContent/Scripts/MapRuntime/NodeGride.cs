using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeGride : MonoBehaviour {
    [SerializeField] private GameObject MyNode;
    void Awake(){
        MyNode = transform.gameObject;
    }
}
