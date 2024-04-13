using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using System.Linq;
using GameEnums;
using System;

public class ShapeManager: MonoBehaviour
{
    protected ShapesColorManager colorManager;
    [SerializeField] protected int m_numOfAddedColors = 0;
    [SerializeField] protected VectorInt m_currentColor;
    [SerializeField] protected RTLTextMeshPro m_numberOfAddedColorsText;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;

    protected EventManager eventManager;
    public int shapeId { get; protected set; }
    private void OnEnable()
    {
        eventManager.StartListening(EventName.OnColorAdded, new Action<int,VectorInt>(AddColorRoutine));
        eventManager.StartListening(EventName.OnColorRemoved, new Action<int,VectorInt>(RemoveColorRoutine));

    }
    private void OnDisable()
    {
        eventManager.StopListening(EventName.OnColorAdded, new Action<int, VectorInt>(AddColorRoutine));
        eventManager.StopListening(EventName.OnColorRemoved, new Action<int, VectorInt>(RemoveColorRoutine));
    }
    public void InitializeShape(VectorInt ConfigColor, int numberOfAddedColor, int shapeId)
    {
        this.m_currentColor = ConfigColor;
        this.m_numOfAddedColors = numberOfAddedColor;
        this.shapeId = shapeId;
    }
    private void Awake()
    {
        eventManager = ServiceLocator._instance.Get<EventManager>();
    }
    private void Start()
    {
        colorManager = new ShapesColorManager();
        m_numberOfAddedColorsText= gameObject.GetComponentsInChildren<RTLTextMeshPro>().First();
        m_spriteRenderer = gameObject.GetComponentsInChildren<SpriteRenderer>()[1];

        UpdateNumOfAddedColorsText();
        UpdateSprite();
    }

    public void AddColorRoutine(int shapeEffected, VectorInt addedColor)
    {
        if (shapeEffected == shapeId)
        {
            if (m_currentColor != VectorInt.Jammed)
            {
                AddColor(addedColor);
            }
            else if (addedColor == VectorInt.Jammed)
            {
                AddColor(addedColor);
            }
            else
                m_numOfAddedColors++;
        }
    }

    private void AddColor(VectorInt addedColor)
    {
        m_numOfAddedColors++;
        VectorInt color = colorManager.GetCombinedColor(m_currentColor, addedColor);
        m_currentColor = color;
        UpdateNumOfAddedColorsText();
        UpdateSprite();
    }

    public void RemoveColorRoutine(int shapeEffected, VectorInt RemovedColor)
    {
        if (shapeEffected == shapeId)
        {
            if (m_currentColor != VectorInt.Jammed)
            {
                RemoveColor(RemovedColor);
            }
            else if (RemovedColor == VectorInt.Jammed)
            {
                RemoveColor(RemovedColor);
            }
            else
                m_numOfAddedColors--;

        }
    }

    private void RemoveColor(VectorInt RemovedColor)
    {
        m_numOfAddedColors--;
        VectorInt color = colorManager.GetSubtractedColor(m_currentColor, RemovedColor);
        m_currentColor = color;

        UpdateNumOfAddedColorsText();
        UpdateSprite();
    }

    protected void UpdateNumOfAddedColorsText()
    {
            if (colorManager.GetColorName(m_currentColor) == GameColorName.White || colorManager.GetColorName(m_currentColor) == GameColorName.Jam)
                m_numberOfAddedColorsText.text = "";
            else
                m_numberOfAddedColorsText.text = m_numOfAddedColors.ToString();

    }

    protected void UpdateSprite()
    {
        Sprite addedColorSprite = colorManager.GetSprite(m_currentColor);
        if (addedColorSprite != null)
            m_spriteRenderer.sprite = addedColorSprite;
    }

    public VectorInt GetShapeColor()
    {
        return m_currentColor;
    }

    //public void SetHexagonColor(HexagonColor hexagonColor)
    //{
    //    m_currentColor = hexagonColor;
    //    UpdateSprite();
    //}

    public int GetShapeNumber()
    {
        return m_numOfAddedColors;
    }
}
