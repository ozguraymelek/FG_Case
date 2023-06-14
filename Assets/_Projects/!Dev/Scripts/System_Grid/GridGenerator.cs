using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace nyy.FG_Case.System_Grid
{
    public class GridGenerator : MonoBehaviour
    {
        #region PROPERTIES
        
        [InfoBox("List of objects spawned on stage via editor")] 
        [BoxGroup][DisableIn(PrefabKind.All)]
        public List<Tile> spawnedTilesList;
        
        [TabGroup("Components")]
        [SerializeField] private TileSpawner tileSpawner;
        
        [TabGroup("Properties")]
        [Title("axis X")]
        public int line;
        
        [TabGroup("Properties")]
        [Title("axis Z")]
        public int column;
        
        [TabGroup("Properties")]
        [Title("Distance between tiles")]
        [InfoBox("Must be at least the scale of tile prefab, otherwise tile objects will be nested.",
            InfoMessageType.Warning)]
        public float gridSpacingOffset;
        
        #endregion
                
        #region EVENT FUNCTIONS
        
        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        [Button]
        private void SpawnGrids()
        {
            tileSpawner.CreateGrids(spawnedTilesList, line, column,
                gridSpacingOffset, transform.position, transform);
        }
        
        [Button]
        private void DeleteGrids()
        {
            foreach (var grid in spawnedTilesList)
            {
                DestroyImmediate(grid.gameObject);
            }
            spawnedTilesList.Clear();
        }
        
        #endregion
    }

    [Serializable]
    public class TileSpawner
    {
        [Title("Properties")]
        [BoxGroup]
        public Tile tilePrefab;

        #region PUBLIC FUNCTIONS

        public void CreateGrids(List<Tile> spawnedList, int gridX, int gridZ, float gridSpacingOffset, Vector3 gridOrigin, Transform parent)
        {
            for (int x = 0; x < gridX; x++)
            {
                for (int z = 0; z < gridZ; z++)
                {
                    var setPosition = new Vector3(x * gridSpacingOffset, 0, z * gridSpacingOffset) + gridOrigin;
                    
                    var gridClone = Object.Instantiate(tilePrefab, setPosition, Quaternion.identity);
                    
                    gridClone.transform.parent = parent;

                    // gridClone.placed = true;

                    spawnedList.Add(gridClone);
                }
            }
        }

        #endregion
    }
}