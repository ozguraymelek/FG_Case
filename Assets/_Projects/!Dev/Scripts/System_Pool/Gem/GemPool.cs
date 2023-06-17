using System;
using GenericScriptableArchitecture;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace nyy.FG_Case.System_Pool
{
    public class GemPool : MonoBehaviour , IEventListener<Gem>
    {
        #region PROPERTIES
        
        [BoxGroup("Data")]
        public GemObjectData GemData;
        
        [Title("Pool")]
        public IObjectPool<Gem> GemObjectPool;
        public int poolCapacity;
        
        [Title("Events")] 
        public ScriptableEvent<Gem> OnGemSold;
        
        [Title("Runtime Set")]
        public RuntimeSet<Gem> SpawnedGemForPool;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void Awake()
        {
            GemObjectPool = new ObjectPool<Gem>(OnSpawn, OnGet, OnRelease);
        }

        public void OnEnable()
        {
            OnGemSold += this;
        }
        
        public void OnDisable()
        {
            OnGemSold -= this;
            SpawnedGemForPool.Clear();
        }
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS

        
        
        public void OnEventInvoked(Gem argument)
        {
            ReleaseGemProcess(argument);
        }
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        [Button]
        private Gem OnSpawn()
        {
            var rand = Random.Range(0, GemData.GemDataList.Count);
            var gemClone = Instantiate(GemData.GemDataList[rand].Prefab,transform);
            gemClone.transform.localScale = Vector3.zero;
            
            SpawnedGemForPool.Add(gemClone);
            
            return gemClone;
        }
        
        private void OnGet(Gem obj)
        {
            obj.gameObject.SetActive(false);
        }
        
        private void OnRelease(Gem obj)
        {
            obj.isStacked = false;
            obj.gameObject.SetActive(false);
            
            obj.transform.parent = transform;
            obj.transform.localScale = Vector3.zero;
            obj.transform.localPosition = Vector3.zero;

            // obj.boxCollider.enabled = true;
        }
        
        private void ReleaseGemProcess(Gem gem)
        {
            GemObjectPool.Release(gem);
            SpawnedGemForPool.Add(gem);
        }
        
        #endregion
    }
}