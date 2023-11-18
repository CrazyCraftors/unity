using UnityEngine;

public class Camara : MonoBehaviour{
    public Transform Cube;
    public Vector3 offset;

    void Update(){
        if(Cube.position.y > -2){
            if(Cube != null){ 
                transform.position = Cube.position+offset;
            }
        }
    }
}