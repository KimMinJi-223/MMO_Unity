using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // 플레이어 -> AudioSource
    // 음원 -> AudioClip
    // 관객이 -> AudioListener
    // 있어야 사운드가 들린다.
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // bgm용 하나 일반적인 이펙트용 하나

    // 사운드 매니저는 매니저 산하에 있다. 매니저는 없어지지 않는다. 씬이 변경되더라고 오디오클립 딕셔너리는 삭제 되지 않는다. 메모리가 터져버릴 수 있다.
    //클리어하는 부분을 넣자
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = $"Sound" };
            Object.DontDestroyOnLoad(root);
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }


    public void Clear()
    {
        // 오디오를 멈추고 현재 설정되어 있는 재생 오디로를 null로 해준다.
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 딕셔너리에 있는 모든 오디오를 지운다.
        _audioClips.Clear();
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sound.Bgm)
        {     
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop(); // 이게 없어도 결과는 똑같긴하다

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {          
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}";

        AudioClip audioCilp = null;

        if (type == Define.Sound.Bgm)
        {
            audioCilp = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            // bgm은 어쩌다 한번바뀌는데 이펙트 소리는 자주 실행된다. 그때마다 Managers.Resource.Load<AudioClip>(path) 이렇게 하는건 부화가 있을 수 있다.
            // 캐싱하는 부분을 만들어보자
            // 캐싱 역할을 한다. 이미 있는 것은 빠르게 찾아온다.    

            if (_audioClips.TryGetValue(path, out audioCilp) == false)
            {
                audioCilp = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, audioCilp);
            }
        }
            if (audioCilp == null)
                Debug.Log($"AudioClip Missing ! {path}");

         
        return audioCilp;
    }
}
