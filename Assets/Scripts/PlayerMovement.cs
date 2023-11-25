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

    public Rigidbody rb;
    public Collider playerCollider;
    public float jumpForce; // Force initiale du saut
    public float jumpTime; // Temps maximal de maintien de la touche d'espace
    private float jumpTimeCounter; // Compteur de temps pour le saut
    public float jumpPreparationTime; // Temps d'avance pour le saut
    private float jumpPreparationTimer; // Temps d'avance pour le saut

    private bool isJumping = false; // Indicateur pour savoir si le joueur est en train de sauter
    public float fallGravityScale; // Gravité appliquée pendant la chute

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bulletReloadTimer = bulletReloadTime;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -4f);

        jumpTimeCounter = jumpTime;
        jumpPreparationTimer = 0;

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

        bool isGrounded = Physics.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 0.01f);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpPreparationTimer = jumpPreparationTime;
        }

        jumpPreparationTimer -= Time.deltaTime;
        jumpTimeCounter -= Time.deltaTime;

        if (isGrounded && rb.velocity.y == 0f)
        {
            if (jumpPreparationTimer > 0 && !isJumping)
            {
                print("test");
                // Débute le saut
                isJumping = true;
                jumpTimeCounter = jumpTime; // Réinitialise le compteur de temps à la fin du saut
                rb.AddForce(Vector3.up * jumpForce * 3, ForceMode.Impulse);
            }
                           
        }

        // Vérifie si la touche d'espace est maintenue
        if (jumpTimeCounter > 0)
        {
            if(isJumping && Input.GetKey(KeyCode.Space))
            {
                // Continue d'appliquer une force tant que la touche est maintenue
                rb.AddForce(Vector3.up * 10 * jumpForce * Time.deltaTime, ForceMode.Impulse);

            }
        }
        else
        {
            isJumping = false;
        }

        // Applique une gravité plus forte pendant la chute
        if (rb.velocity.y < 3f)
        {
            rb.AddForce(Vector3.down * fallGravityScale, ForceMode.Acceleration);
        }
    }
    float CalculateJumpForce()
    {
        // Calcule la force du saut en fonction du temps écoulé
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
