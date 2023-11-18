using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 5.0f;
    public float moveSpeed = 4.0f;
    public float RaycastLength_O = 0.7f;
    public float RaycastLength_P = 0.6f;
    public UIManager ui;

    private Rigidbody rb;
    private bool movingRight = false;

    private const string playerTag = "Player";

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!GameManager.GameRunning)
            return;

        PlayerCollisions();
        ObjectCollisions();
        Movement();

        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        float movimientoHorizontal = movingRight ? 1 : -1;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, 0);
        rb.velocity = new Vector3(movimiento.x * speed, rb.velocity.y, rb.velocity.z);
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;
    }

    void PlayerCollisions()
    {
        if (RaycastHitObject(Vector3.right * (movingRight ? 1 : -1), RaycastLength_P, out RaycastHit hitSides) && hitSides.collider.CompareTag(playerTag))
        {
            Debug.Log("Kill Player");
        }

        if (RaycastHitObject(Vector3.up, RaycastLength_P, out RaycastHit hitUp) && hitUp.collider.CompareTag(playerTag))
        {
            Debug.Log("Kill Goomba");
            Destroy(gameObject);
            ui.IncreaseScore(100);
        }
    }

    void ObjectCollisions()
    {
        if (RaycastHitObject(Vector3.right * (movingRight ? 1 : -1), RaycastLength_O, out RaycastHit hitObstacle))
        {
            ChangeDirection();
        }
    }

    bool RaycastHitObject(Vector3 direction, float length, out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, direction, out hit, length);
    }
}
