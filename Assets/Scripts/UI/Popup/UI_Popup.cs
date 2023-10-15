using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject, true); // 팝업은 순서 필요
    }

    public virtual void ClosePopupUI() // 팝업은 ClosePopupUI()을 호출하면 자동으로 팝업을 지워준다.
    {
        Managers.UI.ClosePopupUI(this);
    }
   
}
