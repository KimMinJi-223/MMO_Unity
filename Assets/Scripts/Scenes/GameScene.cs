using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        // start�� awake�� ���� ������ ���� ������ ������ ��ü ��ü�� � Ÿ������ ó���� ���� �Ǿ������� �߿��ϴ�. �θ�� �����Լ��� ����Ǿ� �־� �̰��� ����ǰ� �ȴ�.
        base.Init();

        SceneType = Define.Scene.Game;

        // Test
        //Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ClosePopupUI();
        //Managers.UI.ClosePopupUI(ui); // ������ �����̴�.

        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void Clear()
    {
        
    }
}
