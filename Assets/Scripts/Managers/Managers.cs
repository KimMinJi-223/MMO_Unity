using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    // � ���� ���� ������� ���� ������Ƽ�� init�� ���� ���̴�.

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
            // ������ �ڵ� ������ ���������
            if (go == null)
            {
                go = new GameObject { name = "@Managers" }; // �� ã������ �ڵ忡�� �����.
                go.AddComponent<Managers>(); // �Ŵ��� ������Ʈ�� ���δ�.
            }
            DontDestroyOnLoad(go); // �Ŵ����� �����Ǹ� �ȵȴ�. ���� ���� ���ϰ� �ϴ� ��
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init(); // �����ʹ� ���� �ٲ��� Ŭ������ �ʿ䰡 ����.
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
        Pool.Clear(); // �ٸ� ������ Ǯ���� ��ü�� ����ϰ� ���� ���� �����ϱ� �������� Ŭ���� �Ѵ�.
    }
}