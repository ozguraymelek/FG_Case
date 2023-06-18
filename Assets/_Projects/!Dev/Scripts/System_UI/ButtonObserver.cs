using System;
using GenericScriptableArchitecture;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace nyy.FG_Case.System_UI
{
    public class ButtonObserver : MonoBehaviour
    {
        #region PROPERTIES
        
        [Title("Data")] 
        public PlayerData PlayerData;
        
        [Title("Text UI Components")] 
        public TMP_Text textStatus;

        [Title("Sound UI Components")] 
        public Image soundButtonImage;
        
        [Title("Sound Sprite Components")] 
        public Sprite soundOnSprite;
        public Sprite soundOffSprite;
        
        [Title("Events")] public ScriptableEvent SavePlayerData;
        
        #endregion
                
        #region EVENT FUNCTIONS

        private void Start()
        {
            Time.timeScale = 0;

            if (PlayerData.soundEffects)
                soundButtonImage.sprite = soundOnSprite;
            else
                soundButtonImage.sprite = soundOffSprite;
        }

        #endregion
                
        #region IMPLEMENTED FUNCTIONS
        
        #endregion  
        
        #region PUBLIC FUNCTIONS

        public void Button_Play()
        {
            Time.timeScale = 1;
        }
        
        public void Button_Pause()
        {
            if (Time.timeScale != 0)
            {
                textStatus.gameObject.SetActive(true);
                textStatus.text = "PAUSED";
                Time.timeScale = 0;
            }
            else
            {
                textStatus.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }

        public void Button_Sound()
        {
            if (PlayerData.soundEffects)
            {
                PlayerData.soundEffects = !PlayerData.soundEffects;
                soundButtonImage.sprite = soundOffSprite;
            }
            
            else
            {
                PlayerData.soundEffects = !PlayerData.soundEffects;
                soundButtonImage.sprite = soundOnSprite;
            }
            
            SavePlayerData.Invoke();
        }
        
        #endregion  
                
        #region PRIVATE FUNCTIONS
        
        #endregion
    }
}