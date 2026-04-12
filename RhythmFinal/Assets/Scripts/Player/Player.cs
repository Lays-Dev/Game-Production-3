using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Unity.Cinemachine;

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

    public Transform cameraTransform; // Reference to the camera's transform for movement direction

    [Header("Colliders")]
    public Collider DetectionBox; // Collider for detecting items

    [Header("QuestBoard")]
    public QuestBoard currentQuestBoard;


    [Header("Layers")]
    public LayerMask itemsLayer; // Layer for items
    private List<Items> itemsInRange = new List<Items>();
    public LayerMask questBoardLayer; // Layer for quest board

    [Header("UI/ MISC stuff")]
    public bool isinteractable = false; // this is for the UI, to make sure the "Press E to interact" only shows up when you can actually interact with something.
    
    public LockMouse mouseLock;
        

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
        }

        // currentQuestBoard = GetComponent<QuestBoard>();
        if (currentQuestBoard != null && isinteractable) // if we hit a quest board and it's interactable
        {
            // Code to interact with quest board
            Debug.Log("Interacted with Quest Board");
            currentQuestBoard.openBoard(this);
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
    }


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // assign rigidbody.

    }

    void FixedUpdate ()
    {
        // to make movement based on camera
        // grabs camera movements
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        
        // Build movement direction relative to camera
        Vector3 movement = forward * movementInput.y + right * movementInput.x;

        rb.MovePosition(rb.position + movement * Speed * Time.fixedDeltaTime);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.pKey.isPressed)
        {
            if (inRhythmGame == false)
            {
                inRhythmGame= true;
                Instantiate(RhythmPrefab);
                controlLock = true;
            }

        }
    }
}
