using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
// This script was created with AI then edited by me! - Lays


public class RisingPlatform : MonoBehaviour
{
    [Header("Platform")]
    public Transform platform;
    public Transform topPoint;
    public Transform bottomPoint;
    public float speed = 2f;

    [Header("Player")]
    public GameObject player;
    public MonoBehaviour playerController; // your movement script
    public PlayerInput playerInput; // drag your PlayerInput here


    private bool isMoving;
    private bool isAtTop;
    private bool playerOnPlatform;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving && !isAtTop)
        {
            playerOnPlatform = true;

            // Always grab ROOT player object
            player = other.transform.root.gameObject;

            StartCoroutine(MoveUpSequence());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerOnPlatform = false;
        }
    }

    // This gets called by PlayerInput when Interact is pressed
    public void OnPlayerInteract()
    {
        if (isAtTop && playerOnPlatform && !isMoving)
        {
            StartCoroutine(MoveDownSequence());
        }
    }


    IEnumerator MoveUpSequence()
    {
        isMoving = true;

        // Disable player movement (cutscene)
        playerController.enabled = false;

        // Stick player to platform
        player.transform.SetParent(platform);

        // Move UP
        yield return StartCoroutine(MovePlatform(topPoint.position));

        isAtTop = true;
        isMoving = false;

        // Re-enable control (start rhythm game)
        playerController.enabled = true;
    }

    IEnumerator MoveDownSequence()
    {
        isMoving = true;

        // Disable control again
        playerController.enabled = false;

        // Move DOWN
        yield return StartCoroutine(MovePlatform(bottomPoint.position));

        // Unstick player
        player.transform.SetParent(null);

        isAtTop = false;
        isMoving = false;

        // Give control back
        playerController.enabled = true;
    }

    IEnumerator MovePlatform(Vector3 target)
    {
        while (Vector3.Distance(platform.position, target) > 0.01f)
        {
            platform.position = Vector3.MoveTowards(
                platform.position,
                target,
                speed * Time.deltaTime
            );

            yield return null;
        }
    }

    // Player script calls TryMoveDown

    public void TryMoveDown()
    {
        if (isAtTop && playerOnPlatform && !isMoving)
        {
            StartCoroutine(MoveDownSequence());
        }
    }
}

