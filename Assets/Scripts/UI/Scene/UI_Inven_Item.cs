using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText
    }

    string _name;
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        // base.Init(); // �̰Ŵ� �� �� ����.
        Bind<GameObject>(typeof(GameObjects)); //  Bind<GameObject>�ڼ��� �����ص� ������ ������ GameObject�� ����

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) => { Debug.Log($"������{_name}�� Ŭ��"); });
        // �� ���ٽ� �ȿ��� PointerEventData������� �ʴ´�. �׷��� �Ű����� �̸��� ������ �ʾƵ� �ȴ�.
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

    void Update()
    {

    }
}
