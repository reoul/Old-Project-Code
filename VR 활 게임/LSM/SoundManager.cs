using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    [SerializeField]
    AudioClip BGMClip; // 배경 소스 지정.

    [SerializeField]
    AudioClip[] audioClip; // 효과음 소스들 지정.
    public struct BgmType
    {
        public string name;
        public AudioClip audio;
    }

    Dictionary<string, AudioClip> audioClipsDic;
    public AudioSource sfxPlayer;
    public AudioSource sfxPlayer2;
    public AudioSource sfxPlayer3;
    public AudioSource sfxPlayer4;
    public AudioSource sfxPlayer5;
    AudioSource StepPlayer;
    AudioSource bgmPlayer;



    void Awake()
    {
        sfxPlayer = GetComponent<AudioSource>();
        GameObject sfx2 = transform.GetChild(0).gameObject;
        sfxPlayer2 = sfx2.GetComponent<AudioSource>(); 
        GameObject sfx3 = transform.GetChild(1).gameObject;
        sfxPlayer3 = sfx3.GetComponent<AudioSource>();
        GameObject sfx4 = transform.GetChild(2).gameObject;
        sfxPlayer4 = sfx4.GetComponent<AudioSource>();
        GameObject sfx5 = transform.GetChild(2).gameObject;
        sfxPlayer5 = sfx5.GetComponent<AudioSource>();


        SetupBGM();
        SetVolumeBGM(0.5f);
        FootStepSetup();

        // 딕셔너리로 오디오클립 배열에서 원하는 오디오를 탐색
        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in audioClip)
        {
            audioClipsDic.Add(a.name, a);
        }
    }

    void FootStepSetup()
    {
        GameObject child = new GameObject("FootStep");
        child.transform.SetParent(transform);
        StepPlayer = child.AddComponent<AudioSource>();
        StepPlayer.volume = masterVolumeSFX;
        StepPlayer.loop = true;
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

    public void BGMChange(string bgm_name, float bgm_volume)
    {
        if(audioClipsDic.ContainsKey(bgm_name))
        {
            bgmPlayer.clip = audioClipsDic[bgm_name];
            bgmPlayer.volume = bgm_volume;
            bgmPlayer.loop = true;
            bgmPlayer.Play();
        }
        else
        {
            Debug.LogWarning($"[{bgm_name}] 음악이 없습니다.");
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
    public void PlaySound(string sfx_name, float sfx_volume = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfx_name) == false || sfxPlayer.GetComponent<AudioSource>().isPlaying)
            {
                Debug.Log(sfx_name + " 이 포함된 오디오가 없습니다.");
                return;
            }
            else
                sfxPlayer.PlayOneShot(audioClipsDic[sfx_name], sfx_volume * masterVolumeSFX);
        }
        catch (System.Exception)
        {
            Debug.Log("error");
        }
    }


    public void PlaySoundSecond(string sfx_name2, float sfx_volume2 = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfx_name2) == false && sfxPlayer2.GetComponent<AudioSource>().isPlaying)
            {
                Debug.Log(sfx_name2 + " 이 포함된 오디오가 없습니다.");
                return;
            }
            else
                sfxPlayer2.PlayOneShot(audioClipsDic[sfx_name2], sfx_volume2 * masterVolumeSFX);
        }
        catch (System.Exception)
        {
            Debug.Log("error");
        }
    }

    public void PlaySoundThird(string sfx_name3, float sfx_volume3 = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfx_name3) == false && sfxPlayer3.GetComponent<AudioSource>().isPlaying)
            {
                Debug.Log(sfx_name3 + " 이 포함된 오디오가 없습니다.");
                return;
            }
            else
                sfxPlayer3.PlayOneShot(audioClipsDic[sfx_name3], sfx_volume3 * masterVolumeSFX);
        }
        catch(System.Exception)
        {
            Debug.Log("error");
        }
    }

    public void PlaySoundFourth(string sfx_name4, float sfx_volume4 = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfx_name4) == false && sfxPlayer4.GetComponent<AudioSource>().isPlaying)
            {
                Debug.Log(sfx_name4 + " 이 포함된 오디오가 없습니다.");
                return;
            }
            else
                sfxPlayer4.PlayOneShot(audioClipsDic[sfx_name4], sfx_volume4 * masterVolumeSFX);
        }
        catch (System.Exception)
        {
            Debug.Log("error");
        }
     
    }

    public void PlaySoundFive(string sfx_name5, float sfx_volume5 = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfx_name5) == false || sfxPlayer5.GetComponent<AudioSource>().isPlaying)
            {
                Debug.Log(sfx_name5 + " 이 포함된 오디오가 없습니다.");
                return;
            }
            else
                sfxPlayer5.PlayOneShot(audioClipsDic[sfx_name5], sfx_volume5 * masterVolumeSFX);
        }
        catch (System.Exception)
        {
            Debug.Log("error");
        }

    }



    // 배경음악 종료
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    // 효과음 볼륨 조절
    public void SetVolumeSFX(float a_volume)
    {
        masterVolumeSFX = a_volume;
    }

    // 배경 볼륨 조절
    public void SetVolumeBGM(float a_volume)
    {
        masterVolumeBGM = a_volume;
        bgmPlayer.volume = masterVolumeBGM;
    }

    private void Update()
    {

    }

}