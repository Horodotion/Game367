using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    inGame,
    inMenu,
    dead,
    other
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Stats playerStats;
    public PlayerState ourState;
    // [HideInInspector] Rigidbody rb;
    [HideInInspector] CharacterController charController;
    [HideInInspector] public Vector2 moveAxis;
    [HideInInspector] public Vector2 lookAxis;
    [HideInInspector] public float moveRotation;
    [HideInInspector] public Vector3 lookRotation;
    [HideInInspector] public float GRAVITY = 9.8f;
    public GameObject tankTurret;
    public GameObject tankBody;
    
    void Awake()
    {
        // This if statement checks if there is a general manager
        // If it finds no manager, it becomes the manager. If not, it destroys itself.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

            playerStats.SetStats();
            charController = GetComponent<CharacterController>();
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        lookRotation = Vector3.zero;
        tankTurret.transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

        }

        if (Input.GetButtonDown("Cancel"))
        {
            GeneralManager.TogglePause();
        }

        moveAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        lookAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }

    void FixedUpdate()
    {
        if (moveAxis != Vector2.zero)
        {
            if (moveAxis.x != 0)
            {
                moveRotation += moveAxis.x * 2f;
                transform.eulerAngles = new Vector3(0, moveRotation, 0);
            }
            if (moveAxis.y != 0)
            {
                Vector3 moveDirection = new Vector3(0, 0, moveAxis.y * Time.deltaTime * playerStats.stat[StatType.speed]);
                moveDirection = transform.TransformDirection(moveDirection);

                charController.Move(moveDirection);
            }
        }

        if (lookAxis != Vector2.zero)
        {
            lookRotation.y += lookAxis.x * 2f;
            lookRotation.x = Mathf.Clamp((lookRotation.x - lookAxis.y * 2f), -30f, 30f);

            tankTurret.transform.localEulerAngles = new Vector3(lookRotation.x, lookRotation.y, 0);
        }

        if (charController.isGrounded == false)
        {
            Vector3 gravitation = new Vector3(0, -GRAVITY * Time.deltaTime, 0);
            charController.Move(gravitation);
        }
    }

    public void EnactGravity()
    {

    }

    public void Shoot()
    {

    }
}
