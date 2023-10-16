using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init(); // init은 UI_Base가 직접 쓰이지 않기 때문에 자식에서 정의한다.

    // 바인드하는 부분
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // string의 배열로 넘겨준다.

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.Findchild(gameObject, names[i], true);
            else
                objects[i] = Util.Findchild<T>(gameObject, names[i], true);
            // where T : UnityEngine.Object이거 없으면 오류 : 조건이 맞지 않아서

            if (objects[i] == null)
                Debug.Log($"Failed to Bind {names[i]}");
        }
    }

    // get하는 부분
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        _objects.TryGetValue(typeof(T), out objects);

        return objects[idx] as T; //T로 캐스팅
    }

    // 자주 쓰는 것들이라 미리 만들어두자 (<>이거 떄문인듯)
    protected GameObject GetObject(int idx) {  return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //Action<PointerEventData>는
        //-PointerEventData를 input으로
        //- void를 output으로 받는 어떤 함수 타입도 받을 수 있으니

        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }
}
