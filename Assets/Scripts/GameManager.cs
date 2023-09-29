using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool activado = false;
    public static bool GameState = false;
    [SerializeField] GameObject play;
    [SerializeField] GameObject ingame;

    public TextMeshProUGUI texto;
    private int contador = 0; 

    void Start()
    {
        ingame.SetActive(false);
        texto = GetComponent<TextMeshProUGUI>();
        ActualizarContador();
    }

    void Update()
    {
        
    }

    private void ActualizarContador(){
        contador++;
        string contadorConCeros = contador.ToString("D5");
        texto.text = contadorConCeros;
    }

    public void activarig(){
        play.SetActive(false);
        ingame.SetActive(true);
        GameManager.activado = true;
        GameManager.GameState= true;
    }
}

