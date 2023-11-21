using UnityEngine;
using System.Collections;

public class LuckyBoxController : MonoBehaviour{
    public GameObject ItemPrefab;
    public GameObject moneda;
    private GameObject generatedItem;
    public float moveSpeed = 1.0f;
    public float RaycastLength_P = 0.6f;
    private bool hasActivated = false;
    public Material changedMaterial;
    public MarioController marioController;
    public UIManager ui;
    public AudioController ac;

    public void ItemLogic(){
        if (hasActivated) return;

        GameObject itemToGenerate = ItemPrefab.CompareTag("Hongo") ? (marioController.isBigMario ? moneda : ItemPrefab) : ItemPrefab;
        GenerateItem(itemToGenerate);
    }

    public void GenerateItem(GameObject item){
        if (ItemPrefab != null){
            generatedItem = Instantiate(item, transform.position, Quaternion.identity);

            StartCoroutine(item.CompareTag("Hongo")
                ? MoveObjectUpAndMoveHongo(generatedItem)
                : MoveObjectUp(generatedItem, transform.position + Vector3.up * moveSpeed, moveSpeed));
        }

        ChangeLuckyBoxTexture();
        hasActivated = true;
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

        if (obj.CompareTag("Moneda")){
            yield return new WaitForSeconds(0.5f);
            ac.PlayLuckyBoxCoinSound();
            Destroy(obj);
            ui.IncreaseCoins();
            ui.IncreaseScore(200);
        }
    }

    private IEnumerator MoveObjectUpAndMoveHongo(GameObject obj){
        float hongoMoveSpeed = 4.0f;

        yield return MoveObjectUp(obj, transform.position + Vector3.up * moveSpeed, moveSpeed);
        ac.PlayLuckyBoxMushroomSound();
        yield return new WaitForSeconds(1.0f);

        while (obj){
            obj.transform.Translate(Vector3.right * hongoMoveSpeed * Time.deltaTime);

            RaycastHit hitLeft, hitRight;
            bool hitLeftObject = Physics.Raycast(obj.transform.position, Vector3.left, out hitLeft, 0.5f);
            bool hitRightObject = Physics.Raycast(obj.transform.position, Vector3.right, out hitRight, 0.5f);

            if ((hitLeftObject && hitLeft.collider.gameObject.tag != "Player") ||
                (hitRightObject && hitRight.collider.gameObject.tag != "Player")){
                hongoMoveSpeed *= -1;
            }else {
                Vector3[] directions = { Vector3.right, Vector3.left, Vector3.up };
                foreach (Vector3 direction in directions)
                {
                    if (Physics.Raycast(obj.transform.position, direction, out RaycastHit hitSides, RaycastLength_P) && hitSides.collider.CompareTag("Player"))
                    {
                        ac.PlayPowerUpSound();
                        marioController.IncreaseSize(obj);
                        break; // Termina el bucle si se encuentra un jugador en alguna dirección
                    }
                }
                yield return null;
            }
        }
    }

    private void ChangeLuckyBoxTexture(){
        Renderer boxRenderer = GetComponent<Renderer>();
        if (boxRenderer && changedMaterial){
            boxRenderer.material = changedMaterial;
        }
    }
}