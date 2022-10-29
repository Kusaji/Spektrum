using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> notes;
    public List<GameObject> spawnPoints;

    public void SpawnLowNote()
    {
        Instantiate(
            notes[0],
            spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position,
            Quaternion.identity);
    }

    public void SpawnMidNote()
    {
        Instantiate(
            notes[1],
            spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position,
            Quaternion.identity);
    }

    public void SpawnHighNote()
    {
        Instantiate(
            notes[2],
            spawnPoints[Random.Range(0, spawnPoints.Count)].transform.position,
            Quaternion.identity);
    }
}
