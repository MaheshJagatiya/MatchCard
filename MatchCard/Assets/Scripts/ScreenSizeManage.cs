using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeManage : MonoBehaviour
{
   
    void Awake()
    {
#if UNITY_STANDALONE && !UNITY_EDITOR
        // 80% of native resolution
        int targetW = Mathf.RoundToInt(Screen.currentResolution.width * 0.8f);
        int targetH = Mathf.RoundToInt(Screen.currentResolution.height * 0.8f);

        // Force windowed mode at this size
       // Screen.fullScreenMode = FullScreenMode.Windowed;
        Screen.SetResolution(targetW, targetH, false);
#endif
    }


}
