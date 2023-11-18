using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool GameRunning = false;
    public static bool HasActivated = false;

    public GameObject play;
    public GameObject ingame;
    public GameObject inicio;
    public GameObject gameoverc;
    public Transform player;
    public UIManager ui;

    private float respawnCoordinateY = -30.0f;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(4f);

    void Start()
    {
        ingame.SetActive(false);
        inicio.SetActive(false);
        gameoverc.SetActive(false);
    }

    void Update()
    {
        if (player.position.y < respawnCoordinateY && GameRunning)
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        GameRunning = false;
        StartCoroutine(EsperarYContinuar());
        float deaths = ui.DecreaseLifes();
        if (deaths <= -1)
        {
            ui.GameOver();
        }
        else
        {
            ui.inicio();
        }
        Debug.Log(deaths);
    }

    IEnumerator EsperarYContinuar()
    {
        yield return waitForSeconds;
        GameRunning = true;
    }
}
