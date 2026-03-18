using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    public float windMultiplier = 10f;
    private WindSystem windSystem;

    void Start()
    {
        windSystem = FindObjectOfType<WindSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Grúa" || other.gameObject.tag == "Caja")
        {
            windSystem.SetMultiplier(windMultiplier);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Grúa" || other.gameObject.tag == "Caja")
        {
            windSystem.SetMultiplier(0.7f);
        }
    }
}
