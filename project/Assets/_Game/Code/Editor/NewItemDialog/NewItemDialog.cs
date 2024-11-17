using System;
using System.Collections.Generic;
using System.IO;
using PixelWizards.Controllers;
using PixelWizards.DTO;
using PixelWizards.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace PixelWizards.EditorTools
{
    public class NewItemWindow : EditorWindow
    {
        private static Vector2 minWindowSize = new Vector2(350, 100);
        private static Vector2 scrollPosition = Vector2.zero;
        private const int dragDropWidth = 750;
        private const int dragDropHeight = 150;
        private static WeaponConfig _thisWeapon = new();
        private static GameData gameData;
        private static List<WeaponConfig> objectList = new();
        private static GUIStyle log;

        /// <summary>
        /// Opens the Editor Window
        /// </summary>
        [MenuItem("Tools/New Item Wizard")]
        public static void ShowWindow()
        {
            var thisWindow = GetWindow<NewItemWindow>(false, "Create New Item", true);
            thisWindow.minSize = minWindowSize;
            thisWindow.Reset();
        }

        private void OnEnable()
        {
            Reset();
        }

        private void Reset()
        {
            var assets = AssetDatabase.FindAssets("t:GameData");
            var path = AssetDatabase.GUIDToAssetPath(assets[0]);
            gameData = (GameData)AssetDatabase.LoadAssetAtPath(path, typeof(GameData));
            if (gameData == null)
            {
                Debug.LogError("No GameData found");
            }
        }

        private void OnGUI()
        {
            log = new GUIStyle(EditorStyles.label);
            log.richText = true;
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(5);
                GUILayout.BeginVertical();
                {
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
                    {
                        EditorGUI.BeginChangeCheck();
                        GUILayout.Label("Create new Item", EditorStyles.boldLabel);
                        GUILayout.Space(10f);
                        
                        // show the users which database we are populating
                        EditorGUILayout.ObjectField("Game Database (readonly):" , gameData, typeof(GameData), false);
                        
                        GUILayout.Space(10f);
                        
                        _thisWeapon.name = EditorGUILayout.TextField("Name", _thisWeapon.name);
                        GUILayout.BeginHorizontal();
                        {
                            _thisWeapon.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab: " , _thisWeapon.prefab, typeof(GameObject), true, GUILayout.ExpandWidth(true));
                            if (_thisWeapon.prefab)
                            {
                                if (GUILayout.Button("Add New", EditorStyles.miniButton, GUILayout.Width(150f)))
                                {
                                    var config = new WeaponConfig()
                                    {
                                        name = _thisWeapon.prefab.name,
                                        prefab = _thisWeapon.prefab
                                    };
                                    objectList.Add(config);
                                    _thisWeapon = new();
                                }
                            }
                        }
                        GUILayout.EndHorizontal();
                        
                        // let the users drop objects so they can bulk edit
                        var meshes = DropZone("Drag and Drop Objects Here", dragDropWidth, dragDropHeight);
                        if (meshes != null)
                        {
                            foreach (Object entry in meshes)
                            {
                                if (entry is not GameObject) continue;
                                
                                var newItem = new WeaponConfig()
                                {
                                    prefab = (GameObject)entry,
                                    name = entry.name
                                };
                                objectList.Add(newItem);
                            }
                        }
                        EditorGUI.EndChangeCheck();
                        
                        foreach (var item in objectList)
                        {
                            GUILayout.BeginVertical();
                            {
                                GUILayout.Label("\t<color=green>" + item.name + "</color>", log);
                            }
                            GUILayout.EndVertical();
                        }
                    }
                    GUILayout.EndScrollView();
                    
                    if ( objectList.Count > 0)
                    {
                        if (GUILayout.Button("Create Prefabs & Add to Metadata", GUILayout.Width(350f), GUILayout.Height(35f)))
                        {
                            CreatePrefabs( objectList);
                        }
                    }
                    else
                    {
                        GUILayout.Label("Drag & Drop items to add to Metadata");
                    }
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            
            GUILayout.Space(35f);
        }

        private void CreatePrefabs(List<WeaponConfig> configList)
        {
            foreach (var entry in configList)
            {
                CreateItemPrefab(entry);
            }
            
            // reset 
            _thisWeapon = new();
            objectList.Clear();
        }

        private void CreateItemPrefab(WeaponConfig weaponConfig)
        {
            if (!Directory.Exists("Assets/Prefabs"))
            {
                AssetDatabase.CreateFolder("Assets", "Prefabs");
            }
            string localPath = "Assets/_Game/GameData/Prefabs/" + weaponConfig.name + ".prefab";

            // create a new gameobject with the item name 
            var newGO = new GameObject
            {
                name = weaponConfig.name
            };
            
            // make sure that the config entry has a name
            weaponConfig.name = weaponConfig.prefab.name;
            
            // add the item prefab as a child of the new game object
            var itemInstance = Instantiate(weaponConfig.prefab, newGO.transform);
            // clean up the instanced item name
            itemInstance.name.Replace("(Clone)", "").Trim();
            
            // and add our itemController script to it
            newGO.AddComponent<WeaponMetadataController>();
            
            // Make sure the file name is unique, in case an existing Prefab has the same name.
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

            // Create the new Prefab and log whether Prefab was saved successfully.
            bool prefabSuccess;
            var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(newGO, localPath, InteractionMode.UserAction, out prefabSuccess);
            if (prefabSuccess == true)
            {
                DestroyImmediate(newGO);
                Debug.Log("Prefab: " + prefab.name + " was saved successfully");

                // save the item in the database
                gameData.AddItem(new WeaponConfig()
                {
                    name = weaponConfig.name,
                    prefab = prefab,
                });
                
            }
            else
            {
                Debug.Log("Prefab failed to save" + prefabSuccess);
            }
        }
        
        /// <summary>
        /// Returns a list of objects that were dropped on us
        /// </summary>
        /// <param name="title">label to display</param>
        /// <param name="w">width of the drop zone</param>
        /// <param name="h">height of the drop zone</param>
        /// <returns>list of objects that were dropped</returns>
        private static object[] DropZone(string title, int w, int h)
        {
        	GUILayout.Box(title, GUILayout.Width(w), GUILayout.Height(h));

        	EventType eventType = Event.current.type;
        	bool isAccepted = false;

        	if (eventType != EventType.DragUpdated && eventType != EventType.DragPerform)
        		return isAccepted ? DragAndDrop.objectReferences : null;

        	DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

        	if (eventType == EventType.DragPerform) {
        		DragAndDrop.AcceptDrag();
        		isAccepted = true;
        	}
        	Event.current.Use();

        	if (isAccepted)
        	{
        		Debug.Log("Drag and Drop complete: " + DragAndDrop.objectReferences.Length + " objects dropped!");
        	}

        	return isAccepted ? DragAndDrop.objectReferences : null;
        }
    }
}