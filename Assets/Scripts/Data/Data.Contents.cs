using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region Stat
    [Serializable] // �޸𸮿� �ִ� �� ���Ϸκ�ȯ�� �� �ִٴ� �ǹ�
    public class Stat
    {
        public int level; // ���� �ۺ����� �ؾ��� �о�����. ���� �̸��� json�̶� ���ƾ� �Ѵ�.
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> Stats = new List<Stat>();
        //Stats�� json�� �ִ� �����Ϳ� �̸��� ���� �ؾ��Ѵ�

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in Stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}