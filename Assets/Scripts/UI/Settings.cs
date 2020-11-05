using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/* When the upper-left button is clicked
 * the options are shown here
 * 
 */

public class Settings : MonoBehaviour
{

    public void GoToNextScene(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


}
