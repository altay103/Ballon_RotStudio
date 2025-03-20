using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SpawnManager instance;

    [SerializeField]
    GameObject ballon;
    [SerializeField]
    float spawnPerSecond;
    [SerializeField]
    float minSpeed, maxSpeed;
    [SerializeField]
    public List<BallonData> ballonData;

    private float screenEdgeMargin = -0.8f; // Negative value extends beyond visible screen

    void Start()
    {
        instance = this;
        StartCoroutine(SpawnBallon());
    }

    IEnumerator SpawnBallon()
    {
        GameManager.State LastState = GameManager.State.Menu;
        while (true)
        {
            while (GameManager.instance == null)
            {
                yield return new WaitForSeconds(1);
            }

            float totalWeight = 0;
            foreach (var item in ballonData)
            {
                totalWeight += item.spawnRate;
            }
            float random = Random.Range(0, totalWeight);

            foreach (var item in ballonData)
            {
                random -= item.spawnRate;
                if (random <= 0)
                {
                    // Get camera reference
                    Camera mainCamera = Camera.main;

                    // With negative margin, viewport coordinates will be outside 0-1 range
                    // extending the spawn area beyond visible screen
                    Vector3 leftEdgeWorld = mainCamera.ViewportToWorldPoint(new Vector3(screenEdgeMargin, 0.5f, 10));
                    Vector3 rightEdgeWorld = mainCamera.ViewportToWorldPoint(new Vector3(1 - screenEdgeMargin, 0.5f, 10));

                    // Generate random X position within these extended boundaries
                    float randomX = Random.Range(leftEdgeWorld.x, rightEdgeWorld.x);
                    Vector3 spawnPosition = new Vector3(randomX, 0, 0);

                    GameObject newBallon = Instantiate(ballon, spawnPosition, Quaternion.identity);
                    BallonController ballonController = newBallon.GetComponent<BallonController>();
                    ballonController.SetBallonData(item);
                    ballonController.speed = Random.Range(minSpeed, maxSpeed);

                    if (GameManager.instance.gameState != GameManager.State.Game)
                    {
                        Destroy(newBallon, 20);
                        Collider[] colliders = newBallon.GetComponents<Collider>();

                        foreach (Collider collider in colliders)
                        {
                            collider.enabled = false;
                        }
                    }
                    break;
                }
            }

            if (GameManager.instance.gameState != LastState)
            {
                if (GameManager.instance.gameState == GameManager.State.Game)
                {
                    foreach (GameObject item in GameObject.FindGameObjectsWithTag("Ballon"))
                    {
                        Destroy(item);
                    }
                }

                LastState = GameManager.instance.gameState;

            }
            yield return new WaitForSeconds(1 / spawnPerSecond);
        }

    }
}
