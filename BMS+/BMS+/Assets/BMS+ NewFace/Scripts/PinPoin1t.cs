using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEnums;
using ConfigData;
using System.Linq;
using RTLTMPro;
using static UnityEngine.GraphicsBuffer;
using System;
public class Pinpoint1 : MonoBehaviour
{
    [SerializeField] List<ShapeManager> m_ownedShapes; //[Arch]: how to handle this in board designer 
    private Sprite pinpointInitalSprite;
    private Color pinpointInitialColor;
    private Button m_pinPoint;
    private Pin1 m_currentPin = null;
    [SerializeField] bool m_isTutorialMode = false;
    private VectorInt m_PinPointColor;
    public VectorInt GetPinPointColor { get { return m_PinPointColor; } }

    //Ui stuff here,maybe recycle later:
    private float m_initialPinPointWidth;
    private float m_initialPinPointHeight;
    private RectTransform m_pinPointRect;
    private float m_additionalPinPointScaleFactor = 1.5f;

    [SerializeField] AudioClip m_pinSound;

    // PinPoint Data
    private List<int> m_shapesId; 
    private DataManager dataManager;
    private LevelManager levelManager;
    private EventManager eventManager;
    private Player1 player;

    private void Awake()
    {
        dataManager = ServiceLocator._instance.Get<DataManager>();
        levelManager = ServiceLocator._instance.Get<LevelManager>();
        player = ServiceLocator._instance.Get<Player1>();
        eventManager = ServiceLocator._instance.Get<EventManager>();    
             
    }
    public void DisableButton()
    {
        m_pinPoint.enabled = false;
    }
    public void InitializePinPoint(List<ShapeManager> ConfigownedShapes, VectorInt configPinPointColor, Color initialpinpointColor, List<int> m_shapesId)
    {
        m_ownedShapes = ConfigownedShapes;
        m_PinPointColor = configPinPointColor;
        pinpointInitialColor = initialpinpointColor;
        this.m_shapesId = m_shapesId;

    }
    private void Start()
    {
        InitializeVariables();
    }
    private void InitializeVariables()
    {
        pinpointInitalSprite = dataManager.GetData<ConfigData.SpriteConfigData>((int)SpriteName.PinPoint).sprite;
        m_pinSound = dataManager.GetData<ConfigData.SoundConfigData>((int)SoundName.playerChoosingSound).audioClip;
        m_pinPointRect = GetComponent<RectTransform>();
        m_pinPoint = GetComponent<Button>();
        m_initialPinPointHeight = m_pinPointRect.rect.height;
        m_initialPinPointWidth = m_pinPointRect.rect.width;
    }
 //   public void SetClickEvent() => GetComponent<Button>().onClick.AddListener(() => GetComponent<Pinpoint1>().ClickPinPoint()); //[kasif] idk witch componant should handle this;


    private void ChangePinpointSprite()
    {
        if (m_currentPin == null)
        {
            RemoveSprite();
        }
        else
        {
            AddSprite();
        }
    }

    private void AddSprite()
    {
        Sprite chosenSprite = ChooseSprite();

        float heightToWidthAspectRatio = chosenSprite.rect.height / chosenSprite.rect.width;

        m_pinPointRect.sizeDelta =
            new Vector2(m_additionalPinPointScaleFactor * m_initialPinPointWidth,
            m_additionalPinPointScaleFactor * m_initialPinPointWidth * heightToWidthAspectRatio);

        m_pinPoint.image.color = new Color(255, 255, 255, 255);
    }

    private Sprite ChooseSprite()
    {
        return dataManager.GetData<ConfigData.PinConfigData>(m_PinPointColor).sprite;
    }

    private void RemoveSprite()
    {
        m_pinPointRect.sizeDelta = new Vector2(m_initialPinPointWidth, m_initialPinPointHeight);
        m_pinPoint.image.sprite = pinpointInitalSprite;
        m_pinPoint.image.color = pinpointInitialColor;
    }

    public void ClickPinPoint()
    {
        if (m_currentPin == null && player.GetPlayerPin() != null)
        {
            AddPin(player.GetPlayerPin());
            SoundManager._instance.PlaySoundEffect(m_pinSound);//I should recycle it later.
            player.ReleasePin();
        }
        else if (m_currentPin != null)
        {
            player.ReloadPin(m_currentPin);
            SoundManager._instance.PlaySoundEffect(m_pinSound);//I should recycle it later.
            RemovePin();
        }

        if (m_isTutorialMode == false)
        {
            levelManager.CompareBoards();
        }


    }

    public void AddPin(Pin1 pin)
    {
        if (m_currentPin == null && pin.m_numOfUsages>0)
        {
            m_currentPin = pin;
            m_PinPointColor = pin.GetPinColor();
            InvokeAddColorEvent(m_PinPointColor);
            Image image = gameObject.GetComponent<Image>();
            image.sprite = dataManager.GetData<PinConfigData>(m_PinPointColor).sprite;
            image.color = Color.white;
        }

    }

    private void RemovePin()
    {
        if (m_currentPin != null)
        {
            InvokeRemoveColorEvent(m_PinPointColor);
            m_currentPin = null;
            Image image = gameObject.GetComponent<Image>();
            image.sprite = dataManager.GetData<SpriteConfigData>((int)SpriteName.PinPoint).sprite;
            image.color = Color.black;

        }
    }

    public void InvokeAddColorEvent(VectorInt addedColor)
    {
        switch (GetPinName())
        {
            case GameEnums.PinName.Unkown:
                List<int> orderedShapes = m_shapesId.OrderByDescending(n => n).ToList();
                List<VectorInt> addedColors = new List<VectorInt> { VectorInt.Red, VectorInt.Green, VectorInt.Blue };
                for (int i = 0; i < m_shapesId.Count; i++)
                {
                    eventManager.TriggerEvent<int, VectorInt>(EventName.OnColorAdded, orderedShapes[i], addedColors[i]);
                }
                break;
            default:
                for (int i = 0; i < m_shapesId.Count; i++)
                    eventManager.TriggerEvent<int, VectorInt>(EventName.OnColorAdded, m_shapesId[i], addedColor);

                break;
        }
    }
    public void InvokeRemoveColorEvent(VectorInt removedColor)
    {
        switch (GetPinName())// should this be implemented here?
        {
            case GameEnums.PinName.Unkown:
                List<int> orderedShapes = m_shapesId.OrderByDescending(n => n).ToList();
                List<VectorInt> RemovedColors = new List<VectorInt> { VectorInt.Red, VectorInt.Green, VectorInt.Blue };
                for (int i = 0; i < m_shapesId.Count; i++)
                {
                    eventManager.TriggerEvent<int, VectorInt>(EventName.OnColorRemoved, orderedShapes[i], RemovedColors[i]);
                }
                break;
            default:
                for (int i = 0; i < m_shapesId.Count; i++)
                    eventManager.TriggerEvent<int, VectorInt>(EventName.OnColorRemoved, m_shapesId[i], removedColor);
                break;
        }
    }
    private GameEnums.PinName GetPinName() => dataManager.GetData<PinConfigData>(m_PinPointColor).name;
}
