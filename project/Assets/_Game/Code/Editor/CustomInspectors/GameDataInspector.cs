using System.Text;
using PixelWizards.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace PixelWizards.CustomInspectors
{
    [CustomEditor(typeof(GameData))]
    public class GameDataInspector : Editor
    {
        GameData gameData;
        private void OnEnable()
        {
             gameData = (GameData)target;
        }
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!GUILayout.Button("Export Object List", GUILayout.ExpandWidth(true), GUILayout.Height(35f))) return;
            
            StringBuilder sb = new();
            foreach (var item in gameData.weaponEntries)
            {
                sb.AppendLine(item.name);
                    
            }
            Debug.Log("Item list:\n\n" + sb.ToString());
        }
    }
}