using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _mosterCount = 0; // 현재 몬스터의 수
    int _reserveCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0; // 유지시켜야하는 몬스터 수

    [SerializeField]
    Vector3 _spawnPos;
    [SerializeField]
    float _spawnRadius = 15.0f;
    [SerializeField]
    float _spawnTime = 5.0f;

    public void AddMonsterCount(int value) { _mosterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }
    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        // 현재 몬스터의 수가 우리가 유지해야하는 수와 비교 해서 로직을 실행
        while (_reserveCount + _mosterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        _reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, _spawnTime));

        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        Vector3 ranPos;
        while (true)
        {
            Vector3 ranDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius); // 방향벡터
            ranDir.y = 0;
            ranPos = _spawnPos + ranDir;

            // 갈 수 있나 체크하기
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(ranPos, path))
                break;
        }

        obj.transform.position = ranPos;
        _reserveCount--;
    }
}
