using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using static RebelGameDevs.Utils.RebelGameDevsEditorHelpers;
#endif
//Notes:

/*
================================================================================
public void ExampleOnHowToAddItemToInventory()
{
    //Creating a type for an example:
    InventoryItemType<int, string> type = new InventoryItemType<int, string>();
    type.name = "Apple";
    type.amount = 1;

    //Simply call AddItem() given a reference to the singleton:
    script.AddItem(type);
}
================================================================================
*/
public class RGD_InventorySystem : MonoBehaviour
{
    public List<InventoryItemType<int, string>> inventoryItems = new List<InventoryItemType<int, string>>();
    private bool FindItem(InventoryItemType<int, string> itemToCompare, out InventoryItemType<int, string> itemFound)
    {
        foreach(InventoryItemType<int, string> inventory in inventoryItems)
        {
            if(inventory.name == itemToCompare.name)
            {//Found Item:

                itemFound = inventory;
                return true;
            }
        }
        itemFound = null;
        return false;
    }
    public void AddItem(InventoryItemType<int, string> itemToAdd)
    {
        if(FindItem(itemToAdd, out InventoryItemType<int, string> itemFound))
        {
            itemFound.amount += itemToAdd.amount;
            return;
        }
        inventoryItems.Add(itemToAdd);
    }
    public void TEST_AddItemTyp2()
    {
        InventoryItemType<int, string> type = new InventoryItemType<int, string>();
        type.name = "Orange";
        type.amount = 1;
        AddItem(type);
    }
}
[System.Serializable] public class InventoryItemType<T, S>
{
    public T amount;
    public S name;
}
#if UNITY_EDITOR
[CustomEditor(typeof(RGD_InventorySystem))]
class RGD_InventorySystem_Editor : Editor
{
    public override void OnInspectorGUI()
    {
    //Check For Script:
        var component = (RGD_InventorySystem)target;
        if (component == null) return;
        Undo.RecordObject(component, "Change Component");

        //Custom Inspector:
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("")) { }
        DrawImage("Assets/RGD_Core/Utilities/Images/Banner_Manager.png", 0.85f, 0.3f);

        DrawDefaultInspector();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("")) { }
    }


}
#endif