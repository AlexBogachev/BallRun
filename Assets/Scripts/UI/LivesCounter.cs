using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : Counter
{
    public override void Initialize()
    {
        GameManager.Instance.livesUpdated.AddListener(UpdateCounter);
    }
}
