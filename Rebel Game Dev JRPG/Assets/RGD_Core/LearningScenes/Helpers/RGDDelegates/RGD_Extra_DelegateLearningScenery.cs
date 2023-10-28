/*
===========================================================================
Creator: 
    Brandhon Bird (Mythic)
Date: 
    10/27/23
Purpose:
    Addon helper extra script for rotating the orbs in the 
    delegates learning scene.
Contact:
    Should you have any questions or concers, feel free to contact me
    via phone - +1 (702) - 857 - 1869 | email: mythicgaming234@gmail.com
===========================================================================
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RGD_Extra_DelegateLearningScenery : MonoBehaviour
{
    [System.Serializable]
    private class _LocalOrb
    {
        public Transform handle;
        [HideInInspector] public Transform orb;
        public float rotationSpeed = 100f;
        public Vector3 distanceFromCenter = new Vector3(0, 0, -1);
        public Vector3 handleDirection = Vector3.right;
    }
    [SerializeField] private List<_LocalOrb> orbs = new List<_LocalOrb>();
    private void Awake()
    {
        foreach(_LocalOrb orb in orbs)
        orb.orb = orb.handle.GetComponentsInChildren<Transform>()[1];
    }
    private void Update()
    {
        foreach(_LocalOrb orb in orbs)
        {
            orb.orb.localPosition = orb.distanceFromCenter;
            orb.handle.Rotate(orb.handleDirection * orb.rotationSpeed * Time.deltaTime);
        }
    }


}
