using UnityEngine; 

[System.Serializable]
public class Pokemon
{
    public string name;
    public string species;
    public string PDno;
    public string IDno;
    public int level;
    public int health;
    public Type type;  
    public Sprite sprite;  

    // Default Constructor
    public Pokemon()
    {
        name = "UNKNOWN";
        species = "UNKNOWN";
        level = 0;
        health = 0;
        type = Type.UNKNOWN;  
        PDno = "000";
        IDno = "00000";
    }

    // Parameterized Constructor
    public Pokemon(string name, string species, int level, int health, Type type, Sprite sprite, string PDno, string IDno)
    {
        this.name = name;
        this.species = species;
        this.level = level;
        this.health = health;
        this.type = type;
        this.sprite = sprite;
        this.PDno = PDno;
        this.IDno = IDno;
    }
}

public enum Type
{
    BUG, DARK, DRAGON, ELECTRIC, FAIRY, FIGHTING, FIRE,
    FLYING, GHOST, GRASS, PSYCHIC, ROCK, STEEL, WATER,
    GROUND, ICE, NORMAL, POISON, UNKNOWN
}
