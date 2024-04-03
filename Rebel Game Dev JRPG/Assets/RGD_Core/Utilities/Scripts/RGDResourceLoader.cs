using UnityEngine;

namespace RebelGameDevs.Utils
{
	public static class RGDResourceLoader
	{
		public static bool LoadRGDResource<Asset>(string location, out Asset asset) where Asset : Object
		{
			asset = Resources.Load<Asset>(location);
			return asset is not null;
		}
	}
}
