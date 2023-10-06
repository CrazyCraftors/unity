using UnityEngine;
using System.Collections.Generic;

public class MarioController : MonoBehaviour
{
    private bool isBigMario = false;
    private Dictionary<GameObject, int> collisionsWithCubes = new Dictionary<GameObject, int>();
    public float bigMarioScaleY = 2f;
    public UIManager ui;
    public Transform raycastOrigin; // Debe ser una Transform en la parte superior del jugador.

    private void Update()
    {
        // Lanza un Raycast hacia abajo desde el punto superior del jugador.
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, Vector3.down, out hit, 0.2f)) // Ajusta la longitud del Raycast según tu jugador.
        {
            if (hit.collider.CompareTag("Ladrillo"))
            {
                if (!isBigMario)
                {
                    if (!collisionsWithCubes.ContainsKey(hit.collider.gameObject))
                    {
                        collisionsWithCubes[hit.collider.gameObject] = 0;
                    }

                    if (collisionsWithCubes[hit.collider.gameObject] < 3)
                    {
                        int randomScore = Random.Range(10, 51);
                        ui.IncreaseScore(randomScore);
                        collisionsWithCubes[hit.collider.gameObject]++;
                    }
                }
                else
                {
                    ui.IncreaseScore(50);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Hongo"))
        {
            if (!isBigMario)
            {
                Vector3 newScale = transform.localScale;
                newScale.y *= bigMarioScaleY;
                transform.localScale = newScale;
                isBigMario = true;
            }

            Destroy(hit.gameObject);
            ui.IncreaseScore(60);
        }
    }
}
