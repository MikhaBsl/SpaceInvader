using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBar : MonoBehaviour
{
    public void DestroyItemLife()
    {
        gameObject.SetActive(false);
    }

    public void RestoreItemLife()
    {
        gameObject.SetActive(true);
    }
}
