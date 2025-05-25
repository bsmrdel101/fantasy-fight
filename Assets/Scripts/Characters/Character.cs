using UnityEngine;

public class Character
{
    [Header("Properties")]
    private string characterName;
    private Sprite sprite;
    private int maxHp;
    private int speed;

    public Character(CharacterData data)
    {
        characterName = data.name;
        sprite = data.sprite;
        maxHp = data.maxHp;
        speed = data.speed;
    }
}
