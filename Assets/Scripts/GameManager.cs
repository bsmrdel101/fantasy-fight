using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private CharacterSpawner characterSpawner;
    
    [Header("TEMPORARY")]
    [SerializeField] private CharacterData[] player1;
    [SerializeField] private CharacterData[] player2;
    
    
    private void Start()
    {
        Vector2 boardSize = boardManager.GetSize();
        boardManager.GenerateBoard((int)boardSize.x, (int)boardSize.y, boardManager.GetSeed());
        List<CharacterData[]> characters = new() { player1, player2 };
        characterSpawner.SpawnCharacters((int)boardSize.x, (int)boardSize.y, characters.ToArray());
    }
}
