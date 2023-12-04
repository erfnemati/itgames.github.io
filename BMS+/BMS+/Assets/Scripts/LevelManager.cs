using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;
    [SerializeField] RefrenceGameBoard m_referenceGameBoard;
    [SerializeField] GameBoard m_gameBoard;
    private void Awake()
    {
        if(_instance != null && _instance!= this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    
    public void CompareBoards()
    {
        List<Hexagon> gameBoardHexagons = m_gameBoard.GetHexagons();
        List<ReferenceHexagon> referenceHexagons = new List<ReferenceHexagon>();

        if (gameBoardHexagons.Count != referenceHexagons.Count)
        {
            return;
        }
        
        for(int i = 0; i <gameBoardHexagons.Count; i++)
        {

        }

        WinLevel();
    }

    private void WinLevel()
    {
        //TODO ui stuff here
        Debug.Log("You have won");
    }

    private void LostLevel()
    {
        //TODO ui stuff here
        Debug.Log("You have lost");

    }

    
}
