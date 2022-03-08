using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Village
{
	public class AssetManager : MonoBehaviour
	{
		private const string addressableLabel = "village";

		public static AssetManager instance;

		private Dictionary<string, Object> assets = new Dictionary<string, Object>();
		private AsyncOperationHandle _assetsHandle;
		//private List<AsyncOperationHandle> assetsHandles = new List<AsyncOperationHandle>();
		private bool loaded;

		public AsyncOperationHandle Handle => _assetsHandle;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void Start()
		{
			LoadAssets();
		}

		public Dictionary<string, Object> GetAssets()
		{
			return assets;
		}

		public T GetAsset<T>(string assetString) where T : Object
		{
			return assets[assetString] as T;
		}

		public async void LoadAssets()
		{
			if (!loaded)
			{
				_assetsHandle = Addressables.LoadAssetsAsync<ScriptableObject>(addressableLabel, (asset) =>
				{
					assets.Add(asset.name, asset);
				});

				await _assetsHandle.Task;

				loaded = true;
				Debug.Log($"Loaded Assets ({assets.Count})");
			}
		}

		//public async Task<T> GetAssetAsync<T>(string assetString) where T : Object
		//{
		//	if (assets.ContainsKey(assetString)) return assets[assetString] as T;

		//	T asset = null;
		//	var handle = Addressables.LoadAssetAsync<T>(assetString);
		//	handle.Completed += (loaded) =>
		//	{
		//		asset = loaded.Result;
		//		try
		//		{
		//			assets.Add(loaded.Result.name, loaded.Result);
		//			assetsHandles.Add(handle);
		//		}
		//		catch (ArgumentException)
		//		{
		//			Debug.Log("Duplicate asset loaded.");
		//			Addressables.Release(handle);
		//		}
		//	};
		//	await handle.Task;
		//	return asset;
		//}

		public T GetResourcesAsset<T>(string assetPath) where T : Object
		{
			return Resources.Load<T>(assetPath);
		}

		private void OnDestroy()
		{
			if (instance == this)
			{
				//foreach (var handle in assetsHandles)
				//{
				//	Addressables.Release(handle);
				//}
				if(_assetsHandle.IsValid()) Addressables.Release(_assetsHandle);
			}
		}
	}
}
