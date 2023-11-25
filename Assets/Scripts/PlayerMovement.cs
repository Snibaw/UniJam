using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public bool canShoot = true;
    

    [Header("Bullet")]

    public float bulletReloadTime = 0.5f;
    private float bulletReloadTimer = 0f;
    public GameObject canon;
    public GameObject bulletPrefab;
    [Header("Player Movement")]
    public float speed = 5f;
    public float sensitivity;
    private Vector3 rotate;

    private Rigidbody rb;
    private Collider playerCollider;
    public float jumpForce; // Force initiale du saut
    public float jumpTime; // Temps maximal de maintien de la touche d'espace
    private float jumpTimeCounter; // Compteur de temps pour le saut

    private bool isJumping = false; // Indicateur pour savoir si le joueur est en train de sauter
    public float fallGravityScale; // Gravit� appliqu�e pendant la chute

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bulletReloadTimer = bulletReloadTime;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -4f);
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();

        jumpTimeCounter = jumpTime;

    }

    // Update is called once per frame
    void Update()
    {
        bulletReloadTimer -= Time.deltaTime;

        RotateThePlayer();
        if(!canMove) return;
        MoveThePlayer();
        if(Input.GetMouseButton(0))
        {
            if(bulletReloadTimer <= 0f)
                ShootBullet();
        }

        bool isGrounded = Physics.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 0.1f);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            // D�bute le saut
            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce * 3, ForceMode.Impulse);
        }

        // V�rifie si la touche d'espace est maintenue
        if (Input.GetKey(KeyCode.Space) && isJumping && jumpTimeCounter > 0)
        {
            print(jumpTimeCounter);
            // Continue d'appliquer une force tant que la touche est maintenue
            rb.AddForce(Vector3.up * 10 * jumpForce * Time.deltaTime, ForceMode.Impulse);
            jumpTimeCounter -= Time.deltaTime;

        }

        // V�rifie si la touche d'espace est rel�ch�e
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpTimeCounter = jumpTime; // R�initialise le compteur de temps � la fin du saut
        }
        // Applique une gravit� plus forte pendant la chute
        if (rb.velocity.y < 3f)
        {
            rb.AddForce(Vector3.down * fallGravityScale, ForceMode.Acceleration);
        }
    }
    float CalculateJumpForce()
    {
        // Calcule la force du saut en fonction du temps �coul�
        if (jumpTimeCounter > 0)
        {
            return jumpForce;
        }
        else
        {
            return 0f;
        }
    }


    void MoveThePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float yRotation = transform.eulerAngles.y;
        Vector3 move = Quaternion.Euler(0f, yRotation, 0f) * new Vector3(x, 0f, z);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 1.5f : speed;

        transform.Translate(move.normalized * currentSpeed * Time.deltaTime, Space.World);
    }
    void RotateThePlayer()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
    
        Vector3 rotation = new Vector3(x, y*sensitivity, 0f);
    
        transform.eulerAngles = transform.eulerAngles - rotation;

        //Rotation max in x is 70 and -70
        float currentX = transform.eulerAngles.x;
        if(currentX > 180f)
        {
            currentX -= 360f;
        }
        currentX = Mathf.Clamp(currentX, -70f, 70f);
        transform.eulerAngles = new Vector3(currentX, transform.eulerAngles.y, transform.eulerAngles.z);
        
    }
    void ShootBullet()
    {
        bulletReloadTimer = bulletReloadTime;
        GameObject bullet = Instantiate(bulletPrefab, canon.transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
        Destroy(bullet, 3f);
    }
}
