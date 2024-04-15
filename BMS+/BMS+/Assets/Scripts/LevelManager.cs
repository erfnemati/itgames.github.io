using ConfigData;
using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour, IGameService
{
    //[SerializeField] ReferenceGameBoard m_referenceGameBoard;
    //[SerializeField] GameBoard m_gameBoard;
    //[SerializeField] List<Button> m_towerButtons = new List<Button>();
    [SerializeField] AudioClip m_victorySound;
    private EventManager eventManager;
    private BmsPlusSceneManager bmsPlusSceneManager;
    private PlayerLifeManager playerLifeManager;
    private LevelInitializer levelInitalizer;
    private LevelConfigData levelConfig;
    private List<ShapeManager> shapeManagerList;
    private List<ReferenceShapeManager> referenceShapeManagerList;

    //public delegate void EndLevelAction();
    //public static event EndLevelAction OnLevelVictory;
    //public static event EndLevelAction OnLevelDefeat;
    //public static event EndLevelAction OnLevelRetreat;
    //public static event EndLevelAction OnGameWin;


    private void OnEnable()
    {

        eventManager.StartListening(EventName.OnTimeOver, new Action(this.LostLevel));
        eventManager.StartListening(EventName.OnLevelVictory,new Action(this.PlayVictorySound));

        //Debug.Log("Level manager enabling");

    }
    public void OnDestroy()
    {
        eventManager.StopListening(EventName.OnTimeOver, new Action(this.LostLevel));
        eventManager.StopListening(EventName.OnLevelVictory, new Action(this.PlayVictorySound));
        ServiceLocator._instance.Unregister<LevelManager>();
    }

    private void Awake()
    {
        ServiceLocator._instance.Register(this);
        m_victorySound = ServiceLocator._instance.Get<DataManager>().GetData<SoundConfigData>((int)SoundName.victorySound).audioClip;
        eventManager = ServiceLocator._instance.Get<EventManager>();
        levelConfig = ServiceLocator._instance.Get<BmsPlusSceneManager>().levelToLoad;
        bmsPlusSceneManager = ServiceLocator._instance.Get<BmsPlusSceneManager>();
        levelInitalizer = new LevelInitializer(levelConfig);
        new Player();
        levelInitalizer.InitalizeLevelFromConfig();
        shapeManagerList = levelInitalizer.GetGeneratedShapes();
        referenceShapeManagerList = levelInitalizer.GetGeneratedReferenceShapes();

    }

    //private void Update() //[con]: whats this for
    //{
    //    if (Input.GetKeyUp(KeyCode.RightArrow))
    //    {
    //        WinLevel();
    //    }
    //}



    public void CompareBoards()
    {

        if (shapeManagerList.Count != referenceShapeManagerList.Count)
        {
            Debug.Log("Something wrong with gameboard hexagons and reference hexagons");
            return;
        }
        
        for(int i = 0; i <shapeManagerList.Count; i++)
        {
            if (shapeManagerList[i].GetShapeColor() == referenceShapeManagerList[i].GetShapeColor())
            {
                if (shapeManagerList[i].GetShapeColor() == referenceShapeManagerList[i].GetShapeColor())
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


    private void WinLevel()
    {
        
        Debug.Log("You have won");
        eventManager.TriggerEvent(EventName.OnLevelVictory);
        if (bmsPlusSceneManager.IsLastLvl())
        {
            eventManager.TriggerEvent(EventName.OnGameWin);

        }

    }

    private void LostLevel()
    {
        Debug.Log("You have lost");
        eventManager.TriggerEvent(EventName.OnLevelDefeat);
    }

    public void RetreatLevel()
    {
        LostLevel();
    }

    public void RetreatMainMenu()
    {
        int numOfCurrentLives = playerLifeManager.GetCurrentNumberOfLives();
        for (int i = 0; i < numOfCurrentLives;i++)
        {
            Debug.Log("Oops i am here");
            ServiceLocator._instance.Get<PersistentDataManager>().IncrementNumOfConsumedLives();
            playerLifeManager.DecrementNumOfLives();
        }
        eventManager.TriggerEvent(EventName.OnLevelRetreat);
    }

    private void PlayVictorySound()
    {
        Debug.Log("Play victory sound");
        SoundManager._instance.StopAllSoundEffects();
        SoundManager._instance.PlaySoundEffect(m_victorySound);
    }
}
