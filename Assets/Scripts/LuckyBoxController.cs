using UnityEngine;
using System.Collections;

public class LuckyBoxController : MonoBehaviour
{
    public GameObject item;
    private int scoreValue = 50;
    public float moveSpeed = 1.0f;
    private bool hasActivated = false;
    public Material changedMaterial;
    public MarioController marioController;
    public UIManager ui;

    public void GenerateItem()
    {
        if(marioController.isBigMario == true){
            if (!hasActivated){
                if (item != null){
                    GameObject generatedItem = Instantiate(item, transform.position, Quaternion.identity);
                    
                    Vector3 targetPosition = transform.position + Vector3.up * moveSpeed;
                    
                    StartCoroutine(MoveObjectUp(generatedItem, targetPosition, moveSpeed));
                }
                ChangeLuckyBoxTexture();
                hasActivated = true;
            }
        }else{
            if (!hasActivated){
            ui.IncreaseScore(scoreValue);
            ChangeLuckyBoxTexture();
            hasActivated = true;
            }
        }
    }

    private IEnumerator MoveObjectUp(GameObject obj, Vector3 targetPosition, float speed){
        float journeyLength = Vector3.Distance(obj.transform.position, targetPosition);
        float startTime = Time.time;
        while (obj.transform.position != targetPosition){
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, fractionOfJourney);
            yield return null;
        }
    }

    private void ChangeLuckyBoxTexture(){
        Renderer boxRenderer = GetComponent<Renderer>();
        if (boxRenderer != null && changedMaterial != null){
            boxRenderer.material = changedMaterial;
        }
    }
}
