using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void PlayGame(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Reset(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public void Level1(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
        
    }
    
    public void Level2(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
    }
    
    public void Level3(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
    }

    public void MainMenu(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
    public void NextLevel(){
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit(){
        Debug.Log("Quitting");
        Application.Quit();
    }





}
