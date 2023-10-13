using UnityEngine;
using System.Collections;

public class LuckyBoxController : MonoBehaviour
{
    public GameObject ItemPrefab;
    public GameObject moneda;
    private GameObject generatedItem;
    public float moveSpeed = 1.0f;
    private bool hasActivated = false;
    public Material changedMaterial;
    public MarioController marioController;
    public UIManager ui;

    public void ItemLogic(){
        if (!hasActivated){
            if(ItemPrefab.CompareTag("Hongo")){
                if(marioController.isBigMario == true){
                    GenerateItem(moneda);
                }else{
                    GenerateItem(ItemPrefab);
                }
            }else{
                    GenerateItem(ItemPrefab);
            }
            
        }
    }
    public void GenerateItem(GameObject Item){
        if (ItemPrefab != null){
            generatedItem = Instantiate(Item, transform.position, Quaternion.identity);
            Vector3 targetPosition = transform.position + Vector3.up * moveSpeed;
            StartCoroutine(MoveObjectUp(generatedItem, targetPosition, moveSpeed));
        }
        ChangeLuckyBoxTexture();
        hasActivated = true;
        ui.IncreaseScore(200);
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
        if(ItemPrefab.CompareTag("Moneda")){
            yield return new WaitForSeconds(0.5f);
            Destroy(generatedItem);
            ui.IncreaseCoins();
        }
    }

    private void ChangeLuckyBoxTexture(){
        Renderer boxRenderer = GetComponent<Renderer>();
        if (boxRenderer != null && changedMaterial != null){
            boxRenderer.material = changedMaterial;
        }
    }
}
