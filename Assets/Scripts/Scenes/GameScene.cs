using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //class CoroutineTest : IEnumerable
    //{
    //    public IEnumerator GetEnumerator()
    //    {
    //        // 함수의 상태를 저장 복원 가능/
    //        // 엄청 오래 걸리는 작업을 잠시 끊거나
    //        // 원하는 타이밍레 함수를 잠시 정지하거나 복원하는 경우
    //        // 리턴은 우리가 원하는 타입으로 가능하다. class도 가능하다.
    //        //yield return 1;
    //        //yield break;
    //        //yield return 2;
    //        //yield return 3;
    //        //yield return 4;

    //        for (int i = 0; i < 1000000; i++)
    //        {
    //            if (i % 10000 == 0)
    //                yield return null; // 만번마다 쉰다.
    //        }

    //    }
    //}

    Coroutine co;

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
