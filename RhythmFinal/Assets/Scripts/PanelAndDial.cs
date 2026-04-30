using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelAndDial : MonoBehaviour
{
    /*
    public GameObject Panel;
    public Animator DialAnimator;
    public AudioSource click;
    public Animator Lever;
    public AudioSource leverSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            Panel.GetComponent<SkyBoxChanger>().ChangeSkybox();
            DialAnimator.SetTrigger("Change");
            click.Play();
        }
        else if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            Panel.GetComponent<SkyBoxChanger>().ChangeSkyboxBackwards();
            DialAnimator.SetTrigger("ChangeBackwards");
            click.Play();
        }

        else if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Panel.GetComponent<SkyBoxChanger>().disableCamera();
        }
        else if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            StartCoroutine(leverPulling());
            
                
        }
    }
    private IEnumerator leverPulling()
    {
        Lever.SetTrigger("PullLever");
        leverSound.Play();
        yield return new WaitForSeconds(1f); // Wait for the animation to finish (adjust the time as needed)
        Panel.GetComponent<SkyBoxChanger>().disableCamera();
    }
    */
}
