using UnityEngine;

public class MachineController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject bulletPrefab;  
    public Transform pivotPoint;     
    public Transform firePoint;      
    public float bulletSpeed = 10f; 
    public float fireRate = 0.5f;   
    public float rotationSpeed = 100f; 
    public float maxRotationAngle = 45f; 
    public float maxLifeTime = 3f;

    


    public int machineHealth = 100;

    private float timeToFire = 0f;  
    private float currentRotationAngle = 0f; 

    void Update()
    {
        float rotationInput = -Input.GetAxis("Horizontal"); 
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;

        currentRotationAngle += rotationAmount;

        currentRotationAngle = Mathf.Clamp(currentRotationAngle, -maxRotationAngle, maxRotationAngle);

        pivotPoint.localRotation = Quaternion.Euler(0f, 0f, currentRotationAngle);

        if (Input.GetMouseButton(0) && Time.time >= timeToFire)
        {
            Shoot();  
            timeToFire = Time.time + 1f / fireRate; 
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Destroy(bullet, maxLifeTime);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;
    }

    public void TakeDamage(int damageAmount)
    {
        machineHealth -= damageAmount;

        if (machineHealth <= 0)
        {
            MachineDestroyed(transform.position);
        }
    }
    public void MachineDestroyed(Vector3 position)
    {
        Instantiate(explosion, position, Quaternion.identity);
        FindObjectOfType<GameManager>().Explode();
        FindObjectOfType<GameManager>().MainMenu();
        Destroy(gameObject);
    }
}

