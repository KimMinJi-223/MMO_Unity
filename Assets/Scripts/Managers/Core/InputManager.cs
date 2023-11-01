using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager // 컴포넌트로 만들 필요가 없다.
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0.0f;

    public void OnUpdate() // 체크하는 부분이 유일하다.
    {
        // 지금 UI버튼이 눌러있는지 확인하는 코드
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) // 마우스를 눌렀다
            {
                if (!_pressed)
                {
                    // 여기에 들어오면 마우스를 뗐다가 다시 처음 누른 그런 상태
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed) // 마우스를 누른 이벤트가 들어왔을때 마우스 이벤트를 처리한다.
                {
                    if (Time.time < _pressedTime + 0.2f) // 0.2초 안에 떼면 클릭이다.
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);


                }
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        keyAction = null;
        MouseAction = null;
    }
}
