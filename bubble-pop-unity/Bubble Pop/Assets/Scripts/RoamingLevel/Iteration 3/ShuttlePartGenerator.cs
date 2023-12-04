using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttlePartGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> m_shuttleParts = new List<GameObject>();
    [SerializeField] Transform m_generationTransfrom;

    
    
    private int m_shuttlePartIndex = -1;
    
    public void ReGenerateShuttlePart()
    {
        if (m_shuttlePartIndex < m_shuttleParts.Count)
        {
            Instantiate(m_shuttleParts[m_shuttlePartIndex], m_generationTransfrom.position, Quaternion.identity);
        }
    }

    public void GenerateShuttlePart()
    {
        if (m_shuttlePartIndex < m_shuttleParts.Count)
        {
            m_shuttlePartIndex++;
            Instantiate(m_shuttleParts[m_shuttlePartIndex], m_generationTransfrom.position, Quaternion.identity);
            
        }

    }
}
