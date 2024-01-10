using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSystem : MonoBehaviour
{
    [SerializeField] GridType myGridType;

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
            GridItem grid = GameObject.Instantiate(GridPerfab, GridParent).GetComponent<GridItem>();
            grid.Index = i;
            GridList.Add(grid);
            grid.SetBackpackSystem(this);
        }
    }
 
    //���������Ƿ�Ϊ�����ж�
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

    //��Ʒ���ͱȽ�
    public bool TypeCompare(ItemType i,GridType j)
    {
        string item = i.ToString();
        string back = j.ToString();
        if (item == back)
        {
            return true;  
        }
        return false;
    }

    //��ȡ��Ʒ
    public void GetItem(int id, int count)
    {
        ItemData data = ItemManager.Instance.GetIDItem(id);

        if (myGridType == GridType.Inventory)
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

        else if(TypeCompare(data.Type, myGridType) && inventoryIsFull)
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

        inventoryIsFull = !IsEmptyBackpack();
    }

    //������Ʒλ��
    public void SwapGrid(GridItem g1,GridItem g2)
    {
        var tmpData = g1.GetData();
        var tmpCount = g1.GetItemCount();
        g1.SetData(g2.GetData(), g2.GetItemCount());
        g2.SetData(tmpData, tmpCount);
    }

    //��ʾ��Ʒ����
    public void ShowTipsContent(string content)
    {
        Tips.gameObject.SetActive(true);
        Tips.SetContent(content);
    }

    //������Ʒ����
    public void HideTipsContent()
    {
        Tips.gameObject.SetActive(false);
    }
}

public enum GridType
{
    Normal = ItemType.Normal,
    Equipment = ItemType.Equipment,
    Inventory
}
