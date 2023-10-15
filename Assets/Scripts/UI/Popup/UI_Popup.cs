using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        Managers.UI.SetCanvas(gameObject, true); // �˾��� ���� �ʿ�
    }

    public virtual void ClosePopupUI() // �˾��� ClosePopupUI()�� ȣ���ϸ� �ڵ����� �˾��� �����ش�.
    {
        Managers.UI.ClosePopupUI(this);
    }
   
}
