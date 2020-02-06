using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoalScript : MonoBehaviour
{

    public string levelToLoad1;
    public string levelToLoad2;
    public string levelToLoad3;
    public string levelToLoad4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            int index = Random.Range(1, 5);
            if(index == 1)
            {
                SceneManager.LoadScene(levelToLoad1);
            }
            else if (index == 2)
            {
                SceneManager.LoadScene(levelToLoad2);
            }
            else if (index == 3)
            {
                SceneManager.LoadScene(levelToLoad3);
            }
            else if (index == 4)
            {
                SceneManager.LoadScene(levelToLoad4);
            }
        }
    }
}
