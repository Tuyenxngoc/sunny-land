using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    public float delayTime = 1f; // Thời gian trễ trước khi chữ ẩn/hiện
    private Text textRenderer;
    private bool isVisible = true;
    void Start()
    {
        // Lấy renderer của object chứa chữ
        textRenderer = GetComponent<Text>();

        // Kiểm tra xem renderer có tồn tại không
        if (textRenderer == null)
        {
            Debug.LogError("Text component not found!");
            enabled = false; // Tắt script nếu không tìm thấy renderer
        }

        // Bắt đầu coroutine để tự động ẩn/hiện chữ sau delayTime
        StartCoroutine(ToggleVisibility());
    }

    IEnumerator ToggleVisibility()
    {
        // Chờ cho một khoảng thời gian trước khi thay đổi trạng thái của chữ
        yield return new WaitForSeconds(delayTime);

        // Đảo ngược trạng thái hiện tại của chữ (ẩn/hiện)
        isVisible = !isVisible;

        // Thay đổi trạng thái của renderer dựa trên isVisible
        textRenderer.enabled = isVisible;

        // Lặp lại coroutine để tiếp tục tự động ẩn/hiện chữ
        StartCoroutine(ToggleVisibility());
    }
}
