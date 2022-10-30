using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerScoreController score;
    public PlayerHealth health;
    public Vector3 mousePosition;
    public Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        score = GetComponent<PlayerScoreController>();
        health = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePosition();
        transform.position = mousePosition;
    }

    public void GetMousePosition()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("HitPoint"))
            {
                mousePosition = hit.point;
                mousePosition.y = 0.0f;
                mousePosition.z = 33.0f;
            }
        }
    }
}
