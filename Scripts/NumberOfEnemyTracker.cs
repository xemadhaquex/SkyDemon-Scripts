using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumberOfEnemyTracker : MonoBehaviour
{
    public TextMeshProUGUI enemiesRemainingText;
    public TextMeshProUGUI addedHealthText;
    public TextMeshProUGUI addedAxesText;
    public TextMeshProUGUI timerText;
    public GameObject Timer;
    public int numEnemiesKilled=0;
    public float score;

    public TextMeshProUGUI levelText;

    GameObject[] Enemies;
    public int numberOfEnemies;
    [SerializeField] private GameObject StagePart2;
    [SerializeField] private GameObject EnemiesPart2;
    [SerializeField] private GameObject StagePart3;
    [SerializeField] private GameObject EnemiesPart3;
    [SerializeField] private GameObject StagePart4;
    [SerializeField] private GameObject EnemiesPart4;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject addedHealthGO;
    [SerializeField] private GameObject addedAxesGO;

    private bool hasStage2Spawned=false;
    private bool hasStage3Spawned=true; //set true until stage 2 spawns
    private bool hasStage3Finished=false;

    private bool stage4ready=false;

    float minutesComplete;
    float secondsComplete;

    //private bool hasStage3Spawned=false;
    // Start is called before the first frame update
    void Start()
    {
        
        Enemies= GameObject.FindGameObjectsWithTag("Enemy");
        numberOfEnemies=Enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        enemiesRemainingText.text=(numberOfEnemies.ToString() + " Enemies");

        if((numberOfEnemies<=0)==true && hasStage2Spawned==false){
            spawnStage2();
            int addhealth=0;
            StartCoroutine(addedHealthOrAxeTimed(addhealth,7));
            //Enemies= GameObject.FindGameObjectsWithTag("Enemy");
            //numberOfEnemies=Enemies.Length;
            numberOfEnemies=7;
            hasStage2Spawned=true;
            hasStage3Spawned=false;
            

        }
        if((numberOfEnemies<=0)==true && hasStage3Spawned==false){
            spawnStage3();
            int addhealth=0;
            StartCoroutine(addedHealthOrAxeTimed(addhealth,5));
            //Enemies= GameObject.FindGameObjectsWithTag("Enemy");
            //numberOfEnemies=Enemies.Length;
            numberOfEnemies=6;
            hasStage3Spawned=true;

        }

        if((numberOfEnemies<=0)==true && hasStage3Spawned==true && hasStage3Finished==false){

            spawnStage4();
            int addhealth=0;
            StartCoroutine(addedHealthOrAxeTimed(addhealth,9));

            numberOfEnemies=8;
            hasStage3Finished=true;
            
            stage4ready=true;


        }
        if((numberOfEnemies<=0)==true && stage4ready==true && hasStage3Finished==true){

            minutesComplete = (timerText.GetComponent<Timer>().t/60);
            secondsComplete = ((timerText.GetComponent<Timer>().t)%60);

            score=(numEnemiesKilled*30)-(minutesComplete*50)-(secondsComplete*2);

            //Debug.Log("Level Beat" + timerText.GetComponent<Timer>().t);
            Timer.SetActive(false);

            levelText.text="You took over Heaven in " + minutesComplete.ToString("f0") + ":" + secondsComplete.ToString("f2") + "! Your Score is " + score;



        }

    }
    void spawnStage2(){
        StagePart2.SetActive(true);
        EnemiesPart2.SetActive(true);
        hasStage2Spawned=true;

    }
    void spawnStage3(){
        StagePart3.SetActive(true);
        EnemiesPart3.SetActive(true);
        hasStage3Spawned=true;

    }

    void spawnStage4(){
        StagePart4.SetActive(true);
        EnemiesPart4.SetActive(true);

    }
    public void gameOverDeath(){
        minutesComplete = (timerText.GetComponent<Timer>().t/60);
        secondsComplete = ((timerText.GetComponent<Timer>().t)%60);

        score=(numEnemiesKilled*30)-(minutesComplete*50)-(secondsComplete*2);

        //Debug.Log("Level Beat" + timerText.GetComponent<Timer>().t);
        levelText.text="You died after " + (Mathf.Floor(minutesComplete)).ToString("f0") + ":" + secondsComplete.ToString("f2") + ". Your Score is " + score;
        hasStage3Finished=true;
        Timer.SetActive(false);

    }

    public void gameOverAxes(){
        minutesComplete = (timerText.GetComponent<Timer>().t/60);
        secondsComplete = ((timerText.GetComponent<Timer>().t)%60);

        score=(numEnemiesKilled*30)-(minutesComplete*50)-(secondsComplete*2);

        //Debug.Log("Level Beat" + timerText.GetComponent<Timer>().t);
        levelText.text="You ran out of axes after " + (Mathf.Floor(minutesComplete)).ToString("f0") + ":" + secondsComplete.ToString("f2") + ". Your Score is " + score;
        hasStage3Finished=true;
        Timer.SetActive(false);

    }

    private IEnumerator addedHealthOrAxeTimed(int addedHealth, int addedAxes){
        Player.GetComponent<Raycast_AxeThrow>().throwsRemaining = Player.GetComponent<Raycast_AxeThrow>().throwsRemaining+addedAxes;
        Player.GetComponent<PlayerMovement>().health = Player.GetComponent<PlayerMovement>().health+addedHealth;
        addedHealthText.text= (addedHealth.ToString());
        addedAxesText.text= ("+ "+ addedAxes.ToString());
        addedHealthGO.SetActive(true);
        addedAxesGO.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        addedHealthGO.SetActive(false);
        addedAxesGO.SetActive(false);
    }
}
