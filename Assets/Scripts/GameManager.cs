using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static bool GameRunning = false;
    public static bool hasActivated = false;
    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;
    [SerializeField] GameObject Inicio;
    public Transform Player;
    public UIManager ui;

    void Start(){
        ingame.SetActive(false);
        Inicio.SetActive(false);
    }
    void Update(){
        if (!hasActivated){
            float respawnCoordinateY = -30.0f;
            if (Player.position.y < respawnCoordinateY){
                Respawn();
                hasActivated = true;
            }
        }
    }
    
    public void uiInGame(){
        play.SetActive(false);
        ingame.SetActive(true);
        Inicio.SetActive(false);
        ui.inicio();
    }
    public void Respawn(){
        GameManager.GameRunning = false;
        ui.timeElapsed=0;
        StartCoroutine(EsperarYContinuar());
        ui.DecreaseLifes();
        ui.inicio();
    }

    IEnumerator EsperarYContinuar(){
        yield return new WaitForSeconds(3f);
    }
}