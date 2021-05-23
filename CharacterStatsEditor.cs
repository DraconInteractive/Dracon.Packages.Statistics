namespace DI_Statistics
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
    [CustomEditor(typeof(CharacterStats))]
    [CanEditMultipleObjects]
    public class CharacterStatsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            CharacterStats myTarget = (CharacterStats)target;

            myTarget.characterIdentifier = EditorGUILayout.TextField("ID: ", myTarget.characterIdentifier);

            EditorGUILayout.Space();

            DrawCharacterStats(myTarget, serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        public static void DrawCharacterStats(CharacterStats myTarget, SerializedObject serializedObject)
        {
            myTarget.damage = EditorGUILayout.FloatField("Damage: ", myTarget.damage);
            myTarget.armour = EditorGUILayout.FloatField("Armour: ", myTarget.armour);

            Vector4 resourceStats = EditorGUILayout.Vector4Field("Resources Max (HMS): ", new Vector3(myTarget.maxHealth, myTarget.maxMana, myTarget.maxStamina, myTarget.maxResource));
            myTarget.maxHealth = resourceStats.x;
            myTarget.maxMana = resourceStats.y;
            myTarget.maxStamina = resourceStats.z;
            myTarget.maxResource = resourceStats.z;

            Vector3 sdc = EditorGUILayout.Vector3Field("STR, DEX, CON: ", new Vector3(myTarget.STR, myTarget.DEX, myTarget.CON));
            myTarget.STR = sdc.x;
            myTarget.DEX = sdc.y;
            myTarget.CON = sdc.z;

            Vector3 iww = EditorGUILayout.Vector4Field("INT, WIS, WILL, LUCK: ", new Vector4(myTarget.INT, myTarget.WIS, myTarget.WILL, myTarget.LUCK));
            myTarget.INT = iww.x;
            myTarget.WIS = iww.y;
            myTarget.WILL = iww.z;

            SerializedProperty awarenessProp = serializedObject.FindProperty("awareness");
            EditorGUILayout.PropertyField(awarenessProp);

            SerializedProperty stealthDetProp = serializedObject.FindProperty("stealthDetection");
            EditorGUILayout.PropertyField(stealthDetProp);

            SerializedProperty viewDistProp = serializedObject.FindProperty("viewDistance");
            EditorGUILayout.PropertyField(viewDistProp);
        }
    }
}
