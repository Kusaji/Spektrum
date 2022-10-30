using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public enum frequency { low, mid, high};
    public frequency lightFrequency;
    public float currentIntensity;
    public float goalIntensity;
    public Light myLight;
    private float time;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        SetIntensity();
    }

    public void SetIntensity()
    {
        switch (lightFrequency)
        {
            case frequency.low:
                goalIntensity = PlayerAudio.Instance.lowAverage;
                break;
            case frequency.mid:
                goalIntensity = PlayerAudio.Instance.midAverage;
                break;
            case frequency.high:
                goalIntensity = PlayerAudio.Instance.highAverage;
                break;
        }


        float delta = goalIntensity - myLight.intensity;
        delta *= Time.deltaTime * 2;
        currentIntensity += delta;

        myLight.intensity = currentIntensity;
    }
}
