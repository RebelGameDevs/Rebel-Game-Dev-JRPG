using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RebelGameDevs.HelperComponents
{
	public class RGD_Extra_HideInWorld : MonoBehaviour
	{
		public void SetHideFlags(HideFlags flag)
		{
			gameObject.hideFlags = flag;
		}
	}
}
