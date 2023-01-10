using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class AiCar : MonoBehaviour
{   
    public int laps = 0;

    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();
    public int maxLaps;
    public float speed;
    public int MaxNum;
    public int pointIndex;
    Transform movePoint;
    bool isGameOver = false;

    public Transform[] points;
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(countingDown ());
        pointIndex = 0;
        movePoint = points[pointIndex]; //Get all Car lap counters in the scene. 
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //Store the lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        if(carLapCounters.Count ==0){
            maxLaps = 100;
        }else maxLaps = carLapCounters[1].getLaps();

    }

    // Update is called once per frame
    void Update()
    {   if(!isGameOver){
        transform.position = Vector2.MoveTowards(transform.position, movePoint.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, movePoint.position) <= 0){
            if(pointIndex > MaxNum){
                pointIndex = 0;
                laps++;
                
            }
            pointIndex++;
            movePoint = points[pointIndex];
        }
        Vector2 pos = movePoint.position - transform.position;
        float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0,angle -90);
        if(laps >= maxLaps){
                    speed = 0;
                    
            transform.rotation = Quaternion.Euler(0,0,0);
                }
    }
    }
    public void stop(){
        speed = 0;
        isGameOver = true;  
        
    }
    IEnumerator countingDown(){


        yield return new WaitForSecondsRealtime(2);
        yield return new WaitForSecondsRealtime(1);
        yield return new WaitForSecondsRealtime(1);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(1);
    }
}
