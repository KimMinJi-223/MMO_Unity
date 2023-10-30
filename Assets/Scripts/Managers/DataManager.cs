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
    // ���� ������ int�� �־��ش�.

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

        // �̷��Ը� �ϸ� �˾Ƽ� ���� ����.
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

}
