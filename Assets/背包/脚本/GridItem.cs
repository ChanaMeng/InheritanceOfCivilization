using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class GridItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerEnterHandler,IPointerExitHandler
{
    public bool IsEmpty { get { return data == null ; } }
    [SerializeField]
    private Image Icon;
    [SerializeField]
    private Text Count;
    private ItemData data;

    private bool isDraging = false;
    private GameObject DragObj;
    private BackpackSystem backpack;

    public int Index;
    private int ItemCount = 0;

    //�����Ʒ����
    public void SetData(ItemData data, int count = 1)
    {
        this.data = data;
        ItemCount = count;
        UpdateGrid();
    }

    //������Ʒ����
    public bool AddCount(int count, bool isAdd = true)
    {
        if (data == null || ItemCount >= 99)
            return false;
        ItemCount = isAdd ? ItemCount + count : ItemCount - count;
        UpdateGrid();
        return true;
    }

    //��ʼ������ϵͳ
    public void SetBackpackSystem(BackpackSystem s)
    {
        backpack = s;
    }

    //��ȡ��Ʒ�ĺ���
    public ItemData GetData()
    {
        return data;
    }

    //�����Ʒ
    public void Clean()
    {
        data = null;
        UpdateGrid(); 
    }

    //��ȡ��Ʒ����
    public int GetItemCount()
    {
        return ItemCount;
    }

    //������Ʒ����
    public void UpdateGrid()
    {
        if(data == null)
        {
            Icon.gameObject.SetActive(false);
            Count.gameObject.SetActive(false);
        }
        else
        {
            Icon.gameObject.SetActive(true);
            Icon.sprite = Resources.Load<Sprite>("Images/Icons Colored/PNG/" + data.Icon);
            if (ItemCount > 1)
            {
                Count.gameObject.SetActive(true);
                Count.text = ItemCount.ToString();
            }               
            else
                Count.gameObject.SetActive(false);
        }
    }

    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (data == null) return;
        DragObj = GameObject.Instantiate(backpack.DragPerfab, backpack.transform);
        DragObj.transform.position = eventData.position;
        DragObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Icons Colored/PNG/" + data.Icon);
        isDraging = true;
        backpack.isDrag = true;
        //backpack.HideTipsContent();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraging && DragObj != null)
        {
            DragObj.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
        Destroy(DragObj);
        DragObj = null;
        backpack.isDrag = false;

        if(eventData.pointerCurrentRaycast.gameObject != null)
        {
            var tmpObj = eventData.pointerCurrentRaycast.gameObject;
            if (tmpObj.CompareTag("GridItem"))
            {
                GridItem tmpGrid = tmpObj.GetComponent<GridItem>();
                if (tmpGrid.IsEmpty)
                {
                    tmpGrid.SetData(data, ItemCount);
                    Clean();
                }
                else if (tmpGrid.Index == Index)
                {
                    return;
                }
                else
                {
                    if(tmpGrid.data.id == data.id)
                    {
                        if (data.Type == ItemType.Normal)
                        {
                            if (tmpGrid.AddCount(ItemCount))
                            {
                                Clean();
                                return;
                            }
                        }
                    }         
                    backpack.SwapGrid(this, tmpGrid);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (backpack.isDrag){ return; }
        if (data == null) { return; }
        string tipContent = data.Name + "\n" + data.Description;
        if(data.Attack > 0)
        {
            tipContent += "\n������ +" + data.Attack;
        }
        if (data.Defense > 0)
        {
            tipContent += "\n������ +" + data.Defense;
        }
        if (data.speed > 0)
        {
            tipContent += "\n�� �� +" + data.speed;
        }
        if (data.HP > 0)
        {
            tipContent += "\n������ +" + data.HP;
        }
        if (data.MP> 0)
        {
            tipContent += "\nħ �� +" + data.MP;
        }
        backpack.ShowTipsContent(tipContent,eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (backpack.isDrag) return;
        backpack.HideTipsContent();
    }
}
