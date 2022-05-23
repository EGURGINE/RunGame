using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    [SerializeField] private GameObject playExText;
    public bool isStart = false;
    private void Awake()
    {
        Instance = this;
        playExText.SetActive(true);
    }
    private void Update()
    {
        if (isStart == false && Input.GetMouseButtonDown(0))
        {
            isStart = true;
            playExText.SetActive(false);
        }

    }
}
