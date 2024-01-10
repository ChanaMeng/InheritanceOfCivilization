using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SampleSceneTemp : MonoBehaviour
{
    private VisualElement rootVE;
    public void Start()
    {
        rootVE = GetComponent<UIDocument>().rootVisualElement;
        // 为每个按钮添加点击事件监听器
        rootVE.Q<Button>("back").RegisterCallback<MouseUpEvent>(OnBackButtonClicked);
        rootVE.Q<Button>("quit").RegisterCallback<MouseUpEvent>(OnQuitButtonClicked);
    }

    private void OnBackButtonClicked(MouseUpEvent evt)
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }

    private void OnQuitButtonClicked(MouseUpEvent evt)
    {
        Application.Quit();
    }
}
