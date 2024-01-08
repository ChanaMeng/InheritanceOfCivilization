using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTips : MonoBehaviour
{
    public Text ContentText;

    //Œƒ±æ…Ë÷√
    public void SetContent(string content)
    {
        ContentText.text = content;
    }
}
