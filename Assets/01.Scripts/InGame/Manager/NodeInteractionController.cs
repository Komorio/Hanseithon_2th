﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInteractionController : MonoBehaviour
{
    private List<List<Node>> activeNormalNodes = new List<List<Node>>();


    private void Awake(){
        for(int i = 0; i < 8; i++){
            activeNormalNodes.Add(new List<Node>());
        }
    }

    public void AddActiveNormalNode(Node node, int position){
        activeNormalNodes[position].Add(node);
    }

    public void RemoveActiveNormalNode(Node node, int position){
        activeNormalNodes[position].Remove(node);
    }


    public void NoramlNodeInteraction(int position){
        try{
            activeNormalNodes[position][0]?.Interaction();
        }catch{
            // FIXME : Empty catch
            return ;
        }
    }
}
