/*
===========================================================================
Creator: 
    Brandhon Bird (Mythic)
Date: 
    10/27/23
Purpose:
    To show how to use the static method RGD_DelegateMethods. This also uses
    the buttons in the UI Learning Scene.
Contact:
    Should you have any questions or concers, feel free to contact me
    via phone - +1 (702) - 857 - 1869 | email: mythicgaming234@gmail.com
===========================================================================
*/
using TMPro;
using UnityEngine;
using static RebelGameDevs.Utils.Containers.RGD_DelegateMethods;
public class RGD_DelegateExampleSingleton : MonoBehaviour
{
    //Delegates:
        //Holds void return type address of methods:
        private RGDDelegate myVoidDelegate;

        //Holds void return type address of methods that have dynamic parameters:
        private RGDDelegate<Collider[]> myDynamicParamDelegateOn;
        private RGDDelegate<Collider[]> myDynamicParamDelegateOff;

        //Holds dynamic return type address of methods:
        private RGDDelegateDynamic<int> myDynamicReturnDelegate;

    //Text in Scene:
    [SerializeField] private TextMeshPro _3DText;

    /*
    ===========================================================================
    - not using for this example, this is the other delegate in the static 
      RGD_DelegateMethods class. As you can see this one can have a dynamic 
      return type (in this case a bool) and a dynamic param.

      private RGDDelegateDynamicParam<bool> myDynamicReturnAndParamDelegate;
    ===========================================================================
    */

    //Awake Method(s):
    private void Awake()
    {
        SetupDelegates();
    }
    private void SetupDelegates()
    {
        myVoidDelegate += PrintTimeCallee;
        myDynamicParamDelegateOff += SetParamsInSceneOff;
        myDynamicParamDelegateOn += SetParamsInSceneOn;
        myDynamicReturnDelegate += GenerateRandomNumber;
    }

    //Called Out Side Of This Script:
    public void PrintTime()
    {
        //Call Delegate:
        myVoidDelegate();
    }
    public void GrabCollidersInScene(bool value)
    {
        //Create 2 lists: a empty one and all colliders in scene:
        var cols = new System.Collections.Generic.List<Collider>(GameObject.FindObjectsOfType<Collider>());
        var otherCols = new System.Collections.Generic.List<Collider>();

        //Add every collider to empty list that does not have the specified tag.
        foreach(Collider col in cols)
            if(col.gameObject.tag != "RebelGameDevsTag") otherCols.Add(col);

        //Turn Green
        if (value)
        {
            //Call Delegate:
            myDynamicParamDelegateOn(otherCols.ToArray());
            return;
        }
        //Else Turn red:
        //Call Delegate:
        myDynamicParamDelegateOff(otherCols.ToArray());
    }
    public void GrabInt()
    {
        _3DText.SetText($"Random Number: <color=#fff000>{myDynamicReturnDelegate()}</color>");
    }

    //Delegate Callee Methods:
    private void SetParamsInSceneOff(Collider[] cols)
    {
        int counter = 0;
        //For every Collider loop and change material color and emission:
        foreach (Collider col in cols)
        {
            var rend = col.GetComponent<MeshRenderer>();
            foreach (Material mat in rend.materials)
            {
                mat.SetColor("_BaseColor", Color.red);

                //For new Unity every time we change emission we need to enable keyword:
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.red * Mathf.LinearToGammaSpace(2f));
            }
            counter++;
        }
    }
    private void SetParamsInSceneOn(Collider[] cols)
    {
        int counter = 0;
        foreach (Collider col in cols)
        {
            var rend = col.GetComponent<MeshRenderer>();
            foreach(Material mat in rend.materials)
            {
                mat.SetColor("_BaseColor", Color.green);
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.green * Mathf.LinearToGammaSpace(2f));
            }
            counter++;
        }
    }
    private void PrintTimeCallee()
    {
        var dateTime = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        _3DText.SetText($"Current Time: <color=red>{dateTime}</color>");
    }
    public int GenerateRandomNumber()
    {
        return Random.Range(-20, 20 + 1);
    }
}