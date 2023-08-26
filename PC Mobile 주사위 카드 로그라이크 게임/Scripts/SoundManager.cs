using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public float masterVolumeSFX = 0.5f;
    public float masterVolumeBGM = 0.5f;

    [SerializeField]
    AudioClip BGMClip; // 배경 소스 지정.

    [SerializeField]
    AudioClip[] sfxClip; // 효과음 소스들 지정.
    
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    private Dictionary<string, AudioClip> audioClipsDic;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioSource bgmPlayer;

    void Awake()
    {
        SetupBGM();
        SetVolumeBGM(0.5f);
        SetVolumeSFX(0.5f);

        // 딕셔너리로 오디오클립 배열에서 원하는 오디오를 탐색
        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in sfxClip)
        {
            audioClipsDic.Add(a.name, a);
        }
    }

    //배경음악 세팅
    void SetupBGM()
    {
        if (BGMClip == null) return;

        GameObject child = new GameObject("BGM");
        child.transform.SetParent(transform);
        bgmPlayer = child.AddComponent<AudioSource>();
        bgmPlayer.clip = BGMClip;
        bgmPlayer.volume = masterVolumeBGM;
        bgmPlayer.loop = true;
    }

    public void BGMChange(string bgmName)
    {
        if (audioClipsDic.ContainsKey(bgmName))
        {
            if (bgmPlayer.clip == audioClipsDic[bgmName])
            {
                return;
            }
            bgmPlayer.clip = audioClipsDic[bgmName];
            bgmPlayer.loop = true;
            bgmPlayer.Play();
        }
        else
        {
            Logger.LogWarning($"[{bgmName}] 음악이 없습니다.");
        }
    }

    private void Start()
    {
        if (bgmPlayer != null)
        {
            bgmPlayer.Play();
        }
    }

    // 효과음 재생
    public void PlaySound(string sfxName, float sfxVolume = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfxName) == false /*|| sfxPlayer.GetComponent<AudioSource>().isPlaying*/)
            {
                Logger.LogError($"{sfxName} 이 포함된 오디오가 없습니다.");
                return;
            }
            sfxPlayer.PlayOneShot(audioClipsDic[sfxName], sfxVolume * masterVolumeSFX);
            //Logger.Log($"{sfxName} 사운드 재생");
        }
        catch (System.Exception)
        {
            Logger.LogError("error");
        }
    }

    // 배경음악 종료
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    // 효과음 볼륨 조절
    public void SetVolumeSFX(float volume)
    {
        masterVolumeSFX = volume;
    }

    // 배경 볼륨 조절
    public void SetVolumeBGM(float volume)
    {
        masterVolumeBGM = volume;
        bgmPlayer.volume = masterVolumeBGM;
    }
}
