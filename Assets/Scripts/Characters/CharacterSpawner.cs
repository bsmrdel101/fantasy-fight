using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Utils utils;
    [SerializeField] private Transform charactersContainer;
    [SerializeField] private GameObject characterPrefab;
    
    
    public void SpawnCharacters(int width, int height, CharacterData[][] selectedCharacters)
    {
        int y = 0;
        foreach (CharacterData[] row in selectedCharacters)
        {
            for (int i = 0; i < row.Length; i++)
            {
                float spacing = width / (float)(row.Length + 1);
                int x = Mathf.RoundToInt((i + 1) * spacing);
                SpawnCharacterAt(x, y, row[i]);
            }
            y = height - 1;
        }
    }

    private void SpawnCharacterAt(int x, int y, CharacterData characterData)
    {
        Vector2 coords = utils.ParseGridCoords(x, y);
        List<Character> characters = new();
        characterPrefab.GetComponent<SpriteRenderer>().sprite = characterData.sprite;
        GameObject obj = Instantiate(characterPrefab, new Vector3(coords.x, coords.y), Quaternion.identity);
        obj.transform.SetParent(charactersContainer);
        obj.name = $"Player {(y == 0 ? "1" : "2")}: {characterData.name}";
        
        Character character = new Character(characterData);
        characters.Add(character);
    }
}
