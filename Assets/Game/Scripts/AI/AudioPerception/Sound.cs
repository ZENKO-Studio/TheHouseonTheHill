using UnityEngine;

public class Sound
{
    public Sound(Vector3 _pos, float _range, int _loudness = 0)
    {
        loudness = _loudness;

        pos = _pos;

        range = _range;
    }

    public readonly int loudness;

    public readonly Vector3 pos;

    public readonly float range;
}