using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Finish : MonoBehaviour
{
    public GameObject ResetPanel;
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
        ResetPanel.SetActive(false);
    }
}
