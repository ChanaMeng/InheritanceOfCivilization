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
        if(Instance == null)
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


    //��ȡcsv�ļ�
    private void LoadIteamData()
    {
        string path = "Data/Item/Data";
        TextAsset database = Resources.Load<TextAsset>(path);
        string datas = database.text;
        string[] total_datas = datas.Split('\n');
        dataList = new List<ItemData>(total_datas.Length);
        if (total_datas != null)
        {
            // ������һ������    
            var firstItem = total_datas[0];
            total_datas = total_datas.Skip(1).ToArray();

            int lineNumber = 1; // ��¼��ǰ������к�
            // ������һ�����ݺ�Ĵ���  
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
                catch (FormatException) // ���ת��ʧ�ܣ��򲶻��ʽ�쳣  
                {
                    Debug.Log($"�ڵ� {lineNumber} ��ת��ʧ��: {item}"); // �����ǰ�кź�����
                    continue; // ������ǰѭ��������������һ��  
                }
                lineNumber++; // �����к�
            }
        }
    }

    //��ȡ�������Ʒ
    public ItemData GetRandonItem()
    {
        return dataList[UnityEngine.Random.Range(0, dataList.Count)];
    }

    //��ȡ�����Normal���͵���Ʒ
    public ItemData GetNormalItem()
    {
        while (true) {

            var tmp = dataList[UnityEngine.Random.Range(0, dataList.Count)];
            if (tmp.Type == ItemType.Normal) {

                return tmp;

            }   
        }
        
    }

    //��ȡ�����Equiment���͵���Ʒ
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

    //ͨ��id��ȡ��Ʒ
    public ItemData GetIDItem(int id)
    {
        for(int i = 0; i < dataList.Count; i++)
        {
            if (dataList[i].id == id)
            {
                return dataList[i];
            }
        }
        return null;
    }
}
