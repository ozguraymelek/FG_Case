using System.Collections.Generic;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace nyy.FG_Case
{
    public class GemGenerator : GlobalConfig<GemGenerator>
    {
        #region PROPERTIES

        [BoxGroup("Gem Data")] 
        public GemObjectData GemData;

        private List<Gem> _createdGems = new List<Gem>();

        [Title("Properties")] [TabGroup("Reference")] 
        public bool setReferencePrefab = false;
        
        [field: SerializeField] 
        [TabGroup("Reference")] [EnableIf("setReferencePrefab")][Indent(3)]
        public GameObject ReferencePrefab;

        [field: SerializeField] [TabGroup("A","Gem Characteristics")]
        public string Name;

        [field: SerializeField]  [TabGroup("A","Gem Characteristics")]
        public int StartingSalePrice;

        [field: SerializeField]  [TabGroup("A","Gem Characteristics")]
        public Sprite Icon;
        
        [TabGroup("A","Gem Characteristics")] public bool Emission = false;
        [field: SerializeField] 
        [TabGroup("A","Gem Characteristics")] [ShowIf("Emission")][Indent(3)]
        [ColorUsage(showAlpha: true, true)]
        public Color ColorHDR;
        
        [field: SerializeField] 
        [TabGroup("A","Gem Characteristics")][HideIf("Emission")][Indent(3)]
        public Color Color;
        
        [TabGroup("A/Paths","Locations")]
        public bool hidePaths = true;
        [field: SerializeField]
        [DisableIf("hidePaths")]
        [TabGroup("A/Paths","Locations")][FolderPath][Indent(3)]
        public string MaterialPath;

        [field: SerializeField] [DisableIf("hidePaths")] [TabGroup("A/Paths", "Locations")] [FolderPath] [Indent(3)]
        public string GemPath;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        [Button, GUIColor(.2f,.7f,.1f)]
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
            
            AssetDatabase.CreateAsset(newMaterial, $"{MaterialPath}/{Name}.mat");
            AssetDatabase.SaveAssets();
            
            AssetDatabase.Refresh();
            
            _createdGems.Add(newGameObject.GetComponent<Gem>());
        }

        [Button, GUIColor(.85f,.5f,.7f)]
        public void DeleteAllGem()
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
        #endregion
    }
}