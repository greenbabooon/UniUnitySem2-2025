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
        if (target.GetComponent<AmmoBoxScript>() != null)
        {
            ammoType = target.GetComponent<AmmoBoxScript>().ammoScript.ammoType;
            ammoAmount = target.GetComponent<AmmoBoxScript>().ammoScript.ammoAmount;
            inventory.SetAmmoCount(ammoType, ammoAmount + inventory.GetAmmoCount(ammoType));
            Destroy(target.gameObject);
            playerController.UpdateAmmoUI();
        }
    }
}
