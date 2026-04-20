using UnityEngine;
using Unity.Cinemachine;

// Place this on the same GameObject as your CinemachineCamera.
// It reads sensitivity from OptionsManager each frame and applies
// it to the Input Axis Controller's gain values.

//Disclaimer AI generated code.


[RequireComponent(typeof(CinemachineInputAxisController))]
public class SensitivityBridge : MonoBehaviour
{
    //Link to the Cinemachine Input Axis Controller game object
    private CinemachineInputAxisController inputAxisController;

    
    private void Awake()
    {
        inputAxisController = GetComponent<CinemachineInputAxisController>();
    }

    //Every frame, update the sensitivity values on the Input Axis Controller based on the current options
    private void Update()
    {
        if (OptionsManager.Instance == null || inputAxisController == null) return;

        // Controllers[0] = horizontal (Look X), Controllers[1] = vertical (Look Y)
        // This assumes the default Cinemachine axis order — swap indices if yours differ
        if (inputAxisController.Controllers.Count >= 2)
        {
            inputAxisController.Controllers[0].Input.Gain = OptionsManager.Instance.GetHorizontalSensitivity();
            inputAxisController.Controllers[1].Input.Gain = OptionsManager.Instance.GetVerticalSensitivity();
        }
    }
}