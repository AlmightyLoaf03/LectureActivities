using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArcherClass : BaseClass
{
    public override void Attack()
    {
        Debug.Log($"{CharacterName} attacks with a Quick Shot! ");
    }

    public void Attack(string target, string skill)
    {
        Debug.Log($"{CharacterName} used {skill} on {target}! ");
    }
}
