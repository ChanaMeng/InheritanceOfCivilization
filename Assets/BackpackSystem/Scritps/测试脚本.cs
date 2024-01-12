using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 测试脚本 : MonoBehaviour
{
    public BackpackSystem normalBackpack;
    public BackpackSystem equipmentBackpack;
    public BackpackSystem inventoryBackpack;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            int id = Random.Range(1001, 1007);
            normalBackpack.GetItem(id,Random.Range(1, 6));
            equipmentBackpack.GetItem(id, Random.Range(1, 6));
            inventoryBackpack.GetItem(id, Random.Range(1, 6));
        }
    }
}
