using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 movementInput;
    public GameObject player;
    private Rigidbody rb;
    public bool inRhythmGame = false;
    public bool controlLock = false;
    public GameObject RhythmPrefab;

    public float Distance = 5f; // Distance for raycasting to detect items

    public Transform cameraTransform; // Camera

    [Header("Colliders")]
    public Collider DetectionBox; // Collider for detecting items


    [Header("Layers")]
    public LayerMask itemsLayer; // Layer for items
    
        

    private void OnMove(InputValue inputValue) // function to make the guy move
    {
        if (controlLock == false)
        {
            movementInput = inputValue.Get<Vector2>();
        }
        Debug.Log("Making sure this works.");
    }

    private void OnInteract(InputValue inputValue)
    {
        // Interact with objects
        if(!inputValue.isPressed) return;

        

        Ray ray = new Ray(DetectionBox.transform.position, DetectionBox.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, itemsLayer))
        {

            Items item = hit.collider.GetComponent<Items>();

            if (item != null)
            {
                Inventory inventory = GetComponent<Inventory>();

                if (inventory != null)
                {
                    item.PickUp(inventory);
                }
            }
            
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
