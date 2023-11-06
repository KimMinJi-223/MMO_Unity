using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 엔진과 관련이 없고 우리 게임과 관련있는 매니저이다.

public class GameManagerEx
{
    GameObject _player;
    // 플레이어가 한명이라 딕셔너리로 할 필요 없다.
    // Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    // Dictionary<int, GameObject> _monster = new Dictionary<int, GameObject>();
    // 서버가 붙기 전까지는 아이디가 필요없어 해쉬로 하자
    HashSet<GameObject> _monster = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }   

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Monster:
                _monster.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.UnKnown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Monster:
                {
                    if (_monster.Contains(go))
                    {
                        _monster.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
