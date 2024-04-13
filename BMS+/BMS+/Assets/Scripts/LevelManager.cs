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
    public static event EndLevelAction OnLevelRetreat;
    public static event EndLevelAction OnGameWin;


    private void OnEnable()
    {
        LevelTimer.OnTimeOver += LostLevel;
        //LevelTimer.OnTimeOver += DeactivateTowerButtons;
        OnLevelDefeat += DeactivateTowerButtons;
        OnLevelVictory += PlayVictorySound;
        OnLevelVictory += DeactivateTowerButtons;
        OnLevelRetreat += DeactivateTowerButtons;
        //Debug.Log("Level manager enabling");

    }

    private void OnDisable()
    {
        LevelTimer.OnTimeOver -= LostLevel;
        //LevelTimer.OnTimeOver -= DeactivateTowerButtons;
        OnLevelDefeat -= DeactivateTowerButtons;
        OnLevelVictory -= PlayVictorySound;
        OnLevelVictory -= DeactivateTowerButtons;
        OnLevelRetreat -= DeactivateTowerButtons;
        //Debug.Log("Level manager disabling");
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

    private void Update() //[con]: whats this for
    {
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            WinLevel();
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
        if (BmsPlusSceneManager._instance.IsLastLvl())
        {
            OnGameWin();
        }

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

    public void RetreatMainMenu()
    {
        int numOfCurrentLives = PlayerLifeManager._instance.GetCurrentNumberOfLives();
        for (int i = 0; i < numOfCurrentLives;i++)
        {
            Debug.Log("Oops i am here");
            if(PersistentDataManager._instance != null)
            {
                PersistentDataManager._instance.IncrementNumOfConsumedLives();
            }
            PlayerLifeManager._instance.DecrementNumOfLives();
        }
        OnLevelRetreat();
    }

    private void PlayVictorySound()
    {
        Debug.Log("Play victory sound");
        SoundManager._instance.StopAllSoundEffects();
        SoundManager._instance.PlaySoundEffect(m_victorySound);
    }
}
