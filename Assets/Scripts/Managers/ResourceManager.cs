using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Faild to load prefab : {path}");
            return null;
        }
        return Object.Instantiate(prefab, parent);
        // Object이거 아까는 안 붙여도 됐는제 지금은 안붙이면 ResourceManager::Instantiate를 호출한다.
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
