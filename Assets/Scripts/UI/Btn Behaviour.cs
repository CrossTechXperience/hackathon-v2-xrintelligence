using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnBehaviour : MonoBehaviour
{
    [SerializeField] private Button ShowInfosBtn;

    private void Awake()
    {
        ShowInfosBtn.interactable = false;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenIA()
    {
        Application.OpenURL("http://localhost:3000/");
    }
}
