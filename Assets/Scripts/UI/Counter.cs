using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Counter : MonoBehaviour
{
    protected Text counter;

    protected void Start()
    {
        counter = GetComponent<Text>();
        Initialize();
    }

    protected void UpdateCounter(int value)
    {
        counter.text = value.ToString();
    }

    public abstract void Initialize();
}
