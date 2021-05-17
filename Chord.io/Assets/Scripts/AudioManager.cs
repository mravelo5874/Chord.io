using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public AudioClip CreateSineClip(float frequency)
    {
        int sampleFreq = 44000;        
        float[] samples = new float[44000];

        for(int i = 0; i < samples.Length; i++)
        {
            samples[i] = Mathf.Sin(Mathf.PI*2*i*frequency/sampleFreq);
        }

        AudioClip ac = AudioClip.Create("Test", samples.Length, 1, sampleFreq, false);
        ac.SetData(samples, 0);

        return ac;
    }
}
