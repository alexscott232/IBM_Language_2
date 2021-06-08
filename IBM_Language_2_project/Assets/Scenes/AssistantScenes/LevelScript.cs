using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{

    public Canvas beginner;
    public Canvas intermediate;
    public Canvas advanced;
    public Canvas levels;

    private void Start()
    {

    }
    public void beginnerLevel()
    {
        beginner = GameObject.Find("BeginnerCanvas").GetComponent<Canvas>();
        beginner.gameObject.SetActive(true);
        intermediate = GameObject.Find("IntermediateCanvas").GetComponent<Canvas>();
        intermediate.gameObject.SetActive(false);
        advanced = GameObject.Find("AdvancedCanvas").GetComponent<Canvas>();
        advanced.gameObject.SetActive(false);
        levels = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
        levels.gameObject.SetActive(false);
    }
    public void intermediateLevel()
    {
        beginner = GameObject.Find("BeginnerCanvas").GetComponent<Canvas>();
        beginner.gameObject.SetActive(false);
        intermediate = GameObject.Find("IntermediateCanvas").GetComponent<Canvas>();
        intermediate.gameObject.SetActive(true);
        advanced = GameObject.Find("AdvancedCanvas").GetComponent<Canvas>();
        advanced.gameObject.SetActive(false);
        levels = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
        levels.gameObject.SetActive(false);
    }
    public void advancedLevel()
    {
        beginner = GameObject.Find("BeginnerCanvas").GetComponent<Canvas>();
        beginner.gameObject.SetActive(false);
        intermediate = GameObject.Find("IntermediateCanvas").GetComponent<Canvas>();
        intermediate.gameObject.SetActive(false);
        advanced = GameObject.Find("AdvancedCanvas").GetComponent<Canvas>();
        advanced.gameObject.SetActive(true);
        levels = GameObject.Find("LevelCanvas").GetComponent<Canvas>();
        levels.gameObject.SetActive(false);
    }
}
