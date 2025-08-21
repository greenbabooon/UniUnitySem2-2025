using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{
    public ammoScript ammoScript;

    void Awake()
    {
        gameObject.GetComponent<Renderer>().material = ammoScript.mat;
    }
}
