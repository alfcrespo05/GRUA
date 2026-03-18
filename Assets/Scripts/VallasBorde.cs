using UnityEngine;

public class PlatformRailings : MonoBehaviour
{
    public float platformSize = 10f;
    public float railingHeight = 1f;
    public float postSpacing = 2f;
    public Material railingMaterial;

    public Color lightColor = Color.yellow;
    public float lightIntensity = 5f;
    public float lightRange = 20f;
    void Start()
    {
        GenerateRailings();
    }

    void GenerateRailings()
    {
        // Four sides
        for (float x = -platformSize; x <= platformSize; x += postSpacing)
        {
            CreatePost(new Vector3(x, railingHeight / 2, platformSize), false);
            CreatePost(new Vector3(x, railingHeight / 2, -platformSize), false);
        }
        for (float z = -platformSize; z <= platformSize; z += postSpacing)
        {
            CreatePost(new Vector3(platformSize, railingHeight / 2, z), false);
            CreatePost(new Vector3(-platformSize, railingHeight / 2, z), false);
        }

        // Corner posts with lights
        CreatePost(new Vector3(platformSize, railingHeight / 2, platformSize), true);
        CreatePost(new Vector3(-platformSize, railingHeight / 2, platformSize), true);
        CreatePost(new Vector3(platformSize, railingHeight / 2, -platformSize), true);
        CreatePost(new Vector3(-platformSize, railingHeight / 2, -platformSize), true);

        // Horizontal rails
        CreateRail(new Vector3(0, railingHeight, platformSize), new Vector3(platformSize * 2, 0.1f, 0.1f));
        CreateRail(new Vector3(0, railingHeight, -platformSize), new Vector3(platformSize * 2, 0.1f, 0.1f));
        CreateRail(new Vector3(platformSize, railingHeight, 0), new Vector3(0.1f, 0.1f, platformSize * 2));
        CreateRail(new Vector3(-platformSize, railingHeight, 0), new Vector3(0.1f, 0.1f, platformSize * 2));
    }

    void CreatePost(Vector3 position, bool hasLight)
    {
        GameObject post = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        post.name = "RailingPost";
        post.transform.position = position;
        post.transform.localScale = new Vector3(0.1f, railingHeight / 2, 0.1f);
        post.transform.SetParent(transform);
        if (railingMaterial != null)
            post.GetComponent<Renderer>().material = railingMaterial;

        if (hasLight)
        {
            GameObject lightObj = new GameObject("RailingLight");
            lightObj.transform.SetParent(post.transform);
            lightObj.transform.localPosition = new Vector3(0, 1f, 0);

            Light light = lightObj.AddComponent<Light>();
            light.color = lightColor;
            light.intensity = lightIntensity;
            light.range = lightRange;
            light.type = LightType.Point;
        }
    }

    void CreateRail(Vector3 position, Vector3 scale)
    {
        GameObject rail = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rail.name = "Rail";
        rail.transform.position = position;
        rail.transform.localScale = scale;
        rail.transform.SetParent(transform);
        if (railingMaterial != null)
            rail.GetComponent<Renderer>().material = railingMaterial;
    }
}
