﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Node : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private int _score;

    [SerializeField]
    private int _reducedHp;

    [SerializeField]
    private float _defaultSpeed;

    [SerializeField]
    private Ease _easeType;

    [SerializeField]
    private float _judgePerfect;
    
    [SerializeField]
    private float _judgeGreat;
    
    [SerializeField]
    private float _judgeGood;
    
    private SpriteRenderer _spriteRenderer;

    protected int score => _score;
    protected int reducedHp => _reducedHp;
    protected float defaultSpeed => _defaultSpeed;    
    
    protected Ease easeType => _easeType;

    protected float judgePerfect => _judgePerfect;
    protected float judgeGreat => _judgeGreat;
    protected float judgeGood => _judgeGood;
    
    protected int positionIndex {get; set;}
    protected SpriteRenderer spriteRenderer => _spriteRenderer;
    
    protected void Awake(){
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual void Execute(Vector2 startPosition, Vector2 endPosition, int index){ }

    public virtual void Interaction(){ }
    
    public virtual void FailedInteraction(){}

    public virtual void ObjectReset(){ 
        gameObject.SetActive(false);
    }   
}
