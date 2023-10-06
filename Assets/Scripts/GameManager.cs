using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool activado = false;
    public static bool GameState = false;
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
        GameManager.GameState= true;
    }
}

