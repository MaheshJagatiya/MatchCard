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
    [Header("Layout")]
    [SerializeField] private int layoutIndex = -1;
    private GameState _state = GameState.None;

    public void Start()
    {      
        if (layoutIndex < 0) layoutIndex = config.defaultLayoutIndex;
       
        NewGame(layoutIndex);
        _state = GameState.Playing;
    }
    public void NewGame(int layoutIdx)
    {
        //Not Go out of Range and pick layout from config
        var layout = config.layouts[Mathf.Clamp(layoutIdx, 0, config.layouts.Length - 1)];
        var ids = BuildShuffledIds(layout);
        cardBoard.LayoutBuild(layout, config, ids);     
        _state = GameState.Playing;
    }
    /// <summary>
    /// Add total card number
    /// check it myst be even if odd count then minus 1
    /// Add total/2 idds 2 time in list 
    /// then send rondom shuffle
    /// </summary>
    /// <param name="layout"></param>
    /// <returns></returns>
    private List<int> BuildShuffledIds(Vector2Int layout)
    {
        int total = layout.x * layout.y; //Total card count
        if (total % 2 != 0) total--; 
        var list = new List<int>(total);
        int pairs = total / 2;
        for (int i = 0; i < pairs; i++) 
        { 
            list.Add(i); 
            list.Add(i); 
        }
        list.RandomShuffleCards();
        return list;
    }
}
