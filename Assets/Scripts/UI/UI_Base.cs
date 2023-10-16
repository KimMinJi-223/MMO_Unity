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

    public abstract void Init(); // init�� UI_Base�� ���� ������ �ʱ� ������ �ڽĿ��� �����Ѵ�.

    // ���ε��ϴ� �κ�
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type); // string�� �迭�� �Ѱ��ش�.

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.Findchild(gameObject, names[i], true);
            else
                objects[i] = Util.Findchild<T>(gameObject, names[i], true);
            // where T : UnityEngine.Object�̰� ������ ���� : ������ ���� �ʾƼ�

            if (objects[i] == null)
                Debug.Log($"Failed to Bind {names[i]}");
        }
    }

    // get�ϴ� �κ�
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        _objects.TryGetValue(typeof(T), out objects);

        return objects[idx] as T; //T�� ĳ����
    }

    // ���� ���� �͵��̶� �̸� �������� (<>�̰� �����ε�)
    protected GameObject GetObject(int idx) {  return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        //Action<PointerEventData>��
        //-PointerEventData�� input����
        //- void�� output���� �޴� � �Լ� Ÿ�Ե� ���� �� ������

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
