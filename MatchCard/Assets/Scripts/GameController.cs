using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class GameController : MonoBehaviour
{
    /// <summary>
    /// Game Controll by config file 
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
    [SerializeField] private MusicController soundManager;
    [SerializeField] private HudUiManager hudUi;
    [SerializeField] private LayoutSelector layOutManage;
    [Header("Layout")]
    [SerializeField] private int layoutIndex = -1;
    private GameState _state = GameState.None;

    private int matchCartCount, totalCardCount;
    private int playerScore, playerComboScore;
    private float elapsedTime;

    private readonly Queue<Card> pendingCardList = new Queue<Card>(4);
    private bool IsCardCheckingRunning;

    public void Start()
    {      
        if (layoutIndex < 0) layoutIndex = config.defaultLayoutIndex;
        soundManager.InitMusic(config);
        layOutManage.InitLayoutDropDownSet(config,this);
        NewGame(layoutIndex);
        _state = GameState.Playing;
    }
    private void Update()
    {
        if (_state != GameState.Playing) return;
        elapsedTime += Time.deltaTime;
        hudUi.UpdateTimer(elapsedTime);
    }
    /// <summary>
    /// set all value when game start as per layout count value
    /// </summary>

    public void NewGame(int layoutIdx)
    {
        pendingCardList.Clear();
        IsCardCheckingRunning = false;

        matchCartCount = 0;
        totalCardCount = 0;
        playerScore = 0;
        playerComboScore = 0;
        elapsedTime = 0;
        hudUi.Awake();
        UpdateScore();
        
        var layout = config.layouts[Mathf.Clamp(layoutIdx, 0, config.layouts.Length - 1)]; //Not Go out of Range and pick layout from config
        var ids = BuildShuffledIds(layout);
        cardBoard.LayoutBuild(layout, config, ids);
        totalCardCount = layout.x * layout.y;
        AddActionOnCard();
        _state = GameState.Playing;   
    }
    /// <summary>
    /// Update Score in UI
    /// </summary>
    private void UpdateScore()
    {
        hudUi.UpdateScore(playerScore, playerComboScore);
    }
    /// <summary>
    /// Add total card number
    /// check it myst be even if odd count then minus 1
    /// Add total/2 idds 2 time in list 
    /// then send rondom shuffle
    /// </summary>
    
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
    private void AddActionOnCard()
    {
        foreach (var c in cardBoard.Cards)
            c.CardClicked += OnCardClicked;
    }
    /// <summary>
    /// Add card in queue and check if card is faceup then not to do any action
    /// </summary> 
    private void OnCardClicked(Card card)
    {
        if (!card.Reveal()) return;
        pendingCardList.Enqueue(card);
        soundManager.PlayFlip();
        if (!IsCardCheckingRunning)        
            StartCoroutine(CheckingForMatchCardEvent());
        
    }
    /// <summary>
    /// Match both card are same if same then Update UI
    /// if Card are not Match then flip back card and reduce score
    /// if total card open then game over
    /// </summary>
 
    private IEnumerator CheckingForMatchCardEvent()
    {
        IsCardCheckingRunning = true;
        yield return new WaitForSeconds(0.3f);       
        while (pendingCardList.Count >= 2)
        {
            var firstCard = DequeueValidCard();
            var SecondCard = DequeueValidCard();
            if (firstCard == null || firstCard == null) break;
            yield return new WaitForSeconds(0.25f);
            if (firstCard.Id == SecondCard.Id && 
               !firstCard.IsMatched && !SecondCard.IsMatched)
            {
                firstCard.MarkMatched();
                SecondCard.MarkMatched();
                soundManager.PlayMatch();
                matchCartCount += 2;
                Debug.Log("Matched Succesfully");
                playerComboScore++;             
                playerScore += config.matchScore;             
                UpdateScore();
            }
            else
            {
                soundManager.PlayMismatch();
                yield return new WaitForSeconds(0.35f);
                firstCard.HideIfUnmatched(); 
                SecondCard.HideIfUnmatched();
               
                Debug.Log("Not Matched Succesfully");
                playerScore = Mathf.Max(0, playerScore - config.mismatchPenalty);
                UpdateScore();

            }
            if (matchCartCount >= totalCardCount)
            {
                soundManager.PlayGameOver();
                _state = GameState.GameOver;
                Debug.Log("All Card Faceup");             
                hudUi.ShowGameOver(playerScore,elapsedTime);
                yield break;
            }         
        }
        IsCardCheckingRunning = false;      
    }
    private Card DequeueValidCard()
    {
        while (pendingCardList.Count > 0)
        {
            var localCard = pendingCardList.Dequeue();
            if (localCard != null && localCard.IsFaceUp && !localCard.IsMatched) return localCard;
        }
        return null;
    }
    /// <summary>
    /// Start New game in reload button
    /// </summary>
    public void OnNewGameButton() => NewGame(layoutIndex);
    /// <summary>
    /// Change Layout event
    /// </summary>
    public void OnChangeLayout(int newIndex)
    {
        layoutIndex = newIndex;
        NewGame(layoutIndex);
    }
}
