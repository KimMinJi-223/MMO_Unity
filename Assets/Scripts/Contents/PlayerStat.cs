using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp
    { 
        get { return _exp; } 
        set
        { 
            _exp = value;
            // 레벨업 체크

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDic.TryGetValue(level + 1, out stat) == false)
                    break; // 다음 레벨이 없다는 의미
                if (_exp < stat.totalExp)
                    break;
                level++; // 레벨이 1이 올라가는게 아니고 여러단계가 한방에 올라갈 수도 있다.
            }

            if (level != Level)
            {
                Debug.Log("Level UP!");
                Level = level;
                SetStat(Level); // 레벨을 기준으로 스탯 업데이트 해준다.
            }
        }
    }
    
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;

        _exp = 0; 
        _defense = 5;
        _moveSpeed = 5.0f;
        _gold = 0;

        SetStat(_level);
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDic;
        Data.Stat stat = dict[level];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("player Dead");
    }
}
