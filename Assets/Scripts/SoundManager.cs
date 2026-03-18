using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip motorSound;
    public AudioClip ropeCreakSound;
    public AudioClip windSound;
    public AudioClip deliverySound;
    public AudioClip seaSound;
    public AudioClip seagullSound;

    [Header("Audio Sources")]
    public AudioSource motorSource;
    public AudioSource ropeSource;
    public AudioSource windSource;
    public AudioSource seaSource;
    public AudioSource seagullSource;
    public AudioSource deliverySource;

    public float minSeagullInterval = 10f;
    public float maxSeagullInterval = 60f;
    private float seagullTimer = 0f;
    void Start()
    {
      
    }
    public void Update()
    {
        seagullTimer -= Time.deltaTime;
        if (seagullTimer <= 0f)
        {
            seagullSource.Play();
            seagullTimer = Random.Range(minSeagullInterval, maxSeagullInterval);
        }
    }
    void Awake()
    {
        motorSource = CreateSource(motorSound, true, 0.04f);
        ropeSource = CreateSource(ropeCreakSound, true, 0.3f);
        windSource = CreateSource(windSound, true, 0.4f);
        deliverySource = CreateSource(deliverySound, false, 1f);
        seaSource = CreateSource(seaSound, true, 0.4f);
        seaSource.Play();
        seagullSource = CreateSource(seagullSound, false, 0.3f);
        seagullTimer = Random.Range(minSeagullInterval, maxSeagullInterval);
    }

    AudioSource CreateSource(AudioClip clip, bool loop, float volume)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.playOnAwake = false;
        return source;
    }

    public void PlayMotor(bool playing)
    {
        if (playing && !motorSource.isPlaying)
            motorSource.Play();
        else if (!playing)
            motorSource.Stop();
    }

    public void PlayRope(bool playing)
    {
        if (playing && !ropeSource.isPlaying)
            ropeSource.Play();
        else if (!playing)
            ropeSource.Stop();
    }

    public void PlayWind(bool playing)
    {
        if (playing && !windSource.isPlaying)
            windSource.Play();
        else if (!playing)
            windSource.Stop();
    }

    public void PlayDelivery()
    {
        deliverySource.PlayOneShot(deliverySound);
    }
}
