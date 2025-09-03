using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudUiManager : MonoBehaviour
{
    [SerializeField] private HudUiRefrence hudUiRefrence;
    public void Awake()
    {
        if (hudUiRefrence.gameOverPanel) 
            hudUiRefrence.gameOverPanel.SetActive(false);

    }
    /// <summary>
    /// Update Score in UI playerscore and total match card
    /// </summary>
   
    public void UpdateScore(int score, int combo)
    {
        if (hudUiRefrence.totalScoreCountText)
            hudUiRefrence.totalScoreCountText.text = $"Score: {score}";
        if (hudUiRefrence.totalMatchCountText)
            hudUiRefrence.totalMatchCountText.text = combo >= 1 ? $"Matched  x{combo}" :"";
    }
    /// <summary>
    /// Timer Countdown display in UI
    /// </summary>

    public void UpdateTimer(float seconds)
    {
        if (!hudUiRefrence.runningTimerText) return;
        int m = Mathf.FloorToInt(seconds / 60f);
        int s = Mathf.FloorToInt(seconds) % 60;
        hudUiRefrence.runningTimerText.text = $"{m:00}:{s:00}";
    }
    /// <summary>
    /// Show Game Over Panel with score detail
    /// </summary>
    public void ShowGameOver(int score, float elapsed)
    {
        if (!hudUiRefrence.gameOverPanel) return;
        hudUiRefrence.gameOverPanel.SetActive(true);
    
        if (hudUiRefrence.gameOverScoreText)
        {
            int m = Mathf.FloorToInt(elapsed / 60f);
            int s = Mathf.FloorToInt(elapsed) % 60;
            hudUiRefrence.gameOverScoreText.text = $"Your Score is {score} in Time: {m:00}:{s:00}";

        }
    }
}
