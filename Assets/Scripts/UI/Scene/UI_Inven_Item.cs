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
        // base.Init(); // 이거는 할 수 없다.
        Bind<GameObject>(typeof(GameObjects)); //  Bind<GameObject>자세히 지정해도 되지만 지금은 GameObject로 하자

        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) => { Debug.Log($"아이템{_name}번 클릭"); });
        // 이 람다식 안에서 PointerEventData사용하지 않는다. 그래서 매개변수 이름을 붙이지 않아도 된다.
    }

    public void SetInfo(string name)
    {
        _name = name;
    }

    void Update()
    {

    }
}
