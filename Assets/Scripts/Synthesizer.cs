using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Synthesizer : MonoBehaviour
{
    private System.Random random;

    [Range(220, 1760)]
    public double frequency = 440;
    
    [Range(0, 1f)]
    public float gain = 0.5f;
    
    private int bufferLength;
    private int numBuffers;
    private int sampleRate;

    private double phase;
    private double increment;
    
    private void Start()
    {
        random = new System.Random();
        
        AudioSettings.GetDSPBufferSize(out bufferLength, out numBuffers);
        sampleRate = AudioSettings.outputSampleRate;
        
        print($"sampleRate = {sampleRate} | bufferLength = {bufferLength} | numBuffers = {numBuffers}");
    }

    private void OnAudioFilterRead(float[] buffer, int channels)
    {
        increment = frequency * 2 * Math.PI / sampleRate;
        
        for (int i = 0; i < buffer.Length; i += channels)
        {
            phase += increment;
            if (phase > Math.PI * 2) phase -= Math.PI * 2;

            // Sin
            buffer[i] = Mathf.Sin((float)phase);
            
            // Square
            //buffer[i] = Mathf.Sin((float)phase) >= 0 ? 1 : -1;

            buffer[i + 1] = buffer[i];
            
            buffer[i]     *= gain;
            buffer[i + 1] *= gain;
        }
    }
}
