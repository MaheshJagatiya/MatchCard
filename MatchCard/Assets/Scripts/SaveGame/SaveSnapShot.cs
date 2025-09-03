using System;
using System.IO;
using UnityEngine;
[Serializable]
public class SaveSnapshot
{
    public int rows, cols;
    public int[] shuffledIds; // length = rows*cols (even)
    public bool[] matched; // per card index
    public bool[] faceUp; // per card index

    public int score;
    public int combo;
    public float elapsed;
}
public static class SaveSystem
{
    private static string Path =>
    System.IO.Path.Combine(Application.persistentDataPath,
    "cardmatch_save.json");
    public static void Save(SaveSnapshot snap)
    {
        var json = JsonUtility.ToJson(snap);
        File.WriteAllText(Path, json);
    }
    public static bool TryLoad(out SaveSnapshot snap)
    {
        if (File.Exists(Path))
        {
            var json = File.ReadAllText(Path);
            snap = JsonUtility.FromJson<SaveSnapshot>(json);
            return true;
        }
        snap = null;
        return false;
    }
    public static void Clear()
    {
        if (File.Exists(Path)) File.Delete(Path);
    }
}

