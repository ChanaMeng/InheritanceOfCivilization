using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : GridItem, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public SlotType mySlotType;

    public enum SlotType
    {
        CraftingSlot,
        ResultSlot,
    }


}
