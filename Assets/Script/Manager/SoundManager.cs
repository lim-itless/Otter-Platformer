using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;

    private AsyncOperationHandle<AudioClip> bgmHandle;

    private void Start()
    {
        PlayBGM("BGM/MainTheme_1");
    }

    public void PlayBGM(string address)
    {
        StartCoroutine(LoadAndPlayBGM(address));
    }

    private IEnumerator LoadAndPlayBGM(string address)
    {
        bgmHandle = Addressables.LoadAssetAsync<AudioClip>(address);

        yield return bgmHandle;

        if (bgmHandle.Status == AsyncOperationStatus.Succeeded)
        {
            bgmSource.clip = bgmHandle.Result;
            bgmSource.loop = true;
            bgmSource.Play();

            Debug.Log($"재생 성공 {address}");
        }
        else
        {
            Debug.LogWarning($"BGM 로드 실패 {address}");
        }
    }

    private void OnDestroy()
    {
        if (bgmHandle.IsValid())
        {
            Addressables.Release(bgmHandle);
        }
    }
}