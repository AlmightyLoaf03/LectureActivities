using UnityEngine;

public class GameManager : MonoBehaviour
{
    //references for enemy behavior (moving, attacking, and healing)
    private IMovable _move;
    private IAttackable _attack;
    private IHealable _heal;

    //dependency injection for flexibilitu and reusability
    //instead of creating the class inside the game manager we inject it with the iterfaces corresponding to what behavior is needed
    public void SetEnemy(IMovable movable, IAttackable attackable, IHealable healable = null)
    {
        _move = movable;
        _attack = attackable;
        _heal = healable;
    }

    //simulation of enemy movement in one turn
    public void runTurn()
    {
        _move.Move();
        _attack.Attack();

        if (_heal != null)
        {
            _heal.Heal();
        }
    }

    public void Start()
    {
        //Goblin (Attack + Move only)
        Goblin goblin = new Goblin();
        SetEnemy(goblin, goblin); //injects attack and move only
        runTurn();

        //Healer (Attack + Move + Heal)
        HealerEnemy healerEnemy = new HealerEnemy();
        SetEnemy(healerEnemy, healerEnemy, healerEnemy); //injects attack, move, and heal
        runTurn();
    }
}

