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
            // ������ üũ

            int level = Level;
            while (true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDic.TryGetValue(level + 1, out stat) == false)
                    break; // ���� ������ ���ٴ� �ǹ�
                if (_exp < stat.totalExp)
                    break;
                level++; // ������ 1�� �ö󰡴°� �ƴϰ� �����ܰ谡 �ѹ濡 �ö� ���� �ִ�.
            }

            if (level != Level)
            {
                Debug.Log("Level UP!");
                Level = level;
                SetStat(Level); // ������ �������� ���� ������Ʈ ���ش�.
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
