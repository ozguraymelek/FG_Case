using System;
using System.Collections;
using DG.Tweening;
using GenericScriptableArchitecture;
using nyy.FG_Case.PlayerSc;
using nyy.FG_Case.ReferenceValue;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using nyy.FG_Case.SystemPool;
using Sirenix.OdinInspector;
using UniRx;
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

        public GemObjectData GemData;
        
        [BoxGroup("Pool")] 
        public IObjectPool<Gem> GemObjectPool;
        [SerializeField] private GemPool _gemPool;

        [TabGroup("Settings")] 
        [SerializeField] private Gem currentPlantedGem;
        public bool planted;
        public float plantDelay;

        #endregion

        #region EVENT FUNCTIONS
        
        private IEnumerator Start()
        {
            if (_gemPool == null) _gemPool = FindObjectOfType<GemPool>();
            
            GemObjectPool = _gemPool.GemObjectPool;
            
            planted = false;
            
            StartCoroutine(Plant());
            
            yield return null;
            
            
        }

        private void Update()
        {
            print(planted);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player) == false) return;

            if (currentPlantedGem.isStacked)
            {
                DOVirtual.DelayedCall(plantDelay, () =>
                {
                    planted = false;
                    StartCoroutine(Plant());
                });
                
            }
        }

        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        private IEnumerator Plant()
        {
            yield return new WaitForSeconds(1f);
            
            currentPlantedGem = null;
            
            GemObjectPool = _gemPool.GemObjectPool;
            
            var gemClone = GemObjectPool.Get();
            
            currentPlantedGem = gemClone;
            
            currentPlantedGem.transform.parent = transform;
            currentPlantedGem.transform.localPosition = Vector3.zero;
            currentPlantedGem.transform.localScale = new Vector3(0f, 0f, 0f);
            
            currentPlantedGem.gameObject.SetActive(true);
                    
            SpawnedGemForPool.Remove(currentPlantedGem);
            currentPlantedGem.canGrow = true;
            
            planted = true;
        }
        
        #endregion
    }
}