using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    public enum ItemType
    {
        health,
        ammo,
        points,
        key,
        other
    }

    public ItemType ourItem;

    public int value;
    //TO BE ADDED NEXT WEEK
    public string lootText;

    public GameObject player;
    private playerContorller playerScript;

    public bool isUsed;
    public bool isReusable;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<playerContorller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseTheObject()
    {
        switch (ourItem)
        {
            case ItemType.ammo:
                {
                    //TODO: DECIDE AMMO AMOUNT
                    break;
                }
            case ItemType.health:
                {
                    //TODO: Decide Health Amount
                    break;
                }
            case ItemType.points:
                {
                    break;
                }
            case ItemType.key:
                {
                    break;
                }
            case ItemType.other:
                {
                    break;
                }
        }
        //IF REUSABLE CHANGE CODE HERE
        Destroy(gameObject);
    }
}
