using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject go = Object.Instantiate(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            /*�⺻������ ����Ƽ���� Component�� ���������� �����ϴ°� �ƴ϶�
             GameObject�� ���(?)�ؼ� ��ư��ϴ�.
            ���� GameObject�� �Ѱ��ֳ� Ư�� Component�� �Ѱ��ֳ� �� ���� �����ϴ�.
            GameObject�� �Ѱܹ��� ��� go.GetComponent<T>�� Component�� ã���� �� �ְ�
            �ݴ��� ��쿡�� gameObject�� �̿��� Component�� �پ��ִ� ������Ʈ�� ã���� �� �ֽ��ϴ�.
            transform�� GameObject�� ��� �ִ� ���̰�,
            ��� Component������ transform�� ���� Transform�� ������ �� �ֽ��ϴ�.
            ���� poolable.transform~ �� �ϴ� ����
            [poolable Component�� �پ��ִ� GameObject�� transform]�� �ǹ̿� �����մϴ�.*/

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            // DonetDestoyOnLoad���� �뵵 : �ѹ� ���� �Ǹ� ���� ������ �� ����. �ļ��� ���� �ƴ� ��ü�� �ڽ����� �ٿ��ش�.
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    // Ǯ�Ŵ����� ����ؼ� ��ư���.
    // ���ҽ� �Ŵ����� �����ٰŴ�.

    Transform _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = $"@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        // �� ����ϰ� ��ȯ ��Ȱ��ȭ �ϴ� ��
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            //���� �������� ���
            GameObject.Destroy(poolable.gameObject);
            return;
        }


        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        // �������� ã�´�. ������ ������ ��ü�� ã�Ƽ� Ȱ��ȭ{ ���ش�.
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }
        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root) // _root�� ��� �ڽĵ� ������
            GameObject.Destroy(child.gameObject);
        _pool.Clear();  
    }
}
