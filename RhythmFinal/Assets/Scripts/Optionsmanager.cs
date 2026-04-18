using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager Instance;

    //Copilot helped me finish writing variables or functions when they were already halfway written.

    // Index 0 = 60fps, 1 = 120fps, 2 = unlimited 
    [Header("Frame Rate")]
    public int[] frameRateOptions = { 60, 120, 0 };
    public int currentFrameRateIndex = 0;
    
    [Header("Resolution and Quality")]
    public int currentDisplayMode = 0; // 0 = Fullscreen, 1 = Windowed, 2 = Fullscreen Borderless
    public int currentResolutionIndex = 0; 
    public int currentQualityIndex = 0; // 0 = Low, 1 = Medium, 2 = High

    [Header("Sensitivity")]
    public float horizontalSensitivity = 1f;
    public float verticalSensitivity = 1f;
    public float sensMin = 0.01f;
    public float sensMax = 10f;

    [Header("Volume")]
    // Master volume goes from 0 to 1
    public float Volume = 1f;

    // DontDestroyOnLoad keeps this alive when loading from the menu into the game.
    private void Awake()
    {
        // If an OptionsManager already exists in the scene, destroy this one
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadOptions();
        ApplyOptions();
    }

    // Gets the current frame rate as a string
    public string GetFrameRate()
    {
        int value = frameRateOptions[currentFrameRateIndex];
        return value.ToString() + " FPS";
    }

    // Uses an integer to set the display mode.
    // 0 = Fullscreen, 1 = Windowed, 2 = Borderless Windowed
    public void SetDisplayMode(int modeIndex)
    {
        currentDisplayMode = modeIndex;

        if (currentDisplayMode == 0)
        {
            // Exclusive fullscreen — best performance, slower to alt-tab
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else if (currentDisplayMode == 1)
        {
            // Standard resizable window
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else if (currentDisplayMode == 2)
        {
            // Looks like fullscreen but behaves like a window — fast alt-tab
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
    
    // Sets the resolution based on an index from the list of available resolutions.
    public void SetResolution(int index)
    {
        Resolution[] resolutions = Screen.resolutions;

        if (index < 0 || index >= resolutions.Length)
        {
            return;
        }

        currentResolutionIndex = index;
        Resolution chosen = resolutions[index];
        Screen.SetResolution(chosen.width, chosen.height, Screen.fullScreen);
    }

    // Quality - 0 = Low, 1 = Medium, 2 = High
    public void SetQualityLevel(int index)
    {
        // Clamp makes sure the index never goes out of range
        currentQualityIndex = Mathf.Clamp(index, 0, QualitySettings.names.Length - 1);
        QualitySettings.SetQualityLevel(currentQualityIndex, true);
    }

    // Horizontal and Vertical Sensitivity for mouse look. Called by sliders in the UI.
    public void SetHorizontalSensitivity(float value)
    {
        horizontalSensitivity = Mathf.Clamp(value, sensMin, sensMax);
    }

    public void SetVerticalSensitivity(float value)
    {
        verticalSensitivity = Mathf.Clamp(value, sensMin, sensMax);
    }

    public float GetHorizontalSensitivity()
    {
        return horizontalSensitivity;
    }

    public float GetVerticalSensitivity()
    {
        return verticalSensitivity;
    }

    // Called by a slider in the UI. Value should be between 0 and 1.
    public void SetVolume(float value)
    {
        Volume = Mathf.Clamp(value, 0f, 1f);
        AudioListener.volume = Volume;
    }

    //
    // This bottom section is AI generated and is used to save the current settings to PlayerPrefs
    //
    public void SaveOptions()
    {
        PlayerPrefs.SetInt("FrameRateIndex", currentFrameRateIndex);
        PlayerPrefs.SetInt("DisplayMode", currentDisplayMode);
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.SetInt("QualityIndex", currentQualityIndex);
        PlayerPrefs.SetFloat("HorizontalSensitivity", horizontalSensitivity);
        PlayerPrefs.SetFloat("VerticalSensitivity", verticalSensitivity);
        PlayerPrefs.SetFloat("MasterVolume", Volume);

        // Flushes to disk immediately so nothing is lost if the game crashes
        PlayerPrefs.Save();
    }

    // Set variables to the saved values. Call this when the options menu is opened
    public void LoadOptions()
    {
        currentFrameRateIndex = PlayerPrefs.GetInt("FrameRateIndex", 0);
        currentDisplayMode = PlayerPrefs.GetInt("DisplayMode", 0);
        currentResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);
        currentQualityIndex = PlayerPrefs.GetInt("QualityIndex", QualitySettings.names.Length - 1);
        horizontalSensitivity = PlayerPrefs.GetFloat("HorizontalSensitivity", 1f);
        verticalSensitivity = PlayerPrefs.GetFloat("VerticalSensitivity", 1f);
        Volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
    }

    // Set the framerate using an index.
    // Pass 0 for 60fps, 1 for 120fps, 2 for unlimited.
    public void SetFrameRate(int index)
    {
        currentFrameRateIndex = index;

        int target = frameRateOptions[index];

        // Unity uses -1 for unlimited, but we store 0 in our array as a placeholder
        if (target == 0)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = target;
        }
    }

    public void ApplyOptions()
    {
        SetFrameRate(currentFrameRateIndex);
        SetDisplayMode(currentDisplayMode);
        SetResolution(currentResolutionIndex);
        QualitySettings.SetQualityLevel(currentQualityIndex, true);
        SetVolume(Volume);
    }

    public void ApplyAndSave()
    {
        SaveOptions();
    }

    public void CancelOptions()
    {
        LoadOptions();
        ApplyOptions();
    }
}