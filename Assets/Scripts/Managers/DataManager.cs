using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager 
{
    public Dictionary<int, Data.Stat> StatDic { get; private set; } = new Dictionary<int, Data.Stat>();
    // 레벨 정보를 int에 넣어준다.

   public void Init()
    {

        StatDic = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
        //foreach(Stat stat in data.Stats)
        //{
        //    StatDic.Add(stat.level, stat);
        //}
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");

        // 이렇게만 하면 알아서 값이 들어간다.
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
