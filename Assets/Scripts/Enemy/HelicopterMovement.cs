using UnityEngine;

public class HelicopterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private ParticleSystem explosionEffect;
    public GameObject explosion;
    public int point = 5;
    public AudioSource blast;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0f); 
        explosionEffect = GetComponent<ParticleSystem>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().IncreaseScore(point);
            Destroy(gameObject);
            ExplodeHelicopter(transform.position);
        }
    }

    void ExplodeHelicopter(Vector3 position)
    {
        Instantiate(explosion,position,Quaternion.identity);
        
    }


}
