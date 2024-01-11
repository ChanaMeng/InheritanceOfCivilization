using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSystem : MonoBehaviour
{
    [SerializeField] BackpackType myBackpackType;

    public enum BackpackType
    {
        Normal,
        Equipment,
        Inventory
    }

    public Transform GridParent;
    public GameObject GridPerfab, DragPerfab;
    public List<GridItem> GridList;
    public ItemTips Tips;
    public int MAX_COUNT = 101;
    public bool isDrag = false;
    public static bool inventoryIsFull = false;

    private void Awake()
    {

        GridList = new List<GridItem>();
        for(int i = 0; i <= MAX_COUNT; i++)
        {
            GridItem grid = Instantiate(GridPerfab, GridParent).GetComponent<GridItem>();
            grid.Index = i;
            GridList.Add(grid);
            grid.SetBackpackSystem(this);
            grid.myBackpackType = myBackpackType;
        }
    }
 
    //背包格子是否为满的判断
    public bool IsEmptyBackpack()
    {
        for (int i = 0; i <= MAX_COUNT; i++)
        {
            if (GridList[i].IsEmpty)
            {
                return true;
            }
        }
        return false;
    }

    //物品类型比较
    public bool TypeCompare(ItemType i,BackpackType j)
    {
        string item = i.ToString();
        string back = j.ToString();
        if (item == back)
        {
            return true;  
        }
        return false;
    }

    //获取物品
    public void GetItem(int id, int count)
    {
        ItemData data = ItemManager.Instance.GetIDItem(id);

        if (myBackpackType == BackpackType.Inventory)
        {
            for (int i = 0; i <= MAX_COUNT; i++)
            {
                if (GridList[i].IsEmpty)
                {
                    GridList[i].SetData(data, count);
                    inventoryIsFull = !IsEmptyBackpack();
                    return;
                }
            }
        }

        else if(TypeCompare(data.Type, myBackpackType) && inventoryIsFull)
        {
            for (int i = 0; i <= MAX_COUNT; i++)
            {
                if (GridList[i].IsEmpty)
                {
                    GridList[i].SetData(data, count);
                    return;
                }
            }
        }
    }

    //交换物品位置
    public void SwapGrid(GridItem g1,GridItem g2)
    {
        var tmpData = g1.GetData();
        var tmpCount = g1.GetItemCount();
        g1.SetData(g2.GetData(), g2.GetItemCount());
        g2.SetData(tmpData, tmpCount);
    }

    //显示物品描述
    public void ShowTipsContent(string content)
    {
        Tips.gameObject.SetActive(true);
        Tips.SetContent(content);
    }

    //隐藏物品描述
    public void HideTipsContent()
    {
        Tips.gameObject.SetActive(false);
    }
}
