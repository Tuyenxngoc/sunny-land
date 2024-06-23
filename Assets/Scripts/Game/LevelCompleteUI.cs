using System.Collections;
using TMPro;
using UnityEngine;

public class LevelCompleteUI : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI enemiesText;
    public TextMeshProUGUI gemsText;
    public TextMeshProUGUI cherriesText;

    float playTime;
    int enemiesKilled;
    int gemsCollected;
    int cherriesCollected;


    void Start()
    {
        // Lấy thông số từ PlayerPrefs
        playTime = PlayerPrefs.GetFloat("PlayTime", 0f);
        enemiesKilled = PlayerPrefs.GetInt("EnemiesKilled", 0);
        gemsCollected = PlayerPrefs.GetInt("GemsCollected", 0);
        cherriesCollected = PlayerPrefs.GetInt("CherriesCollected", 0);

        // Bắt đầu hiển thị số tăng dần cho từng TextMeshProUGUI
        StartCoroutine(AnimateTimeText());
    }

    IEnumerator AnimateTimeText()
    {
        float timeCounter = 0f;
        float duration = 2f; // Thời gian hiệu ứng

        while (timeCounter < duration)
        {
            // Tăng giá trị số theo tỷ lệ thời gian
            float timeProgress = timeCounter / duration;
            timeText.text = Mathf.Lerp(0, playTime, timeProgress).ToString("F2") + "s";

            timeCounter += Time.deltaTime;
            yield return null;
        }

        // Hiển thị giá trị cuối cùng
        timeText.text = playTime.ToString("F2") + "s";

        // Sau khi hoàn thành timeText, chạy coroutine cho enemiesText
        StartCoroutine(AnimateEnemiesText());
    }

    IEnumerator AnimateEnemiesText()
    {
        float enemiesCounter = 0f;
        float duration = 1f; // Thời gian hiệu ứng

        while (enemiesCounter < duration)
        {
            // Tăng giá trị số theo tỷ lệ thời gian
            float enemiesProgress = enemiesCounter / duration;
            enemiesText.text = "" + Mathf.RoundToInt(Mathf.Lerp(0, enemiesKilled, enemiesProgress));

            enemiesCounter += Time.deltaTime;
            yield return null;
        }

        // Hiển thị giá trị cuối cùng
        enemiesText.text = "" + enemiesKilled;

        // Sau khi hoàn thành enemiesText, chạy coroutine cho gemsText
        StartCoroutine(AnimateGemsText());
    }

    IEnumerator AnimateGemsText()
    {
        float gemsCounter = 0f;
        float duration = 1f; // Thời gian hiệu ứng

        while (gemsCounter < duration)
        {
            // Tăng giá trị số theo tỷ lệ thời gian
            float gemsProgress = gemsCounter / duration;
            gemsText.text = "" + Mathf.RoundToInt(Mathf.Lerp(0, gemsCollected, gemsProgress));

            gemsCounter += Time.deltaTime;
            yield return null;
        }

        // Hiển thị giá trị cuối cùng
        gemsText.text = "" + gemsCollected;


        // Sau khi hoàn thành gemsText, chạy coroutine cho cherriesText
        StartCoroutine(AnimateCherriesText());
    }

    IEnumerator AnimateCherriesText()
    {
        float cherriesCounter = 0f;
        float duration = 1f; // Thời gian hiệu ứng

        while (cherriesCounter < duration)
        {
            // Tăng giá trị số theo tỷ lệ thời gian
            float cherriesProgress = cherriesCounter / duration;
            cherriesText.text = "" + Mathf.RoundToInt(Mathf.Lerp(0, cherriesCollected, cherriesProgress));

            cherriesCounter += Time.deltaTime;
            yield return null;
        }

        // Hiển thị giá trị cuối cùng
        cherriesText.text = "" + cherriesCollected;
    }
}
