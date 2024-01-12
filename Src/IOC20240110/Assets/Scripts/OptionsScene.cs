using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Toggle = UnityEngine.UIElements.Toggle;

public class OptionsScene : MonoBehaviour
{
    private VisualElement rootVE;

    public void Start()
    {
        //设置根元件
        rootVE = GetComponent<UIDocument>().rootVisualElement;
        
        //默认画面面板显示
        rootVE.Q<VisualElement>(name: "huaMianPanel").style.display = DisplayStyle.Flex;
        
        //监听类别按钮
        rootVE.Q<Button>(name:"huaMian").RegisterCallback<MouseUpEvent>(OnHuaMianButtonClicked);
        rootVE.Q<Button>(name:"yinPin").RegisterCallback<MouseUpEvent>(OnYinPinButtonClicked);
        rootVE.Q<Button>(name:"caoZuo").RegisterCallback<MouseUpEvent>(OnCaoZuoButtonClicked);
        
        //监听返回按钮
        rootVE.Q<Button>(name:"back").RegisterCallback<MouseUpEvent>(OnBackButtonClicked);
        
        //画面选项中中，设置分辨率下拉菜单
        SettingDropdownField();
    }

    private void OnHuaMianButtonClicked(MouseUpEvent evt)
    {
        rootVE.Q<VisualElement>(name: "huaMianPanel").style.display = DisplayStyle.Flex;
        rootVE.Q<VisualElement>(name: "yinPinPanel").style.display = DisplayStyle.None;
        rootVE.Q<VisualElement>(name: "caoZuoPanel").style.display = DisplayStyle.None;
        
        //全屏选择框事件
        rootVE.Q<Toggle>(name: "full").RegisterCallback<ChangeEvent<bool>>(OnFullscreenToggleChanged);
        
        //分辨率下拉菜单事件
        rootVE.Q<DropdownField>("res").RegisterCallback<ChangeEvent<string>>(OnResDropdownFieldChanged);
    }
    private void OnYinPinButtonClicked(MouseUpEvent evt)
    {
        rootVE.Q<VisualElement>(name: "huaMianPanel").style.display = DisplayStyle.None;
        rootVE.Q<VisualElement>(name: "yinPinPanel").style.display = DisplayStyle.Flex;
        rootVE.Q<VisualElement>(name: "caoZuoPanel").style.display = DisplayStyle.None;
    }
    private void OnCaoZuoButtonClicked(MouseUpEvent evt)
    {
        rootVE.Q<VisualElement>(name: "huaMianPanel").style.display = DisplayStyle.None;
        rootVE.Q<VisualElement>(name: "yinPinPanel").style.display = DisplayStyle.None;
        rootVE.Q<VisualElement>(name: "caoZuoPanel").style.display = DisplayStyle.Flex;
    }
    private void OnBackButtonClicked(MouseUpEvent evt)
    {
        SceneManager.LoadScene("WelcomeScene", LoadSceneMode.Single);
    }

    //画面选项--全屏选择框
    private void OnFullscreenToggleChanged(ChangeEvent<bool> e)
    {
        Screen.fullScreen = e.newValue;
    }

    private void SettingDropdownField()
    {
        // 获取系统可用的分辨率列表并填充DropdownField
        Resolution[] resolutions = Screen.resolutions;
        List<string> choices = new List<string>();

        foreach (var resolution in resolutions)
        {
            choices.Add($"{resolution.width}x{resolution.height}");
        }

        rootVE.Q<DropdownField>(name: "res").choices = choices;
    }

    //画面选项--分辨率下拉菜单
    private void OnResDropdownFieldChanged(ChangeEvent<string> evt)
    {
        // 解析所选分辨率字符串
        string[] resolutionParts = evt.newValue.Split('x');
        if (resolutionParts.Length == 2 && int.TryParse(resolutionParts[0], out int width) && int.TryParse(resolutionParts[1], out int height))
        {
            Screen.SetResolution(width, height, FullScreenMode.ExclusiveFullScreen); // 使用当前全屏模式设置分辨率
            Debug.Log($"Resolution changed to {width}x{height}");
        }
        else
        {
            Debug.LogWarning("Invalid resolution format");
        }
    }
}
