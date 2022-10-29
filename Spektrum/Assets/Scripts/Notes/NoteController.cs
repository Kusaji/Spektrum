using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime); 
    }

    private void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
