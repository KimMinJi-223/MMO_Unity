using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    // 어떤 것이 먼저 실행될지 몰라 프로퍼티에 init을 넣은 것이다.

    #region Contents
    GameManagerEx _game = new GameManagerEx();

    public static GameManagerEx Game { get { return Instance._game; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();   
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();   
    UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance._data; } }
    public static ResourceManager Resource {  get { return Instance._resource; } }
    public static PoolManager Pool { get { return Instance._pool; } }

    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion
    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            // 없으면 코드 상으로 만들어주자
            if (go == null)
            {
                go = new GameObject { name = "@Managers" }; // 못 찾았으면 코드에서 만든다.
                go.AddComponent<Managers>(); // 매니저 컴포넌트를 붙인다.
            }
            DontDestroyOnLoad(go); // 매니저는 삭제되면 안된다. 쉽게 삭제 못하게 하는 것
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init(); // 데이터는 씬이 바껴도 클리어할 필요가 없다.
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
  
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear(); // 다른 곳에서 풀링된 객체를 사용하고 있을 수도 있으니까 마지막에 클리어 한다.
    }
}