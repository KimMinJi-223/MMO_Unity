using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup // UI_Base�� ��� ���� �������̾ ���������� ��� �޴´�.
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

    // �����Ʈ�� �ƴ϶� ������Ʈ�̴�.
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
        base.Init();// ���� �θ���� init�� ȣ���Ѵ�.

        Bind<Button>(typeof(Buttons)); // �� Ÿ���� �ѱ�ڴ�.(���÷����̴�.)
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // �̷��Ե� �� �� �ְ�
        //Get<Text>((int)Texts.ScoreText).text = "Bind Text";
        // �̷��Ե� �� �� �ִ�.
        //GetText((int)Texts.ScoreText).text = "Bind2 Text";

        // Images.ItemIcon���⿡�� ������Ʈ�� �ִ�. �� ������Ʈ�� ���ӿ�����Ʈ�� �����ؼ� �� ������Ʈ�� ������ �ִ� UI_�̺�Ʈ�ڵ鷯�� �����´�.

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go.gameObject, ((PointerEventData data) => { go.transform.position = data.position; }), Define.UIEvent.Drag);
    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data)//public �ݵ�� �ۺ����� �ؾ��Ѵ�.
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"{_score}";
    }
}
