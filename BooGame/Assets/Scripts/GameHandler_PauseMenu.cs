using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameHandler_PauseMenu : MonoBehaviour {

        public static bool GameisPaused = false;
        public GameObject pauseMenuUI;
        public AudioMixer mixer;
        public float volumeLevel = 1.0f;
        public Slider sliderVolumeCtrl;

        void Awake (){
                GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
                if (sliderTemp != null){
                        sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
                        sliderVolumeCtrl.value = volumeLevel;
                }
        }

        void Start (){
                //pauseMenuUI = GameObject.FindWithTag("PauseMenuUI");
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }
        
        void OnLevelWasLoaded (){
                //pauseMenuUI = GameObject.FindWithTag("PauseMenuUI") != null ? GameObject.FindWithTag("PauseMenuUI") : pauseMenuUI;
                pauseMenuUI.SetActive(false);
                GameisPaused = false;
        }

        void Update (){
                if (Input.GetKeyDown(KeyCode.Escape)){
                        if (GameisPaused){
                                Resume();
                        }
                        else{
                                Pause();
                        }
                }
                /*if(!GameisPaused){
                        pauseMenuUI = GameObject.FindWithTag("PauseMenuUI") != null ? GameObject.FindWithTag("PauseMenuUI") : pauseMenuUI;
                        pauseMenuUI.SetActive(false);
                }*/
        }

        void Pause(){
                pauseMenuUI.SetActive(true);
                Time.timeScale = 0f;
                GameisPaused = true;
        }

        public void Resume(){
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                GameisPaused = false;
        }

        public void SetLevel (){
                volumeLevel = sliderVolumeCtrl.value;
                mixer.SetFloat("MusicVolume", Mathf.Log10 (volumeLevel) * 20);
        }
}