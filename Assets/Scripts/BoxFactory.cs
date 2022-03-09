using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType
{
    Score,
    Damage
}

public class BoxFactory : MonoBehaviour
{
    public static BoxFactory Instance;

    [SerializeField] GameObject boxPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void BuildBox(Transform parent, Vector3 position)
    {
        int boxTypeInt = UnityEngine.Random.Range(0, 2);
        BoxType boxType = (BoxType)boxTypeInt;

        GameObject newBox = Instantiate(boxPrefab, position, Quaternion.identity);
        newBox.GetComponent<Box>().Initialize(boxType);
        newBox.transform.parent = parent;

        if (boxType == BoxType.Score)
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.green;

        }
        else if (boxType == BoxType.Damage)
        {
            newBox.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
}
