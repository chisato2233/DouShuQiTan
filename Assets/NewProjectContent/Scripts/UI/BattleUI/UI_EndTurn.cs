using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DouShuQiTan;
using UnityEngine;

public class UI_EndTurn:MonoBehaviour {
    public void EndTurn() {
        Debug.Log("Click End Turn ");
        GameIntro.GameSystem.UserOperationSystem.EndPlayerTurn();
    }
}
