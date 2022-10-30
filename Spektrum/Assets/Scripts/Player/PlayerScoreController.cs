using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    public int combo;
    public float score;
    private AudioSource speaker;
    public AudioClip hitSound;

    private void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            combo += 1;
            score += 50f;
            PlayerController.Instance.health.HealthHealth();
            speaker.PlayOneShot(hitSound, 0.10f);
            Destroy(other.gameObject);
        }
    }

    public void PlayHitSound()
    {
        speaker.PlayOneShot(hitSound, 0.10f);
    }
}
