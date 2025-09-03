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
}
