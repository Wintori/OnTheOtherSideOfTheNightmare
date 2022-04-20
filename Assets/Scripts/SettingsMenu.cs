using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
   public void SetQality(int qalityindex)
    {
       QualitySettings.SetQualityLevel(qalityindex);
    }
    public void SetFullScreen (bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
    }
}
