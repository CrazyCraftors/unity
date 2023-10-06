using UnityEngine;

public class MarioController : MonoBehaviour
{
    private bool isBigMario = false; // Variable para rastrear si Mario es grande o no.
    public float bigMarioScale = 2f; // Tamaño de Mario cuando es grande.

    private void OnCollisionEnter(Collision objeto)
    {
        if (objeto.gameObject.CompareTag("Hongo")) // Asegúrate de que el hongo tenga el tag "Hongo".
        {
            if (!isBigMario)
            {
                // Aumenta el tamaño de Mario si no es grande.
                transform.localScale *= bigMarioScale;
                isBigMario = true;
            }

            Destroy(objeto.gameObject); // Destruye el hongo después de la colisión.
        }
    }
}