using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill
    }

    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    PlayerStat _stat;
    Vector3 _destPos;

    [SerializeField]
    PlayerState _state = PlayerState.Idle;


    GameObject _lockTarget;

    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.MouseAction -= OnMouseEvent;
        Managers.Input.MouseAction += OnMouseEvent;

        Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    public PlayerState State
    {
        get { return _state; }
        set
        {
            _state = value;

            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case PlayerState.Die:
                    break;
                case PlayerState.Idle:
                    anim.CrossFade("WAIT", 0.1f);
                    break;
                case PlayerState.Moving:
                    anim.CrossFade("RUN", 0.1f);
                    break;
                case PlayerState.Skill:
                    anim.CrossFade("ATTACK", 0.1f, -1, 0); // ���������ڸ� 0���� �ָ� ó������ �ٽ� ����̵ȴ�. (���� ȿ��)
                    break;
            }
        }

    }

    void UpdateDie()
    {
        // �ƹ��͵� ���Ѵ�.
    }

    void UpdateMoving()
    {


        // ���Ͱ� �� �����Ÿ� �ȿ� ������ ����
        if (_lockTarget != null)
        {
            float distance = (_lockTarget.transform.position - transform.position).magnitude;
            if (distance <= 1)
            {

                State = PlayerState.Skill;
                return;
            }
        }

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDest = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude); // �̵��ϴ� �Ÿ�

            nma.Move(dir.normalized * moveDest);

            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);
            if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block"))) // ���� ����
            {
                // ���¸� �ٲٰ� �����Ѵ�.
                if (Input.GetMouseButton(0) == false)
                    State = PlayerState.Idle;
                return;
            }
            //transform.position += dir.normalized * moveDest;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime); // ù���� ���ڰ� ��� ��ġ���� �ι������ڰ� �Ǳ� ���� ������ ������ �ۼ�Ʈ                   
        }


    }

    void OnRunEvent(int a)
    {
    }

    void UpdateIdle()
    {


    }

    void UpdataSkill()
    {
        if (_lockTarget != null)
        {
            // �����϶� ���͸� �ٶ󺸰�
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if (_lockTarget != null)
        {
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            PlayerStat myStat = gameObject.GetComponent<PlayerStat>();
            int damage = Mathf.Max(0, myStat.Attack - targetStat.Defense);
            Debug.Log(damage);
            targetStat.Hp -= damage;    

        }

        if (_stopSkill)
        {
            State = PlayerState.Idle;
        }
        else
        {
            State = PlayerState.Skill;
        }

    }

    void Update()
    {
        switch (State)
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
            case PlayerState.Skill:
                UpdataSkill();
                break;
        }
    }




    bool _stopSkill = false;
    void OnMouseEvent(Define.MouseEvent evt)
    {
        switch (State)
        {
            case PlayerState.Idle:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case PlayerState.Skill:
                {
                    if (evt == Define.MouseEvent.PointerUp)
                        _stopSkill = true;
                }
                break;
        }


    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown: // ó�� ������
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = PlayerState.Moving;
                        _stopSkill = false;
                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster) // ���͸� Ÿ�����Ѵ�.
                            _lockTarget = hit.collider.gameObject;
                        else
                            _lockTarget = null;
                    }
                }
                break;
            case Define.MouseEvent.Press: // �� ������ �ִ� ����
                {
                    if (_lockTarget == null && raycastHit) // ���͸� ���󰡾���                    
                        _destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                _stopSkill = true;
                break;

        }
    }
}
