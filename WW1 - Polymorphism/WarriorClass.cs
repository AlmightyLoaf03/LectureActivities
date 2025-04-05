using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WarriorClass : BaseClass
{
    public override void Attack()
    {
        Debug.Log($"{CharacterName} attacks with a Sword Slash! ");
    }

    public void Attack(string target, bool HeavyAttack)
    {
        if (HeavyAttack)
        {
            Debug.Log($"{CharacterName} performs a heavy attack on {target}");
        }
        else
        {
            base.Attack(target);
        }
    }
}

