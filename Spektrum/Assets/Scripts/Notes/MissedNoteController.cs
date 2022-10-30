using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissedNoteController : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            player.score.combo = 0;
            player.health.TakeDamage();
        }
    }
}
