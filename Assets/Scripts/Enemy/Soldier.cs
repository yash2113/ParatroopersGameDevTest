using UnityEngine;

public class Soldier : MonoBehaviour
{
    public GameObject footSoldier;
    public GameObject parachuteSoldier;
    public Transform machineCenter;

    public float fallSpeed = 5f;
    public float moveSpeed = 2f;
    public int point = 1;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        FallDown();
    }

    private void Update()
    {
        if (isLanded)
        {
            TurnAndMoveTowardsMachine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            FindObjectOfType<GameManager>().IncreaseScore(point);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Land") && !isLanded)
        {
            isLanded = true;
            rb.velocity = Vector2.zero;
        }
        else if (other.CompareTag("Machine"))
        {
            MachineController machine = other.gameObject.GetComponent<MachineController>();
            if(machine != null)
            {
                machine.TakeDamage(20);
                Destroy(gameObject);
            }


        }
    }

    private void TurnAndMoveTowardsMachine()
    {
        parachuteSoldier.SetActive(false);
        footSoldier.SetActive(true);

        Vector3 machinePosition = machineCenter.position;

        float targetRotation = (machinePosition.x > transform.position.x) ? 180f : 0f;
        Quaternion targetRotationQuaternion = Quaternion.Euler(0f, targetRotation, 0f);

        transform.rotation = targetRotationQuaternion;

        Vector3 moveDirection = new Vector3((machinePosition.x > transform.position.x) ? 1f : -1f, 0f, 0f);

        moveDirection.Normalize();

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void FallDown()
    {
        rb.velocity = new Vector2(0f, -fallSpeed);
    }
}

