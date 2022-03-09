using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour
{
    [SerializeField] Text headerText;
    [SerializeField] Text scoreCounter;
    [SerializeField] Button button;

    public void OpenPanel(bool isGameOver)
    {
        if (!isGameOver)
        {
            print("GAME OVER NOT");
            headerText.gameObject.SetActive(false);
            scoreCounter.gameObject.SetActive(false);
            button.GetComponentInChildren<Text>().text = "BEGIN";
            button.onClick.AddListener(delegate { GameManager.Instance.StartGame(); });
        }
        else
        {
            headerText.gameObject.SetActive(true);
            scoreCounter.gameObject.SetActive(true);
            scoreCounter.text = "SCORE: " + GameManager.Instance.GetScore();
            button.GetComponentInChildren<Text>().text = "RESTART";
            button.onClick.AddListener(delegate { GameManager.Instance.RestartGame(); });
        }
        button.onClick.AddListener(ClosePanel);
    }

    public void ClosePanel()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
