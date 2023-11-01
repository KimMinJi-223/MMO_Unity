using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{
    #region Stat
    [Serializable] // 메모리에 있는 걸 파일로변환할 수 있다는 의미
    public class Stat
    {
        public int level; // 여기 퍼블릭으로 해야지 읽어진다. 변수 이름도 json이랑 같아야 한다.
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> Stats = new List<Stat>();
        //Stats는 json에 있는 데이터와 이름을 같게 해야한다

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