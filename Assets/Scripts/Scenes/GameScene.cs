using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        // start나 awake가 없어 실행이 되지 않을거 같지만 객체 자체가 어떤 타입으로 처음에 생성 되었는지가 중요하다. 부모는 가상함수로 선언되어 있어 이곳이 실행되게 된다.
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
    }

    public override void Clear()
    {

    }
}
