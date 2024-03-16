using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinpoint1 : MonoBehaviour
{
    [SerializeField] List<ShapeManager> m_ownedShapes; //[Arch]: how to handle this in board designer

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

    public void InitializePinPoint(List<ShapeManager> ConfigownedShapes, Color configPinPointColor)
    {
        m_ownedShapes = ConfigownedShapes;
        m_PinPointColor = configPinPointColor;

    }
    private void Start()
    {
        InitializeVariables();
        SetClickEvent();
    }
    private void InitializeVariables()
    {
        m_initialPinPointHeight = m_pinPointRect.rect.height;
        m_initialPinPointWidth = m_pinPointRect.rect.width;
        m_pinPointRect = GetComponent<RectTransform>();
        m_pinPoint = GetComponent<Button>();
    }
    private void SetClickEvent() => m_pinPoint.onClick.AddListener(() => GetComponent<Pinpoint>().ClickPinPoint()); // why??


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
        return DataManager._instance.GetColorData<PinColorData>(m_PinPointColor).sprite;
    }

    private void RemoveSprite()
    {
        m_pinPointRect.sizeDelta = new Vector2(m_initialPinPointWidth, m_initialPinPointHeight);
       // m_pinPoint.image.sprite = # need a new DataManager Config for this sprite
       // m_pinPoint.image.color = # what is the initial color?
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
            LevelManager1._instance.CompareBoards();
        }


    }

    public void AddPin(Pin1 chosenPin)
    {
        m_currentPin = chosenPin;
        if (m_currentPin != null)
        {
            Debug.Log("This pinpoint is full");
            return;
        }
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
