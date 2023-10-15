using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Timeline;

// 2가지 의미로 사용
// 1. 위치 벡터
// 2. 방향 벡터 
// transform.Translate(Vector3.forward * Time.deltaTime * _speed);
// 여기서 forward는 위치가 아닌 방향을 의미
struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
    // normalized의 의미 : 단위 벡터를 이용하면 그 방향에 대한 정보만 추출
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
    // public으로 하면 유니티 에디터에서 값을 변경할 수 있다. 
    [SerializeField]
    float _speed = 20.0f;

    // 스테이트로 관리할거니까 필요없어짐
    //bool _moveToDest = false;
    Vector3 _destPos;

    // Start is called before the first frame update
    void Start()
    {
        // 키보드로 움직이는거는 일단 구현에서 뺀다.
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
        // 방향벡터는 2가지 정보를 얻을 수 있다.
        // 1. 거리(크기) -> 5 (magnitude)
        // 2. 실제 방향 (normalized)
        //dir = dir.normalized;

        //MyVector newPos = from + dir * _speed; // from이라는 점에서 dir방향으로 speed만큼 이동
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
        // 아무것도 못한다.
    }

    void UpdateMoving()
    {
        // 이동하는 코드를 넣어준다.
        Vector3 dir = _destPos - transform.position;
        // 벡터에서 벡터를 뺸 크기는 0이 나오지 않는 경우가 많다.
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDest = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // 이동하는 거리
            transform.position += dir.normalized * moveDest;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime); // 첫번쨰 인자가 출발 위치에서 두번쨰이자가 되기 위해 마지막 인자의 퍼센트
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
        Debug.Log($"뚜벅뚜벅 {a}");
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
        // 회전의 2가지 방법
        // 절대 회전값을 입력하는 방법
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
        // +-delta 하는 방법
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        // Quaternion qt = transform.rotation;
        // 왜 제4원소가 있나? 짐발락 현상 때문) : 두 축이 겹쳐 회전이 먹통이 된다.
        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f)); // x,y,z를 입력하면 쿼터니언을 반환 ( 오일러각 -> 쿼터니언)
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // 키로 방향 회전하는 방법 (방향만 회전한다.) 
        //if (Input.GetKey(KeyCode.W))
        //    transform.rotation = Quaternion.LookRotation(Vector3.forward);
        //if (Input.GetKey(KeyCode.S))
        //    transform.rotation = Quaternion.LookRotation(Vector3.back);
        //if (Input.GetKey(KeyCode.A))
        //    transform.rotation = Quaternion.LookRotation(Vector3.left);
        //if (Input.GetKey(KeyCode.D))
        //    transform.rotation = Quaternion.LookRotation(Vector3.right);

        // 위 방법은 이동이 경직되어 있다.
        // 게임 규모가 클때 업데이트 문에서 이렇게 하나하나 체크하는 것은 성능이 좋지 않다.
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
        // 업데이트 문은 한 프레임당 호출 정말 빠르게 이동된다.
        // 이전 프레임과 현재 프레임의 시간차이를 이용해서 만들어야 한다.
        //if (Input.GetKey(KeyCode.W))
        //    transform.position += new Vector3(0.0f, 0.0f, 1.0f);
        //if (Input.GetKey(KeyCode.S))
        //    transform.position -= new Vector3(0.0f, 0.0f, 1.0f);
        //if (Input.GetKey(KeyCode.A))
        //    transform.position -= new Vector3(1.0f, 0.0f, 0.0f);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += new Vector3(1.0f, 0.0f, 0.0f);

        // 이거는 월드를 기준으로 이동하는 것
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


        //transform.position.magnitude; // 벡터의 크기 반환
        //transform.position.normalized; // magnitude가 1인 벡터 반환

        // Translate라는 함수가 있다.
        // 바라보고 있는 러컬을 기준으로 계산을 한다.
        //if (Input.GetKey(KeyCode.W))
        //    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.S))
        //    transform.Translate(Vector3.back * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.A))
        //    transform.Translate(Vector3.left * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.D))
        //    transform.Translate(Vector3.right * Time.deltaTime * _speed);
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // 무빙함수로 이동
        //if (_moveToDest)
        //{
        //    Vector3 dir = _destPos - transform.position;
        //    // 벡터에서 벡터를 뺸 크기는 0이 나오지 않는 경우가 많다.
        //    if (dir.magnitude < 0.0001f)
        //    {
        //        _moveToDest = false;
        //    }
        //    else
        //    {
        //        float moveDest = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // 이동하는 거리
        //        transform.position += dir.normalized * moveDest;
        //        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime); // 첫번쨰 인자가 출발 위치에서 두번쨰이자가 되기 위해 마지막 인자의 퍼센트
        //        //transform.LookAt(_destPos);
        //    }
        //}

        ///////////////////////////////////////////////
        //if (_moveToDest)
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>(); // GetComponent는 컴포넌트를 뺴올 수 있다.
        //    //anim.SetFloat("wait_run_ratio", 1); // 이렇게는 스르륵 안 변한다.
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        //else
        //{
        //    wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
        //    Animator anim = GetComponent<Animator>(); // GetComponent는 컴포넌트를 뺴올 수 있다.
        //    //anim.SetFloat("wait_run_ratio", 0);
        //    anim.SetFloat("wait_run_ratio", wait_run_ratio);
        //    anim.Play("WAIT_RUN");
        //}
        // 이거 대신 state패턴을 쓰자
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
        //    return; // 이게 있으면 클릭할 떄마 움직인다. 없으면 마우스를 누르고 있으면 따라온다.

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
            // 이제는 state가 바뀌는 것이다.
            _state = PlayerState.Moving;
            //Debug.Log($"Raycast Camera {hit.collider.gameObject.tag}");
        }
    }
}
