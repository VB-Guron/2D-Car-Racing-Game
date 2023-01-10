using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using TMPro;

public class PositionHandler : MonoBehaviour
{

    public AudioSource audioSource;
    public GameObject TryAgainButton;

    public GameObject NextLevelButton;
    public TextMeshProUGUI countdown;
    public GameObject countdownPanel;
    public GameObject Enemy;
    Stopwatch stopwatch = new Stopwatch();

    public Text timer , testComparison, enemyBanner;

    bool playerDone = false;
    bool enemyDone = false;

    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    // Start is called before the first frame update
    void Start()
    {
        
        AudioListener.pause=false;
        StartCoroutine(countingDown ());
        
       
        
        //Get all Car lap counters in the scene. 
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //Store the lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        //Hook up the pased checkpoint event
        foreach (CarLapCounter lapCounters in carLapCounters)
            lapCounters.OnPassCheckpoint += OnPassCheckpoint;
    }

    void Update()
    {
        timer.text = string.Format("{0:mm\\:ss\\:ff}", stopwatch.Elapsed);
    }


    IEnumerator countingDown(){



        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        countdown.text = ("3");
        yield return new WaitForSecondsRealtime(1);
        countdown.text = ("2");
        yield return new WaitForSecondsRealtime(1);
        countdown.text = ("1");
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        countdown.text = ("GO!");
        stopwatch.Start();

        timer.text = string.Format("{0:mm\\:ss\\:ff}", stopwatch.Elapsed);
        
        yield return new WaitForSecondsRealtime(1);
        countdown.text = ("");
        countdownPanel.SetActive(false);
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {   

        Enemy = GameObject.Find("Enemy");

        // GAME MONITOR IF ALL CARS HAVE FINISHED ALL THE LAPS
        if(carLapCounter.gameObject.name == "Player"){
            
            UnityEngine.Debug.Log("Player: " + playerDone);
            playerDone = carLapCounter.statusChange();
            UnityEngine.Debug.Log("Player: " +playerDone);
        }

        if(carLapCounter.gameObject.name == "Enemy"){
            
            UnityEngine.Debug.Log("Enemy: " + enemyDone);
            enemyDone = carLapCounter.statusChange();
            UnityEngine.Debug.Log("Enemy: " + enemyDone);
        }

                    // Banner if you win or lose
                    if(playerDone && enemyDone){
                        AudioListener.pause=true;
                        testComparison.text = "~ You Lose ~";
                        
                        NextLevelButton.SetActive(false);
                        TryAgainButton.SetActive(true);
                        stopwatch.Stop();
                        
                        Time.timeScale = 0;
                    }else if(playerDone){
                        AudioListener.pause=true;
                        NextLevelButton.SetActive(true);
                        TryAgainButton.SetActive(false);
//                        Enemy.GetComponent<AiCar>().stop();
                        stopwatch.Stop();
                        testComparison.text = "~ You Win! ~"; 
                        Time.timeScale = 0;
                        
                    }
    }
    
    

}
