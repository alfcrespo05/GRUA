using UnityEngine;

public class LauncherPad : MonoBehaviour
{
    public float launchForce = 20f;
    public ParticleSystem explosionEffect;
    public ParticleSystem directionalEffect;

    public void SetDirection(Vector3 newDirection)
    {
        transform.forward = newDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Caja"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = transform.forward * launchForce;

                if (explosionEffect != null)
                    explosionEffect.Play();

                if (directionalEffect != null)
                {
                    directionalEffect.transform.forward = transform.forward;
                    directionalEffect.Play();
                }
            }
        }
    }
}
