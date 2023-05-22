using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Represents a sound in the game.
/// </summary>
[System.Serializable]
public class Sound
{
    /// <summary>
    /// The name of the sound.
    /// </summary>
    public string name;

    /// <summary>
    /// The audio clip for the sound.
    /// </summary>
    public AudioClip clip;

    /// <summary>
    /// The volume of the sound (range: 0 to 1).
    /// </summary>
    [Range(0f, 1f)]
    public float volume;

    /// <summary>
    /// The pitch of the sound (range: 0.1 to 3).
    /// </summary>
    [Range(.1f, 3f)]
    public float pitch;

    /// <summary>
    /// Indicates whether the sound should loop.
    /// </summary>
    public bool loop;

    /// <summary>
    /// The audio source component associated with the sound.
    /// </summary>
    [HideInInspector]
    public AudioSource source;
}
