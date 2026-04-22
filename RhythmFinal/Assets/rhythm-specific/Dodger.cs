using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dodger : MonoBehaviour
{
    public bool isOnTop;
    public bool isOnMiddle;
    public bool isOnBottom;
    public RectTransform RectTransform;
    public bool canMove;
    public bool hasBeenHurt;
    public HealthTracking healthTracking;
    public float invisibilityDuration = 0.5f;
    



    public void Start()
    {
        canMove = true;
    }
   
    void Update()
    {
        //So all of this follows really simple logic. If OnTop, Then hitting S moves you to Middle.
        // If on Middle, hitting W moves you to Top and hitting S moves you to Bottom ViceVersa.
        // Also has a cooldown to make sure hitting S on top doesn't teleport you to bottom
        if (isOnTop)
        {
            //HARDCODED KEYBOARD INPUT BAD. WILL CHANGE ONCE PLAYER CONTROLS ARE IN PLACE
            if (Keyboard.current.sKey.wasPressedThisFrame && canMove )
            {
                //
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 230);
                isOnTop = false;
                isOnMiddle = true;
                canMove = false;
                StartCoroutine(CheckIfCanMove());
            }
        }
        else if (isOnMiddle)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame && canMove )
            {
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 415);
                isOnMiddle = false;
                isOnTop = true;
                canMove = false;
                StartCoroutine(CheckIfCanMove());
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame && canMove )
            {
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 45);
                isOnMiddle = false;
                isOnBottom = true;
                canMove = false;
                StartCoroutine(CheckIfCanMove());
            }
        }
        else if(isOnBottom)
        {
            if(Keyboard.current.wKey.wasPressedThisFrame && canMove )
            {
                RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 230);
                isOnBottom = false;
                isOnMiddle = true;
                canMove = false;
                StartCoroutine(CheckIfCanMove());
            }
        }
        if (Gamepad.all.Count > 0)
        {
            if (isOnTop)
            {
                //HARDCODED KEYBOARD INPUT BAD. WILL CHANGE ONCE PLAYER CONTROLS ARE IN PLACE
                if (Keyboard.current.sKey.wasPressedThisFrame && canMove || Gamepad.current.dpad.down.wasPressedThisFrame && canMove)
                {
                    //
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 230);
                    isOnTop = false;
                    isOnMiddle = true;
                    canMove = false;
                    StartCoroutine(CheckIfCanMove());
                }
            }
            else if (isOnMiddle)
            {
                if (Keyboard.current.wKey.wasPressedThisFrame && canMove || Gamepad.current.dpad.up.wasPressedThisFrame && canMove)
                {
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 415);
                    isOnMiddle = false;
                    isOnTop = true;
                    canMove = false;
                    StartCoroutine(CheckIfCanMove());
                }
                else if (Keyboard.current.sKey.wasPressedThisFrame && canMove || Gamepad.current.dpad.down.wasPressedThisFrame && canMove)
                {
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 45);
                    isOnMiddle = false;
                    isOnBottom = true;
                    canMove = false;
                    StartCoroutine(CheckIfCanMove());
                }
            }
            else if (isOnBottom)
            {
                if (Keyboard.current.wKey.wasPressedThisFrame && canMove || Gamepad.current.dpad.up.wasPressedThisFrame && canMove)
                {
                    RectTransform.anchoredPosition = new Vector2(RectTransform.anchoredPosition.x, 230);
                    isOnBottom = false;
                    isOnMiddle = true;
                    canMove = false;
                    StartCoroutine(CheckIfCanMove());
                }
            }
        }
    }
    public IEnumerator CheckIfCanMove()
    {
        yield return new WaitForSeconds(0.01f);
        canMove = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Evil"))
        {
            //THIS IS IMPORTANT. I'm using the wrong healthbar here, I'm using the same one for the other rhythm games.
            //We need to make a seperate one specifically for the boss fights, we were speaking about changing it to
            //Lives for the rhythm Games Total and Healthbar for boss fights and inside of the rhythm game.
            GameObject healthTracking = GameObject.FindWithTag("HealthTracker");
            if (hasBeenHurt == false)
            {
                StartCoroutine(healthTracking.GetComponent<HealthTracking>().TakeSmallDamage());
                hasBeenHurt = true;
                
                StartCoroutine(InvisibilityFrames());
            }
        }
    }
    public IEnumerator InvisibilityFrames()
    {
        //
        StartCoroutine(FlashWhite());
        yield return new WaitForSeconds(invisibilityDuration);
        hasBeenHurt = false;
    }
    public IEnumerator FlashWhite()
    {
        
        RawImage rawImage = GetComponent<RawImage>();
        Color originalColor = rawImage.color;
        rawImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rawImage.color = originalColor;
        yield return new WaitForSeconds(0.1f);
        rawImage.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rawImage.color = originalColor;
        
    }
}

