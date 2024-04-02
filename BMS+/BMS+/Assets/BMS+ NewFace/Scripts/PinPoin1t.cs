using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameEnums;
public class Pinpoint1 : MonoBehaviour
{
    [SerializeField] List<ShapeManager> m_ownedShapes; //[Arch]: how to handle this in board designer 
    private Sprite pinpointInitalSprite;
    private Color pinpointInitialColor;
    private Button m_pinPoint;
    private Pin1 m_currentPin = null;
    [SerializeField] bool m_isTutorialMode = false;
    private Color m_PinPointColor;

    //Ui stuff here,maybe recycle later:
    private float m_initialPinPointWidth;
    private float m_initialPinPointHeight;
    private RectTransform m_pinPointRect;
    private float m_additionalPinPointScaleFactor = 1.5f;

    [SerializeField] AudioClip m_pinSound;



    /// <summary>
    /// add an Initialize() => do all the initialization
    /// </summary>

    //[q]: should all of data get initialized via level Manager
    public void DisableButton()
    {
        m_pinPoint.enabled = false;
    }
    public void InitializePinPoint(List<ShapeManager> ConfigownedShapes, Color configPinPointColor, Color initialpinpointColor )
    {
        m_ownedShapes = ConfigownedShapes;
        m_PinPointColor = configPinPointColor;
        pinpointInitialColor = initialpinpointColor;

    }
    private void Start()
    {
        InitializeVariables();
        SetClickEvent();
    }
    private void InitializeVariables()
    {
        pinpointInitalSprite = DataManager._instance.GetData<GameSpriteData>((int)SpriteName.PinPoint).sprite;
        m_pinPointRect = GetComponent<RectTransform>();
        m_pinPoint = GetComponent<Button>();
        m_initialPinPointHeight = m_pinPointRect.rect.height;
        m_initialPinPointWidth = m_pinPointRect.rect.width;
    }
    private void SetClickEvent() => m_pinPoint.onClick.AddListener(() => GetComponent<Pinpoint1>().ClickPinPoint()); // why??


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
        return DataManager._instance.GetData<PinColorData>(m_PinPointColor).sprite;
    }

    private void RemoveSprite()
    {
        m_pinPointRect.sizeDelta = new Vector2(m_initialPinPointWidth, m_initialPinPointHeight);
        m_pinPoint.image.sprite = pinpointInitalSprite;
        m_pinPoint.image.color = pinpointInitialColor;
    }

    public void ClickPinPoint()
    {
        if (m_currentPin == null && Player._instance.GetPlayerPin() != null)
        {
            AddPin(Player1._instance.GetPlayerPin());
            SoundManager._instance.PlaySoundEffect(m_pinSound);//I should recycle it later.
            Player1._instance.ReleasePin();
        }
        else if (m_currentPin != null)
        {
            Player1._instance.ReloadPin(m_currentPin);
            SoundManager._instance.PlaySoundEffect(m_pinSound);//I should recycle it later.
            RemovePin();
        }

        if (m_isTutorialMode == false)
        {
            ServiceLocator.Current.Get<LevelManager1>().CompareBoards();
        }


    }

    public void AddPin(Pin1 chosenPin)
    {
        if (m_currentPin != null)
        {
            Debug.Log("This pinpoint is full");
            return;
        }
        m_currentPin = chosenPin;
        UpdateShapeColorsAfterPinAddition(chosenPin);
        ChangePinpointSprite();

    }

    private void RemovePin()
    {
        if (m_currentPin == null)
        {
            return;
        }

        UpdateShapeColorsAfterPinDeletion();
        m_currentPin = null;
        ChangePinpointSprite();

    }

    private void UpdateShapeColorsAfterPinDeletion()
    {
        foreach (ShapeManager tempShape in m_ownedShapes)
        {
            tempShape.DeleteColor(m_PinPointColor);
        }

    }

    private void UpdateShapeColorsAfterPinAddition(Pin1 chosenPin)
    {

        foreach (ReferenceShapeManager tempShape in m_ownedShapes)
        {
            tempShape.AddColor(chosenPin.GetPinColor());
        }
    }
}
