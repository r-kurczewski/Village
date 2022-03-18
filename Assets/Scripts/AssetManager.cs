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

		[SerializeField]
		private bool loaded;

		public AsyncOperationHandle _assetsHandle;
		//private List<AsyncOperationHandle> assetsHandles = new List<AsyncOperationHandle>();

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
				Debug.LogWarning("Duplicate of Asset Manager.");
			}
		}

		private void Start()
		{
			if (instance == this)
			{
				//LoadAssets();
				Debug.Log("Start asset loading...");
			}
		}

		public Dictionary<string, Object> GetAssets()
		{
			return assets;
		}

		public T GetAsset<T>(string assetString) where T : Object
		{
			return assets[assetString] as T;
		}

		public async Task LoadAssets()
		{
			if (!loaded)
			{
				_assetsHandle = Addressables.LoadAssetsAsync<ScriptableObject>(addressableLabel, (asset) =>
				{
					assets.Add(asset.name, asset);
				});

				await _assetsHandle.Task;
				loaded = true;
			}
		}
		public T GetResourcesAsset<T>(string assetPath) where T : Object
		{
			return Resources.Load<T>(assetPath);
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

		private void OnDestroy()
		{
			//if(instance == this)
			//{
			//	foreach (var handle in assetsHandles)
			//	{
			//		Addressables.Release(handle);
			//	}
			//}

			if (instance == this && _assetsHandle.IsValid())
			{
				Debug.Log("Unloading assets.");
				Addressables.Release(_assetsHandle);
			}
		}
	}
}
