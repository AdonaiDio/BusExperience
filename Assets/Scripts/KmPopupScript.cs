using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class KmPopupScript : MonoBehaviour
{
    //[SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    [SerializeField] private TMP_Text text;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        gameObject.SetActive(false);
    }

    public void OnStartPopup(Transform lookAt, int kilometers)
    {
        text.text = kilometers.ToString() + "\n<size=75%>KM";
        Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);
        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
    public void OnEndPopup()
    {
        gameObject.SetActive(false);
    }
}
