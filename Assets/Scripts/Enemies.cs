using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour{
    public float speed = 5.0f; // Velocidad de movimiento del enemigo

    private Rigidbody rb;
    private bool movingRight = true;
    public float RaycastLength_O = 0.7f;
    public float RaycastLength_P = 0.6f;

    public UIManager ui;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        PlayerCollisions();
        ObjectCollisions();
        Movement();

        if (transform.position.y < -30){
            Destroy(gameObject);
        }
    }

    void Movement(){
        // Mueve al enemigo en la dirección actual
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, 0);

        rb.velocity = movimiento * speed;

        if (movingRight==false)
        {
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
        else if (movingRight==true)
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }
    }

    void ChangeDirection(){
        // Cambia la dirección del enemigo
        movingRight = !movingRight;
    }

    void PlayerCollisions(){
        // Realiza un raycast hacia la derecha para detectar al jugador
        RaycastHit hitSides;
        if (Physics.Raycast(transform.position, Vector3.right, out hitSides, RaycastLength_P) && hitSides.collider.CompareTag("Player")|| 
        Physics.Raycast(transform.position, Vector3.left, out hitSides, RaycastLength_P) && hitSides.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Player");
        }

        // Realiza un raycast hacia arriba para detectar colisiones con el jugador
        RaycastHit hitUp;
        if (Physics.Raycast(transform.position, Vector3.up, out hitUp, RaycastLength_P) && hitUp.collider.CompareTag("Player"))
        {
            Debug.Log("Kill Goomba");
            Destroy(gameObject);
            ui.IncreaseScore(800);
        }
    }

    void ObjectCollisions(){
        // Comprueba colisiones con cualquier objeto para cambiar de dirección
        RaycastHit hitObstacle;
        if (Physics.Raycast(transform.position, Vector3.right, out hitObstacle, RaycastLength_O) || Physics.Raycast(transform.position, Vector3.left, out hitObstacle, RaycastLength_O))
        {
            ChangeDirection();
        }
    }

    IEnumerator Pausa (){
        yield return new WaitForSeconds(1.0f);
    }
}