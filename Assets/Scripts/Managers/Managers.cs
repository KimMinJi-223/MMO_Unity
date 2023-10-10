using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    // � ���� ���� ������� ���� ������Ƽ�� init�� ���� ���̴�.
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource {  get { return Instance._resource; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
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
        }
  
    }
}