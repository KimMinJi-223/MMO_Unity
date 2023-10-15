using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Timeline;

// 2���� �ǹ̷� ���
// 1. ��ġ ����
// 2. ���� ���� 
// transform.Translate(Vector3.forward * Time.deltaTime * _speed);
// ���⼭ forward�� ��ġ�� �ƴ� ������ �ǹ�
struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
    // normalized�� �ǹ� : ���� ���͸� �̿��ϸ� �� ���⿡ ���� ������ ����
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } }
    public MyVector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public static MyVector operator +(MyVector v1, MyVector v2)
    {
        return new MyVector(v1.x + v2.x, v1.y + v2.y, v1.z + v2.z);
    }
    public static MyVector operator -(MyVector v1, MyVector v2)
    {
        return new MyVector(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);
    }
    public static MyVector operator *(MyVector v1, float d)
    {
        return new MyVector(v1.x * d, v1.y * d, v1.z * d);
    }
}
public class PlayerController : MonoBehaviour
{
    //public float _speed = 10.0f;
    // public���� �ϸ� ����Ƽ �����Ϳ��� ���� ������ �� �ִ�. 
    [SerializeField]
    float _speed = 20.0f;

    // ������Ʈ�� �����ҰŴϱ� �ʿ������
    //bool _moveToDest = false;
    Vector3 _destPos;

    // Start is called before the first frame update
    void Start()
    {
        // Ű����� �����̴°Ŵ� �ϴ� �������� ����.
        //Managers.Input.keyAction -= OnKeyboard;
        //Managers.Input.keyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");

        //MyVector pos = new MyVector(0.0f, 10.0f, 0.0f);
        //pos += new MyVector(0.0f, 2.0f, 0.0f);
        //
        //MyVector to = new MyVector(10.0f, 0.0f, 0.0f);
        //MyVector from = new MyVector(5.0f, 0.0f, 0.0f);
        //MyVector dir = to - from;
        // ���⺤�ʹ� 2���� ������ ���� �� �ִ�.
        // 1. �Ÿ�(ũ��) -> 5 (magnitude)
        // 2. ���� ���� (normalized)
        //dir = dir.normalized;

        //MyVector newPos = from + dir * _speed; // from�̶�� ������ dir�������� speed��ŭ �̵�
    }

    // Update is called once per frame
    float _yAngle = 0.0f;
    float wait_run_ratio = 0.0f;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {
        // �ƹ��͵� ���Ѵ�.
    }

