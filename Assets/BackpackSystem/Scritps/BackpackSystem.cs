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

    //��ȡ��Ʒ
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
