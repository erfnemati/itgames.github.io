using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager _instance;
    [SerializeField] ReferenceGameBoard m_referenceGameBoard;
    [SerializeField] GameBoard m_gameBoard;
    [SerializeField] List<Button> m_towerButtons = new List<Button>();
    [SerializeField] AudioClip m_victorySound;

    public delegate void EndLevelAction();
    public static event EndLevelAction OnLevelVictory;
    public static event EndLevelAction OnLevelDefeat;


    private void OnEnable()
    {
        LevelTimer.OnTimeOver += LostLevel;
        //LevelTimer.OnTimeOver += DeactivateTowerButtons;
        OnLevelDefeat += DeactivateTowerButtons;
        OnLevelVictory += PlayVictorySound;
        OnLevelVictory += DeactivateTowerButtons;
        Debug.Log("Level manager enabling");

    }

    private void OnDisable()
    {
        LevelTimer.OnTimeOver -= LostLevel;
        //LevelTimer.OnTimeOver -= DeactivateTowerButtons;
        OnLevelDefeat -= DeactivateTowerButtons;
        OnLevelVictory -= PlayVictorySound;
        OnLevelVictory -= DeactivateTowerButtons;
        Debug.Log("Level manager disabling");
    }

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
        List<ReferenceHexagon> referenceHexagons = m_referenceGameBoard.GetReferenceHexagons();

        if (gameBoardHexagons.Count != referenceHexagons.Count)
        {
            Debug.Log("Something wrong with gameboard hexagons and reference hexagons");
            return;
        }
        
        for(int i = 0; i <gameBoardHexagons.Count; i++)
        {
            if (gameBoardHexagons[i].GetHexagonColor() == referenceHexagons[i].GetHexagonColor())
            {
                if (gameBoardHexagons[i].GetHexagonNumber() == referenceHexagons[i].GetHexagonNumber())
                {
                    continue;
                }
                else
                {
                    Debug.Log("Not there yet");
                    return;
                }
            }
            else
            {
                Debug.Log("Not there yet");
                return;
            }
        }

        WinLevel();
    }

    private void DeactivateTowerButtons()
    {
        foreach(Button tempButton in m_towerButtons)
        {
            tempButton.enabled = false;
        }
    }

    private void WinLevel()
    {
        
        Debug.Log("You have won");
        OnLevelVictory();
    }

    private void LostLevel()
    {
        Debug.Log("You have lost");
        if (PlayerLifeManager._instance != null)
        {
            PlayerLifeManager._instance.DecrementNumOfLives();
        }
        OnLevelDefeat();
    }

    public void RetreatLevel()
    {
        LostLevel();
    }

    private void PlayVictorySound()
    {
        Debug.Log("Play victory sound");
        SoundManager._instance.StopAllSoundEffects();
        SoundManager._instance.PlaySoundEffect(m_victorySound);
        
    }

    
}
