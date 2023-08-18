using System.Collections;
using UnityEngine;
namespace RebelGameDevs.Utils.World
{
	public static class RGD_LerpMethods
	{
		/*
		==========================================================================================
		IEnumerator SimpleLerpToDestroy:

		Params:
			obj - GameObject, the object to lerp
			timeToLerp - float, the time it takes to lerp the object in unity time.
				=> note the timeToLerp is based on unity time and not unscaled delta time so
				time will vary if the unity timescale is not set to 1.

		Description:
			A easy way to lerp out a object with not strings attached. Note the callee will be the
			object and will stop coroutine if the object is turned off/inactive.
		==========================================================================================
		*/
		public static IEnumerator SimpleLerpToDestroy(GameObject obj, float timeToLerp)
		{
			Vector3 startScale = obj.transform.localScale;
			float localTTIme = 0;
			while(localTTIme < 1)
			{
				localTTIme += Time.deltaTime;
				obj.transform.localScale = Vector3.Lerp(startScale, Vector3.zero, localTTIme);
				yield return null;
			}
			Object.Destroy(obj);
		}
	}
}
