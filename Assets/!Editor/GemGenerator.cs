#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using GenericScriptableArchitecture;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using nyy.FG_Case.System_UI;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace nyy.FG_Case
{
    public class GemGenerator : GlobalConfig<GemGenerator>
    {
        #region PROPERTIES

        [BoxGroup("Gem Data")] 
        public GemObjectData GemData;
        public List<GemListItem> CreatedGemItemListEditor;

        private List<Gem> _createdGems = new List<Gem>();

        [Title("Properties")] [TabGroup("Reference")] 
        public bool setReferencePrefab = false;
        
        [TabGroup("Reference")] [EnableIf("setReferencePrefab")][Indent(3)]
        public GameObject ReferencePrefab;
        
        [TabGroup("Reference")] [EnableIf("setReferencePrefab")][Indent(3)]
        public GemListItem ReferenceListItemPrefab;
        
        [TabGroup("Reference")] [EnableIf("setReferencePrefab")][Indent(3)]
        private Stack _stack;

        [TabGroup("A","Gem Characteristics")]
        public string Name;

        [TabGroup("A","Gem Characteristics")]
        public int StartingSalePrice;

        [TabGroup("A","Gem Characteristics")]
        public Sprite Icon;
        
        [TabGroup("A","Gem Characteristics")] public bool Emission = false;
        [TabGroup("A","Gem Characteristics")] [ShowIf("Emission")][Indent(3)]
        [ColorUsage(showAlpha: true, true)]
        public Color ColorHDR;
        
        [TabGroup("A","Gem Characteristics")][HideIf("Emission")][Indent(3)]
        public Color Color;
        
        [TabGroup("A/Paths","Locations")]
        public bool hidePaths = true;
        [DisableIf("hidePaths")]
        [TabGroup("A/Paths","Locations")][FolderPath][Indent(3)]
        public string MaterialPath;

        [DisableIf("hidePaths")] [TabGroup("A/Paths", "Locations")] [FolderPath] [Indent(3)]
        public string GemPath;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        [Button, GUIColor(.2f,.7f,.1f)]
        [Obsolete("Obsolete")]
        public void CreateNewGem()
        {
            var newPrefab = EditorUtility.CreateEmptyPrefab(GemPath + "/" + Name + ".prefab");
            var newGameObject = EditorUtility.ReplacePrefab(ReferencePrefab, newPrefab);

            var newMaterial = new Material(Shader.Find("Standard"))
            {
                name = Name, 
                color = Emission ? ColorHDR : Color
            };
                
            newGameObject.GetComponentInChildren<Renderer>().sharedMaterial = newMaterial;
            
            SetGemData(newGameObject.GetComponent<Gem>());
            SetGemUI();
            
            PlayerPrefs.DeleteAll();
            
            AssetDatabase.CreateAsset(newMaterial, $"{MaterialPath}/{Name}.mat");
            AssetDatabase.SaveAssets();
            
            AssetDatabase.Refresh();
            
            _createdGems.Add(newGameObject.GetComponent<Gem>());
        }

        [Button, GUIColor(.85f,.5f,.7f)]
        public void DeleteAllGemData()
        {
            string[] allGemsPath = { GemPath };
            string[] allGemMaterialsPath = { MaterialPath };
            
            foreach (var asset in AssetDatabase.FindAssets("", allGemsPath))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }
            
            foreach (var asset in AssetDatabase.FindAssets("", allGemMaterialsPath))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }
            
            GemData.GemDataList.Clear();
            DeleteGemListItems();
        }
        #endregion  
                
        #region PRIVATE FUNCTIONS

        private void SetGemData(Gem gem)
        {
            GemData.GemDataList.Add(new GemProperties()
            {
                Name = Name,
                StartingSalePrice = StartingSalePrice,
                Icon = Icon,
                Prefab = gem
            });

            gem.GemData = GemData;
            gem.GemProperties.Name = Name;
            gem.GemProperties.StartingSalePrice = StartingSalePrice;
            gem.GemProperties.Icon = Icon;
            gem.GemProperties.Prefab = gem;
        }

        private void SetGemUI()
        {
            var displayGemRef = FindObjectOfType<DisplayGem>();
            var listItem = GameObject.Instantiate(ReferenceListItemPrefab, displayGemRef.contentTr);

            listItem.name = Name + " Gem List Item";
            listItem.Type = Name;
            listItem.StartingSalePrice = StartingSalePrice;
            listItem.Icon.sprite = Icon;

            listItem.SetData();
            
            SetGemListItems(listItem);
        }

        private void SetGemListItems(GemListItem gemListItem)
        {
            CreatedGemItemListEditor.Add(gemListItem);
        }

        private void DeleteGemListItems()
        {
            foreach (var gemListItem in CreatedGemItemListEditor)
            {
                DestroyImmediate(gemListItem.gameObject);
            }

            CreatedGemItemListEditor.Clear();
        }
        
        #endregion
    }
}

#endif