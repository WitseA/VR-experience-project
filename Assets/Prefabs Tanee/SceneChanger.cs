using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string targetSceneName;
    public string playerTag = "Player"; 
    //public bool condition = false; // Voorwaarde die true moet zijn om de scène te veranderen

    void OnTriggerEnter(Collider other)
    {
        // Controleer of de speler de trigger binnenkomt en of de conditie waar is
        if (other.CompareTag(playerTag)) //&& condition
        {
            // Laad de doelscène
            SceneManager.LoadScene(targetSceneName);
        }
    }

    // Deze functie kan worden aangeroepen om de conditie te veranderen
    public void SetCondition(bool newCondition)
    {
        //condition = newCondition;
    }
}
