using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MissionsPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private List<GameObject> listOfObjects;
    [SerializeField] private TextMeshProUGUI CurrenteMission;
    [SerializeField] private TextMeshProUGUI totalMissionsUI;
    [SerializeField] private TextMeshProUGUI CurrenteMissionNumberUI;
    [SerializeField] private Button playButton;

    private Stack<GameObject> stackOfMissions;
    private int CurrentMissionIndex;
    private string ClickedtMissionText;
    private int missionsCounter = 0;
    [SerializeField] private GameObject winPanel;



    void Start()
    {
        playButton.onClick.AddListener(PlayAgain);//agrega el envento de juagar otra vez a su respectivo boton
        listOfObjects = RandomiseListOfMissions(listOfObjects);//toma las missiones en el inspector y usa la funciona para randomisar el orden de aparicion.
        StartMissions();
    }

    private void StartMissions() 
    {
        foreach (GameObject obj in listOfObjects)//añade el evento de click para boton
        {
            obj.GetComponent<Button>().onClick.AddListener(CompletedMission); 
        }
        totalMissionsUI.text = listOfObjects.Count.ToString();
        stackOfMissions = new Stack<GameObject>();
        foreach (GameObject obj in listOfObjects)
        {
            stackOfMissions.Push(obj);
        } 
        CurrentMission();
    }

    private void CompletedMission() 
    {
        ClickedtMissionText = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;//agarra en texto del boton clickeado y lo guarda en una variable
        if (ClickedtMissionText != CurrenteMission.text)
        {
            return;
        }
        stackOfMissions.Pop();
        listOfObjects[CurrentMissionIndex].SetActive(false);
        missionsCounter++;
        CurrenteMissionNumberUI.text = missionsCounter.ToString();  
        CurrentMission();
        if (stackOfMissions.Count == 0) 
        {
            winPanel.SetActive(true);
        }    
    }

    private void CurrentMission() 
    {
        if (stackOfMissions.Count == 0) { return; }
        GameObject GameObjectCurrenteMission = stackOfMissions.Peek();
        CurrenteMission.text = GameObjectCurrenteMission.GetComponentInChildren<TextMeshProUGUI>().text;
        CurrentMissionIndex = listOfObjects.IndexOf(GameObjectCurrenteMission);

    }

    private List<GameObject> RandomiseListOfMissions(List<GameObject> listOfObjects)
    {
        List<GameObject> shuffledList = new List<GameObject>(listOfObjects);

        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);

            GameObject temp = shuffledList[i];
            shuffledList[i] = shuffledList[randomIndex];
            shuffledList[randomIndex] = temp;
        }
        return shuffledList;
    }
    private void PlayAgain() 
    {
        SceneManager.LoadScene(0);
    }
}
