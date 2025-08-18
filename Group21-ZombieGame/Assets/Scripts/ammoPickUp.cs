using UnityEngine;

public class ammoPickUp : MonoBehaviour
{   
    int ammoType;
    int ammoAmount;
    Inventory inventory;
    PlayerController playerController;
    void Start()
    {
        inventory = gameObject.GetComponentInParent<Inventory>();
        playerController = gameObject.GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter(Collider target)
    {
        if (target.GetComponent<ammoScript>() != null)
        {
            ammoType = target.GetComponent<ammoScript>().ammoType;
            ammoAmount = target.GetComponent<ammoScript>().ammoAmount;
            inventory.SetAmmoCount(ammoType, ammoAmount + inventory.GetAmmoCount(ammoType));
            Destroy(target.gameObject);
            playerController.UpdateAmmoUI();
        }
    }
}
