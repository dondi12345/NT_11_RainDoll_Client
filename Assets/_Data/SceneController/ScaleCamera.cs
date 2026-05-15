using UnityEngine;
using UnityEngine.UI;

public class ScaleCamera : MonoBehaviour
{
    [SerializeField] private float scaleValue;
    [SerializeField] private Camera mainCamera; // Kéo Camera vào đây
    
    private CanvasScaler ss;
    public float baseSize; // Size mục tiêu khi scale = 1

    void Start()
    {
        ss = GetComponent<CanvasScaler>();
        if (mainCamera == null) mainCamera = Camera.main;
        
        ScaleScr();
    }

    void Update()
    {
        // Lưu ý: Gọi Update liên tục có thể tốn tài nguyên, 
        // bạn nên cân nhắc chỉ gọi khi độ phân giải màn hình thay đổi.
        ScaleScr();
    }

    public void ScaleScr()
    {
        float x = Screen.width;
        float y = Screen.height;
        
        // Tính tỉ lệ hiện tại so với tỉ lệ chuẩn 16:9
        scaleValue = (x / y) / (1920f / 1080f);
        
        // Cập nhật Canvas Scaler
        if (ss != null)
        {
            // Ép kiểu về float (0 đến 1) thay vì int
            ss.matchWidthOrHeight = Mathf.Clamp01(1f - scaleValue); 
        }

        // Cập nhật Camera Size theo logic:
        // Nếu scaleValue < 1 (màn hình hẹp hơn 16:9), size sẽ > 5
        // Nếu scaleValue > 1 (màn hình rộng hơn 16:9), size sẽ < 5
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = baseSize / scaleValue;
        }
    }
}
