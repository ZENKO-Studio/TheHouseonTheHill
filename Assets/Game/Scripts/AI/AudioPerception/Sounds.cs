using UnityEngine;

public static class Sounds
{
    /// <summary>
    /// Static class to make sounds
    /// </summary>
    public static void MakeSound(Sound sound)
    {
        Debug.Log($"Sound Made at {sound.pos}");

        Collider[] col = Physics.OverlapSphere(sound.pos, sound.range);

        for (int i = 0; i < col.Length; i++)
            if (col[i].TryGetComponent(out IHear hearer))
                hearer.RespondToSound(sound);
    }
}