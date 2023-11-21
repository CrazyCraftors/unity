using UnityEngine;

public class ActivadorObjeto : MonoBehaviour{
    public GameObject[] objetosAActivar;
    
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            foreach (var objeto in objetosAActivar){
                if (objeto != null){
                    Enemies enemiesScript = objeto.GetComponent<Enemies>();
                    if (enemiesScript != null){
                        enemiesScript.enabled = true;
                    }
                }
            }
            
        }
    }
}