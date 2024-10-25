using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour
{

    AudioSource audioSource;

    float soundTime;
    float nextPlayTime = 0.1f;

    public float maxVolume = 1.0f; 
    public float speedThreshold = 10f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        soundTime += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (soundTime > nextPlayTime)
        {
            soundTime = 0f;
            float impactSpeed = collision.relativeVelocity.magnitude;

            // Normalize impact speed to a volume level (0 to 1)
            float volume = Mathf.Clamp01(impactSpeed / speedThreshold) * maxVolume;

            PlaySoundWithVariance(volume);
        }
    }
       


    void PlaySoundWithVariance(float volume)
    {
        audioSource.pitch = Random.Range(0.87f, 1.12f);
        audioSource.volume = Random.Range(volume * 0.87f, volume * 1.11f);
        audioSource.Play();
    }
}
