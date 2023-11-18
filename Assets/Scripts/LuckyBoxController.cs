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

    public void ItemLogic()
    {
        if (hasActivated) return;

        if (ItemPrefab.CompareTag("Hongo"))
        {
            GenerateItem(marioController.isBigMario ? moneda : ItemPrefab);
        }
        else
        {
            GenerateItem(ItemPrefab);
        }
    }

    public void GenerateItem(GameObject item)
    {
        if (ItemPrefab != null)
        {
            generatedItem = Instantiate(item, transform.position, Quaternion.identity);

            if (item.CompareTag("Hongo"))
            {
                StartCoroutine(MoveObjectUpAndMoveHongo(generatedItem));
            }
            else
            {
                Vector3 targetPosition = transform.position + Vector3.up * moveSpeed;
                StartCoroutine(MoveObjectUp(generatedItem, targetPosition, moveSpeed));
            }
        }

        ChangeLuckyBoxTexture();
        hasActivated = true;
    }

    private IEnumerator MoveObjectUp(GameObject obj, Vector3 targetPosition, float speed)
    {
        float journeyLength = Vector3.Distance(obj.transform.position, targetPosition);
        float startTime = Time.time;

        while (obj.transform.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distanceCovered / journeyLength;
            obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, fractionOfJourney);
            yield return null;
        }

        if (obj.CompareTag("Moneda"))
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(obj);
            ui.IncreaseCoins();
            ui.IncreaseScore(200);
        }
    }

    private IEnumerator MoveObjectUpAndMoveHongo(GameObject obj)
    {
        float hongoMoveSpeed = 4.0f; // Ajusta este multiplicador según tus necesidades

        yield return StartCoroutine(MoveObjectUp(obj, transform.position + Vector3.up * moveSpeed, moveSpeed));
        yield return new WaitForSeconds(1.0f);

        while (obj != null)
        {
            obj.transform.Translate(Vector3.right * hongoMoveSpeed * Time.deltaTime);

            // Comprobar raycasts a cada lado para cambiar la dirección en 3D
            RaycastHit hitLeft, hitRight;
            bool hitLeftObject = Physics.Raycast(obj.transform.position, Vector3.left, out hitLeft, 0.5f);
            bool hitRightObject = Physics.Raycast(obj.transform.position, Vector3.right, out hitRight, 0.5f);

            if ( (hitLeftObject && hitLeft.collider.gameObject.tag != "Player") || (hitRightObject && hitRight.collider.gameObject.tag != "Player"))
            {
                hongoMoveSpeed *= -1;
            }else if( (hitLeftObject && hitLeft.collider.gameObject.tag == "Player") || (hitRightObject && hitRight.collider.gameObject.tag == "Player")){
                marioController.IncreaseSize(obj);
            }

            yield return null;
        }
    }

    private void ChangeLuckyBoxTexture()
    {
        Renderer boxRenderer = GetComponent<Renderer>();
        if (boxRenderer != null && changedMaterial != null)
        {
            boxRenderer.material = changedMaterial;
        }
    }
}
