﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InGameManager : MonoBehaviour
{
    [HideInInspector]
    public ScoreManager scoreManager;
    
    private BackgroundController backgroundController;
    
    private void Awake(){
        backgroundController = gameObject.GetComponent<BackgroundController>();
        scoreManager - gameObject.GetComponent<ScoreManager>();
    }

    [Button("Change Background Color")]
    private void ChangeBackgroundColor() { 
        backgroundController.Execute();
    }

    public void Death(){

    }

    public void GameEnd(){

    }

}