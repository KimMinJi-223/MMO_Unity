using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager // ������Ʈ�� ���� �ʿ䰡 ����.
{
    public Action keyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0.0f;

    public void OnUpdate() // üũ�ϴ� �κ��� �����ϴ�.
    {
        // ���� UI��ư�� �����ִ��� Ȯ���ϴ� �ڵ�
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && keyAction != null)
            keyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) // ���콺�� ������
            {
                if (!_pressed)
                {
                    // ���⿡ ������ ���콺�� �ôٰ� �ٽ� ó�� ���� �׷� ����
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed) // ���콺�� ���� �̺�Ʈ�� �������� ���콺 �̺�Ʈ�� ó���Ѵ�.
                {
                    if (Time.time < _pressedTime + 0.2f) // 0.2�� �ȿ� ���� Ŭ���̴�.
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
