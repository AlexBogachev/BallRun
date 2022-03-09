using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject panelPrefab;
    [SerializeField] Transform canvas;

    private void Awake()
    {
        CreatePanel(false);
    }

    private void Start()
    {
        GameManager.Instance.gameIsEnded.AddListener(delegate { CreatePanel(true); });
    }

    private void CreatePanel(bool isGameOver)
    {
        GameObject panelObj = Instantiate(panelPrefab, canvas);
        panelObj.GetComponent<MainPanel>().OpenPanel(isGameOver);
    }
}
