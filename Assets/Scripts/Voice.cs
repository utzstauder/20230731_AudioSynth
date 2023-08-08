using System;
using UnityEngine;

public class Voice
{
    public Synthesizer.WaveType waveType = Synthesizer.WaveType.Sin;

    public int noteNumber;
    public float velocity;

    private float frequency;

    private float sampleRate;
    private double increment;
    private double phase;

    private System.Random random;

    public Voice()
    {
        noteNumber = -1;
        velocity = 0;
        random = new System.Random();
    }

    public void NoteOn(int noteNumber, float velocity)
    {
        this.noteNumber = noteNumber;
        this.velocity = velocity;

        frequency = NoteInput.NoteToFrequency(noteNumber);

        phase = 0;
        sampleRate = AudioSettings.outputSampleRate;
    }

    public void NoteOff(int noteNumber)
    {
        if (noteNumber != this.noteNumber) return;
        
        // TODO: start release time
    }

    public void WriteAudioBuffer(float[] buffer, int channels)
    {
        increment = frequency * 2 * Math.PI / sampleRate;
        
        for (int i = 0; i < buffer.Length; i += channels)
        {
            phase += increment;
            if (phase > Math.PI * 2) phase -= Math.PI * 2;

            switch (waveType)
            {
                case Synthesizer.WaveType.Sin:
                    buffer[i] += Mathf.Sin((float)phase);
                    break;
                
                case Synthesizer.WaveType.Square:
                    buffer[i] += Mathf.Sin((float)phase) >= 0 ? 1 : -1;
                    break;
                
                case Synthesizer.WaveType.Saw:
                    break;
                
                case Synthesizer.WaveType.Triangle:
                    break;
                
                case Synthesizer.WaveType.Noise:
                default:
                    buffer[i] += (float)random.NextDouble();
                    break;
            }
        }
    }
}
