using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }
    private List<ItemData> dataList;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadIteamData();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //读取csv文件
    private void LoadIteamData()
    {
        string path = "Data/Item/Data";
        TextAsset database = Resources.Load<TextAsset>(path);
        string datas = database.text;
        string[] total_datas = datas.Split('\n');
        dataList = new List<ItemData>(total_datas.Length);
        if (total_datas != null)
        {
            // 跳过第一行数据    
            var firstItem = total_datas[0];
            total_datas = total_datas.Skip(1).ToArray();

            int lineNumber = 1; // 记录当前处理的行号
            // 跳过第一行数据后的处理  
            foreach (var item in total_datas)
            {
                string[] data = item.Split(',');
                ItemData tmp = new ItemData();
                try
                {
                    tmp.id = int.Parse(data[0]);
                    tmp.Name = data[1];
                    tmp.Icon = data[2];
                    tmp.Type = (ItemType)Enum.Parse(typeof(ItemType), data[3]);
                    tmp.EType = (Equipment)Enum.Parse(typeof(Equipment), data[4]);
                    tmp.Attack = int.Parse(data[5]);
                    tmp.Defense = int.Parse(data[6]);
                    tmp.speed = float.Parse(data[7]);
                    tmp.HP = int.Parse(data[8]);
                    tmp.MP = int.Parse(data[9]);
                    tmp.Description = data[10];
                    dataList.Add(tmp);
                }
                catch (FormatException) // 如果转换失败，则捕获格式异常  
                {
                    Debug.Log($"在第 {lineNumber} 行转换失败: {item}"); // 输出当前行号和内容
                    continue; // 跳过当前循环迭代并处理下一行  
                }
                lineNumber++; // 更新行号
            }
        }
    }

    //获取随机的物品
    public ItemData GetRandonItem()
    {
        return dataList[UnityEngine.Random.Range(0, dataList.Count)];
    }

    //获取随机的Normal类型的物品
    public ItemData GetNormalItem()
    {
        while (true)
        {

            var tmp = dataList[UnityEngine.Random.Range(0, dataList.Count)];
            if (tmp.Type == ItemType.Normal)
            {

                return tmp;

            }
        }

    }

    //获取随机的Equiment类型的物品
    public ItemData GetEquipmetItem()
    {
        while (true)
        {

            var tmp = dataList[UnityEngine.Random.Range(0, dataList.Count)];
            if (tmp.Type == ItemType.Equipment)
            {
                return tmp;
            }
        }

    }

    //通过id获取物品
    public ItemData GetIDItem(int id)
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].id == id)
            {
                return dataList[i];
            }
        }
        return null;
    }
}
