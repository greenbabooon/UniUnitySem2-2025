
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public interface IInteractable
{
    void Interact();
    string InteractionPrompt();
    //void MakeGlow(Material glowMaterial);
    //void StopGlow();

}
public class PlayerController : MonoBehaviour, IDamageable
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
    private Inventory inv;
    private GameObject equippedObj;
    private Weapon equippedWeapon;
    public GameObject hand;
    public Image[] HotBarImages;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI reloadText;//temporary until we implement a reload animation
    public TextMeshProUGUI equippedText;
    int CurMag = 0;
    int CurSpare = 0;
    bool isReloading = false;
    Color selected = Color.grey;
    Color unselected = Color.white;
    private int currentIndex = 0;
    public Sprite slotEmpty;
    int ammoType;
    int mkOrKeyboard;
    float interactReach = 3f;
    bool interacting = false;
    public GameObject HUD;
    bool isPaused = false;
    public bool inMenu = false;
    public HealthScript healthScript;
    bool damageAlert = false;
    public TextMeshProUGUI HealthText;
    //Material highlightMat;


    //inputs handling below
    void Awake()
    {
       // highlightMat = Resources.Load<Material>("Mats/Glow");
    }
    private void OnEnable()
    {
        controller = GetComponent<CharacterController>();
        inv = GetComponent<Inventory>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        currentIndex = 0;
        UpdateAmmoUI();
        UpdateHotbarUI();
        UpdateHealthUI();
    }
    private void Start()
    {
        UpdateAmmoUI();
    }

    private void Update()
    {
        HandleMovement();
        HandleLook();
        HandleInteraction();

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (isPaused) return;
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
        if (isPaused) return;
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
    }
    public void OnSprint(InputAction.CallbackContext context)   //note: sprint is set to "hold" if you would prefer toggle use context.performed and only one if statement
    {
        if (isPaused) return;
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
        if (isPaused) return;
        if (equippedWeapon != null)
        {
            if (context.performed)
            {
                equippedWeapon.weaponType.AttackPressed();
            }
            if (context.canceled)
            {
                equippedWeapon.weaponType.AttackReleased();
            }
        }
    }
    public void OnReload(InputAction.CallbackContext context)
    {
        if (isPaused||equippedWeapon == null) return;
        equippedWeapon.weaponType.Reload();
    }
    private void CancelReload()
    {
        //cancel reload in weapon type script
        equippedWeapon.weaponType.CancelReload();

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

    public void HandleLook()
    {
        float mouseX = lookInput.x * lookSensitivity / 4;
        float mouseY = lookInput.y * lookSensitivity / 4;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    public void OnHotBarChange(InputAction.CallbackContext context)
    {
        if (isPaused) return;
        if (context.performed)
        {
            if (isReloading)
            {
                CancelReload();
            }
            int index = Mathf.Clamp((int)context.ReadValue<float>() + currentIndex, 0, inv.weapons.Count - 1);
            currentIndex = index;
            if (index >= 0 && index < inv.weapons.Count&& inv.weapons.Count > 0)
            {
                Weapon selectedWeapon = inv.GetItem(index);
                GameObject selectedWeaponObj = inv.GetWeaponObject(index);
                if (selectedWeapon != null && selectedWeapon != equippedWeapon)
                {
                    Equip(index);
                }
            }
            else
            {
                Debug.Log("Invalid weapon index: " + index);
            }
        }
    }
    private void Equip(int index)
    {
        CancelInvoke("DisableEquippedText");
        // Logic to equip the weapon
        if (equippedObj != null)
        {
            equippedObj.SetActive(false);

        }
        equippedObj = inv.GetWeaponObject(index);
        equippedWeapon = inv.GetItem(index);
        equippedWeapon.weaponType.SetFirePoint(cameraTransform.gameObject);
        equippedWeapon.weaponType.SetPlayerOwned(true);
        equippedObj.SetActive(true);
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
        if (equippedWeapon != null)
        {
            equippedText.text = "Equipped: " + equippedWeapon.weaponName;
        }
        else
        {
            equippedText.text = "Equipped: None";
        }
        equippedText.enabled = true;
        Invoke("DisableEquippedText", 2f);
        Debug.Log("Equipped weapon: " + equippedWeapon.weaponName);
    }
    private void DisableEquippedText()
    {
        equippedText.enabled = false;
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
        }
        else
        {
            ammoType = 0;
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && !inMenu)
        {
            interacting = true;
        }
        else if (context.canceled && !inMenu)
        {
            interacting = false;
        }
    }
    private IInteractable LastInteractable;
    void HandleInteraction()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactReach))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (LastInteractable != interactable)
                {
                    if (LastInteractable != null)
                    {
                       // LastInteractable.StopGlow();
                    }
                   // interactable.MakeGlow(highlightMat);
                    print("Made glow");
                    LastInteractable = interactable;
                }
                interactText.text = interactable.InteractionPrompt();
                interactText.enabled = true;
                if (interacting)
                {
                    interactable.Interact();
                    interacting = false;
                }
            }
            else
            {
                if (LastInteractable != null)
                {
                   // LastInteractable.StopGlow();
                    LastInteractable = null;
                }
                interactText.enabled = false;
            }
        }
        else
        {
            

            if (LastInteractable != null)
            {
               // LastInteractable.StopGlow();
                LastInteractable = null;
            }
            interactText.enabled = false;
            
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void HideHUD()
    {
        HUD.SetActive(false);
    }
    public void ShowHUD()
    {
        HUD.SetActive(true);
    }
    public void damage(float damageAmount)
    {
        healthScript.currentHealth -= damageAmount;
        UpdateHealthUI();
        if (!damageAlert)Invoke("damageAlertCancel", 1f);
        damageAlert = true;
        HealthText.color = Color.red;
        if (healthScript.currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    void damageAlertCancel()
    {
        damageAlert = false;
        HealthText.color = Color.black;
    }
    void UpdateHealthUI()
    {
        HealthText.text = "Health: " + healthScript.currentHealth + " / " + healthScript.maxHealth;
  
    }   

}