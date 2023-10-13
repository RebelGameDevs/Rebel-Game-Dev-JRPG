using System.Net;
using UnityEngine;

namespace RebelGameDevs.Utils.World
{
	public static class RGD_GrabComponentMethods
	{
		/*
		====================================================================================================
		TryAndShootRay:
			=> These are never called by you and will only be called from methods inside of this static class:
		====================================================================================================
		*/
		private static bool TryAndShootRay(Camera cam, float distance, out RaycastHit hitResult)
		{
			if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out hitResult, distance))
				return true;

			hitResult = new RaycastHit();
			return false;
		}
		private static bool TryAndShootRay(Transform pointOfInterest, float distance, LayerMask layer, out RaycastHit hitResult)
		{
			if(Physics.Raycast(new Ray(pointOfInterest.position, pointOfInterest.forward * distance), 
				out hitResult, distance, layer)) return true;

			hitResult = new RaycastHit();
			return false;
		}
		/*
		====================================================================================================
		RGD_TryGrabRayCasterFromCam<T>:
		Params: 
			Cam - Camera, used for the camera viewport
			Distance - Float, a distance that the ray should travel
			HitResult - out RaycastHit, a returning raycast hit
			Type - out T, a template paramater given to grab any type given it has the base class Mono.

		example use to grab a transform:
			if(RGD_TryGrabRayCasterFromCam<Transform>(myCam, distanceToShoot, out RaycastHit hitResult, 
			out Transform type))
			{
				//do some code with the Transform
			}
			
		//Note that you can remove the <Type> in this method as we already specify the type in the out.
		====================================================================================================
		*/
		public static bool RGD_TryGrabRayCasterFromCam<T>(Camera cam, float distance, out RaycastHit hitResult, out T type)
		{
			if (TryAndShootRay(cam, distance, out hitResult))
				if (hitResult.collider.TryGetComponent(out type)) return true;

			hitResult = new RaycastHit();
			type = (T)default;
			return false;
		}

		/*
		====================================================================================================
		RGD_TryGrabRayCasterFromCam:
		Params: 
			Cam - Camera, used for the camera viewport
			Distance - Float, a distance that the ray should travel
			HitResult - out RaycastHit, a returning raycast hit

			=> Like the other method above. This one will return the hitresult and will take no template.

		example use to grab a transform:
			if(RGD_TryGrabRayCasterFromCam(myCam, distanceToShoot, out RaycastHit hitResult))
			{
				//do some code with the hitResult:

				//ex:
				Debug.Log($"The object is <color="red">{hitResult.distance}</color> <b>Unity Units<b> away.");
			}

		//Note that you can remove the <Type> in this method as we already specify the type in the out.
		====================================================================================================
		*/
		public static bool RGD_TryGrabRayCasterFromCam(Camera cam, float distance, out RaycastHit hitResult)
		{
			if (TryAndShootRay(cam, distance, out hitResult))
			{
				return true;
			}
			hitResult = new RaycastHit();
			return false;
		}
		/*
		====================================================================================================
		RGD_TryGrabRayCasterFromPoint:

		Params:
			pointOfInterest - Transfrom, the point where to shoot the ray from
			distance - float, the distance that the ray should travel
			hitResult - out RaycastHit, the data returned from the ray hit
			layer - LayerMask, the layer to ignore when shooting the ray
				note => put a tilda '~' symbol in front of the layer to ignore this layer:

		Description:
			Will shoot a ray from the transform in a forward direction (z axis) given a length and a layer.
		====================================================================================================
		*/
		public static bool RGD_TryGrabRayCasterFromPoint(Transform pointOfInterest, float distance, out RaycastHit hitResult, LayerMask layer)
		{
			if(TryAndShootRay(pointOfInterest, distance, layer, out hitResult)) return true;

			return false;
		}
		/*
		====================================================================================================
		RGD_TryGrabRayCasterFromPoint<T>:

		Params:
			pointOfInterest - Transfrom, the point where to shoot the ray from
			distance - float, the distance that the ray should travel
			hitResult - out RaycastHit, the data returned from the ray hit
			layer - LayerMask, the layer to ignore when shooting the ray
				note => put a tilda '~' symbol in front of the layer to ignore this layer:
			type - out T, a template type which needs to derive from MonoBehavior. 

		Description:
			Will shoot a ray from the transform in a forward direction (z axis) given a length and a layer.
			Addirionally this will also check and return the template script type specified.
		====================================================================================================
		*/
		public static bool RGD_TryGrabRayCasterFromPoint<T>(Transform pointOfInterest, float distance, out RaycastHit hitResult, LayerMask layer, out T type)
		{
			if (TryAndShootRay(pointOfInterest, distance, layer, out hitResult))
				if(hitResult.collider.TryGetComponent(out type)) return true;

			//Cast to a default type of the template:
			type = (T)default;
			return false;
		}
		/*
		====================================================================================================
		RGD_TryGrabMouseWorldPosition:

		Params:
			camera - Camera, camera to shoot a raycast from:
			hitResult - out RaycastHit, the hitPoint from the raycast:

		Description:
			Will return true if we sucessfully hit a location on a plane in 3D space.
		====================================================================================================
		*/
		public static bool RGD_TryGrabMouseWorldPosition(Camera camera, out RaycastHit hitResult)
		{
			Ray ray = camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
			if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() 
				&& Physics.Raycast(ray, out hitResult)) return true;

			hitResult = new RaycastHit();
			return false;
		}
		/*
		====================================================================================================
		RGD_TryGrabMouseWorldPosition<T>:

		Params:
			camera - Camera, camera to shoot a raycast from:
			type - out Template, a template type that can be any type that derives from mono specified:

		Description:
			Will return true if we sucessfully hit a location on a plane in 3D space and has that specific 
			type specified. This needs to have a event system in the scene or this will not work:
		====================================================================================================
		*/
		public static bool RGD_TryGrabMouseWorldPosition<T>(Camera camera, out T type)
		{
			//Grab Mouse Position:
			if(RGD_TryGrabMouseWorldPosition(camera, out RaycastHit hitResult))
			{
				if(hitResult.collider.TryGetComponent(out type))
				{
					return true;
				}
			}

			//Else not all conditions are met:
			type = (T)default;
			return false;
		}

	}
}
