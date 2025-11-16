using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;


    [Header("Audio Source")]
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    [SerializeField] private int bgmIndex;

    void Awake()
    {
        Debug.Log("AUDIO MANAGER AWAKE");
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        // PlayBgm(0);
        InvokeRepeating(nameof(PlayMusicIfNeeded), 0, 2);
    }

    public void PlayBgm(int bgmToPlay)
    {

        Debug.Log("AUDIO MANAGER playbgm");

        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }

        bgmIndex = bgmToPlay;
        bgm[bgmToPlay].Play();
    }

    public void PlayRandomBgm()
    {
        Debug.Log("AUDIO MANAGER PlayRandomBgm");
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBgm(bgmIndex);
    }

    public void PlayMusicIfNeeded()
    {
        Debug.Log("AUDIO MANAGER PlayMusicIfNeeded");

        if (bgm[bgmIndex].isPlaying == false)
        {
            PlayRandomBgm();
        }
    }

    public void PlaySFX(int sfxToPlay, bool randomPitch = true)
    {
        if (sfxToPlay >= sfx.Length)
        {
            return;
        }

        if (randomPitch)
            sfx[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

        sfx[sfxToPlay].Play();
    }

    public void StopSFX(int sfxToStop) => sfx[sfxToStop].Stop();
}
