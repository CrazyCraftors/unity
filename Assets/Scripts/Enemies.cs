using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 5.0f;
    public float moveSpeed = 4.0f;
    private Rigidbody rb;
    private bool movingRight = false;
    public float RaycastLength_O = 0.7f;
    public float RaycastLength_P = 0.6f;
    public UIManager ui;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.GameRunning == false)return;
        PlayerCollisions();
        ObjectCollisions();
        Movement();

        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }

    void Movement(){
        float movimientoHorizontal = movingRight ? 1 : -1;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, 0);
        rb.velocity = new Vector3(movimiento.x * speed, rb.velocity.y, rb.velocity.z);
    }

    void ChangeDirection()
    {
        // Cambia la dirección del enemigo
        movingRight = !movingRight;
    }

    void PlayerCollisions()
    {
        // Realiza raycasts hacia la derecha e izquierda para detectar al jugador
        RaycastHit hitSides;
        if (Physics.Raycast(transform.position, Vector3.right * (movingRight ? 1 : -1), out hitSides, RaycastLength_P) && hitSides.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Player");
        }

        // Realiza un raycast hacia arriba para detectar colisiones con el jugador
        RaycastHit hitUp;
        if (Physics.Raycast(transform.position, Vector3.up, out hitUp, RaycastLength_P) && hitUp.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Goomba");
            Destroy(gameObject);
            ui.IncreaseScore(100);
        }
    }

    void ObjectCollisions()
    {
        // Comprueba colisiones con cualquier objeto para cambiar de dirección
        RaycastHit hitObstacle;
        if (Physics.Raycast(transform.position, Vector3.right * (movingRight ? 1 : -1), out hitObstacle, RaycastLength_O))
        {
            ChangeDirection();
        }
    }
}
