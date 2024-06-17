using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundplayer : MonoBehaviour
{
    public List<AudioSource> audioClips = new List<AudioSource>();

    void Start()
    {
        // Start playing audio at random intervals
        Invoke("PlayRandomAudio", Random.Range(30f, 50f));
    }

    void PlayRandomAudio()
    {
        // Choose a random audio clip from the list
        int randomIndex = Random.Range(0, audioClips.Count);
        AudioSource clipToPlay = audioClips[randomIndex];

        // Play the selected audio clip
        clipToPlay.Play();

        // Schedule the next invocation
        Invoke("PlayRandomAudio", Random.Range(15f, 20f));
    }
}
