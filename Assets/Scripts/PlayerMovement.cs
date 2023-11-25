using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public bool canShoot = true;
    

    [Header("Shooting")]
    public ParticleSystem paintParticles;
    public List<Color> paintColors;
    private int currentColor = 0;

    [Header("Player Movement")]
    public float speed = 5f;
    public float sensitivity;

    private Rigidbody rb;
    private Collider playerCollider;
    public float jumpForce; // Force initiale du saut
    public float jumpTime; // Temps maximal de maintien de la touche d'espace
    private float jumpTimeCounter; // Compteur de temps pour le saut
    public float jumpPreparationTime; // Temps d'avance pour le saut
    private float jumpPreparationTimer; // Temps d'avance pour le saut

    private bool isJumping = false; // Indicateur pour savoir si le joueur est en train de sauter
    public float fallGravityScale; // Gravit� appliqu�e pendant la chute


    private int _xModifier = 1, _yModifier = 1;
    private float x, z;
    private float xRotation, yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -4f);

        jumpTimeCounter = jumpTime;
        jumpPreparationTimer = 0;

        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();

        
        // Init the color of the paint to the first color in the list
        paintParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = paintColors[currentColor];
        GetComponentInChildren<ParticlesController>().paintColor = paintColors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        RotateThePlayer();
        if (!canMove) return;
        MoveThePlayer();
        if (Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeColor();
        }

        bool isGrounded = Physics.Raycast(playerCollider.bounds.center, Vector3.down,
            playerCollider.bounds.extents.y + 0.01f);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPreparationTimer = jumpPreparationTime;
        }

        jumpPreparationTimer -= Time.deltaTime;
        jumpTimeCounter -= Time.deltaTime;

        if (isGrounded && rb.velocity.y == 0f)
        {
            if (jumpPreparationTimer > 0 && !isJumping)
            {
                // D�bute le saut
                isJumping = true;
                jumpTimeCounter = jumpTime; // R�initialise le compteur de temps � la fin du saut
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        }

        // V�rifie si la touche d'espace est maintenue
        if (jumpTimeCounter <= 0)
        
        {
            isJumping = false;
        }
    }

    public void ChangeControlDependingOnGravity()
    {
        if(Physics.gravity.y > 0)
        {
            x *= -1;
        }   
        else if(Physics.gravity.x<0)
        {
            x*=-1;
        }
       
    }
    private void ChangeViewDependingOnGravity()
    {
        if(Physics.gravity.y > 0)
        {
            xRotation *= -1;
            yRotation *= -1;
        }

    }
    void MoveThePlayer()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        ChangeControlDependingOnGravity();

        float yRotation = transform.eulerAngles.y;
        Vector3 move;
        if (Physics.gravity.y != 0)
        {
            move = Quaternion.Euler(0f, yRotation, 0f) * new Vector3(x, 0f, z);
        }
        else
        {
            move = Quaternion.Euler(0f, yRotation, 0f) * new Vector3(0f, x, z);
        }
        

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 1.5f : speed;

        transform.Translate(move.normalized * currentSpeed * Time.deltaTime, Space.World);
    }
    void RotateThePlayer()
    {
        yRotation = Input.GetAxis("Mouse X");
        xRotation = Input.GetAxis("Mouse Y");
        ChangeViewDependingOnGravity();

        Vector3 rotation = new Vector3(xRotation, yRotation * sensitivity, 0f);
    
        if (Physics.gravity.y!=0)
        {
            rotation = new Vector3(xRotation, yRotation * sensitivity, 0f);
        }
        else if (Physics.gravity.x>0)
        {
            rotation = new Vector3(-yRotation * sensitivity, xRotation, 0f);
        }
        else if (Physics.gravity.x<0)
        {
            rotation = new Vector3(yRotation * sensitivity, -xRotation, 0f);
        }

        
    
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
    void StartShooting()     {         
        paintParticles.Play();     
        }          
    void StopShooting()     {         
        paintParticles.Stop();     
        }          
    void ChangeColor()     {         
        currentColor++;         
        if(currentColor >= paintColors.Count)         
        {             
            currentColor = 0;         
        }         
        paintParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = paintColors[currentColor];         
        GetComponentInChildren<ParticlesController>().paintColor = paintColors[currentColor];     
        }
}
