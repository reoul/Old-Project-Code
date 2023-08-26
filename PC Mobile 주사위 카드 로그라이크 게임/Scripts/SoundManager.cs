using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public float masterVolumeSFX = 0.5f;
    public float masterVolumeBGM = 0.5f;

    [SerializeField]
    AudioClip BGMClip; // ��� �ҽ� ����.

    [SerializeField]
    AudioClip[] sfxClip; // ȿ���� �ҽ��� ����.
    
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

        // ��ųʸ��� �����Ŭ�� �迭���� ���ϴ� ������� Ž��
        audioClipsDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip a in sfxClip)
        {
            audioClipsDic.Add(a.name, a);
        }
    }

    //������� ����
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
            Logger.LogWarning($"[{bgmName}] ������ �����ϴ�.");
        }
    }

    private void Start()
    {
        if (bgmPlayer != null)
        {
            bgmPlayer.Play();
        }
    }

    // ȿ���� ���
    public void PlaySound(string sfxName, float sfxVolume = 1f)
    {
        try
        {
            if (audioClipsDic.ContainsKey(sfxName) == false /*|| sfxPlayer.GetComponent<AudioSource>().isPlaying*/)
            {
                Logger.LogError($"{sfxName} �� ���Ե� ������� �����ϴ�.");
                return;
            }
            sfxPlayer.PlayOneShot(audioClipsDic[sfxName], sfxVolume * masterVolumeSFX);
            //Logger.Log($"{sfxName} ���� ���");
        }
        catch (System.Exception)
        {
            Logger.LogError("error");
        }
    }

    // ������� ����
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    // ȿ���� ���� ����
    public void SetVolumeSFX(float volume)
    {
        masterVolumeSFX = volume;
    }

    // ��� ���� ����
    public void SetVolumeBGM(float volume)
    {
        masterVolumeBGM = volume;
        bgmPlayer.volume = masterVolumeBGM;
    }
}
