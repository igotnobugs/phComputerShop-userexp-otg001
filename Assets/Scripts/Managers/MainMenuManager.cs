using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* In the MainMenu scene
 * Handles menu button behaviour
 */

public class MainMenuManager : Singleton<MainMenuManager>  
{   
    public string GameScene;
    public TransistionUI transistion;
    public Button[] mainMenuButtons;

    private List<HoveredUI> menuButtonTweens = new List<HoveredUI>();

    private void Awake() {
        if (transistion == null) Debug.Log("Transistion is missing.");
        if (mainMenuButtons.Length <= 0) Debug.Log("No menu buttons assigned.");
    }

    private void Start() {
        for (int i = 0; i < mainMenuButtons.Length; i++) {
            menuButtonTweens.Add(mainMenuButtons[i].GetComponent<HoveredUI>());

            
        }
    }

    public void IsButtonHovered() {
        Debug.Log("A button is hovered!");
    }


    public void StartGame() {
        transistion.Show().setOnComplete(() => GoToNextScene());

        //Disable buttons and hide
        for (int i = 0; i < mainMenuButtons.Length; i++) {
            mainMenuButtons[i].interactable = false;
        }
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void GoToNextScene() {
        StartCoroutine(NextSceneSequence());
    }

    private IEnumerator NextSceneSequence() {

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
        yield return null;
    }

}
