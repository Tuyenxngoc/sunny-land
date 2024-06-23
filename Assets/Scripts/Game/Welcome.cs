using UnityEngine;
using UnityEngine.SceneManagement;


public class Welcome : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            HandleEnterPressed();
        }
    }

    void HandleEnterPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
