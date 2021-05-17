using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Note {
    C, Cs, D, Ds, E, F, Fs, G, Gs, A, As, B
}

public class NoteData : MonoBehaviour
{
    public static NoteData instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public float GetNoteFrequency(Note note)
    {
        switch (note)
        {
            default:
            case Note.C:
                return 523f;
            case Note.Cs:
                return 554f;
            case Note.D:
                return 587f;
            case Note.Ds:
                return 622f;
            case Note.E:
                return 659f;
            case Note.F:
                return 698f;
            case Note.Fs:
                return 740f;
            case Note.G:
                return 784f;
            case Note.Gs:
                return 831f;
            case Note.A:
                return 880f;
            case Note.As:
                return 932f;
            case Note.B:
                return 988f;
        }
    }
}
