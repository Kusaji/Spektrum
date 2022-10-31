using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public float delayTime;

    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        startTime = Time.time;
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        delayTime = Time.time - startTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log(delayTime);
        }
    }
}
