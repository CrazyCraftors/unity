using UnityEngine;
using System.Collections;
using TMPro;

public class UIManager : MonoBehaviour{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI worldTextInicio;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI LifesText;

    public Transform Player;
    public GameManager gm;

    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;
    [SerializeField] GameObject InicioC;
    [SerializeField] GameObject canvas_go;

    private int score = 0;
    private int Coins = 0;
    private int Lifes = 3;
    private int currentWorld = 1;
    private int currentLevel = 1;
    private int timeElapsed = 400;

    private Vector3 SpawnPoint = new Vector3(-238.8f, 1.08f, 0f);

    void Start(){
        StartCoroutine(Tiempo());
    }

    void Update(){
        UpdateUI();
        HandleInput();
    }

    void UpdateUI(){
        scoreText.text = score.ToString("D6");
        CoinsText.text = "x" + Coins.ToString("D2");
        LifesText.text = "x     " + Lifes.ToString("D2");
        worldText.text = "WORLD " + currentWorld + " - " + currentLevel;
        worldTextInicio.text = worldText.text;
        timeText.text = Mathf.FloorToInt(timeElapsed).ToString("D6");
    }

    void HandleInput(){
        if (Input.GetKeyDown(KeyCode.Escape)){
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
        return --Lifes;
    }

    public void ChangeWorldAndLevel(int newWorld, int newLevel){
        currentWorld = newWorld;
        currentLevel = newLevel;
    }

    public void inicio(){
        InicioC.SetActive(true);
        Player.position = SpawnPoint;
        StartCoroutine(EsperarYContinuar());
        timeElapsed = 400;
    }

    public void GameOver(){
        ingame.SetActive(false);
        canvas_go.SetActive(true);
        Player.position = SpawnPoint;
        StartCoroutine(reset());
    }

    IEnumerator EsperarYContinuar(){
        yield return new WaitForSeconds(3f);
        InicioC.SetActive(false);
        GameManager.GameRunning = true;
    }

    IEnumerator reset(){
        yield return new WaitForSeconds(5f);
        Lifes = 3;
        score = 0;
        Coins = 0;
        timeElapsed = 400;
        timeText.text = "0";
        play.SetActive(true);
        canvas_go.SetActive(false);
        GameManager.GameRunning = false;
    }

    IEnumerator Tiempo(){
        while (true){
            if (GameManager.GameRunning){
                timeElapsed--;
            }
            yield return new WaitForSeconds(1f);
        }
    }
}