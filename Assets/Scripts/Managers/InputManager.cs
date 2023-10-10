using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager // 컴포넌트로 만들 필요가 없다.
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false;
    public void OnUpdate() // 체크하는 부분이 유일하다.
    {
        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }
}
