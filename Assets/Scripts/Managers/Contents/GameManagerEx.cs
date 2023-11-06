using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ������ ������ ���� �츮 ���Ӱ� �����ִ� �Ŵ����̴�.

public class GameManagerEx
{
    GameObject _player;
    // �÷��̾ �Ѹ��̶� ��ųʸ��� �� �ʿ� ����.
    // Dictionary<int, GameObject> _player = new Dictionary<int, GameObject>();
    // Dictionary<int, GameObject> _monster = new Dictionary<int, GameObject>();
    // ������ �ٱ� �������� ���̵� �ʿ���� �ؽ��� ����
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