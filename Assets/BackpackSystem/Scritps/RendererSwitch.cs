using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RendererSwitch : MonoBehaviour
{
    private CanvasGroup myCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    public void UpdateRenderer(bool isRenderer )
    {
        if (isRenderer) {
            myCanvasGroup.alpha = 1;
            myCanvasGroup.blocksRaycasts = true;
        }
        else
        {
            myCanvasGroup.alpha = 0;
            myCanvasGroup.blocksRaycasts = false;
        }
    }
}
