using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlePartGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> m_shuttleParts = new List<GameObject>();
    [SerializeField] Transform m_generationTransfrom;
    [SerializeField] float m_shuttleScale;
    private GameObject m_currentShuttlePart = null;
    private int m_shuttlePartIndex = 0;
    
    public void ReGenerateShuttlePart()
    {
        if (m_shuttlePartIndex < m_shuttleParts.Count)
        {
            
        }
    }

    public void GenerateShuttlePart()
    {
        if (m_shuttlePartIndex < m_shuttleParts.Count)
        {
            m_currentShuttlePart = Instantiate(m_shuttleParts[m_shuttlePartIndex], m_generationTransfrom.position, Quaternion.identity);
            m_currentShuttlePart.transform.localScale = new Vector3(m_shuttleScale,m_shuttleScale,m_shuttleScale);
            m_shuttlePartIndex++;
        }
    }
}
