
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
    private GameObject equippedObj;
    private Weapon equippedWeapon;
    public GameObject hand;
    public Image[] HotBarImages;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;//temporary until we implement a reload animation
    int CurMag = 0;
    int CurSpare = 0;
    bool isReloading = false;
    Color selected = Color.grey;
    Color unselected = Color.white;
    private int currentIndex = 0;
    public Sprite slotEmpty;
    int ammoType;
    int mkOrKeyboard;

    //inputs handling below

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inv = GetComponent<Inventory>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Equip(inv.GetItem(0)); // Equip the first weapon in the inventory by default
        UpdateAmmoUI();
        UpdateHotbarUI();

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
        if (context.control.device is Gamepad)
        {
            lookSensitivity = 8;
            lookInput = context.ReadValue<Vector2>();
            
        }
        if (context.control.device is Mouse)
        {
            lookSensitivity = 0.4f;
            lookInput = context.ReadValue<Vector2>();
        }
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
        if (context.started && equippedWeapon.isAutomatic == false)
        {
            fireProjectile();
        }
        if (context.performed && equippedWeapon.isAutomatic == true)
        {
            StartFiring();
        }
        if (context.canceled && equippedWeapon.isAutomatic == true)
        {
            StopFiring();
        }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (context.performed && !isReloading && equippedWeapon.currentAmmo < equippedWeapon.magazineCapacity)
        {
            Invoke("ReloadWeapon", equippedWeapon.reloadTime);
            isReloading = true;
            reloadText.enabled = true;
        }
    }
    private void ReloadWeapon()
    {
        if (equippedWeapon.currentAmmo < equippedWeapon.magazineCapacity && inv.GetAmmoCount(equippedWeapon.ammoType) > 0)
        {
            int ammoNeeded = equippedWeapon.magazineCapacity - equippedWeapon.currentAmmo;
            int ammoAvailable = inv.GetAmmoCount(equippedWeapon.ammoType);
            int ammoToReload = Mathf.Min(ammoNeeded, ammoAvailable);
            equippedWeapon.currentAmmo += ammoToReload;
            inv.SetAmmoCount(equippedWeapon.ammoType, ammoAvailable - ammoToReload);
            UpdateAmmoUI();
            isReloading = false;
            reloadText.enabled = false;
        }
    }
    private void CancelReload()
    {
        if (isReloading)
        {
            CancelInvoke("ReloadWeapon");
            isReloading = false;
            reloadText.enabled = false;
        }
    }
    public void UpdateAmmoUI()
    {
        if (equippedWeapon != null)
        {
            CurMag = equippedWeapon.currentAmmo;
            CurSpare = inv.GetAmmoCount(equippedWeapon.ammoType);
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
        float mouseX = lookInput.x * lookSensitivity/4;
        float mouseY = lookInput.y * lookSensitivity/4;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void fireProjectile()
    {

        if (equippedWeapon.projectilePrefab != null && canShoot && equippedWeapon.currentAmmo > 0 && isReloading == false)
        {
            GameObject newProjectile = Instantiate(equippedWeapon.projectilePrefab, cameraTransform.position + cameraTransform.forward, cameraTransform.rotation);
            Rigidbody rb = newProjectile.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(cameraTransform.forward * equippedWeapon.force);
            }
            equippedWeapon.currentAmmo--;
            UpdateAmmoUI();
            canShoot = false;
            Invoke("enableShooting", equippedWeapon.fireRate);
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
            if (isReloading)
            {
                CancelReload();
            }
            int index = Mathf.Clamp((int)context.ReadValue<float>() + currentIndex, 0, inv.weapons.Count - 1);
            currentIndex = index;
            if (index >= 0 && index < inv.weapons.Count)
            {
                Weapon selectedWeapon = inv.GetItem(index);
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
    private void Equip(Weapon weaponEquip)
    {
        // Logic to equip the weapon
        if (equippedWeapon != null)
        {
            Destroy(equippedObj); // Destroy the currently equipped weapon
        }
        equippedObj = Instantiate(weaponEquip.weaponPrefab, hand.transform.position, hand.transform.rotation); // Instantiate the new weapon
        equippedWeapon = weaponEquip;
        equippedObj.transform.SetParent(hand.transform);
        equippedObj.transform.localPosition = Vector3.zero;
        equippedObj.transform.localRotation = Quaternion.Euler(0, 90f, 0f);
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
        Debug.Log("Equipped weapon: " + weaponEquip.weaponName);
    }


    //below are the methods that talk to the inventory script
    public Weapon GetWeapon(int index)
    {
        return inv.GetItem(index);
    }
    public void UpdateHotbarUI()
    {
        for (int i = 0; i < HotBarImages.Length; i++)
        {
            if (i < inv.weapons.Count)
            {
                HotBarImages[i].sprite = inv.weapons[i].WeaponIcon;
            }
            else
            {
                HotBarImages[i].sprite = slotEmpty;
            }
        }
    }
    private void InitializeAmmoType()
    {
        if (equippedWeapon != null)
        {
            ammoType = equippedWeapon.ammoType;
        } else
        {
            ammoType = 0; 
        }
    }
    

}