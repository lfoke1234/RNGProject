using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBGM;
    private int bgmIndex;

    [SerializeField] private bool canPlaySFX;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        StartCoroutine(EnableSFXAfterDelay(1f));
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (!playBGM)
        {
            StopAllBgm();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayBGM(bgmIndex);
            }
        }
    }

    #region SFX

    public void PlaySFX(int _sfxIndex)
    {
        if (canPlaySFX == false)
            return;

        if (sfx[_sfxIndex].isPlaying)
        {
            StopSFX(_sfxIndex);
        }

        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].Play();
        }
    }

    public void StopSFX(int _sfxIndex) => sfx[_sfxIndex].Stop();

    public void StopSFXWithTime(int _index)
    {
        StartCoroutine(DecreaseVolume(sfx[_index]));
    }

    private IEnumerator DecreaseVolume(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while (_audio.volume > .1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSeconds(.6f);

            if (_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }

    }
    private IEnumerator EnableSFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canPlaySFX = true;
    }

    #endregion


    #region BGM

    public void PlayRandomBGM()
    {
        bgmIndex = Random.Range(0, bgm.Length);
        PlayBGM(bgmIndex);
    }

    public void PlayBGM(int _bgmIndex)
    {
        playBGM = true;
        bgmIndex = _bgmIndex;

        StopAllBgm();
        bgm[bgmIndex].Play();
    }

    public void StopAllBgm()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void DontPlayBGM() => playBGM = false;
    public void CanPlayBGM() => playBGM = true;

    #endregion

}
