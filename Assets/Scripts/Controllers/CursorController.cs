using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);
    Texture2D _attackIcon;
    Texture2D _handIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand
    }

    CursorType _cursorType = CursorType.None;


    // Start is called before the first frame update
    void Start()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            return; // 마우스가 눌리고 있으면 커서를 바꾸지 말자
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    // 커서 이미지 바꾸는 것이 이전 상태가 업데이트 되어야할 상태랑 다를때만 바꾼다. (깜빡 거리는 것을 방지하기 위해
                    Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                    // 두번째인자는 실제 클릭하는 텍스처 위치이다.
                    _cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
