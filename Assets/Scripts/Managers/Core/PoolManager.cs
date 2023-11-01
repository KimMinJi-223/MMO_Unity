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

            /*기본적으로 유니티에서 Component는 독단적으로 존재하는게 아니라
             GameObject에 기생(?)해서 살아갑니다.
            따라서 GameObject를 넘겨주나 특정 Component를 넘겨주나 별 차이 없습니다.
            GameObject를 넘겨받을 경우 go.GetComponent<T>로 Component를 찾아줄 수 있고
            반대의 경우에도 gameObject를 이용해 Component가 붙어있는 오브젝트를 찾아줄 수 있습니다.
            transform은 GameObject가 들고 있는 것이고,
            모든 Component에서는 transform을 통해 Transform에 접근할 수 있습니다.
            따라서 poolable.transform~ 을 하는 순간
            [poolable Component가 붙어있는 GameObject의 transform]의 의미와 동일합니다.*/

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

            // DonetDestoyOnLoad해제 용도 : 한번 저게 되면 저길 빠져날 수 없다. 꼼수로 저게 아닌 객체의 자식으로 붙여준다.
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    // 풀매니저는 기생해서 살아간다.
    // 리소스 매니저를 도와줄거다.

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
        // 다 사용하고 반환 비활성화 하는 것
        string name = poolable.gameObject.name;
        if (_pool.ContainsKey(name) == false)
        {
            //정말 예외적인 경우
            GameObject.Destroy(poolable.gameObject);
            return;
        }


        _pool[name].Push(poolable);
    }

    public Poolable Pop(GameObject original, Transform parent = null)
    {
        // 원본으로 찾는다. 원본과 동일한 객체를 찾아서 활성화{ 해준다.
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
        foreach (Transform child in _root) // _root의 모든 자식들 날리기
            GameObject.Destroy(child.gameObject);
        _pool.Clear();  
    }
}
