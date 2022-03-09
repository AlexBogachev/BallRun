using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : Counter
{
    public override void Initialize()
    {
        GameManager.Instance.scoreUpdated.AddListener(UpdateCounter);
    }
}
