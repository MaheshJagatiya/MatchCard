using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBoardGenerator : MonoBehaviour
{
    /// <summary>
    /// Generate Grid layout of card as per config file requirment
    /// </summary>

    [SerializeField] private RectTransform boardArea;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private GridLayoutGroup grid;
    private readonly List<Card> _cards = new List<Card>(32);
    public IReadOnlyList<Card> Cards => _cards;

    private void Awake()
    {     
        if (boardArea == null) boardArea = (RectTransform)transform;
    }
    public void ClearBoard()
    {
        foreach (var card in _cards) 
            if (card) Destroy(card.gameObject);
        _cards.Clear();
    }
    /// <summary>
    /// Create card display layout using id and game config perameter
    /// set value in grid layout component as per config file requirment
    /// instatiate card set value of cards like ids and card Ui
    /// </summary>
    /// <param name="layout"></param>
    /// <param name="cfg"></param>
    /// <param name="shuffledIds"></param>
    public void LayoutBuild(Vector2Int layout, GameConfig gameConfig, List<int> shuffledIds)
    {
        ClearBoard();
      
        int rows = layout.x, cols = layout.y;
        var area = boardArea.rect.size;
        var spacing = gameConfig.spacing;
        float cellW = (area.x - spacing.x * (cols - 1)) / cols;
        float cellH = (area.y - spacing.y * (rows - 1)) / rows;
        Vector2 cellSize = new Vector2(Mathf.Floor(cellW), Mathf.Floor(cellH));
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = cols;
        grid.spacing = spacing;
        grid.cellSize = cellSize;
        grid.childAlignment = TextAnchor.MiddleCenter;
        int total = rows * cols;
        for (int i = 0; i < total; i++)
        {
            var card = Instantiate(cardPrefab, boardArea);
            int id = shuffledIds[i];
            Sprite front = gameConfig.frontSprites[id % gameConfig.frontSprites.Count];
            card.CardInit(id, front, gameConfig.backSprite);
            _cards.Add(card);
        }
    }

}
