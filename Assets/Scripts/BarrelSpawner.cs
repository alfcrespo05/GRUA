using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    public int barrelCount = 8;
    public float platformSize = 8f;
    public Material barrelMaterial;

    void Start()
    {
        SpawnBarrels();
    }

    void SpawnBarrels()
    {
        for (int i = 0; i < barrelCount; i++)
        {
            GameObject barrel = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            barrel.name = "Barrel";
            barrel.transform.localScale = new Vector3(2f, 2f, 2f);

            // Random position near edges
            // Spawn near edges only
            int side = Random.Range(0, 4);
            float randomX, randomZ;
            float edgeOffset = Random.Range(6f, platformSize);

            if (side == 0) { randomX = edgeOffset; randomZ = Random.Range(-platformSize, platformSize); }
            else if (side == 1) { randomX = -edgeOffset; randomZ = Random.Range(-platformSize, platformSize); }
            else if (side == 2) { randomX = Random.Range(-platformSize, platformSize); randomZ = edgeOffset; }
            else { randomX = Random.Range(-platformSize, platformSize); randomZ = -edgeOffset; }
            barrel.transform.position = new Vector3(randomX, 0.6f, randomZ);
            barrel.transform.rotation = Quaternion.Euler(Random.Range(0, 30f), Random.Range(0, 360f), Random.Range(0, 30f));
           
            barrel.transform.SetParent(transform);

            if (barrelMaterial != null)
                barrel.GetComponent<Renderer>().material = barrelMaterial;


        }
    }
}
