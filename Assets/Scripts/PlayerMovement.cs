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
    public float sensitivity = -1f;
    private Vector3 rotate;

    private int _xModifier = 1, _yModifier = 1;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -1f);
        
        // Init the color of the paint to the first color in the list
        paintParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = paintColors[currentColor];
        GetComponentInChildren<ParticlesController>().paintColor = paintColors[currentColor];
    }

    // Update is called once per frame
    void Update()
    {
        RotateThePlayer();
        if(!canMove) return;
        MoveThePlayer();
        if(Input.GetMouseButtonDown(0))
        {
            StartShooting();
        }
        if(Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }
        if(Input.GetMouseButtonDown(1))
        {
            ChangeColor();
        }
    }

    public void ChangeControlDependingOnGravity()
    {
        if(Physics.gravity.y < 0)
        {
            _xModifier = 1;
            _yModifier = 1;
        }
        else if(Physics.gravity.y > 0)
        {
            _xModifier = -1;
        }
        
    }
    void MoveThePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal") * _xModifier;
        float z = Input.GetAxisRaw("Vertical") * _yModifier;

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
    void StartShooting()
    {
        paintParticles.Play();
    }
    
    void StopShooting()
    {
        paintParticles.Stop();
    }
    
    void ChangeColor()
    {
        currentColor++;
        if(currentColor >= paintColors.Count)
        {
            currentColor = 0;
        }
        paintParticles.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = paintColors[currentColor];
        GetComponentInChildren<ParticlesController>().paintColor = paintColors[currentColor];
    }
}
