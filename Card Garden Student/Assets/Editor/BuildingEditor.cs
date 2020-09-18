using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(Building))]
public class BuildingEditor : Editor
{
    //Overrides the default inspector of the Card script
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Building building = (Building)target;

        //Building Type
        building.buildingSubtype = (BuildingSubtype)EditorGUILayout.EnumPopup("Building Subtype", building.buildingSubtype);
        
        //Tower Exclusive Fields
        if (building.buildingSubtype == BuildingSubtype.Tower)
        {
            building.rangeSphere = (GameObject)EditorGUILayout.ObjectField("Range Sphere", building.rangeSphere, typeof(GameObject), true);
            building.firePoint = (Transform)EditorGUILayout.ObjectField("Fire Point", building.firePoint, typeof(Transform), true);
            building.projectile = (GameObject)EditorGUILayout.ObjectField("Projectile", building.projectile, typeof(GameObject), true);
        }
        //Generator Exclusive Fields
        else if (building.buildingSubtype == BuildingSubtype.Generator)
        {
            //building.goldGenRate = EditorGUILayout.FloatField("Gold Gen Rate", building.goldGenRate);
        }

        //Save any changes to this Scriptable Object
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(building);
            AssetDatabase.SaveAssets();
            Debug.Log(building.name + " Saved");
        }
    }
}
