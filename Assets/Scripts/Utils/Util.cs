using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) // 없으면 T를 컴포넌트로 추가해준다.
        {
            component = go.AddComponent<T>();
        }
        return component;
    }    

    public static GameObject Findchild(GameObject go, string name, bool recursive = false)
    {
        // 모든 게임 오브젝트는 Transform을 가지고 있다.
        // 그래서 굳이 만들필요가 없고 기존에 있는 함수 쓰면 된다.
        Transform transform = Findchild<Transform>(go, name, recursive);

        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T Findchild<T>(GameObject go, string name, bool recursive = false) where T : UnityEngine.Object
    {
        //recursive 가 true면 모든 자식을 
        //false면 하위 자식만 검샥
        if (go == null)
            return null;

        if (recursive == false)
        {
            // 직속 자식만 찾는다.
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
