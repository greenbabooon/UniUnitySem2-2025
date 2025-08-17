
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private bool isSprinting = false;
    private float sprintMultiplier = 1;
    public float sprintSpeed = 2f;
    private bool isCrouching = false;
    private float verticalRotation = 0f;
    private bool canShoot = true;
    private Inventory inv;
    private GameObject equipped;
    private Weapon equippedWeapon;
    public GameObject hand;
    public Image[] HotBarImages;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;//temporary until we implement a reload animation
    int CurMag=0;
    int CurSpare = 0;
    bool isReloading = false;
    Color selected = Color.grey;
    Color unselected = Color.white;
    private int currentIndex = 0;

    //inputs handling below

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inv = GetComponent<Inventory>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Equip(inv.GetItem(0)); // Equip the first weapon in the inventory by default

    }
    private void Start()
    {
        UpdateAmmoUI();
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
        if (context.started && equippedWeapon.IsAutomatic() == false)
        {
            StartFiring();
        }
        if (context.performed && equippedWeapon.IsAutomatic() == true)
        {
            StartFiring();
        }
        if (context.canceled && equippedWeapon.IsAutomatic() == true)
        {
            StopFiring();
        }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed && !isReloading && equippedWeapon.GetCurrentAmmo() < equippedWeapon.GetMagazineCapacity())
        {
            Invoke("ReloadWeapon", equippedWeapon.GetReloadTime());
            isReloading = true;
            reloadText.enabled = true;  
        }
    }
    private void ReloadWeapon()
    {
        if (equippedWeapon.GetCurrentAmmo() < equippedWeapon.GetMagazineCapacity() && equippedWeapon.GetAmmoSpare() > 0)
        {
            int ammoNeeded = equippedWeapon.GetMagazineCapacity() - equippedWeapon.GetCurrentAmmo();
            int ammoToReload = Mathf.Min(ammoNeeded, equippedWeapon.GetAmmoSpare());

            equippedWeapon.currentAmmo += ammoToReload;
            equippedWeapon.ammoSpare -= ammoToReload;
            UpdateAmmoUI();
            isReloading = false;
            reloadText.enabled = false;
        }
    }
    private void UpdateAmmoUI()
    {
        if (equippedWeapon != null)
        {
            CurMag = equippedWeapon.GetCurrentAmmo();
            CurSpare = equippedWeapon.GetAmmoSpare();
            ammoText.text = "Ammo: " + CurMag + " | Spare: " + CurSpare;
        }
        else
        {
            ammoText.text = "∞";
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
    private void enableShooting()
    {
        canShoot = true;
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

        if (equippedWeapon.GetProjectile() != null && canShoot && equippedWeapon.GetCurrentAmmo() > 0&&isReloading == false)
        {
            GameObject newProjectile = Instantiate(equippedWeapon.GetProjectile(), cameraTransform.position + cameraTransform.forward, cameraTransform.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(cameraTransform.forward * equippedWeapon.GetForce());
            }
            equippedWeapon.currentAmmo--;
            UpdateAmmoUI();
            canShoot = false;
            Invoke("enableShooting", equippedWeapon.GetFireRate());
        }
    }
    public void StartFiring()
    {
        InvokeRepeating("fireProjectile", 0f, 0.01f);
    }
    public void StopFiring()
    {
        CancelInvoke("fireProjectile");
    }
    public void OnHotBarChange(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int index = Mathf.Clamp((int)context.ReadValue<float>(), 0, inv.weapons.Count - 1);
            currentIndex = index; 
            if (index >= 0 && index < inv.weapons.Count)
            {
                GameObject selectedWeapon = inv.GetItem(index);
                if (selectedWeapon != null)
                {
                    Equip(selectedWeapon);
                }
            }
            else
            {
                Debug.LogError("Invalid weapon index: " + index);
            }
        }
    }
    private void Equip(GameObject weaponObj)
    {
        // Logic to equip the weapon
        if (equipped != null)
        {
            Destroy(equipped); // Destroy the currently equipped weapon
        }
        equipped = Instantiate(weaponObj, hand.transform.position, hand.transform.rotation); // Instantiate the new weapon
        equippedWeapon = weaponObj.GetComponent<Weapon>();
        equipped.transform.SetParent(hand.transform);
        equipped.transform.localPosition = Vector3.zero;
        equipped.transform.localRotation = Quaternion.Euler(0, 90f, 0f);
     for (int i = 0; i < HotBarImages.Length; i++)
                    {
                        if (i == currentIndex)
                        {
                            HotBarImages[i].color = selected; // Highlight selected weapon
                        }
                        else
                        {
                            HotBarImages[i].color = unselected; // Reset color for unselected weapons

                        }
                    }
        UpdateAmmoUI(); 
        Debug.Log("Equipped weapon: " + weaponObj.name);
    }


    //below are the methods that talk to the inventory script
    public GameObject GetWeapon(int index)
    {
        return inv.GetItem(index);
    }
    
}