using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 10f;

    public GameObject player;
    public GameObject firePosition;
    public GameObject projectile;

    private Renderer rend;

    public float projectileSpeed;
    private float nextTimeToFire;
    public float fireRate;
    private EnemySpawner enemySpawning;
    
    

    public int ammoCount;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            health = 0;
            enemySpawning = FindObjectOfType<EnemySpawner>();
            enemySpawning.enemiesInRoom--;
            if (enemySpawning.enemiesInRoom <= 0)
            {
                enemySpawning.SpawnerReset();
            }
            Destroy(gameObject) ;
        }
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * speed);
        transform.LookAt(player.transform.position);

        if ((distance < 100f && distance > 40f) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            if(ammoCount > 0)
            {
                Shoot();
            }
        }
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            TakeDamage(5);
        }
    }
    private void Shoot()
    {
        GameObject ourprojectile = Instantiate(projectile, firePosition.transform.position, transform.rotation);
        if (ourprojectile == null)
            return;

        //add rigidbody to projectile
        Rigidbody ourRb = ourprojectile.GetComponent<Rigidbody>();

        //add velocity to the projectile
        ourRb.velocity = projectile.transform.forward * projectileSpeed;

        //Destroy projectile after a certain time
        Destroy(ourprojectile, 5f);

        //DECREASE AMMO
        DecreaseAmmo(1);
    }
    private void  DecreaseAmmo(int ammo)
    {
        ammoCount -= ammo;
    }
  
}
