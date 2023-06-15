using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.System_Gem;
using nyy.FG_Case.SystemPool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace nyy.FG_Case.System_Grid
{
    public class Tile : MonoBehaviour
    {
        #region PROPERTIES

        [Title("Runtime Set")]
        public RuntimeSet<Gem> SpawnedGemForPool;
        
        [BoxGroup("Pool")] 
        public IObjectPool<Gem> GemObjectPool;
        [SerializeField] private GemPool _gemPool;

        [TabGroup("Settings")] 
        [SerializeField] private Gem currentPlantedGem;
        public bool planted = false;

        #endregion

        #region EVENT FUNCTIONS
        
        private void Start()
        {
            if (_gemPool == null) _gemPool = FindObjectOfType<GemPool>();
            
            GemObjectPool = _gemPool.GemObjectPool;
            
            Plant();
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS

        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        private void Plant()
        {
            GemObjectPool = _gemPool.GemObjectPool;
            
            var gemClone = GemObjectPool.Get();
            
            currentPlantedGem = gemClone;
            
            currentPlantedGem.transform.parent = transform;
            currentPlantedGem.transform.localPosition = Vector3.zero;
            currentPlantedGem.transform.localScale = new Vector3(0f, 0f, 0f);
            
            currentPlantedGem.gameObject.SetActive(true);
                    
            SpawnedGemForPool.Remove(currentPlantedGem);
            
            planted = true;
            
            currentPlantedGem.canGrow = true;
        }
        
        #endregion
    }
}