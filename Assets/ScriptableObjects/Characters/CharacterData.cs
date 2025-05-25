using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class CharacterData : ScriptableObject
{
    public Sprite sprite;
    public int maxHp = 10;
    public int speed = 5;
}
