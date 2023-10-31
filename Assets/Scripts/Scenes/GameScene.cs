using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        // start�� awake�� ���� ������ ���� ������ ������ ��ü ��ü�� � Ÿ������ ó���� ���� �Ǿ������� �߿��ϴ�. �θ�� �����Լ��� ����Ǿ� �־� �̰��� ����ǰ� �ȴ�.
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;

        gameObject.GetOrAddComponent<CursorController>();
        
    }

    public override void Clear()
    {

    }
}
