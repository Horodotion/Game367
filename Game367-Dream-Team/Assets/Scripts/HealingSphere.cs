using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSphere : MonoBehaviour
{

    public playerContorller playerScript;
    public GameObject player;

    public bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<playerContorller>();
        isIn = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(isIn = true)
        {
            playerScript.GainHealth();
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isIn = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            isIn = false;
        }
    }
}
