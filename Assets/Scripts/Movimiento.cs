using UnityEngine;

public class Movimiento : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool hasJumped = false;
    private bool hitCube = false;
    
    public float playerSpeed = 6.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;
    public float extraFallForce = 10.0f;

    private void Start(){
        controller = this.GetComponent<CharacterController>();
    }
    void Update(){
        if (GameManager.GameRunning == false)return;
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer){
            hasJumped = false;
            hitCube = false;
            if (playerVelocity.y < 0){
                playerVelocity.y = 0f;
            }
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero){
            gameObject.transform.forward = move;
        }
        if (Input.GetButtonDown("Jump") && (!hasJumped || groundedPlayer)){
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            hasJumped = true;
        }
        playerVelocity.y += gravity * Time.deltaTime;
        if (hitCube){
            playerVelocity.y -= extraFallForce * Time.deltaTime;
        }
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit){
        if (!groundedPlayer && (hit.gameObject.CompareTag("Ladrillo") || hit.gameObject.CompareTag("LuckyBox"))){
            hitCube = true;
        }
    }
}