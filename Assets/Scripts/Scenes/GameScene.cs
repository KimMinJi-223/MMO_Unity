using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //class CoroutineTest : IEnumerable
    //{
    //    public IEnumerator GetEnumerator()
    //    {
    //        // �Լ��� ���¸� ���� ���� ����/
    //        // ��û ���� �ɸ��� �۾��� ��� ���ų�
    //        // ���ϴ� Ÿ�ַ̹� �Լ��� ��� �����ϰų� �����ϴ� ���
    //        // ������ �츮�� ���ϴ� Ÿ������ �����ϴ�. class�� �����ϴ�.
    //        //yield return 1;
    //        //yield break;
    //        //yield return 2;
    //        //yield return 3;
    //        //yield return 4;

    //        for (int i = 0; i < 1000000; i++)
    //        {
    //            if (i % 10000 == 0)
    //                yield return null; // �������� ����.
    //        }

    //    }
    //}

    Coroutine co;

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

        for (int i = 0; i < 5; i++)
        {
            Managers.Resource.Instantiate("UnityChan");
        }

        //CoroutineTest test = new CoroutineTest();
        //foreach (int t in test)
        //{
        //    Debug.Log(t);
        //}
        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        StopCoroutine(co);
    }
    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("ExplodeAfterSeconds.....");
        yield return new WaitForSeconds(seconds);
        Debug.Log("ExplodeAfterSeconds!!!!!");
        co = null;
    }

    public override void Clear()
    {

    }
}
