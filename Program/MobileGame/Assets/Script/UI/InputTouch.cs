﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputTouch : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float StD = 100;//向饱和距离

    Vector2 PushStart = Vector2.zero;
    Vector2 PushingPs = Vector2.zero;
    bool _IsInputting = false;
    public Text LogOutPut;
    void Awake()
    {
        PlayerCtrl.RefreshInputRoundArr();
        StD = GetComponent<RectTransform>().sizeDelta.x / 2;
    }
    void ClearAllPs( )
    {
        PushStart = Vector2.zero;
        PushingPs = Vector2.zero;
    }
    public void OnDrag(PointerEventData eventData)
    {
        PushingPs = eventData.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PushStart = eventData.position;
        PushingPs = eventData.position;
        _IsInputting = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _IsInputting = false;
        OutPutCommand( );
        ClearAllPs();
    }

	// Update is called once per frame
	void Update( )
    {
        LogOutPut.text = PushingPs.ToString();
        if( _IsInputting )
        {
            OutPutCommand();
        }
    }

    void OutPutCommand( )
    {
        Vector2 InputVector = PushingPs - PushStart;
        InputInfo OutPut = new InputInfo( true );
        OutPut.IsPushing = _IsInputting;
        OutPut.Shift = InputVector;
        OutPut.MaxDst = StD;
        PlayerCtrl.InputHandTouch(OutPut);
    }
}
