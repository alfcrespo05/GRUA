using UnityEngine;

public class LevitateEffect : MonoBehaviour
{
    public int ringCount = 3;
    public float maxRadius = 2f;
    public float minRadius = 0.1f;
    public float speed = 1.5f;
    public float effectHeight = 2f;
    public Material ringMaterial;

    private GameObject[] rings;
    private float[] ringOffsets;

    void Start()
    {
        rings = new GameObject[ringCount];
        ringOffsets = new float[ringCount];

        for (int i = 0; i < ringCount; i++)
        {
            rings[i] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            rings[i].name = "Ring_" + i;
            rings[i].transform.SetParent(transform);
            Destroy(rings[i].GetComponent<Collider>());

            if (ringMaterial != null)
                rings[i].GetComponent<Renderer>().material = ringMaterial;

            // Offset each ring evenly in time
            ringOffsets[i] = (float)i / ringCount;
        }
    }

    void Update()
    {
        for (int i = 0; i < ringCount; i++)
        {
            // Progress 0 to 1 looping
            float progress = Mathf.Repeat(Time.time * speed + ringOffsets[i], 1f);

            // Start at top, move downward
            float height = Mathf.Lerp(effectHeight, 0f, progress);
            rings[i].transform.localPosition = new Vector3(0, height, 0);

            // Shrink as they go down
            float radius = Mathf.Lerp(maxRadius, minRadius, progress);
            rings[i].transform.localScale = new Vector3(radius, 0.02f, radius);

            // Fade out near the bottom
            Renderer rend = rings[i].GetComponent<Renderer>();
            Color c = rend.material.color;
            c.a = 1f - progress;
            rend.material.color = c;
        }
    }
}
