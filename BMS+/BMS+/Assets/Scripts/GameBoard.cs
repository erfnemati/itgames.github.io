using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField] List<Hexagon> m_hexagons = new List<Hexagon>();

    public List<Hexagon> GetHexagons()
    {
        return m_hexagons;
    }


}
