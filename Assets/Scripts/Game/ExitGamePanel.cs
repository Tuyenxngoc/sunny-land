using UnityEngine;

public class ExitGamePanel : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject settingPanel;

    void Start()
    {
        if (exitPanel != null)
            exitPanel.SetActive(false);
        if (settingPanel != null)
            settingPanel.SetActive(false);
    }

    public void OnYesButtonClick()
    {
        // Quit the game
        Application.Quit();

        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void OnNoButtonClick()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(false);
        }
    }

    public void ShowExitPanel()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(true);
        }

        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }

    public void ShowSettingPanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(true);
        }

        if (exitPanel != null)
        {
            exitPanel.SetActive(false);
        }
    }

    public void CloseSettingPanel()
    {
        if (settingPanel != null)
        {
            settingPanel.SetActive(false);
        }
    }
}
