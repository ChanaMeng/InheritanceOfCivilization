using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ForgeSystem : Singleton<ForgeSystem>
{
    private Dictionary<int,ItemBase> items = new Dictionary<int, ItemBase>();

    public void PutItemInForge(ItemBase item,int index = 0)
    {
        items[index] = item;
    }

    public void RemoveItemInForge(int index)
    {
        if (items.ContainsKey(index))
        {
            items.Remove(index);
        }
        //UIRefresh
    }

    public void OnForge()
    {
        var meterials = items.Values.ToList();
        Forge(meterials);
    }

    public ItemBase Forge( List<ItemBase> meterials)
    {
        meterials.Sort();
        var forgedata = CheckForgeData(meterials.Select(m=>m.id).ToList());
        
        if (forgedata != null)
        {
            Debug.Log( $"[Forge]Forge Id:{forgedata.Id}");
        }
        Debug.Log("Forge Failed");
        return null;
    }

    public ForgeData CheckForgeData(List<int> meterialIds)
    {
        ForgeData forgeData = new ForgeData();
        //TODO:对照表格得出ForgeData，返回锻造的数据
        return forgeData;
    }
}

public class ForgeData
{
    public int Id;
    public List<ItemBase> meterials = new();
    public List<ItemBase> RewardItems = new();
}
