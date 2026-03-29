using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float Speed = 5f;
    public Vector2 movementInput;
    public GameObject RhythmGameTest;
    public GameObject player;
    private Rigidbody rb;

    public Transform cameraTransform; // Camera
    

    private void OnMove(InputValue inputValue) // function to make the guy move
    {
        movementInput = inputValue.Get<Vector2>();

        Debug.Log("Making sure this works.");
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
            RhythmGameTest.SetActive(true);
            player.SetActive(false);

        }
    }
}
