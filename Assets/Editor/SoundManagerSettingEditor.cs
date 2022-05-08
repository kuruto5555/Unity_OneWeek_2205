using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace BTLGeek.Manager
{
    [CustomEditor(typeof(SoundManagerSetting))]
    public class SoundManagerSettingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            ShowGUISkin(ShowInspectorGUI);
            serializedObject.ApplyModifiedProperties();
        }

        private void ShowInspectorGUI()
        {
            ShowGUISkin(() =>
            {
                ShowPropertyField("volume_", "Volume", "マスタ音量");
                ShowPropertyField("bgm_volume_", "BGM Volume", "BGMの基準音量");
                ShowPropertyField("se_volume_", "SE Volume", "SEの基準音量");
            });
            EditorGUILayout.Space();

        }

        /// <summary>
        /// プロパティを変更するGUIを表示
        /// </summary>
        /// <param name="property_name"></param>
        /// <param name="property_display_name"></param>
        /// <param name="summary"></param>
        /// <returns></returns>
        private SerializedProperty ShowPropertyField(string property_name, string property_display_name, string summary)
        {
            var property = serializedObject.FindProperty(property_name);
            EditorGUILayout.PropertyField(property, new GUIContent(property_display_name));
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField(summary);
            EditorGUI.indentLevel--;
            return property;
        }

        /// <summary>
        /// GUIスキンで挟んで表示
        /// </summary>
        /// <param name="show_gui_action"></param>
        private static void ShowGUISkin(Action show_gui_action)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            show_gui_action();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }
    }
}
