using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        // start나 awake가 없어 실행이 되지 않을거 같지만 객체 자체가 어떤 타입으로 처음에 생성 되었는지가 중요하다. 부모는 가상함수로 선언되어 있어 이곳이 실행되게 된다.
        base.Init();

        SceneType = Define.Scene.Game;

        // Test
        //Managers.UI.ShowPopupUI<UI_Button>();
        //Managers.UI.ClosePopupUI();
        //Managers.UI.ClosePopupUI(ui); // 안전한 삭제이다.

        Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void Clear()
    {
        
    }
}
