using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class InspectObject : MonoBehaviour
{
    [Header("Camera")]
    private Camera activeCamera;
    private CinemachineBrain brain; // reference to cinemachine brain to get active camera for raycasting and object movement

    [Header("Layer")]
    public LayerMask inspectLayer; //Interaction layer for raycast to detect what can be inspected

    [Header("Distance")]
    public float inspectDistance = 2; // default distance from camera when inspecting
    public float minDistance = 1; // minimum distance from camera when inspecting
    public float maxDistance = 4; // maximum distance from camera when inspecting
    public float scrollSpeed = 8; // speed at which the object moves when scrolling while inspecting

    [Header("Rotation")]
    public float rotationSensitivity = 0.005f; // sensitivity for how much the object rotates based on mouse movement
    public float rotationDamping = 3; // how quickly the rotation slows down

    [Header("Raycast Origin")]
    public CapsuleCollider raycastCapsule; //player mesh reference

    [Header("Camera Control")]
    public CinemachineInputAxisController inputAxisController;

    public float rayDistance = 5; //inspection max distance

    private Transform target;

    public GameObject Violin;

    private Vector3 startPos;
    private Quaternion startRot;
    private Vector3 startScale;

    private Vector2 rotationVelocity;

    private bool inspecting;

    private Player player;

    //Get Cine Machine brain and active camera
    void Start()
    {
        brain = FindFirstObjectByType<CinemachineBrain>();
        activeCamera = brain.OutputCamera;

        player = GetComponent<Player>();
        target = Violin.transform;

        //Save original position, rotation, and scale of object

        startPos = target.position;
        startRot = target.rotation;
        startScale = target.localScale;

        inspecting = true;

        if (player != null)
            player.controlLock = true;

        if (inputAxisController != null)
            inputAxisController.enabled = false;

        MoveToCamera();
        ScrollZoom();
        RotateObject();
    }

    //Check every frame for input to start/stop inspecting, and if inspecting, move, zoom, and rotate object
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (!inspecting)
                TryInspect();
            else
                StopInspect();
        }

        if (!inspecting) return;

        MoveToCamera();
        ScrollZoom();
        RotateObject();
    }

    //Cast ray to inspect objects and set up necessary variables for inspection
    void TryInspect()
    {
        if (raycastCapsule == null)
            return;

        Vector3 origin = raycastCapsule.bounds.center;
        Vector3 direction = activeCamera.transform.forward;

        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, inspectLayer))
        {
            //Target = inspected object

            
        }
    }

    //Reset object to original position, rotation, and scale
    void StopInspect()
    {

        //Target = inspected object

        target.position = startPos;
        target.rotation = startRot;
        target.localScale = startScale;

        rotationVelocity = Vector2.zero;

        inspecting = false;

        //Unlock player controls when done inspecting

        if (player != null)
            player.controlLock = false;

        if (inputAxisController != null)
            inputAxisController.enabled = true;
    }

    //Move object to a position in front of the camera - frame based
    void MoveToCamera()
    {
        Vector3 goal =
            activeCamera.transform.position +
            activeCamera.transform.forward * inspectDistance;

        target.position = Vector3.Lerp(
            target.position,
            goal,
            Time.deltaTime * 12f
        );
    }

    //Scroll to zoom in and out while inspecting - frame based
    void ScrollZoom()
    {
        float scroll = Mouse.current.scroll.ReadValue().y;
        float controllerScroll = Gamepad.current.leftStick.ReadValue().y;
        scroll += controllerScroll * 0.5f;


        inspectDistance += scroll * scrollSpeed * Time.deltaTime;

        inspectDistance = Mathf.Clamp(
            inspectDistance,
            minDistance,
            maxDistance
        );
    }

    //Rotate object based on mouse movement while left click is held - frame based
    void RotateObject()
    {
        //Check if left mouse button is held and add to rotation velocity based on mouse movement and sensitivity
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            
               
            rotationVelocity += delta * rotationSensitivity;
        }
        else {
            Vector2 rightStickInput = Gamepad.current.rightStick.ReadValue();
            
            rotationVelocity += rightStickInput * rotationSensitivity * 2f;
        }
        target.Rotate(
            activeCamera.transform.up,
            -rotationVelocity.x,
            Space.World
        );

        target.Rotate(
            activeCamera.transform.right,
            rotationVelocity.y,
            Space.World
        );

        rotationVelocity = Vector2.Lerp(
            rotationVelocity,
            Vector2.zero,
            Time.deltaTime * rotationDamping
        );
    }

    //Debug for interaction raycast
    void OnDrawGizmos()
    {
        if (raycastCapsule == null)
            return;

        if (brain == null)
            brain = FindFirstObjectByType<CinemachineBrain>();

        if (brain == null)
            return;

        Camera cam = brain.OutputCamera;

        if (cam == null)
            return;

        Vector3 origin = raycastCapsule.bounds.center;

        Vector3 direction = cam.transform.forward;

        Ray ray = new Ray(origin, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, inspectLayer))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(origin, hit.point);
            Gizmos.DrawSphere(hit.point, 0.07f);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(origin, origin + direction * rayDistance);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(origin, 0.05f);
    }
}