using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CrafitingManager : MonoBehaviour
{
    public Slot[] craftingSlots;

    public string[] recipes;
    public List<string> recipeResultsString;
    private List<ItemData> recipeResults;
    public Slot resultSlot;

    private void Awake()
    {
        RecipeResult();
    }

    private void RecipeResult()
    {
        recipeResults = new List<ItemData>();
        foreach (string name in recipeResultsString)
        {
            ItemData itemData = ItemManager.Instance.GetItemByName(name);
            if (itemData != null)
            {
                recipeResults.Add(itemData);
            }
            else
            {
                Debug.LogWarning($"Cannot find ItemData with name: {name}");
            }
        }
    }

    public void Compositing()
    {
        resultSlot.data = null;

        string currentRecipeString = "";
        foreach (Slot slot in craftingSlots)
        {
            if (slot.data != null)
            {
                currentRecipeString += slot.data.Name;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        // 检查是否找到匹配的配方并设置结果物品
        for (int i = 0; i < recipes.Length; i++)
        {
            if (currentRecipeString == recipes[i])
            {
                resultSlot.SetData(recipeResults[i], 1);
                break; // 找到匹配的配方后立即跳出循环
            }
        }
    }

}
