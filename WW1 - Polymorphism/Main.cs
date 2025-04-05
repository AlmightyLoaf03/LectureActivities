using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    private BaseClass player;

    void Start()
    {
        SimulateBattle();
    }

    void SimulateBattle()
    {
        Debug.Log("Choose a character: 1 = Warrior, 2 = Archer");
        
        int choice = Random.Range(1, 3);
        string enemy = "Goblin";

        if (choice == 1)
        {
            player = gameObject.AddComponent<WarriorClass>();
            player.Initialize("Henry");
            Debug.Log("Warrior Class selected!");
        }
        else
        {
            player = gameObject.AddComponent<ArcherClass>();
            player.Initialize("Rosa");
            Debug.Log("Archer Class selected!");
        }

        for(int i = 0; i < 3; i++)
        {
            Debug.Log($"\nTurn {i + 1}:");

            if (i == 1)
            {
                SwitchClass();
            }

            if (player is WarriorClass w)
            {
                bool heavy = Random.value > 0.5;
                w.Attack(enemy, heavy);
            }
            else if (player is ArcherClass a)
            {
                string[] skills = { "Hawkshot", "CrippleShot", "FrostArrow" };
                string skill = skills[Random.Range(0, skills.Length)];
                a.Attack(enemy, skill);
            }
        }
        Debug.Log("\nBattle simulation complete.");
    }

    void SwitchClass()
    {
        Debug.Log("Switching Classes...");
        Destroy(player);

        if(player is WarriorClass)
        {
            player = gameObject.AddComponent<ArcherClass>();
            player.Initialize("Rosa");
            Debug.Log("Archer Class selected!");
        }
        else
        {
            player = gameObject.AddComponent<WarriorClass>();
            player.Initialize("Henry");
            Debug.Log("Warrior Class selected!");
        }
    }
}
