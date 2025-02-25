using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OOP : MonoBehaviour
{   
    public List<Pokemon> pokemonList = new List<Pokemon>(); // | Public - ACCESS MODIFIER |  | List - COLLECTION TYPE |
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public Sprite charmanderSprite;
    public Sprite squirtleSprite;
    public Sprite bulbasaurSprite;
    public Sprite pikachuSprite;
    public Sprite dragoniteSprite;
    public Sprite rayquazaSprite;

    public Sprite dragonSprite;
    public Sprite electricSprite;
    public Sprite fireSprite;   
    public Sprite grassSprite;
    public Sprite waterSprite;
    public Sprite unknownSprite;

    private Dictionary<Type, Sprite> typeSprites;

    //Game Info Panel
    public GameObject infoPanel;
    public Image PokemonImage;
    public Image TypeIcon;
    public TextMeshProUGUI PDno;
    public TextMeshProUGUI IDno;
    public TextMeshProUGUI OwnerName;
    public TextMeshProUGUI MetAtLv;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Level;

    public Button ExitButton;

    public Button ProgramExitButton;

    public void Start()
    {
        Pokemon pokemon1 = new Pokemon("Charmander", "Lizard Pokémon", 5, 29, Type.FIRE, charmanderSprite, "004", "12345");
        Pokemon pokemon2 = new Pokemon("Squirtle", "Tiny Turtle Pokémon", 5, 44, Type.WATER, squirtleSprite, "007", "23456");
        Pokemon pokemon3 = new Pokemon("Bulbasaur", "Seed Pokémon", 5, 45, Type.GRASS, bulbasaurSprite, "001", "34567");
        Pokemon pokemon4 = new Pokemon("Pikachu", "Mouse Pokémon", 5, 35, Type.ELECTRIC, pikachuSprite, "025", "45678");
        Pokemon pokemon5 = new Pokemon("Dragonite", "Dragon Pokémon", 25, 91, Type.DRAGON, dragoniteSprite, "149", "56789");
        Pokemon pokemon6 = new Pokemon("Rayquaza", "Sky High Pokémon", 30, 105, Type.DRAGON, rayquazaSprite, "384", "67890");

        pokemonList.Add(pokemon1);
        pokemonList.Add(pokemon2);
        pokemonList.Add(pokemon3);
        pokemonList.Add(pokemon4); 
        pokemonList.Add(pokemon5);
        pokemonList.Add(pokemon6);

        typeSprites = new Dictionary<Type, Sprite>
        {        
            { Type.DRAGON, dragonSprite },
            { Type.ELECTRIC, electricSprite },             
            { Type.FIRE, fireSprite },               
            { Type.GRASS, grassSprite },                  
            { Type.WATER, waterSprite },
            { Type.UNKNOWN, unknownSprite }
        };


        foreach (Pokemon p in pokemonList)
        {
            CreatePokemonButton(p);
        }

        infoPanel.SetActive(false);

        ExitButton.onClick.AddListener(HidePokemonInfo);

        ProgramExitButton.onClick.AddListener(ExitProgram);

    }

    public void ExitProgram()
    {
        Debug.Log("Exiting Program"); // For debugging in the editor
        Application.Quit();
    }


    public void CreatePokemonButton(Pokemon pokemon)
    {
        GameObject objectButton = Instantiate(buttonPrefab, buttonContainer);
        pokemonButton pokemonButton = objectButton.GetComponent<pokemonButton>();
        Button button = pokemonButton.GetComponent<Button>();
        button.onClick.AddListener(() => ShowPokemonInfo(pokemon));

        pokemonButton.SetData(pokemon);
    }

    public void ShowPokemonInfo(Pokemon pokemon)
    {
        infoPanel.SetActive(true); // Show panel

        // Get the index of the current Pokémon
        int index = pokemonList.IndexOf(pokemon);
        string nextPokemonName = "None"; // Default if there's no next Pokémon

        // Check if there's a next Pokémon
        if (index != -1 && index < pokemonList.Count - 1)
        {
            nextPokemonName = pokemonList[index + 1].name; // Get next Pokémon's name
        }

        PDno.text = $"{pokemon.PDno}";
        IDno.text = $"{pokemon.IDno}";
        Name.text = $"{pokemon.name}\n/{nextPokemonName}";
        Level.text = $"{pokemon.level}";
        OwnerName.text = "OT/ Joseph";  
        MetAtLv.text = $"Met at Lv.{pokemon.level}";

        if (pokemon.sprite != null)
        {
            PokemonImage.sprite = pokemon.sprite; // Assign Pokémon image
        }

        // Change Type Icon
        if (typeSprites.ContainsKey(pokemon.type))
        {
            TypeIcon.sprite = typeSprites[pokemon.type];
        }
        else
        {
            TypeIcon.sprite = unknownSprite; // Default icon
        }

        Debug.Log($"Showing info for {pokemon.name}");
    }


    public void HidePokemonInfo()
    {
        infoPanel.SetActive(false); // Hide panel
    }



}
