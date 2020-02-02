using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    public float shakeTime;
    public float strength;
    public int vibation;
    Camera cam;
    Vector3 initialPos;

    static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        cam = GetComponent<Camera>();
    }

    public void ShakeCamera()
    {
        initialPos = cam.transform.position;
        cam.DOShakePosition(shakeTime, strength, vibation).OnComplete(() =>
        {
            cam.transform.position = initialPos;
        });
    }

    public static void Shake()
    {
        if(Instance == null)
        {
            return;
        }
        Instance.ShakeCamera();
    }
}
