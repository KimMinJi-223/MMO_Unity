using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    // 어떤 것이 먼저 실행될지 몰라 프로퍼티에 init을 넣은 것이다.
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();   
    UIManager _ui = new UIManager();


    public static ResourceManager Resource {  get { return Instance._resource; } }
    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }

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

            s_instance._sound.Init();
        }
  
    }

    public static void Clear()
    {
        Input.Clear();
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
    }
}