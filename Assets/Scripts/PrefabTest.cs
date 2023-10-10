using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    //public GameObject prefab; // 가장 간단한 방법
    GameObject prefab;
    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");

        // prefab = Resources.Load<GameObject>("Prefabs/Tank");
        // tank = Instantiate(prefab);

        Destroy(tank, 3.0f);


    }
}
