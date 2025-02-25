using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class pokemonButton : MonoBehaviour
{
    public TextMeshProUGUI PokemonName;
    public TextMeshProUGUI health;
    public TextMeshProUGUI level;
    public Image pokemonImage;

    public void SetData(Pokemon pokemon)
    {
        this.PokemonName.text = pokemon.name;
        this.health.text = $"{pokemon.health} / {pokemon.health}";
        this.level.text = $"Lv{pokemon.level} ";
        this.pokemonImage.sprite = pokemon.sprite;
    }
}
