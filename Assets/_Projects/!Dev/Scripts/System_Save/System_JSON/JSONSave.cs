using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using GenericScriptableArchitecture;
using nyy.FG_Case.ReferenceValue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace nyy.FG_Case.System_Save
{
    public class JSONSave : MonoBehaviour, IEventListener
    {
        #region PROPERTIES

        [Title("Objects to Save")] public IntRef PlayerCoin;

        [Title("Events")] public ScriptableEvent SavePlayerCoin;

        #endregion

        #region EVENT FUNCTIONS
        
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            SavePlayerCoin += this;
        }

        private void OnDisable()
        {
            SavePlayerCoin -= this;
        }

        private void Start()
        {
            LoadData();
        }
        
        #endregion

        #region IMPLEMENTED FUNCTIONS
        
        public void OnEventInvoked()
        {
            SaveData();
        }
        
        #endregion

        #region PUBLIC FUNCTIONS

        #endregion

        #region PRIVATE FUNCTIONS

        private void SaveData()
        {
            if (!IsSaveFile())
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
            }

            if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/game_save/game_data");
            }

            Debug.Log("Game Saved");

            var json = JsonUtility.ToJson(PlayerCoin);

            File.WriteAllText(Application.persistentDataPath + "/game_save/game_data/player_coin.txt", json);
        }

        private void LoadData()
        {
            if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data"))
            {
                SaveData();
            }

            var bf = new BinaryFormatter();

            if (File.Exists(Application.persistentDataPath + "/game_save/game_data/player_coin.txt"))
            {
                var file = File.ReadAllText(Application.persistentDataPath + "/game_save/game_data/player_coin.txt");
                JsonUtility.FromJsonOverwrite((string)file, PlayerCoin);
            }
        }

        private bool IsSaveFile()
        {
            return Directory.Exists(Application.persistentDataPath + "player_coin");
        }

        #endregion
    }
}