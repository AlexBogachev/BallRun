using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Section : MonoBehaviour
{
    [SerializeField] GameObject bonusBoxPrefab;
    [SerializeField] GameObject damageBoxPrefab;

    [SerializeField] List<Transform> boxesSpawnPoints; 

    [HideInInspector] public UnityEvent SectionIsOutsidePlayArea;

    public void Initialize()
    {
        PlaceBoxes();
        StartCoroutine(CheckPosition());
    }

    public Vector3 GetNewSectionSpawnPoint()
    {
        Vector3 spawnPoint = transform.GetChild(0).transform.position;
        return spawnPoint;
    }

    private void PlaceBoxes()
    {
        foreach(Transform spawnPoint in boxesSpawnPoints)
        {
            float xPos = Random.Range(-4.0f, 4.0f);
            Vector3 boxPosition = new Vector3(xPos, 0.0f, spawnPoint.transform.position.z);
            BoxFactory.Instance.BuildBox(spawnPoint, boxPosition);
        }
    }

    IEnumerator CheckPosition()
    {
        while (gameObject.transform.position.z >= -15.0f)
        {
            transform.Translate(-transform.forward * Time.deltaTime * GameManager.Instance.GetSpeed());
            yield return null;
        }
        SectionIsOutsidePlayArea.Invoke();
    }
}
