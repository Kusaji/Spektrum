using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointAudioController : MonoBehaviour
{
    public AudioSource speaker;
    public AudioClip hitSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitSound()
    {
        speaker.PlayOneShot(hitSound, 0.2f);
    }

    private void OnTriggerEnter(Collider other)
    {
/*        if (other.gameObject.CompareTag("Note"))
        {
            PlayHitSound();
        }*/
    }
}
