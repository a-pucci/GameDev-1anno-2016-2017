using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    Pokemon pokemon;
    void Start()
    {
        pokemon = Resources.Load<Pokemon>("Data/Charmender");
        GetComponent<SpriteRenderer>().sprite = pokemon.sprite;
    }
}
