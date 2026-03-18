using UnityEngine;
using System.Collections;
public class BoxSpawner : MonoBehaviour
{
    public int boxCount = 6;
    public float spawnRange = 10f;
    public Material boxMaterial;
    public Transform targetZone;
    void Start()
    {
      
    }

    void SpawnBoxes()
    {
        Debug.Log("Spawning " + boxCount + " boxes");
        for (int i = 0; i < boxCount; i++)
        {
            Debug.Log("Spawning box " + i);
            GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Outline outline = box.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 5f;
            box.name = "Caja";
            box.tag = "Caja";
            float randomSize = Random.Range(0.3f, 1f);
            box.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

            // Random position around the map
            float randomX = targetZone.position.x + Random.Range(-spawnRange, spawnRange);
            float randomZ = targetZone.position.z + Random.Range(-spawnRange, spawnRange);
            box.transform.position = new Vector3(randomX, 0.3f, randomZ);
           

            // Add physics
            Rigidbody rb = box.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.mass = randomSize * 5f;

            // Apply material
            if (boxMaterial != null)
                box.GetComponent<Renderer>().material = boxMaterial;
        }
    }
    public void SpawnSingleBox()
    {
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Outline outline = box.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 5f;
        box.name = "Caja";
        box.tag = "Caja";
        float randomSize = Random.Range(0.3f, 1f);
        box.transform.localScale = new Vector3(randomSize, randomSize, randomSize);

        float randomX = targetZone.position.x + Random.Range(-spawnRange, spawnRange);
        float randomZ = targetZone.position.z + Random.Range(-spawnRange, spawnRange);
        box.transform.position = new Vector3(randomX, 0.3f, randomZ);
        

        Rigidbody rb = box.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.mass = randomSize * 5f;

        if (boxMaterial != null)
            box.GetComponent<Renderer>().material = boxMaterial;
    }
    public void RespawnAll()
    {
        // Clear existing boxes
        GameObject[] existing = GameObject.FindGameObjectsWithTag("Caja");
        foreach (GameObject b in existing)
            Destroy(b);

        // Wait a frame then spawn
        StartCoroutine(SpawnAfterClear());
    }

    IEnumerator SpawnAfterClear()
    {
        yield return null; // wait for destroys to complete
        SpawnBoxes();
    }
}
