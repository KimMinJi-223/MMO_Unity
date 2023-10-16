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

        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)"); // ���忡 �����ɶ� �ڿ� clone�� �ٴ´� �� ��ġ�� ã�´�.
        if(index > 0)
            go.name = go.name.Substring(0, index); // �ڸ���

        return go;
        // Object�̰� �Ʊ�� �� �ٿ��� �ƴ��� ������ �Ⱥ��̸� ResourceManager::Instantiate�� ȣ���Ѵ�.
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
