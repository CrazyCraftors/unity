using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool GameRunning = false;
    public static bool hasActivated = false;
    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;
    [SerializeField] GameObject Inicio;
    [SerializeField] GameObject gameoverc;
    public Transform Player;
    public UIManager ui;

    float respawnCoordinateY = -30.0f;

    void Start(){
        ingame.SetActive(false);
        Inicio.SetActive(false);
        gameoverc.SetActive(false);
    }

    void Update(){
        if (Player.position.y < respawnCoordinateY){
            Respawn();
        } 
    }

    public void Respawn(){
        GameManager.GameRunning = false;
        StartCoroutine(EsperarYContinuar());
        float deaths = ui.DecreaseLifes();
        if(deaths <= -1){
            ui.GameOver();
        }else{
            ui.inicio();
        }
        Debug.Log(deaths);
    }

    IEnumerator EsperarYContinuar(){
        yield return new WaitForSeconds(4f);
    }
}