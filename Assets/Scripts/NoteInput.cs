using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteInput : MonoBehaviour
{
    public delegate void NoteOnDelegate(int noteNumber, float velocity);
    public static event NoteOnDelegate NoteOn;

    public delegate void NoteOffDelegate(int noteNumber);
    public static event NoteOffDelegate NoteOff;

    Dictionary<KeyCode, int> virtualKeysDict;

    private void Awake()
    {
        virtualKeysDict = new Dictionary<KeyCode, int>();
        virtualKeysDict.Add(KeyCode.A, 60); // C
        virtualKeysDict.Add(KeyCode.W, 61); // C#
        virtualKeysDict.Add(KeyCode.S, 62); // D
        virtualKeysDict.Add(KeyCode.E, 63); // D#
        virtualKeysDict.Add(KeyCode.D, 64); // E
        virtualKeysDict.Add(KeyCode.F, 65); // F
        virtualKeysDict.Add(KeyCode.T, 66); // F#
        virtualKeysDict.Add(KeyCode.G, 67); // G
        virtualKeysDict.Add(KeyCode.Z, 68); // G#
        virtualKeysDict.Add(KeyCode.H, 69); // A
        virtualKeysDict.Add(KeyCode.U, 70); // A#
        virtualKeysDict.Add(KeyCode.J, 71); // B
        virtualKeysDict.Add(KeyCode.K, 72); // C

        // ...
    }

    private void Update()
    {
        foreach (var key in virtualKeysDict.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                NoteOn?.Invoke(virtualKeysDict[key], 1f);
            }

            if (Input.GetKeyUp(key))
            {
                NoteOff?.Invoke(virtualKeysDict[key]);
            }
        }
    }

    public static float NoteToFrequency(int noteNumber)
    {
        float twelfthRoot = Mathf.Pow(2f, 1f / 12f);
        int fixedNoteNumber = 69;
        return 440f * Mathf.Pow(twelfthRoot, (noteNumber - fixedNoteNumber));
    }
}