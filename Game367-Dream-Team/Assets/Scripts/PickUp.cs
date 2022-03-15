using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject player;
    private playerContorller playerScript;
   // public GameObject itemSpawn;


    // Start is called before the first frame update
    void Start()
    {
        this.transform.Rotate(0, 20f * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Pickup();

        }
    }
    void Pickup()
    {

        playerScript.GainHealth();

        Destroy(gameObject);

    }
}
