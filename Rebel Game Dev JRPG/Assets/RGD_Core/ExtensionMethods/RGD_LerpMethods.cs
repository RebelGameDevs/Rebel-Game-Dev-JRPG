using System.Collections;
using UnityEngine;
namespace RebelGameDevs.Utils.World
{
	public static class RGD_LerpMethods
	{
		/*
		==========================================================================================
		IEnumerator SimpleLerpScaleToDestroy:

		Params:
			obj - GameObject, the object to lerp.
			timeToLerp - float, the time it takes to lerp the object in unity time.
				=> note the timeToLerp is based on unity time and not unscaled delta time so
				time will vary if the unity timescale is not set to 1.

		Description:
			A easy way to lerp out a object with not strings attached. Note the callee will be the
			object and will stop coroutine if the object is turned off/inactive.
		==========================================================================================
		*/
		public static IEnumerator SimpleLerpScaleToDestroy(GameObject obj, float timeToLerp)
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
		/*
		==========================================================================================
		IEnumerator SimpleLerpScale:

		Params:
			obj - GameObject, the object to lerp.
			timeToLerp - float, the time it takes to lerp the object in unity time.
				=> note the timeToLerp is based on unity time and not unscaled delta time so
				time will vary if the unity timescale is not set to 1.
			scaleToLerpTo - Vector3, the end scale.

		Description:
			A easy way to lerp out a object with not strings attached. Note the callee will be the
			object and will stop coroutine if the object is turned off/inactive.
		==========================================================================================
		*/
		public static IEnumerator SimpleLerpScale(GameObject obj, Vector3 scaleToLerpTo, float timeToLerp)
		{
			Vector3 startScale = obj.transform.localScale;
			float localTTIme = 0;
			while(localTTIme < 1)
			{
				localTTIme += Time.deltaTime;
				obj.transform.localScale = Vector3.Lerp(startScale, scaleToLerpTo, localTTIme);
				yield return null;
			}
			obj.transform.localScale = scaleToLerpTo;
		}
		/*
		==========================================================================================
		IEnumerator SimpleLerpLocation:

		Params:
			obj - GameObject, the object to lerp.
			timeToLerp - float, the time it takes to lerp the object in unity time.
				=> note the timeToLerp is based on unity time and not unscaled delta time so
				time will vary if the unity timescale is not set to 1.
			endPos - Vector3; the end position when finished/

		Description:
			A easy way to lerp out a object with not strings attached. Note the callee will be the
			object and will stop coroutine if the object is turned off/inactive.
		==========================================================================================
		*/
		public static IEnumerator SimpleLerpLocalLocation(GameObject obj, Vector3 endPos, float timeToLerp)
		{
			Vector3 startLocation = obj.transform.localPosition;
			float localTTIme = 0;
			while(localTTIme < 1)
			{
				localTTIme += Time.deltaTime;
				obj.transform.localPosition = Vector3.Lerp(startLocation, endPos, localTTIme);
				yield return null;
			}
			obj.transform.localPosition = endPos;
		}
	}
}
