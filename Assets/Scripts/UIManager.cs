using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI worldTextInicio;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI LifesText;
    Vector3 SpawnPoint = new Vector3(-224.94f, 1.08f, 0f);
    public Transform Player;

    [SerializeField] GameObject InicioC;

    private int score = 0;
    private int Coins = 0;
    private int Lifes = 3;
    private int currentWorld = 1;
    private int currentLevel = 1;
    public float timeElapsed = 0.0f;

    void Start (){
        StartCoroutine (Tiempo());
    }

    private void Update(){
        scoreText.text = score.ToString("D6");
        CoinsText.text = "x" + Coins.ToString("D2");
        LifesText.text = "x     " + Lifes.ToString("D2");
        worldText.text = "WORLD " + currentWorld.ToString() + " - " + currentLevel.ToString();
        worldTextInicio.text = "WORLD " + currentWorld.ToString() + " - " + currentLevel.ToString();
        timeText.text = Mathf.FloorToInt(timeElapsed).ToString("D6");
    }

    public void IncreaseScore(int points){
        score += points;
    }

    public void IncreaseCoins(){
        Coins++;
    }

    public void DecreaseLifes(){
        Lifes--;
    }

    public void ChangeWorldAndLevel(int newWorld, int newLevel){
        currentWorld = newWorld;
        currentLevel = newLevel;
    }

    public void inicio(){
        InicioC.SetActive(true);
        Player.position=(SpawnPoint);
        StartCoroutine(EsperarYContinuar());
    }

    IEnumerator EsperarYContinuar(){
        yield return new WaitForSeconds(5f);
        InicioC.SetActive(false);
        GameManager.GameRunning = true;
        GameManager.hasActivated=false;
    }

    IEnumerator Tiempo(){
        while(true){
            if (GameManager.GameRunning == true){
                timeElapsed ++;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    
}