using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 5.0f;

    [SerializeField]
    float _attackRange = 2.0f;

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;

        _stat = gameObject.GetComponent<Stat>();

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {

        Debug.Log("UpdateIdle");

        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;

        float distance = (player.transform.position - transform.position).magnitude;

        if (distance <= _scanRange) // �����Ÿ��ȿ� ������ �������� ���¸� �ٲ۴�.
        {
            _lockTarget = player;
            State = Define.State.Moving;
            return;
        }
    }

    protected override void UpdateMoving()
    {
        Debug.Log("UpdateMoving");

        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position); // ���� �����ȿ� �������� ����� �ϴµ� �и���. �и��� �ʰ� �ϱ� ���� ��ǥ ��ġ�� ���� ��ġ�� �ٲ۴�.
                State = Define.State.Skill;
                return; // ���Ⱑ ����
            }
        }

        // ����� ���� ���� ���ݹ������� �� �°�
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            // �÷��̾�� �̵�
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }
    }
    protected override void UpdataSkill()
    {
        Debug.Log("UpdataSkill");
        if (_lockTarget != null)
        {
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
            targetStat.OnAttacked(_stat);

            // �ѹ� ������ ������ �׾����� Ȯ���Ѵ�.
            if (targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                // ���׾����� �Ÿ��� Ȯ���ؼ� ���� �����ȿ� �ִٸ� �ٽ� �����ϰ�
                if (distance < _attackRange)
                    State = Define.State.Skill;
                // ���ٸ� �÷��̾� ������ �����δ�.
                else
                    State = Define.State.Moving;
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }

    }

}