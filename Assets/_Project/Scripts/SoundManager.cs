using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<AudioClip> pops;
    [SerializeField]
    AudioClip button;
    public static SoundManager instance;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    private AudioClip RandomClip(List<AudioClip> clips)
    {
        if (clips == null || clips.Count == 0)
            return null;
        return clips[Random.Range(0, clips.Count)];
    }

    public void PlayPop()
    {
        instance.GetComponent<AudioSource>().PlayOneShot(RandomClip(pops));
    }
    public void PlayButton()
    {
        instance.GetComponent<AudioSource>().PlayOneShot(button);
    }
}
