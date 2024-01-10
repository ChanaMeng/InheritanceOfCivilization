using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackSystem : MonoBehaviour
{
    [SerializeField] ItemType myType;

    public Transform GridParent;
    public GameObject GridPerfab, DragPerfab;
    public List<GridItem> GridList;
    public ItemTips Tips;
    private const int MAX_COUNT = 101;
    public bool isDrag = false;
    

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
 
    //背包格子是否为空的判断
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


    //获取物品
    public void GetItem(int id, int count)
    {
        ItemData data = ItemManager.Instance.GetIDItem(id);
        if(data.Type == myType)
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
    public void ShowTipsContent(string content, Vector2 pos)
    {
        Tips.gameObject.SetActive(true);
        Tips.SetContent(content);
        //Tips.transform.position = pos;
    }

    //隐藏物品描述
    public void HideTipsContent()
    {
        Tips.gameObject.SetActive(false);
    }
}
