using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RefrenceGameBoard : MonoBehaviour
{
    [SerializeField] List<HexagonColor> m_hexagonColorList = new List<HexagonColor>();
    [SerializeField] List<Hexagon> m_referenceHexagons = new List<Hexagon>();

    private void Start()
    {
        InitialiseReferenceHexagons();
    }

    private void InitialiseReferenceHexagons()
    {
        for(int i = 0; i < m_referenceHexagons.Count; i++)
        {
            m_referenceHexagons[i].SetHexagonColor(m_hexagonColorList[i]);
        }
    }

    public List<HexagonColor> GetHexagonColors()
    {
        return m_hexagonColorList;
    }


}
