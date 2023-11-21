using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI worldText;
    public TextMeshProUGUI worldTextInicio;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI LifesText;

    public Transform Player;
    public GameManager gm;
    public AudioController ac;

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

    public Vector3 SpawnPoint = new Vector3(-238.8f, 1.08f, 0f);

    public MarioController mc;
    float coordenadax;

    void Start(){
        StartCoroutine(Tiempo());
    }

    void Update(){
        UpdateUI();
        HandleInput();
        updatechecktpoints();
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
            if(GameManager.GameRunning==true){
                ac.levelMusicSource.UnPause();
                ac.PlayUnpauseSound();
            }else{
                ac.levelMusicSource.Pause();
                ac.PlayPauseSound();
            }
        }
    }

    public void uiInGame(){
        play.SetActive(false);
        ingame.SetActive(true);
        InicioC.SetActive(false);
        canvas_go.SetActive(false);
        ac.menuMusicSource.Pause();
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
        ac.levelMusicSource.time = 0f;
        ac.levelMusicSource.Play();
        timeElapsed = 400;
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
        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator Tiempo(){
        while (true){
            if (GameManager.GameRunning){
                timeElapsed--;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void updatechecktpoints(){
        if(mc.coordenadaX==-195.22f){
            Debug.Log("aa");
            //float[] spawnpoints = { -238.8f, -195.22f, -175.07f, -139.43f, -113.19f, -98.98f, -84.69f, -73.18f, -23.32f };
        }
    }
}