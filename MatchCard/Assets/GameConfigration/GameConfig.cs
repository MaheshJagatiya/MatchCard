using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Scriptable Objects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Layouts (rows x cols)")]
    public Vector2Int[] layouts = new[] { new Vector2Int(2, 2), new Vector2Int(2, 3), new Vector2Int(4, 4), new Vector2Int(5, 6) };
    public int defaultLayoutIndex = 2;

    [Header("Board UI")]
    public Vector2 spacing = new Vector2(16, 16);
    [Header("Sprites")]
    public Sprite backSprite;
    public List<Sprite> frontSprites;

    [Header("SoundClip")]
    public AudioClip soundFlip;
    public AudioClip soundMatch;
    public AudioClip soundMismatch;
    public AudioClip soundGameOver;
}
