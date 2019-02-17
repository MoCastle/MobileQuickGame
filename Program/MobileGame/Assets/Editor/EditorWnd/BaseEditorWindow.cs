using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

abstract public class BaseEditorWindow : EditorWindow
{
    protected Vector2 _UISize;
    protected float _VValue;
    protected float _HValue;
    public BaseEditorWindow()
    {
        _UISize = Vector2.zero;
        _VValue = 0;
        _HValue = 0;
    }
    protected void _AddPosition(Rect rect)
    {
        float MaxHeight = rect.y + rect.height;
        if (_UISize.y< MaxHeight)
        {
            _UISize.y = MaxHeight;
        }
        float MaxWidth = rect.x + rect.width;
        if(_UISize.x<MaxWidth)
        {
            _UISize.x = MaxWidth;
        }
    }
    public void OnGUI()
    {
        _ShowWnd();
        _SetScroll();
    }
    protected abstract void _ShowWnd();
    protected virtual void _SetScroll()
    {
        if(_UISize.y> Screen.height)
        {
            _VValue = GUI.VerticalScrollbar(new Rect(Screen.width - 50, 0, 50, Screen.height), _VValue, Screen.height/ _UISize.y, 0, _UISize.y - Screen.height);
        }
        if (_UISize.x > Screen.width)
        {
            _HValue = GUI.VerticalScrollbar(new Rect(Screen.width - 50, 0, 50, Screen.height), _HValue, Screen.width/ _UISize.x, 0, _UISize.x- Screen.width);
        }
    }
    protected Rect GetResAfterOffPS(Rect rect)
    {
        rect.x += _HValue;
        rect.y += _VValue;
        return rect;
    }
}
