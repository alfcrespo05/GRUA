using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public float windStrength = 1f;
    public float windSpeed = 0.5f;

    private Rigidbody[] ropeSegments;

    private SoundManager soundManager;

    private float multiplier;

    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.PlayWind(true);
    }
    void Update()
    {
        //Encuentra los segmentos de la cuerda
        ropeSegments = FindObjectsOfType<Rigidbody>();

        //La brisa va y viene
        float breeze = Mathf.Max(0, Mathf.Sin(Time.time * 0.3f));

        //La direccion de la brisa cambia
        float windX = Mathf.Sin(Time.time * windSpeed) * windStrength * breeze * multiplier;
        float windZ = Mathf.Sin(Time.time * windSpeed * 0.7f) * windStrength * breeze * multiplier;


        Vector3 wind = new Vector3(windX, 0, windZ);

        // Apply wind to every rigidbody
        foreach (Rigidbody rb in ropeSegments)
        {
            // Heavier objects get pushed less
            Vector3 adjustedWind = wind / rb.mass;
            rb.AddForce(adjustedWind);
        }
    }
    public void SetMultiplier(float value)
    {
        multiplier = value;
    }
}
