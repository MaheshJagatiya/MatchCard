using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Card : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// Card selection logic and flip Animation
    /// </summary>

    public Image frontCard;
    public Image backCard;

    public int Id { get; private set; }
    public bool IsMatched { get; private set; }
    public bool IsFaceUp { get; private set; }

    private bool isAnimating;
    private float halfFlipSeconds = 0.08f;//Flip time 
   
    public event Action<Card> CardClicked;
    public bool IsCardInteractable => !isAnimating && !IsMatched;
    public void CardInit(int id, Sprite front, Sprite back)
    {     
        Id = id; 
        IsMatched = false; 
        IsFaceUp = false; 
        isAnimating = false;
        frontCard.sprite = front;
        backCard.sprite = back;
        frontCard.gameObject.SetActive(false);
        backCard.gameObject.SetActive(true);
        transform.localScale = Vector3.one; 
    }
    /// <summary>
    /// Add event when card is clicked 
    /// so call needed changes by call different function
    /// </summary>
  
    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsCardInteractable) CardClicked?.Invoke(this);
    }
    /// <summary>
    /// if card is not faceup then then go for animation
    /// </summary>
    
    public bool Reveal()
    {
        if (!IsCardInteractable || IsFaceUp) return false;
        StartCoroutine(FlipCardAnimation(true));
        return true;
    }
    /// <summary>
    /// If Card is not matched then do animation and hide again 
    /// </summary>
    public void HideIfUnmatched()
    {
        if (!IsMatched && IsFaceUp && !isAnimating)
            StartCoroutine(FlipCardAnimation(false));
    }
    public void MarkMatched()
    {
        IsMatched = true;      
    }
    /// <summary>
    /// Flip Card Animation as per tie peramter
    /// Set card peramter attribute
    /// </summary>
    
    private IEnumerator FlipCardAnimation(bool showFront)
    {
        isAnimating = true;       
        var rt = (RectTransform)transform;
        
        yield return ScaleYPosition(rt, 1f, 0f, halfFlipSeconds);
        frontCard.gameObject.SetActive(showFront);
        backCard.gameObject.SetActive(!showFront);
        IsFaceUp = showFront;
       
        yield return ScaleYPosition(rt, 0f, 1f, halfFlipSeconds);
        isAnimating = false;
    }
    /// <summary>
    /// This coroutine smoothly animates a UI element’s Y scale from a to b over seconds
    /// </summary>
    /// <returns></returns>
    private static IEnumerator ScaleYPosition(RectTransform rt, float startScalePos, float endScalePos, float seconds)
    {
        float el = 0f;
        while (el < seconds)
        {
            el += Time.unscaledDeltaTime;
            float k = Mathf.Clamp01(el / seconds);
            var s = rt.localScale; 
            s.y = Mathf.Lerp(startScalePos, endScalePos, k);
            rt.localScale = s;
            yield return null;

        }
        var s2 = rt.localScale; 
        s2.y = endScalePos; 
        rt.localScale = s2;
    }
    public void ApplyResumeState(bool matched, bool faceUp)
    {
        IsMatched = matched;
        IsFaceUp = faceUp;
        frontCard.gameObject.SetActive(IsFaceUp);
        backCard.gameObject.SetActive(!IsFaceUp);
        gameObject.SetActive(!IsMatched ||
        IsFaceUp); 
    }
}
