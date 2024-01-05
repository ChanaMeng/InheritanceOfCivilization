using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : ItemBase,CanUse
{
    public int Attack;
    public float AttackSpeed;
    public float AttackTime;
    public float AttackRate;
    public int UseLimit;

    public void Use()
    {
        //装备了武器的代码
    }
}
