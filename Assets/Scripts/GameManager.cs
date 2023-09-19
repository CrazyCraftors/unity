using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool activado = false;
    public static string GameState = "none";
    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;
    void Start()
    {
        ingame.SetActive(false);
    }

    void Update()
    {
        
    }

    public void activarig(){
    play.SetActive(false);
    ingame.SetActive(true);
    GameManager.activado = true;
    GameManager.GameState= "play";
    }
}

