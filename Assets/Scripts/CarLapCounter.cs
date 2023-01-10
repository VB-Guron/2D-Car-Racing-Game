using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Diagnostics;

public class CarLapCounter : MonoBehaviour
{

    long playerCarFinalTime, enemyCarFinalTime;
    
    Animator anim;
                            
    public GameObject Panel;
    Stopwatch _stopwatch = new Stopwatch(); //Overall Timer
    Stopwatch _stopwatch2 = new Stopwatch();// Reseting Timer for Player
    Stopwatch _stopwatch3 = new Stopwatch();// Reseting Timer for Enemy

    public Text playerCarTimer, playerLapCounter, enemyCarTimer, enemyLapCounter, banner, enemyBanner;
    int passedCheckPointNumber = 0;
    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    public int lapsToComplete = 3;

    bool isRaceCompleted = false;


    void Start(){
        
        Panel.SetActive(false);
        _stopwatch.Start();
        _stopwatch2.Start();
        _stopwatch3.Start();
        
        playerLapCounter.text = ("P: " + lapsCompleted + "/" + lapsToComplete);
        enemyLapCounter.text = ("E: " + lapsCompleted + "/" + lapsToComplete);

        banner.text = "PLAYER\n\n";
        enemyBanner.text = "ENEMY\n\n";
    }

    void Update(){
        
        enemyCarFinalTime = _stopwatch3.ElapsedMilliseconds;
    }

public int getLaps(){
    return lapsToComplete;
}

    //Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    public bool statusChange(){
        return isRaceCompleted; 
    }
    public void stop(){
    }



    void OnTriggerEnter2D(Collider2D collider2D)
    {
        // Checkpoints to make sure car stays inside the track
        if (collider2D.CompareTag("CheckPoint"))
        {

            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            //Make sure that the car is passing the checkpoints in the correct order. The correct checkpoint must have exactly 1 higher value than the passed checkpoint
            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;
                
                // If car cross the finishline
                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    // IF PLAYER PASSES FINISHLINE
                    if(gameObject.name == "Player"){
                        
                        //Record the Time 
                        banner.text += string.Format("Lap: "+ lapsCompleted + " {0:mm\\:ss\\:ff} \n", _stopwatch.Elapsed);
                        
                        //Record the Lap
                        playerLapCounter.text = ("P: " + lapsCompleted + "/" + lapsToComplete);

                        //Record the Time
                        playerCarTimer.text += string.Format("Lap no."+ lapsCompleted +": {0:mm\\:ss\\:ff} \n", _stopwatch.Elapsed);
                        
                        //Restart Timer
                        _stopwatch.Restart();

                        //Final Lap
                        if (lapsCompleted >= lapsToComplete){
                            
                            //Put Result in the Ending Banner
                            UnityEngine.Debug.Log("Player is changed");
                            banner.text += "______________\n"; 
                            banner.text += string.Format("Total: {0:mm\\:ss\\:ff} \n", _stopwatch2.Elapsed);
                            isRaceCompleted = true;
                            //Open Banner
                            Panel.SetActive(true);
                        }

                        // IF Enemy PASSES FINISHLINE
                    }else if (gameObject.name == "Enemy"){
                         //Record the Time 
                        enemyBanner.text += string.Format("Lap "+ lapsCompleted + ": {0:mm\\:ss\\:ff} \n", _stopwatch.Elapsed);
                        
                        //Record the lap
                        enemyLapCounter.text = ("E: " + lapsCompleted + "/" + lapsToComplete);

                        //Record the time
                        enemyCarTimer.text += string.Format("Lap no."+ lapsCompleted +": {0:mm\\:ss\\:ff} \n", _stopwatch.Elapsed);
                        
                        //Restart the Timer
                        _stopwatch.Restart();

                        //If Final Lap
                        if (lapsCompleted >= lapsToComplete){
                            
                            //Stop the Animation if all laps are completed
                            UnityEngine.Debug.Log("Enemy is changed");
                            enemyBanner.text += "______________\n"; 
                            enemyBanner.text += string.Format("Total: {0:mm\\:ss\\:ff} \n", _stopwatch2.Elapsed);
                            isRaceCompleted = true;
                        
                        }


                    }


                }


                //Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);

            }
        }
    }
}
