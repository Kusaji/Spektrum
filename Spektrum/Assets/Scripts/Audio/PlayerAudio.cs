using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource speaker;

    public static PlayerAudio Instance;
    public GameObject skyboxController;
    public GameObject skyboxAnimator;
    public Borodar.FarlandSkies.NebulaOne.SkyboxController skybox;
    public float currentIntensity;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        speaker = GetComponent<AudioSource>();
        skyboxController.SetActive(true);
        skyboxAnimator.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        AnalyzeAudio();
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

        float delta = lowAverage - skybox.Exposure;
        delta *= Time.deltaTime * 2;
        currentIntensity += delta;


        skybox.Exposure = Mathf.Clamp(currentIntensity, 0.25f, 3f);
    }
}
