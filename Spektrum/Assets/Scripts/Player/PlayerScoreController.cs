using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreController : MonoBehaviour
{
    public int combo;
    public float score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            combo += 1;
            score += 50f;
            Destroy(other.gameObject);
        }
    }
}
