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
    public float sensitivity = -1f;
    private Vector3 rotate;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bulletReloadTimer = bulletReloadTime;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -1f);
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
