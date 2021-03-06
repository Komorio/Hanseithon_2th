﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour, IKeyObserver
{
    private bool isHolding;

    private int leftValue;
    private int rightValue;

    private int leftHoldingValue;
    private int rightHoldingValue;

    private IEnumerator leftAreaCoroutine;
    private IEnumerator rightAreaCoroutine;

    private Tween leftAreaTween;
    private Tween rightAreaTween;

    private Dictionary<KeyCode,Action> keyDownActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode,Action> keyUpActions = new Dictionary<KeyCode, Action>();
    private Dictionary<KeyCode,Action> keyHoldingActions = new Dictionary<KeyCode, Action>();

    [Header("Objects")]
    [SerializeField]
    private VerticalLine[] verticalLines;
    
    [SerializeField]
    private GameObject centerEffect;

    [SerializeField]
    private Image leftAreaImage;

    [SerializeField]
    private Image rightAreaImage;

    [Header("Events")]
    [SerializeField]
    private IntEvent leftEvent;

    [SerializeField]
    private IntEvent rightEvent;

    [Space(30)]
    [SerializeField]
    private IntEvent leftUpEvent;

    [SerializeField]
    private IntEvent rightUpEvent;

    [Space(30)]
    [SerializeField]
    private IntEvent leftHoldingEvent;
    
    [SerializeField]
    private IntEvent rightHoldingEvent;

    private Color areaDefaultColor;

    private void Awake(){
        areaDefaultColor = Color.white;
        areaDefaultColor.a = 0.5f;

        keyDownActions.Add(KeyCode.Mouse0, () => {
            leftEvent.Invoke(leftValue);
            leftHoldingValue = leftValue;

            leftAreaCoroutine?.Stop(this);
            leftAreaCoroutine = AreaClick(leftAreaImage, leftAreaTween).Start(this);
        });

        keyDownActions.Add(KeyCode.Mouse1, () => {
            rightEvent.Invoke(rightValue);
            rightHoldingValue = rightValue;

            rightAreaCoroutine?.Stop(this);
            rightAreaCoroutine = AreaClick(rightAreaImage, rightAreaTween).Start(this);
        });

        keyUpActions.Add(KeyCode.Mouse0, () => {
            leftUpEvent.Invoke(leftValue);
        });

        keyUpActions.Add(KeyCode.Mouse1, () => {
            rightUpEvent.Invoke(rightValue);
        });

        keyHoldingActions.Add(KeyCode.Mouse0, () => {
            if(leftValue.Equals(leftHoldingValue)){
                leftHoldingEvent.Invoke(leftHoldingValue);
            } else {
                leftUpEvent.Invoke(leftHoldingValue);
            }
        });

        keyHoldingActions.Add(KeyCode.Mouse1, () => {
            if(rightValue.Equals(rightHoldingValue)){
                rightHoldingEvent.Invoke(rightHoldingValue);
            } else {
                rightUpEvent.Invoke(rightHoldingValue);
            }
        });        
        
        keyDownActions.Add(KeyCode.Z, () => {
            leftEvent.Invoke(leftValue);
            leftHoldingValue = leftValue;

            leftAreaCoroutine?.Stop(this);
            leftAreaCoroutine = AreaClick(leftAreaImage, leftAreaTween).Start(this);
        });

        keyDownActions.Add(KeyCode.X, () => {
            rightEvent.Invoke(rightValue);
            rightHoldingValue = rightValue;

            rightAreaCoroutine?.Stop(this);
            rightAreaCoroutine = AreaClick(rightAreaImage, rightAreaTween).Start(this);
        });
        
        keyDownActions.Add(KeyCode.Escape, () => {
            InGameManager.instance.GameEnd();
        });

        keyUpActions.Add(KeyCode.Z, () => {
            leftUpEvent.Invoke(leftValue);
        });

        keyUpActions.Add(KeyCode.X, () => {
            rightUpEvent.Invoke(rightValue);
        });

        keyHoldingActions.Add(KeyCode.Z, () => {
            if(leftValue.Equals(leftHoldingValue)){
                leftHoldingEvent.Invoke(leftHoldingValue);
            } else {
                leftUpEvent.Invoke(leftHoldingValue);
            }
        });

        keyHoldingActions.Add(KeyCode.X, () => {
            if(rightValue.Equals(rightHoldingValue)){
                rightHoldingEvent.Invoke(rightHoldingValue);
            } else {
                rightUpEvent.Invoke(rightHoldingValue);
            }
        });
    }

    private void Start(){
        MouseInputHandler.instance.AddObserver(this);
    }

    private void Update(){
        SetPosition();
        GetAdjacentLineValue();
    }

    public void KeyDownNotify(KeyCode key){
        if(keyDownActions.ContainsKey(key)){
            keyDownActions[key]();
        }
    }

    public void KeyHoldingNotify(KeyCode key){
        if(keyHoldingActions.ContainsKey(key)){
            keyHoldingActions[key]();
        }
    }

    public void KeyUpNotify(KeyCode key){
        if(keyUpActions.ContainsKey(key)){
            keyUpActions[key]();
        }
    }

    private IEnumerator AreaClick(Image image, Tween tween){
        image.color = Color.white;
        yield return YieldInstructionCache.WaitSeconds(0.1f);
        image.color = areaDefaultColor;
    }

    private void SetPosition(){
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Vector2 newPosition = mousePosition;

        if(Mathf.Abs(newPosition.x) < 5.1f){
            newPosition.y = gameObject.transform.position.y;
            gameObject.transform.position = newPosition;
        }

        if(Vector2.Distance(mousePosition, gameObject.transform.position) < 3.2f){
            mousePosition.x = gameObject.transform.position.x;
            centerEffect.transform.position = mousePosition;
        }
    }

    private void GetAdjacentLineValue(){
        var query = from o in verticalLines
                orderby (gameObject.transform.position - o.gameObject.transform.position).sqrMagnitude
                select o;

        VerticalLine selectObject = query.FirstOrDefault();

        leftValue = selectObject.LeftIndex;
        rightValue = selectObject.RightIndex;
    }

    private void OnDestroy() {
        MouseInputHandler.instance.RemoveObserver(this);    
    }

}
