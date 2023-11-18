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
    public GameManager gm;


    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;
    [SerializeField] GameObject InicioC;
    [SerializeField] GameObject canvas_go;

    private int score = 0;
    private int Coins = 0;
    public int Lifes = 3;
    private int currentWorld = 1;
    private int currentLevel = 1;
    public int timeElapsed = 400;

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

        //cuando se le da al esc pausa el juego
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.GameRunning = !GameManager.GameRunning;
            Debug.Log(GameManager.GameRunning);
        }
    }

    public void uiInGame(){
        play.SetActive(false);
        ingame.SetActive(true);
        InicioC.SetActive(false);
        canvas_go.SetActive(false);
        inicio();
    }

    public void IncreaseScore(int points){
        score += points;
    }

    public void IncreaseCoins(){
        Coins++;
    }

    public float DecreaseLifes(){
        Lifes--;
        return(Lifes);
    }

    public void ChangeWorldAndLevel(int newWorld, int newLevel){
        currentWorld = newWorld;
        currentLevel = newLevel;
    }

    public void inicio(){
        InicioC.SetActive(true);
        Player.position=(SpawnPoint);
        StartCoroutine(EsperarYContinuar());
        timeElapsed=400;
    }

    public void GameOver(){
        ingame.SetActive(false);
        canvas_go.SetActive(true);
        Player.position=(SpawnPoint);
        StartCoroutine(pausa(5f));
    }

    IEnumerator EsperarYContinuar(){
        yield return new WaitForSeconds(5f);
        InicioC.SetActive(false);
        GameManager.GameRunning = true;
        GameManager.hasActivated=false;
    }

    IEnumerator pausa(float tiempo){
        yield return new WaitForSeconds(tiempo);
        Lifes=3;
        score=0;
        Coins=0;
        timeElapsed=400;
        timeText.text="0";
        play.SetActive(true);
        canvas_go.SetActive(false); 
        GameManager.GameRunning = false;
    }

    IEnumerator Tiempo(){
        while(true){
            if (GameManager.GameRunning == true){
                timeElapsed --;
            }
            yield return new WaitForSeconds(1f);
        }
    }
    
}