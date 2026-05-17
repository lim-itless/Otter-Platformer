using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Inst { get; set; }

    private void Awake()
    {
        Inst = this;
    }

    private Dictionary<string, AsyncOperationHandle> _handles = new Dictionary<string, AsyncOperationHandle>();

    public void LoadAsset<T>(string address, System.Action<T> callback) where T : UnityEngine.Object
    {
        if (_handles.TryGetValue(address, out AsyncOperationHandle handle))
        {
            callback?.Invoke(handle.Result as T);
            return;
        }

        AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(address);

        loadHandle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                _handles[address] = op;
                callback?.Invoke(op.Result);
            }
            else
            {
                Debug.LogError($"에셋 로드 실패: {address}");
            }
        };
    }

    public async UniTask<T> LoadAsset<T>(string address) where T : UnityEngine.Object
    {
        if (_handles.TryGetValue(address, out AsyncOperationHandle handle))
        {
            return handle.Result as T;
        }

        AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(address);

        try
        {
            T result = await loadHandle.ToUniTask();

            _handles[address] = loadHandle;
            return result;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"에셋 로드 실패: {address} / Error: {e.Message}");

            if (loadHandle.IsValid())
                Addressables.Release(loadHandle);

            return null;
        }
    }

    private void OnDestroy()
    {
        foreach (var handle in _handles.Values)
        {
            if (handle.IsValid())
            {
                Addressables.Release(handle);
            }
        }

        _handles.Clear();
    }
}
