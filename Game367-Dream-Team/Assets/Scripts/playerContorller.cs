using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContorller : MonoBehaviour
{
    private CharacterController charController;
    [SerializeField] private float speed = default;

    public float rotateSensitivity = 10f;
    public int health;
    public int ammo;
    public int healthpack;
    public int ammopack;

    [SerializeField] float fireRate = 10f;
    private float nextTimeToFire;
    public int ammoCount = 100;
    [SerializeField] int maxAmmo = 100;

    public bool showUse;
    

    public GameObject firePosition;
    public GameObject ourProjectile;
    [SerializeField] float projectileSpeed = 5f;
    [SerializeField] float distanceForUse = 10;

    public PlayerHUD playerHud;
    public enum ShootType
    {
        raycast,
        physics
    };

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        ammo = 100;
        charController = GetComponent<CharacterController>();

        playerHud.SetAmmoText(ammo);
        playerHud.SetHealthText(health);

    }

    public ShootType shootMethod;

    // Update is called once per frame

    void Update()
    {
        // Movement through the keyboard 
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        moveDirection = transform.TransformDirection(moveDirection);

        charController.Move(moveDirection * Time.deltaTime * speed);
        // Rotation through the mouse

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSensitivity, 0);
        //transform.Rotate(Input.GetAxis("Mouse Y") * rotateSensitivity, 0, 0);

        if (Input.GetKeyDown(KeyCode.P))
        {
            GainHealth();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoseHealth();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainAmmo();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoseAmmo();
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)

        {
            nextTimeToFire = Time.time + 1 / fireRate;
            if (ammoCount > 0)
            {
                Shoot();
            }
        }
        CheckForUseable();
    }



    void Shoot()
    {
        if (shootMethod == ShootType.raycast)
        {
            RaycastHit hit;
            if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out hit, 200f))
            {
                Debug.Log(hit.transform.name);
                EnemyController enemyController = hit.transform.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.TakeDamage(10);
                }
            }
        }
        else if (shootMethod == ShootType.physics)
        {
            // prefab as a projectile
            GameObject projectile = Instantiate(ourProjectile, firePosition.transform.position, transform.rotation);
            if (projectile == null)
                return;
            // get the rigidbody
            Rigidbody ourRB = projectile.GetComponent<Rigidbody>();
            // velocity
            ourRB.velocity = projectile.transform.forward * projectileSpeed;
            Destroy(projectile, 5f);
            LoseAmmo();
        }
    }

    void LoseHealth()
    {
        health -= 10;
        Debug.Log("Current Health is at: " + health);
        playerHud.SetHealthText(health);
    }

    public void GainHealth()
    {
        health += healthpack;
        Debug.Log("I gained some health!  My health is at: " + health);
        playerHud.SetHealthText(health);
    }

    void LoseAmmo()
    {
        ammo --;
       // Debug.Log("I fired my gun!  Ammo Left: " + ammo);
        playerHud.SetAmmoText(ammo);
        //Decrease ammo by 1
    }

    public void GainAmmo()
    {
        ammo += ammopack;
        Debug.Log("I gained some ammo!  Ammo: " + ammo);
        playerHud.SetAmmoText(ammo);
        //Increase ammo by ammopack
    }
    void CheckForUseable()
    {
        RaycastHit hit;

        if (Physics.Raycast(firePosition.transform.position, firePosition.transform.forward, out hit, distanceForUse))
        {
            showUse = true;

            if (Input.GetKeyDown(KeyCode.F))
            {
                showUse = false;
                // to be used in the useobject script
            }
            else
                showUse = false;
        }
        else showUse = false;
    }

}

