using UnityEngine;

public class DescendPad : MonoBehaviour
{
    public float targetHeight = 0.79f;
    public ParticleSystem descendEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.name == "Grúa")
        {
            CraneController crane = other.transform.root.GetComponent<CraneController>();
            if (crane != null && !crane.isJumping)
                crane.StartCoroutine(crane.Descend(targetHeight));

            if (descendEffect != null)
                descendEffect.Play();
        }
    }
}
