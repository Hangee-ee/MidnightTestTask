using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchVCam : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private bool isZoomedIn = false;

    public float zoomedInFieldOfView = 30f;
    public float zoomedOutFieldOfView = 60f;
    public float zoomSpeed = 5f;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        virtualCamera.m_Lens.FieldOfView = zoomedOutFieldOfView;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (!isZoomedIn)
            {
                isZoomedIn = true;
                StartCoroutine(Zoom(zoomedInFieldOfView));
            }
        }
        else
        {
            if (isZoomedIn)
            {
                isZoomedIn = false;
                StartCoroutine(Zoom(zoomedOutFieldOfView));
            }
        }
    }

    private IEnumerator Zoom(float targetFieldOfView)
    {
        float initialFieldOfView = virtualCamera.m_Lens.FieldOfView;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * zoomSpeed;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(initialFieldOfView, targetFieldOfView, t);
            yield return null;
        }

        virtualCamera.m_Lens.FieldOfView = targetFieldOfView;
    }
}
