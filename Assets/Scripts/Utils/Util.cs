using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) // ������ T�� ������Ʈ�� �߰����ش�.
        {
            component = go.AddComponent<T>();
        }
        return component;
    }    

    public static GameObject Findchild(GameObject go, string name, bool recursive = false)
    {
        // ��� ���� ������Ʈ�� Transform�� ������ �ִ�.
        // �׷��� ���� �����ʿ䰡 ���� ������ �ִ� �Լ� ���� �ȴ�.
        Transform transform = Findchild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T Findchild<T>(GameObject go, string name, bool recursive = false) where T : UnityEngine.Object
    {
        //recursive �� true�� ��� �ڽ��� 
        //false�� ���� �ڽĸ� �˼�
        if (go == null)
            return null;

        if (recursive == false)
        {
            // ���� �ڽĸ� ã�´�.
            for (int i = 0; i < go.transform.childCount; ++i)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
}
