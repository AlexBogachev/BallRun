using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    BoxType boxType;

    public void Initialize(BoxType boxType)
    {
        this.boxType = boxType;
    }

    public BoxType GetBoxType()
    {
        return boxType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            if(boxType == BoxType.Damage)
            {
                GameManager.Instance.CheckGameOver();
            }
            else if (boxType == BoxType.Score)
            {
                GameManager.Instance.UpdateScore();
            }
        }
        Destroy(gameObject);
    }
}
