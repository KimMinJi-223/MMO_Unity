using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static void AddUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //어떠한 GameObject더라도 AddUIEvent를 불러올 수 있다고 생각하는 거라함
        UI_Base.AddUIEvent(go, action, type);
    }
}
