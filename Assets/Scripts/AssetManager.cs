using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Village
{
	public class AssetManager : MonoBehaviour
	{
		public static AssetManager instance;

		private Dictionary<string, MonoBehaviour> assets;
		private AsyncOperationHandle assetsHandle;

		public bool Ready => assetsHandle.IsDone;

		private void Awake()
		{
			if (instance is null)
			{
				DontDestroyOnLoad(gameObject);
				instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		private void Start()
		{
			LoadAssets("Village");
		}

		public void LoadAssets(string assetsLabel)
		{
			var assets = new Dictionary<string, ScriptableObject>();
			assetsHandle = Addressables.LoadAssetsAsync<ScriptableObject>(assetsLabel, (asset) =>
			{
				assets.Add(asset.name, asset);
			});
		}

		public Dictionary<string, MonoBehaviour> GetAssets()
		{
			return assets;
		}

		public T GetAsset<T>(string assetString) where T : MonoBehaviour
		{
			return assets[assetString] as T;
		}

	}
}
