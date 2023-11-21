using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour{
    public AudioController ac;
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            ac.levelMusicSource.Pause();
            ac.PlayWinSound();
            StartCoroutine(reset());
        }
    }

    IEnumerator reset(){
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("SampleScene");
    }
}
