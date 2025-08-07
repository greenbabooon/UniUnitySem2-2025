using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Look Settings")]
    public Transform cameraTransform;
    public float lookSensitivity = 2f;
    public float verticalLookLimit = 90f;
    public float jumpHeight = 1f;
    public GameObject projectile;
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private bool isSprinting = false;
    private float sprintMultiplier = 1;
    public float sprintSpeed = 2f;
    private bool isCrouching = false;
    private float verticalRotation = 0f;
    public float fireRate = 0.1f; // Rate of fire for the projectile
    public bool isAuto = false; //change firing mode from semi auto to auto.
    private bool canShoot = true;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
    }
    public void OnSprint(InputAction.CallbackContext context)   //note: sprint is set to "hold" if you would prefer toggle use context.performed and only one if statement
    {
        if (context.started)
        {
            sprintMultiplier = sprintSpeed;
        }
        if (context.canceled)
        {
            sprintMultiplier = 1f;
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && isAuto == false)
        {
            fireProjectile();
        }
        if (context.performed && isAuto == true)
        {
            StartFiring();
        }
        if (context.canceled && isAuto == true)
        {
            StopFiring();
        }
    }
    public void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime * sprintMultiplier);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity / 4f;
        float mouseY = lookInput.y * lookSensitivity / 4f;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    public void fireProjectile()
    {

        if (projectile != null)
        {
            GameObject newProjectile = Instantiate(projectile, cameraTransform.position + cameraTransform.forward, cameraTransform.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(cameraTransform.forward * 20f, ForceMode.Impulse);
            }
        }
    }
    public void StartFiring()
    {
        InvokeRepeating("fireProjectile", 0f, 0.1f); // Adjust the rate of fire as needed
    }
    public void StopFiring()
    {
        CancelInvoke("fireProjectile");
    }
}