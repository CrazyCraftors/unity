using UnityEngine;
using System.Collections;

public class Enemies : MonoBehaviour{
    public float speed = 5.0f;
    public float moveSpeed = 4.0f;
    public float RaycastLength_O = 0.7f;
    public float RaycastLength_P = 0.6f;
    public UIManager ui;
    public GameManager gm;
    public AudioController ac;
    public MarioController mc;

    bool invulnerability;

    private Rigidbody rb;
    private bool movingRight = false;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if (!GameManager.GameRunning)
            return;

        PlayerCollisions();
        ObjectCollisions();
        Movement();

        if (transform.position.y < -30){
            Destroy(gameObject);
        }
    }

    void Movement(){
        float movimientoHorizontal = movingRight ? 1 : -1;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0, 0);
        rb.velocity = new Vector3(movimiento.x * speed, rb.velocity.y, rb.velocity.z);
    }

    void ChangeDirection(){
        movingRight = !movingRight;
    }

    void PlayerCollisions(){
        if ((RaycastHitObject(Vector3.right , RaycastLength_P, out RaycastHit hitSides) && hitSides.collider.CompareTag("Player"))||
            (RaycastHitObject(Vector3.left , RaycastLength_P, out hitSides) && hitSides.collider.CompareTag("Player"))){
            if(mc.isBigMario==true){
                invulnerability = true;
                ac.PlayBigMarioHitSound();
                mc.newScale.y = 2;
                mc.transform.localScale = mc.newScale;
                mc.isBigMario = false;
                StartCoroutine(pausa());
            }else if(invulnerability==false){
                gm.Respawn();
                Debug.Log("Kill Player");
            }
        }

        if (RaycastHitObject(Vector3.up, RaycastLength_P, out RaycastHit hitUp) && hitUp.collider.CompareTag("Player")){
            ac.PlayHitBlockSound();
            Destroy(gameObject);
            ui.IncreaseScore(100);
        }
    }

    void ObjectCollisions(){
        if (RaycastHitObject(Vector3.right * (movingRight ? 1 : -1), RaycastLength_O, out RaycastHit hitObstacle)){
            if (!hitObstacle.collider.CompareTag("Trigger")) {
                ChangeDirection();
            }
        }
    }

    bool RaycastHitObject(Vector3 direction, float length, out RaycastHit hit){
        return Physics.Raycast(transform.position, direction, out hit, length);
    }

    IEnumerator pausa(){
        yield return new WaitForSeconds(3f);
        invulnerability = false;
    }
}