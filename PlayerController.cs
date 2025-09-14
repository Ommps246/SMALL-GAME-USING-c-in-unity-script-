using UnityEngine;

public class PlayerController: MonoBehaviour
{
    public float moveSpeed = 5f;        // movement speed
    public float jumpForce = 5f;        // jump force
    public float respawnHeight = -2f;   // if player goes below this Y, respawn

    private Rigidbody rb;
    private Vector3 startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position; // save starting position
    }

    void Update()
    {
        // --- Movement ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 velocity = rb.linearVelocity;
        velocity.x = moveX * moveSpeed;
        velocity.z = moveZ * moveSpeed;
        rb.linearVelocity = velocity;

        // --- Jump ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // --- Respawn if below height ---
        if (transform.position.y < respawnHeight)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero; // stop movement
        transform.position = startPos; // reset position
    }

    // --- Enemy Collision ---
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Respawn();
        }
    }

    // (Optional for trigger colliders)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Respawn();
        }
    }
}