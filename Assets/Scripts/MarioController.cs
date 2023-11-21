using UnityEngine;
using System.Collections.Generic;

public class MarioController : MonoBehaviour{
    public bool isBigMario = false;
    private Dictionary<GameObject, int> collisionsWithCubes = new Dictionary<GameObject, int>();
    private Dictionary<GameObject, float> lastTimeScoredWithCube = new Dictionary<GameObject, float>();
    public float bigMarioScaleY = 1.25f;
    public float timeBetweenScoring = 0.5f;
    public float raycastLength = 1f;
    public int maxScorePerCube = 3;
    public Material newMaterial;
    public UIManager ui;
    public AudioController ac;

    public Vector3 newScale;
    public float coordenadaX;
    Transform objetoTransform;

    void Start(){
        objetoTransform = GetComponent<Transform>();
    }

    private void Update(){
        Collisions();
        coordenadaX = objetoTransform.position.x;
    }

    public void IncreaseSize(GameObject HongoD){
        if (!isBigMario){
            newScale = transform.localScale;
            newScale.y *= bigMarioScaleY;
            transform.localScale = newScale;
            isBigMario = true;
        }
        Destroy(HongoD);
        ui.IncreaseScore(1000);
    }

    private void Collisions(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, raycastLength)){
            if (hit.collider.CompareTag("Ladrillo")){
                HandleLadrilloCollision(hit);
            }
            else if (hit.collider.CompareTag("LuckyBox")){
                HandleLuckyBoxCollision(hit);
            }
        }
    }

    private void HandleLadrilloCollision(RaycastHit hit){
        if (!isBigMario){
            HandleSmallMarioCollision(hit);
        }
        else{
            HandleBigMarioCollision(hit);
        }
    }

    private void HandleSmallMarioCollision(RaycastHit hit){
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
                ac.PlayHitBlockSound();
                ui.IncreaseScore(10);
                collisionsWithCubes[hit.collider.gameObject]++;
                lastTimeScoredWithCube[hit.collider.gameObject] = Time.time;
            }
        }
    }

    private void HandleBigMarioCollision(RaycastHit hit){
        ui.IncreaseScore(50);
        ac.PlayBreakBlockSound();
        Destroy(hit.collider.gameObject);
    }

    private void HandleLuckyBoxCollision(RaycastHit hit){
        LuckyBoxController luckyBox = hit.collider.GetComponent<LuckyBoxController>();
        if (luckyBox != null){
            luckyBox.ItemLogic();
        }
    }
}