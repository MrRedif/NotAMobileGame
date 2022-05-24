using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneButton : MonoBehaviour
{
    string sceneName;

    [SerializeField] UIWindow parentWindow;

    void LoadLevel(){
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadSceneAsync(name);
    }

    public void SetLevelToReload()
    {
        if (parentWindow == null) return;

        sceneName = SceneManager.GetActiveScene().name;
        parentWindow.OnCloseEvent.AddListener(LoadLevel);
    }

    public void SetLevelToLoad(string name)
    {
        if (parentWindow == null) return;

        sceneName = name;
        parentWindow.OnCloseEvent.AddListener(LoadLevel);
    }

    public void OpenWindow(GameObject window)
    {
        Instantiate(window).transform.SetParent(transform.parent,false);
    }
}
