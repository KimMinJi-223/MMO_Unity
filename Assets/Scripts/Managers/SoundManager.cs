using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    // �÷��̾� -> AudioSource
    // ���� -> AudioClip
    // ������ -> AudioListener
    // �־�� ���尡 �鸰��.
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount]; // bgm�� �ϳ� �Ϲ����� ����Ʈ�� �ϳ�

    // ���� �Ŵ����� �Ŵ��� ���Ͽ� �ִ�. �Ŵ����� �������� �ʴ´�. ���� ����Ǵ���� �����Ŭ�� ��ųʸ��� ���� ���� �ʴ´�. �޸𸮰� �������� �� �ִ�.
    //Ŭ�����ϴ� �κ��� ����
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
        // ������� ���߰� ���� �����Ǿ� �ִ� ��� ����θ� null�� ���ش�.
        foreach(AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ��ųʸ��� �ִ� ��� ������� �����.
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
                audioSource.Stop(); // �̰� ��� ����� �Ȱ����ϴ�

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
            // bgm�� ��¼�� �ѹ��ٲ�µ� ����Ʈ �Ҹ��� ���� ����ȴ�. �׶����� Managers.Resource.Load<AudioClip>(path) �̷��� �ϴ°� ��ȭ�� ���� �� �ִ�.
            // ĳ���ϴ� �κ��� ������
            // ĳ�� ������ �Ѵ�. �̹� �ִ� ���� ������ ã�ƿ´�.    

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
