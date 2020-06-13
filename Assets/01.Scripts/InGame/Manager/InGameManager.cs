﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class InGameManager : MonoBehaviour
{

    public static InGameManager instance;

    [HideInInspector]
    public ScoreManager scoreManager;
    
    [HideInInspector]
    public NodeInteractionController nodeInteractionController;

    private BackgroundController backgroundController;
    private NodeGenerator nodeGenerator;

    private void Awake(){
        if(instance is null){
            instance = this;
        }

        backgroundController = gameObject.GetComponent<BackgroundController>();
        nodeInteractionController = gameObject.GetComponent<NodeInteractionController>();
        nodeGenerator = gameObject.GetComponent<NodeGenerator>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
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
