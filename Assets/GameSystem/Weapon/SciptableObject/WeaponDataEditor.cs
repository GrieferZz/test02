using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(WeaponStates_SO))]
public class WeaponDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 强制转换为WeaponData对象
        WeaponStates_SO weaponStates = (WeaponStates_SO)target;
        weaponStates.weaponType = (WeaponStates_SO.WeaponType)EditorGUILayout.EnumPopup("Weapon Type", weaponStates.weaponType);

        // 根据武器类型显示不同的武器数据变量
        switch (weaponStates.weaponType)
        {
            case WeaponStates_SO.WeaponType.Follow:
                weaponStates.attackMultiplier = EditorGUILayout.FloatField("Attack Multiplier", weaponStates.attackMultiplier);
                weaponStates.shootInterval = EditorGUILayout.FloatField("Shoot Interval", weaponStates.shootInterval);
                weaponStates.flightSpeed = EditorGUILayout.FloatField("Flight Speed", weaponStates.flightSpeed);
                break;
            case WeaponStates_SO.WeaponType.Track:
                weaponStates.attackMultiplier = EditorGUILayout.FloatField("Attack Multiplier", weaponStates.attackMultiplier);
                weaponStates.shootInterval = EditorGUILayout.FloatField("Shoot Interval", weaponStates.shootInterval);
                weaponStates.flightSpeed = EditorGUILayout.FloatField("Flight Speed", weaponStates.flightSpeed);
                weaponStates.steerSpeed = EditorGUILayout.FloatField("Steer Speed", weaponStates.steerSpeed);
                weaponStates.trackRadius = EditorGUILayout.FloatField("Track Radius", weaponStates.trackRadius);
                break;
            case WeaponStates_SO.WeaponType.Sticky:
                SerializedProperty attackMultipliersProperty = serializedObject.FindProperty("attackMultipliers");
                EditorGUILayout.PropertyField(attackMultipliersProperty, true);
               

                // 应用所有修改
               
                weaponStates.shootInterval = EditorGUILayout.FloatField("Shoot Interval", weaponStates.shootInterval);
                weaponStates.flightSpeed = EditorGUILayout.FloatField("Flight Speed", weaponStates.flightSpeed);
                weaponStates.maxLayers = EditorGUILayout.IntField("Max Layers", weaponStates.maxLayers);
                SerializedProperty explosionRangeProperty = serializedObject.FindProperty("explosionRange");
                EditorGUILayout.PropertyField(explosionRangeProperty, true);
                serializedObject.ApplyModifiedProperties();
                break;
        }
        
    }
}
    
