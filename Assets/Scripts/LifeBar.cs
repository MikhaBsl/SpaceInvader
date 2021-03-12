using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    private Image m_Image;
    private bool m_IsDestroying;
    private bool m_IsGivingLife;

    public void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void Update()
    {
        if (m_IsDestroying)
        {
            if (m_Image.color.a > 0)
            {
                var color = m_Image.color;
                color.a = Mathf.Lerp(m_Image.color.a, 0, Time.deltaTime * 60f * 0.1f);
                m_Image.color = color;
            }
            else 
            {
                m_IsDestroying = false;
            }
        }
        else if (m_IsGivingLife)
        {
            if (m_Image.color.a < 1 )
            {
                var color = m_Image.color;
                color.a = Mathf.Lerp(m_Image.color.a, 1, Time.deltaTime * 60f * 0.1f);
                m_Image.color = color;
            }
            else
            {
                m_IsGivingLife = false;
            }
        }
        
    }

    public void DestroyItemLife()
    {
        m_IsDestroying = true;
    }

    public void RestoreItemLife()
    {
        m_IsGivingLife = true;
    }
}
