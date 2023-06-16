using System.Collections;
using GenericScriptableArchitecture;
using nyy.FG_Case.System_Data;
using nyy.FG_Case.System_Gem;
using nyy.FG_Case.System_Pool;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Pool;

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

        public LayerMask mask;
        
        #endregion

        #region EVENT FUNCTIONS

        private void Start()
        {
            if (_gemPool == null) _gemPool = FindObjectOfType<GemPool>();
            
            GemObjectPool = _gemPool.GemObjectPool;
            
            planted = false;
            
            this.ObserveEveryValueChanged(_ => planted).Where(_ => planted == false)
            .Subscribe(unit =>
            {
                StartCoroutine(Plant());
            });
        }

        // private void Start()
        // {
        //     if (_gemPool == null) _gemPool = FindObjectOfType<GemPool>();
        //     
        //     GemObjectPool = _gemPool.GemObjectPool;
        //     
        //     planted = false;
        //     
        //     
        //     this.ObserveEveryValueChanged(_ => planted).Where(_ => planted == false)
        //         .Subscribe(unit =>
        //         {
        //             StartCoroutine(Plant());
        //         });
        // }
        //
        // private void OnTriggerExit(Collider other)
        // {
        //     if (other.TryGetComponent(out Player player) == false) return;
        //
        //     if (currentPlantedGem.isStacked)
        //     {
        //         planted = false;
        //     }
        // }
        
        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        #endregion

        #region PUBLIC FUNCTIONS

        private void FixedUpdate()
        {
            if (currentPlantedGem == null) return;
            
            if (Physics.Raycast(transform.position, transform.up, Vector3.one.y,mask) == false)
            {
                if(currentPlantedGem.isStacked)
                    planted = false;
            }
        }

        #endregion

        #region PRIVATE FUNCTIONS

        private IEnumerator Plant()
        {
            yield return new WaitForSeconds(1f);
            
            currentPlantedGem = null;
            
            GemObjectPool = _gemPool.GemObjectPool;
            
            // var rand = Random.Range(0, GemData.GemDataList.Count);
            // var gemClone = Instantiate(GemData.GemDataList[rand].Prefab,transform);
            
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

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position,transform.up);
        }
    }
}