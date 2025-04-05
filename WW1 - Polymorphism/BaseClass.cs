using System;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour
{
    public string CharacterName;

    public virtual void Initialize(string name)
    {
       CharacterName = name; 
    }

    public abstract void Attack();

    public void Attack(string target)
    {
        Debug.Log($"{CharacterName} attacks {target} with a basic strike! ");
    }
}
