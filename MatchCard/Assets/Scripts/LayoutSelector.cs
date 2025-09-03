using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class LayoutSelector : MonoBehaviour
{
    [SerializeField] private Dropdown dropdown;
     private GameConfig config;
     private GameController gameController;

    public void InitLayoutDropDownSet(GameConfig conf, GameController gameCon)
    {
        config = conf;
        gameController = gameCon;
        SetLayerBox();
    }
    private void SetLayerBox()
    {
        if (!dropdown) return;
        dropdown.ClearOptions();
        var opts = new List<string>();
        foreach (var v in config.layouts) 
            opts.Add($"{v.x}x{v.y}");
        dropdown.AddOptions(opts);
        dropdown.value = config.defaultLayoutIndex;
        dropdown.onValueChanged.AddListener(gameController.OnChangeLayout);

    }
}
