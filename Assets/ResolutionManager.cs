using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    void Start()
    {
        // Set the resolution to the current screen resolution
        SetCurrentResolution();
    }

    void SetCurrentResolution()
    {
        // Get the current screen resolution using Screen.currentResolution
        Resolution currentResolution = Screen.resolutions[0]; // Get the first resolution in the list of available resolutions

        // Apply the resolution to the game window (for desktop builds) or fullscreen mode
        Screen.SetResolution(currentResolution.width, currentResolution.height, true);
    }
}
