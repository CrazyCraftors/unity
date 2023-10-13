using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    private bool movingRight = true;
    private RaycastHit hitInfo;
    public float RaycastLenght = 0.6f;

    void Update()
    {
        // Moverse en la dirección actual
        Movement();

        // Realizar un raycast para detectar obstáculos
        ObjectCollisions();

        // Verificar colisiones con el jugador
        PlayerCollisions();
    }

    void Movement(){
        if (movingRight)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    void PlayerCollisions(){
        if (Physics.Raycast(transform.position, -transform.up, out hitInfo, RaycastLenght))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                Vector3 hitPoint = hitInfo.point;
                Vector3 goombaPosition = transform.position;

                if (hitPoint.x < goombaPosition.x)
                {
                    Debug.Log("Kill Player"); // Colisión por la izquierda
                }
                else if (hitPoint.x > goombaPosition.x)
                {
                    Debug.Log("Kill Player"); // Colisión por la derecha
                }
                else
                {
                    Debug.Log("Kill Goomba"); // Colisión desde arriba
                }
            }
        }
    }

    void ObjectCollisions(){
        if (Physics.Raycast(transform.position, transform.right, out hitInfo, RaycastLenght))
        {
            if (hitInfo.collider != null)
            {
                // Cambiar de dirección al colisionar con un obstáculo
                ChangeDirection();
            }
        }
    }
}
