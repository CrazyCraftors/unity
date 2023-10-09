using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MarioController : MonoBehaviour
{
    public bool isBigMario = false;
    private Dictionary<GameObject, int> collisionsWithCubes = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, float> lastTimeScoredWithCube = new Dictionary<GameObject, float>();
    public float bigMarioScaleY = 2f;
    public UIManager ui;
    public float timeBetweenScoring = 0.5f;
    public int maxScorePerCube = 3;
    public float raycastLength = 1f;
    public Material newMaterial;


    private void Update(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, raycastLength)){
            if (hit.collider.CompareTag("Ladrillo")){
                if (!isBigMario){
                    if (!collisionsWithCubes.ContainsKey(hit.collider.gameObject)){
                        collisionsWithCubes[hit.collider.gameObject] = 0;
                    }
                    if (collisionsWithCubes.ContainsKey(hit.collider.gameObject) && collisionsWithCubes[hit.collider.gameObject] >= maxScorePerCube){
                            Renderer cubeRenderer = hit.collider.gameObject.GetComponent<Renderer>();
                            if (cubeRenderer != null)
                            {
                                cubeRenderer.material = newMaterial;
                            }
                        }
                    if (!lastTimeScoredWithCube.ContainsKey(hit.collider.gameObject) || Time.time - lastTimeScoredWithCube[hit.collider.gameObject] >= timeBetweenScoring){
                        if (collisionsWithCubes[hit.collider.gameObject] < maxScorePerCube){
                            int randomScore = Random.Range(10, 50);
                            ui.IncreaseScore(randomScore);
                            collisionsWithCubes[hit.collider.gameObject]++;
                            lastTimeScoredWithCube[hit.collider.gameObject] = Time.time;
                        }
                    }
                }
                else{
                    ui.IncreaseScore(50);
                    Destroy(hit.collider.gameObject);
                }
            }
            if (hit.collider.CompareTag("LuckyBox")){
            LuckyBoxController luckyBox = hit.collider.GetComponent<LuckyBoxController>();
            if (luckyBox != null)
            {
                luckyBox.GenerateItem();
            }
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit){ 
        if (hit.gameObject.CompareTag("Hongo")){
            if (!isBigMario){
                Vector3 newScale = transform.localScale;
                newScale.y *= bigMarioScaleY;
                transform.localScale = newScale;
                isBigMario = true;
            }
            Destroy(hit.gameObject);
            ui.IncreaseScore(60);
        }
    }
}
