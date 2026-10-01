using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private AsyncOperationHandle<AudioClip> bgmHandle;

    public static SoundManager Inst { get; private set; }

    private void Awake()
    {
        Inst = this;
    }

    public void PlayBGM(string address, bool isLoop = true)
    {
        StopBGM();
        StartCoroutine(LoadAndPlayBGM(address, isLoop));
    }

    private IEnumerator LoadAndPlayBGM(string address, bool isLoop)
    {
        if (bgmHandle.IsValid())
        {
            Addressables.Release(bgmHandle);
        }

        bgmHandle = Addressables.LoadAssetAsync<AudioClip>(address);
        yield return bgmHandle;

        if (bgmHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning($"BGM 로드 실패 {address}");
            yield break;
        }

        bgmSource.clip = bgmHandle.Result;
        bgmSource.loop = isLoop;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource == null)
        {
            return;
        }

        if (clip == null)
        {
            return;
        }

        sfxSource.PlayOneShot(clip);
    }

    public void StopBGM()
    {
        if (bgmSource == null)
        {
            return;
        }

        bgmSource.Stop();
    }

    private void OnDestroy()
    {
        if (bgmHandle.IsValid())
        {
            Addressables.Release(bgmHandle);
        }
    }
}