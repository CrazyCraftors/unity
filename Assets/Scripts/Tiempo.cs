using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tiempo : MonoBehaviour
{
    public TextMeshProUGUI texto;
    private int contador = 0;   

    void Update()
    {
        if(GameManager.GameState != "play") return;
        if (contador==0){
            Start();
        }
    }

    void Start(){
        InvokeRepeating("actualizarcont",1f,1f);
    }

    void actualizarcont()
    {
        texto.text = contador.ToString();
        contador++;
    }
}
