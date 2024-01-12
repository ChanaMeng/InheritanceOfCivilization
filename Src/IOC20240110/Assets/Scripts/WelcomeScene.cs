using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class WelcomeScene : MonoBehaviour
{
    private VisualElement rootVE;
    public void Start()
    {
        rootVE = GetComponent<UIDocument>().rootVisualElement;
        // 为每个按钮添加点击事件监听器
        rootVE.Q<Button>("load").RegisterCallback<MouseUpEvent>(OnLoadButtonClicked);
        rootVE.Q<Button>("start").RegisterCallback<MouseUpEvent>(OnStartButtonClicked);
        rootVE.Q<Button>("options").RegisterCallback<MouseUpEvent>(OnOptionsButtonClicked);
        rootVE.Q<Button>("quit").RegisterCallback<MouseUpEvent>(OnQuitButtonClicked);
    }

    private void OnLoadButtonClicked(MouseUpEvent evt)
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    private void OnStartButtonClicked(MouseUpEvent evt)
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    private void OnOptionsButtonClicked(MouseUpEvent evt)
    {
        SceneManager.LoadScene("OptionsScene", LoadSceneMode.Single);
    }

    private void OnQuitButtonClicked(MouseUpEvent evt)
    {
        Application.Quit();
    }
}
