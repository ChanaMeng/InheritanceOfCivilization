using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 测试脚本 : MonoBehaviour
{
    public BackpackSystem backpackSystem;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int id = Random.Range(1001, 1007);
            backpackSystem.GetItem(id,Random.Range(1, 6));
        }
    }
}
