using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Components")]
    public AudioSource speaker;
    public NoteSpawner spawner; 

    [Header("Gameplay")]
    public bool noteAvailable;
    public float noteCooldown;

    [Header("Note Thresholds")]
    public float lowThreshold;
    public float midThreshold;
    public float highThreshold;

    [Header("Averages of Spectrums")]
    public float lowAverage;
    public float midAverage;
    public float highAverage;

    [Header("Average Multipliers")]
    public float lowMultiplier;
    public float midMultiplier;
    public float highMultiplier;

    [Header("Spectrums")]
    public float[] audioSpectrum;
    public float[] lows;
    public float[] mids;
    public float[] highs;

    private void Start()
    {
        noteAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeAudio();
    }

    public void SpawnNote()
    {
        if (noteAvailable)
        {
            if (midAverage >= midThreshold)
            {
                noteAvailable = false;
                spawner.SpawnMidNote();
                StartCoroutine(NoteCooldownRoutine());
            }
            else if (highAverage >= highThreshold)
            {
                noteAvailable = false;
                spawner.SpawnHighNote();
                StartCoroutine(NoteCooldownRoutine());
            }
            else if (lowAverage >= lowThreshold)
            {
                noteAvailable = false;
                spawner.SpawnLowNote();
                StartCoroutine(NoteCooldownRoutine());
            }
        }
    }

    public void AnalyzeAudio()
    {
        //Reset audio spectrums
        audioSpectrum = new float[256];
        lows = new float[85];
        mids = new float[85];
        highs = new float[85];

        //Record current spectrum from song
        speaker.GetSpectrumData(audioSpectrum, 0, FFTWindow.Rectangular);

        //Break up spectrums into lows, mids, highs

        //Lows
        for (int i = 0; i < 85; i++)
        {
            lows[i] = audioSpectrum[i];
        }

        //Mids
        for (int i = 85; i < 170; i++)
        {
            mids[i - 85] = audioSpectrum[i];
        }

        //Highs
        for (int i = 170; i < 255; i++)
        {
            highs[i - 170] = audioSpectrum[i];
        }

        //Reset Averages
        lowAverage = 0;
        midAverage = 0;
        highAverage = 0;

        //Calculate averages of Lows / Mids / Highs
        for (int i = 0; i < 85; i++)
        {
            lowAverage += lows[i];
            midAverage += mids[i];
            highAverage += highs[i];
        }

        lowAverage *= lowMultiplier;
        midAverage *= midMultiplier;
        highAverage *= highMultiplier;

        //Divide to get average
        lowAverage /= 85;
        midAverage /= 85;
        highAverage /= 85;

        SpawnNote();
    }

    public IEnumerator NoteCooldownRoutine()
    {
        yield return new WaitForSeconds(noteCooldown);
        noteAvailable = true;
    }
}
