using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour
{
    public GameObject ResetPanel;
    public GameObject CompeletePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameRest()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CharacaterMove.canmove=true;
        ResetPanel.SetActive(false);
    }
    public void GameNext2()
    {

        SceneManager.LoadScene("Test Demo2");
        Time.timeScale = 1;
        CompeletePanel.SetActive(false);
    }
    public void GameNext3()
    {

        SceneManager.LoadScene("Test Demo3");
        Time.timeScale = 1;
        CompeletePanel.SetActive(false);
    }
    public void GameNext4()
    {

        SceneManager.LoadScene("Test Demo1");
        Time.timeScale = 1;
        CompeletePanel.SetActive(false);
    }
}
