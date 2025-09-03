using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
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
    private float halfFlipSeconds = 1.0f;//Flip time 
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
}
