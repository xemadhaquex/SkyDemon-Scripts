using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class OptionsBackButton : MonoBehaviour
{
    public GameObject Player;
    public GameObject MusicPlayer;
    public GameObject SenseSliderGO;
    public GameObject EffectsSoundSliderGO;
    public GameObject MusicSliderGO;
    public GameObject crosshair;



    public void BackToGame(){
        Time.timeScale = 1;
        crosshair.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);

        

    }
    public void ChangeMouseSensitivity(){

        Player.GetComponent<PlayerMovement>().lookSpeed=(SenseSliderGO.GetComponent<Slider>().value)*4f;

    }
    public void ChangeSoundEffectsVolume(){
        Player.GetComponent<AudioSource>().volume=EffectsSoundSliderGO.GetComponent<Slider>().value;

    }
    public void ChangeMusicVolume(){
        MusicPlayer.GetComponent<AudioSource>().volume=(MusicSliderGO.GetComponent<Slider>().value)*.4f;

    }
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
