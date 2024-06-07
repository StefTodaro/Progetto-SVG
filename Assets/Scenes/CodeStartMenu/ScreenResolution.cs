using UnityEngine;

public class ScreenResolution : MonoBehaviour
{
    void Start()
    {
        
        int width = 1920;
        int height = 1080;
        bool isFullScreen = true; 

        Screen.SetResolution(width, height, isFullScreen);
    }
}
