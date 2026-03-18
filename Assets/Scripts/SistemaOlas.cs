using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public float waveHeight = 0.5f;
    public float waveSpeed = 1f;
    public float waveFrequency = 0.5f;

    private MeshFilter meshFilter;
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;

    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        mesh = meshFilter.mesh;
        originalVertices = mesh.vertices;
        modifiedVertices = new Vector3[originalVertices.Length];
    }

    void Update()
    {
        for (int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 vertex = originalVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveSpeed + originalVertices[i].x * waveFrequency)
                      * Mathf.Sin(Time.time * waveSpeed + originalVertices[i].z * waveFrequency)
                      * waveHeight;
            modifiedVertices[i] = vertex;
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}
