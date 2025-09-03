using System.Collections.Generic;
using UnityEngine;

public static class ShuffleCard
{
    /// <summary>
    /// Shuffle Display Card when Game Start  
    /// </summary>
    private static readonly System.Random _rng = new System.Random();
    public static void RandomShuffleCards<T>(this IList<T> list)
    {
        //Pick a random index j between 0 and current index i
        //Swap element at i with element at j
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = _rng.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
