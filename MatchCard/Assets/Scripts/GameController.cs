using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Game Controll by config file a
    /// Refrence of different Game Module
    /// </summary>
    public enum GameState
    {
        None,
        Init,
        Playing,
        Paused,
        GameOver
    }

    [Header("Scene References")]
    [SerializeField] private GameConfig config;
    [SerializeField] private CardBoardGenerator cardBoard;
}
