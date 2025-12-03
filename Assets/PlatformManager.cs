using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platformPrefab;

    public int startingPlatforms = 12;
    public float minGapY = 1.5f;
    public float maxGapY = 2.4f;
    public float sideMargin = 0.5f;

    public float bufferAbove = 3f;
    public float destroyBelowOffset = 8f;

    private List<GameObject> platforms = new List<GameObject>();
    private float camHalfHeight;
    private float camHalfWidth;
    private float highestYSpawned;

    // Start is called before the first frame update
    void Start()
    {
        camHalfHeight = Camera.main.orthographicSize;
        camHalfWidth = camHalfHeight * Camera.main.aspect;

        float camY = Camera.main.transform.position.y;
        float startY = camY - camHalfHeight + 0.5f;

        for (int i = 0; i < startingPlatforms; i++) {
            float x = Random.Range(-camHalfWidth + sideMargin, camHalfWidth - sideMargin);
            Vector3 pos = new Vector3(x, startY, 0f);

            GameObject plat = Instantiate(platformPrefab, pos, Quaternion.identity, transform);
            platforms.Add(plat);

            startY += Random.Range(minGapY, maxGapY);
        }

        highestYSpawned = startY;
    }

    // Update is called once per frame
    void Update()
    {
        float camY = Camera.main.transform.position.y;
        float topOfView = camY + camHalfHeight;

        while (highestYSpawned < topOfView + bufferAbove) {
            float x = Random.Range(-camHalfWidth + sideMargin, camHalfWidth - sideMargin);
            float gap = Random.Range(minGapY, maxGapY);
            highestYSpawned += gap;

            Vector3 spawnPos = new Vector3(x, highestYSpawned, 0f);
            GameObject plat = Instantiate(platformPrefab, spawnPos, Quaternion.identity, transform);
            platforms.Add(plat);
        }

        float bottomLimit = camY - camHalfHeight - destroyBelowOffset;

        for (int i = platforms.Count - 1; i >= 0; i--) {
            GameObject p = platforms[i];
            if (p == null) {
                platforms.RemoveAt(i);
                continue;
            }

            if (p.transform.position.y < bottomLimit) {
                Destroy(p);
                platforms.RemoveAt(i);
            }

        }
    }
}
