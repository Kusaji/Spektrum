using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public List<GameObject> notes;
    public GameObject lowNoteSpawnpoint;
    public GameObject midNoteSpawnpoint;
    public GameObject highNoteSpawnpoint;


    public void SpawnLowNote()
    {
        Instantiate(
            notes[0],
            lowNoteSpawnpoint.transform.position,
            Quaternion.identity);
    }

    public void SpawnMidNote()
    {
        Instantiate(
            notes[1],
            midNoteSpawnpoint.transform.position,
            Quaternion.identity);
    }

    public void SpawnHighNote()
    {
        Instantiate(
            notes[2],
            highNoteSpawnpoint.transform.position,
            Quaternion.identity);
    }
}
