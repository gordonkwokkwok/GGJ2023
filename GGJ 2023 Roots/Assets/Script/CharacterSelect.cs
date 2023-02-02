using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterSelect : MonoBehaviour
{

    public GameObject[] characters;
    public int selectedCharacter = 0;
    public int playerPosition;
    public GameObject joinCanvas;
    public GameObject selectingCanvas;
    public GameObject readyCanvas;
    public GameObject retireText1;
    public GameObject retireText2;
    private enum readyStatus {
        RETIRED,SELECTING,READY
    };
    private readyStatus playerReadyStatus = readyStatus.RETIRED;

    public void Confirm(){
        switch (playerReadyStatus)
        {
            case readyStatus.RETIRED:
            playerReadyStatus = readyStatus.SELECTING;
            selectedCharacter = 0;
            characters[0].SetActive(true);
            joinCanvas.SetActive(false);
            selectingCanvas.SetActive(true);
            readyCanvas.SetActive(false);
            break;

            case readyStatus.SELECTING:
            playerReadyStatus = readyStatus.READY;

            joinCanvas.SetActive(false);
            selectingCanvas.SetActive(false);
            readyCanvas.SetActive(true);
            break;

            default:
            break;
        }
    }

    public void Cancel(){
        switch (playerReadyStatus)
        {
            case readyStatus.READY:
            playerReadyStatus = readyStatus.SELECTING;
            joinCanvas.SetActive(false);
            selectingCanvas.SetActive(true);
            readyCanvas.SetActive(false);
            break;

            case readyStatus.SELECTING:
            characters[selectedCharacter].SetActive(false);
            playerReadyStatus = readyStatus.RETIRED;
            joinCanvas.SetActive(true);
            selectingCanvas.SetActive(false);
            readyCanvas.SetActive(false);
            break;
            
            default:
            break;
        }
    }



    public void NextCharacter(){
        if (playerReadyStatus != readyStatus.SELECTING) {return;}
        characters[selectedCharacter].SetActive(false);
        selectedCharacter = (selectedCharacter+1)%characters.Length;
        characters[selectedCharacter].SetActive(true);
        
    }

    public void PreviousCharacter(){
    if (playerReadyStatus != readyStatus.SELECTING) {return;}
    characters[selectedCharacter].SetActive(false);
    selectedCharacter--;
    if (selectedCharacter < 0){
        selectedCharacter += characters.Length;
    }
    characters[selectedCharacter].SetActive(true);
    
    }


    void Update()
    {
        if (Input.GetButtonDown("P"+(playerPosition+1).ToString()+"A"))
        {
            Confirm();
            Debug.Log("P"+playerPosition.ToString()+"Triggered Confirm");
        }
        if (Input.GetButtonDown("P"+(playerPosition+1).ToString()+"B"))
        {
            Cancel();
            Debug.Log("P"+playerPosition.ToString()+"Triggered Cancel");
        }
        if (Input.GetButtonDown("P"+(playerPosition+1).ToString()+"Right"))
        {
            NextCharacter();
            Debug.Log("P"+playerPosition.ToString()+"Triggered next Char");
        }else if (Input.GetButtonDown("P"+(playerPosition+1).ToString()+"Left"))
        {
            PreviousCharacter();
            Debug.Log("Triggered prev Char");
        }
    }

    
    public void StartGame()
    {
        PlayerPrefs.SetInt("selectedCharcater",selectedCharacter);
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

}
