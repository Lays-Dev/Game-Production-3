using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Cinemachine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 movementInput;
    public GameObject player;
    private Rigidbody rb;
    public bool inRhythmGame = false;
    public bool controlLock = false;
    public GameObject RhythmPrefab;
    public CinemachineCamera playerCamera; // Reference to the player's main camera

    

    public float Distance = 5f; // Distance for raycasting to detect items

    public Transform currentCameraTransform; // Reference to the camera's transform for movement direction

    [Header("Colliders")]
    public Collider DetectionBox; // Collider for detecting items

    [Header("QuestBoard")]
    public QuestBoard currentQuestBoard;

    [Header("Run")]
    public float RunSpeed = 10f;
    public bool isRunning = false;
    private float currentSpeed;


    [Header("HitNote")]
    public bool hitNotePressed = false;

    [Header("Sky Box & Level Changer")]
    public SkyBoxChanger skyBoxChanger; // Reference to the SkyBoxChanger script to change skyboxes
    public DoorManager doorManager; // Reference to the DoorManager script to change scenes
    public bool doorInRange = false; // To track if the player is in range of a door
    public bool skyBoxChangerInRange = false; // To track if the player is in range of a skybox changer



    [Header("Layers")]
    public LayerMask itemsLayer; // Layer for items
    private List<Items> itemsInRange = new List<Items>();
    public LayerMask questBoardLayer; // Layer for quest board

    [Header("UI/ MISC stuff")]
    public bool isinteractable = false; // this is for the UI, to make sure the "Press E to interact" only shows up when you can actually interact with something.
    
    //public LockMouse mouseLock;

    Animator animator;

    public Items itemObject;

        

    private void OnMove(InputValue inputValue) // function to make the guy move
    {
        if (controlLock == false)
        {
            movementInput = inputValue.Get<Vector2>();
        }
        Debug.Log("Making sure this works.");
    }

    private void OnInteract(InputValue inputValue) // this is the thing that picks up items
    {
        // basic checks
        if(!inputValue.isPressed) return;

        if (itemsInRange.Count > 0)
        {
            Items item = itemsInRange[0]; // get the first item in range

            if (item != null && itemsInRange.Contains(item)) // if we hit an item and it's in range
            {
                Inventory inventory = GetComponent<Inventory>(); 

                if (inventory != null && item != null)
                {
                    item.PickUp(inventory); // places item in inventory
                    itemsInRange.Remove(item); // remove from list of items in range after picking up
                }
            }

            if (item != null && item.startsRhythmGame)
            {
                StartRhythmGame();
            }
        }

        // currentQuestBoard = GetComponent<QuestBoard>();
        if (currentQuestBoard != null && isinteractable) // if we hit a quest board and it's interactable
        {
            // Code to interact with quest board
            Debug.Log("Interacted with Quest Board");
            currentQuestBoard.openBoard(this);
        }

        if (skyBoxChanger != null && skyBoxChangerInRange)
        {
            skyBoxChanger.ChangeSkybox();
            return;
        }

        if (doorManager != null && doorInRange)
        {
            doorManager.EnterLevel();
            return;
        }

        
    }

    private void OnBack(InputValue inputValue)
    {
        if(!inputValue.isPressed) return;

        if(currentQuestBoard != null)
        {
            currentQuestBoard.closeBoard(this);
        }
        
    }

    private void OnDash(InputValue inputValue) //technically should be OnRun but I don't want to mess around with input names
    {

        isRunning = inputValue.isPressed;



    }

    public void StartRhythmGame()
    {
        if (inRhythmGame) return;

        inRhythmGame = true;
        controlLock = true;

        movementInput = Vector2.zero;
        Instantiate(RhythmPrefab);
    }


    private void OnTriggerEnter(Collider other) // doesn't pick up item, that's onInteract that does that
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items")) // reads for objects in items layer
        {
            Items item = other.GetComponent<Items>(); // gets the item component of the object, if it has one
            if (item != null && !itemsInRange.Contains(item)) // if it has an item component and isn't already in the list of items in range
            {
                itemsInRange.Add(item); // adds to list of items that can be picked up from any range (within detection box)
                Debug.Log("Item entered range: " + item.itemName);
            }
        }

        
        if (other.gameObject.layer == LayerMask.NameToLayer("QuestBoard"))
        {
            currentQuestBoard = other.GetComponent<QuestBoard>();
            isinteractable = true; 
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("SkyboxChanger"))
        {
            skyBoxChanger = other.GetComponent<SkyBoxChanger>();
            skyBoxChangerInRange = true; // this and door have different bools due to issues with the interaction system
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            doorManager = other.GetComponent<DoorManager>();
            doorInRange = true;
        }

        
    }

    private void OnTriggerExit(Collider other) // system to remove items from list of ones that can be picked up
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Items"))
        {
            Items item = other.GetComponent<Items>();
            if (item != null && itemsInRange.Contains(item))
            {
                itemsInRange.Remove(item); // removes from list of items that can be picked up when they leave the detection box
                Debug.Log("Item left range: " + item.itemName);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("QuestBoard"))
        {
            currentQuestBoard = null;
            isinteractable = false; 
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("SkyboxChanger"))
        {
            skyBoxChanger = null;
            skyBoxChangerInRange = false;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Door"))
        {
            doorManager = null;
            doorInRange = false;
        }
    }

    private void OnHitNote(InputValue inputValue) // this is where we press the note button 
    {
        if (!inputValue.isPressed) return;

        hitNotePressed = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // assign rigidbody.

        animator = GetComponentInChildren<Animator>();

        if (currentCameraTransform == null)
        {
            currentCameraTransform = Camera.main.transform;
        }

        LockMouse();

    }

    public void FixedUpdate ()
    {
        if (controlLock) return;
        
        
        // to make movement based on camera
        // grabs camera movements
        Vector3 forward = currentCameraTransform.forward;
        Vector3 right = currentCameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        
        // Build movement direction relative to camera
        Vector3 movement = forward * movementInput.y + right * movementInput.x;

        float moveSpeed = isRunning ? RunSpeed : Speed;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Smoothly transition to the target speed

        //makes character rotate to movement direction
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        
    }

    void LateUpdate()
    {
        hitNotePressed = false; // resets the hit note button every frame so it only registers once per press
    }
    
    // Update is called once per frame
    void Update()
    {

        // animator settings
        float moveAmount = movementInput.magnitude;

        bool isMoving = moveAmount > 0.1f;

        animator.SetBool("IsRunning", isRunning && isMoving);
        animator.SetFloat("Speed", isMoving ? 1f : 0f);

    }
}
