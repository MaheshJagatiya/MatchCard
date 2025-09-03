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
    
    private void Awake()
    {     
        if (boardArea == null) boardArea = (RectTransform)transform;
    }
    public void Clear()
    {
      
    }
   
}