    void UpdateMoving()
    {
        // �̵��ϴ� �ڵ带 �־��ش�.
        Vector3 dir = _destPos - transform.position;
        // ���Ϳ��� ���͸� �A ũ��� 0�� ������ �ʴ� ��찡 ����.
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDest = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // �̵��ϴ� �Ÿ�
            transform.position += dir.normalized * moveDest;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime); // ù���� ���ڰ� ��� ��ġ���� �ι������ڰ� �Ǳ� ���� ������ ������ �ۼ�Ʈ
                                                                                                                          //transform.LookAt(_destPos);
        }

        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        //anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //anim.Play("WAIT_RUN");
        anim.SetFloat("speed", _speed);

    }

    void OnRunEvent(int a)
    {
        Debug.Log($"�ѹ��ѹ� {a}");
    }

    void UpdateIdle()
    {
        //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        Animator anim = GetComponent<Animator>();
        //anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //anim.Play("WAIT_RUN");
        anim.SetFloat("speed", 0);
    }

    void Update()
    {
        _yAngle += Time.deltaTime * 100;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ȸ���� 2���� ���
        // ���� ȸ������ �Է��ϴ� ���
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        // +-delta �ϴ� ���
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // Quaternion qt = transform.rotation;
        // �� ��4���Ұ� �ֳ�? ���߶� ���� ����) : �� ���� ���� ȸ���� ������ �ȴ�.
        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f)); // x,y,z�� �Է��ϸ� ���ʹϾ��� ��ȯ ( ���Ϸ��� -> ���ʹϾ�)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Ű�� ���� ȸ���ϴ� ��� (���⸸ ȸ���Ѵ�.) 
        //if (Input.GetKey(KeyCode.W))
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward);
        //if (Input.GetKey(KeyCode.S))
        //    transform.rotation = Quaternion.LookRotation(Vector3.back);
        //if (Input.GetKey(KeyCode.A))
        //    transform.rotation = Quaternion.LookRotation(Vector3.left);
        //if (Input.GetKey(KeyCode.D))
        //    transform.rotation = Quaternion.LookRotation(Vector3.right);

        // �� ����� �̵��� �����Ǿ� �ִ�.
        // ���� �Ը� Ŭ�� ������Ʈ ������ �̷��� �ϳ��ϳ� üũ�ϴ� ���� ������ ���� �ʴ�.
        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
        //    transform.position += Vector3.forward * Time.deltaTime * _speed;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
        //    transform.position += Vector3.back * Time.deltaTime * _speed;

        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
        //    transform.position += Vector3.left * Time.deltaTime * _speed;

        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
        //    transform.position += Vector3.right * Time.deltaTime * _speed;

        //}
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // ������Ʈ ���� �� �����Ӵ� ȣ�� ���� ������ �̵��ȴ�.
        // ���� �����Ӱ� ���� �������� �ð����̸� �̿��ؼ� ������ �Ѵ�.
        //if (Input.GetKey(KeyCode.W))
        //    transform.position += new Vector3(0.0f, 0.0f, 1.0f);
        //if (Input.GetKey(KeyCode.S))
        //    transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
        //if (Input.GetKey(KeyCode.A))
        //    transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += new Vector3(1.0f, 0.0f, 0.0f);

        // �̰Ŵ� ���带 �������� �̵��ϴ� ��
        //if (Input.GetKey(KeyCode.W))
        //    transform.position += Vector3.forward * Time.deltaTime * _speed;
        //if (Input.GetKey(KeyCode.S))
        //    transform.position += Vector3.back * Time.deltaTime * _speed;
        //if (Input.GetKey(KeyCode.A))
        //    transform.position += Vector3.left * Time.deltaTime * _speed;
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += Vector3.right * Time.deltaTime * _speed;

        //if (Input.GetKey(KeyCode.W))
        //    transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.S))
        //    transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.A))
        //    transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);


        //transform.position.magnitude; // ������ ũ�� ��ȯ
        //transform.position.normalized; // magnitude�� 1�� ���� ��ȯ

        // Translate��� �Լ��� �ִ�.
        // �ٶ󺸰� �ִ� ������ �������� ����� �Ѵ�.
        //if (Input.GetKey(KeyCode.W))
        //    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.S))
        //    transform.Translate(Vector3.back * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.A))
        //    transform.Translate(Vector3.left * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.D))
        //    transform.Translate(Vector3.right * Time.deltaTime * _speed);
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // �����Լ��� �̵�
        //if (_moveToDest)
        //{
        //    Vector3 dir = _destPos - transform.position;
        //    // ���Ϳ��� ���͸� �A ũ��� 0�� ������ �ʴ� ��찡 ����.
        //    if (dir.magnitude < 0.0001f)
        //    {
        //        _moveToDest = false;
        //    }
        //    else
        //    {
        //        float moveDest = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // �̵��ϴ� �Ÿ�
        //        transform.position += dir.normalized * moveDest;
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime); // ù���� ���ڰ� ��� ��ġ���� �ι������ڰ� �Ǳ� ���� ������ ������ �ۼ�Ʈ
        //        //transform.LookAt(_destPos);
        //    }
        //}

        ///////////////////////////////////////////////
        //if (_moveToDest)
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>(); // GetComponent�� ������Ʈ�� ���� �� �ִ�.
        //    //anim.SetFloat("wait_run_ratio", 1); // �̷��Դ� ������ �� ���Ѵ�.
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        //else
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>(); // GetComponent�� ������Ʈ�� ���� �� �ִ�.
        //    //anim.SetFloat("wait_run_ratio", 0);
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        // �̰� ��� state������ ����
        ////////////////////////////////////////////////////

        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    //void OnKeyboard()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
    //        transform.position += Vector3.forward * Time.deltaTime * _speed;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
    //        transform.position += Vector3.back * Time.deltaTime * _speed;

    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
    //        transform.position += Vector3.left * Time.deltaTime * _speed;

    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
    //        transform.position += Vector3.right * Time.deltaTime * _speed;

    //    }
    //    _moveToDest = false;
    //}

    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if (evt != Define.MouseEvent.Click)
        //    return; // �̰� ������ Ŭ���� ���� �����δ�. ������ ���콺�� ������ ������ ����´�.

        //Debug.Log("OnMouseClicked");

        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            //_moveToDest = true;
            // ������ state�� �ٲ�� ���̴�.
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera {hit.collider.gameObject.tag}");
        }
    }
}
