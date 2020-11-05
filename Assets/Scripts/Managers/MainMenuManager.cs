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
    public UITween transistion;
    public Button[] mainMenuButtons;

    private List<UITween> menuButtonTweens = new List<UITween>();

    private void Awake() {
        if (transistion == null) Debug.Log("Transistion is missing.");
        if (mainMenuButtons.Length <= 0) Debug.Log("No menu buttons assigned.");
    }

    private void Start() {
        for (int i = 0; i < mainMenuButtons.Length; i++) {
            menuButtonTweens.Add(mainMenuButtons[i].GetComponent<UITween>());
        }
    }


    public void StartGame() {
        void endFunction() { GoToNextScene(); }
        transistion.GoToRelativePosition(endFunction);

        //Disable buttons and hide
        for (int i = 0; i < mainMenuButtons.Length; i++) {
            mainMenuButtons[i].interactable = false;
            menuButtonTweens[i].TweenToX(-500, 0.5f, LeanTweenType.easeOutCirc);
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
