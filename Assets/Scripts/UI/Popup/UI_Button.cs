using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup // UI_Base를 상속 받자 모노비헤이어도 간접적으로 상속 받는다.
{
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }

    // 컴토넌트가 아니라 오브젝트이다.
    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        ItemIcon
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();// 나의 부모님의 init도 호출한다.

        Bind<Button>(typeof(Buttons)); // 이 타입을 넘기겠다.(리플렉션이다.)
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // 이렇게도 할 수 있고
        //Get<Text>((int)Texts.ScoreText).text = "Bind Text";
        // 이렇게도 할 수 있다.
        //GetText((int)Texts.ScoreText).text = "Bind2 Text";

        // Images.ItemIcon여기에는 컴포넌트가 있다. 이 컴포턴트의 게임오브젝트에 접근해서 그 오브젝트가 가지고 있는 UI_이벤트핸들러를 가져온다.

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go.gameObject, ((PointerEventData data) => { go.transform.position = data.position; }), Define.UIEvent.Drag);
    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data)//public 반드시 퍼블릭으로 해야한다.
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"{_score}";
    }
}
