using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase :IComparer<ItemBase>
{
    public int id;
    public string name;
    public string description;
    public string icon;
    public string receiveWay;
    public string quality;
    public bool isMaterial;
    public int num;

    public int Compare(ItemBase x, ItemBase y)
    {
        if (x.id < y.id)
        {
            return -1;
        }
        else if (x.id == y.id)
        {
            return 0;
        }
        return 1;
    }
}
