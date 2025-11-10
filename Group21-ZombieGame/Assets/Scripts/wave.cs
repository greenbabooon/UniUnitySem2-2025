using UnityEngine;

public class wave : MonoBehaviour
{
    public Animator anim;
    public MeshRenderer rend;
    float baseInnerRadius=1f;
    float baseOuterRadius=1.1f;
    Transform player;
    bool waveActive = false;
    public bool test;
    float height =2f;
    
    void Awake()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        
    }
    public void fire()
    {
        rend.enabled = true;
        anim.SetTrigger("fire");
        waveActive = true;
    }
    public void EndFire()
    {
        transform.localScale = new Vector3(1,1,1);
        rend.enabled = false;
        waveActive = false;
        
       
    }
    void Update()
    {
        if (waveActive == true)
        {
            float scale = (transform.localScale.x + transform.localScale.z) * 0.5f;


            float innerRadius = baseInnerRadius * scale;
            float outerRadius = baseOuterRadius * scale;


            Vector3 flatPlayerPos = new Vector3(player.position.x, transform.position.y - 0.7f, player.position.z);
            float dist = Vector3.Distance(flatPlayerPos, transform.position);

            float verticalDist = Mathf.Abs(player.position.y - transform.position.y);


            if (dist >= innerRadius && dist <= outerRadius && verticalDist <= height * 0.5f)
            {
                EndFire();
                Debug.Log("Player hit by wave!");
                player.GetComponent<PlayerController>().damage(5);
                player.GetComponent<PlayerController>().TVEffect();

            }
        }
        if (test == true)
        {
            fire();
            test = false;
        }

    }
        void OnDrawGizmosSelected()
    {
        float scale = (transform.localScale.x + transform.localScale.z) * 0.5f;
        float innerRadius = baseInnerRadius * scale;
        float outerRadius = baseOuterRadius * scale;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, innerRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, outerRadius);

        // Draw wave height box
        Gizmos.color = new Color(0, 1, 0, 0.25f);
        Gizmos.DrawCube(transform.position, new Vector3(outerRadius * 2, height, outerRadius * 2));
    }
}
