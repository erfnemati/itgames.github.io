using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tutotrial
{
    public class ReferenceGameBoard : MonoBehaviour
    {
        //[SerializeField] List<HexagonColor> m_hexagonColorList = new List<HexagonColor>();
        [SerializeField] List<ReferenceHexagon> m_referenceHexagons = new List<ReferenceHexagon>();

        private void Start()
        {
            //InitialiseReferenceHexagons();
        }

        //private void InitialiseReferenceHexagons()
        //{
        //    for(int i = 0; i < m_referenceHexagons.Count; i++)
        //    {
        //        m_referenceHexagons[i].SetHexagonColor();
        //    }
        //}

        //public List<HexagonColor> GetHexagonColors()
        //{
        //    return m_hexagonColorList;
        //}

        public List<ReferenceHexagon> GetReferenceHexagons()
        {
            return m_referenceHexagons;
        }


    }

}