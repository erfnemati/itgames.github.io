using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTLTMPro;
using System.Linq;

public class ShapeManager: MonoBehaviour
{
    protected ShapesColorManager colorManager;
    [SerializeField] protected int m_numOfAddedColors = 0;
    [SerializeField] protected Color m_currentColor;
    [SerializeField] protected RTLTextMeshPro m_numberOfAddedColorsText;
    [SerializeField] protected SpriteRenderer m_spriteRenderer;
    public int shapeId { get; private set; }
    public void InitializeShape(Color ConfigColor, int numberOfAddedColor, int shapeId)
    {
        this.m_currentColor = ConfigColor;
        this.m_numOfAddedColors = numberOfAddedColor;
        this.shapeId = shapeId;
    }
    private void Start()
    {
        colorManager = new ShapesColorManager();
        m_numberOfAddedColorsText= gameObject.GetComponentsInChildren<RTLTextMeshPro>().First();
        m_spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        UpdateNumOfAddedColorsText();
        UpdateSprite();
    }

    public void AddColor(Color chosenColor)
    {
        m_currentColor=colorManager.GetCombinedColor(m_currentColor,chosenColor); //ReloadColorFromAddition(chosenColor);
        UpdateSprite();
        m_numOfAddedColors++; // in chikar mikone
        UpdateNumOfAddedColorsText();
    }

    public void DeleteColor(Color deletedColor)
    {
        m_currentColor=colorManager.GetSubtractedColor(m_currentColor,deletedColor);
        m_numOfAddedColors--;
        UpdateSprite();
        UpdateNumOfAddedColorsText();
    }

    protected void UpdateNumOfAddedColorsText()
    {
        if (m_numOfAddedColors > 0)
        {
            m_numberOfAddedColorsText.text = m_numOfAddedColors + "";
        }
        else
        {
            m_numberOfAddedColorsText.text = " ";
        }


    }

    protected void UpdateSprite()
    {
        //m_spriteRenderer.sprite =  HexagonSpriteManager._instance.GetHexagonSprite(m_currentColor);
        if (m_spriteRenderer == null)
        {
            Debug.Log("Sprite null here");
        }
        m_spriteRenderer.sprite = colorManager.GetSprite(m_currentColor);//HexagonSpritePicker._instance.GetHexagonSprite(m_currentColor);

    }

    public Color GetShapeColor()
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
