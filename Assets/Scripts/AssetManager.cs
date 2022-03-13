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
			}
		}
		public T GetResourcesAsset<T>(string assetPath) where T : Object
		{
			return Resources.Load<T>(assetPath);
		}

		private void OnDestroy()
		{
			if (instance == this)
			{
				if(_assetsHandle.IsValid()) Addressables.Release(_assetsHandle);
			}
		}
	}
}
