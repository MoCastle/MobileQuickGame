using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
struct KeyValue
{
    string Key;
    string Value;
}
public class ItemEditor : BaseEditorWindow
{
    List<LinkedList<KeyValue>> CfgData;
    public ItemEditor()
    {
        CfgData = new List<LinkedList<KeyValue>>();
    }

    protected override void _ShowWnd()
    {
        _ShowList();
    }

    void _ShowList()
    {
        Rect staticRect;
        staticRect= new Rect(0,0,60,30);
        _AddPosition(staticRect);
        staticRect = GetResAfterOffPS(staticRect);
        GUI.Label(staticRect, "键修改");
    }
}

