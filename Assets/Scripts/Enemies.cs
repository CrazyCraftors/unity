using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float speed = 2.0f; // Velocidad de movimiento del enemigo

    private Rigidbody rb;
    private bool movingRight = true;
    public float RaycastLength_O = 0.6f;
    public float RaycastLength_P = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerCollisions();
        ObjectCollisions();
        FixedUpdate();

        if (transform.position.y < -30){
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        // Mueve al enemigo en la dirección actual
        Vector3 moveDirection = movingRight ? Vector3.right : Vector3.left;
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, 0);
    }

    void ChangeDirection()
    {
        // Cambia la dirección del enemigo
        movingRight = !movingRight;
    }

    void PlayerCollisions()
    {
        // Realiza un raycast hacia la derecha para detectar al jugador
        RaycastHit hitRight;
        if (Physics.Raycast(transform.position, Vector3.right, out hitRight, RaycastLength_P) && hitRight.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Player");
        }

        // Realiza un raycast hacia la izquierda para detectar al jugador
        RaycastHit hitLeft;
        if (Physics.Raycast(transform.position, Vector3.left, out hitLeft, RaycastLength_P) && hitLeft.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Player");
        }

        // Realiza un raycast hacia arriba para detectar colisiones con el jugador
        RaycastHit hitUp;
        if (Physics.Raycast(transform.position, Vector3.up, out hitUp, RaycastLength_P) && hitUp.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Goomba");
            Destroy(gameObject);
        }
    }

    void ObjectCollisions()
    {
        // Comprueba colisiones con cualquier objeto para cambiar de dirección
        RaycastHit hitObstacle;
        if (Physics.Raycast(transform.position, Vector3.right, out hitObstacle, RaycastLength_O) || Physics.Raycast(transform.position, Vector3.left, out hitObstacle, RaycastLength_O))
        {
            ChangeDirection();
        }
    }
}
