using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShuttleControllerThirdEdition : MonoBehaviour
{
    [SerializeField] List<Sprite> m_shuttlePartSprites = new List<Sprite>();
    [SerializeField] Sprite m_finalSprite;
    [SerializeField] Sprite m_sadSprite;
    [SerializeField] Sprite m_finalRoamingSprite;
    [SerializeField] int m_numOfNeededParts;
    private int m_shuttlePartIndex = 0;
    private SpriteRenderer m_spriteRenderer;

    [SerializeField] Transform m_targetTransform;
    [SerializeField] float m_translationCycleTime;
    [SerializeField] float m_finalScale;

    [SerializeField] GameObject m_shuttleBasis;

    //Without roaming : 
    private Vector3 m_initialPos;
    [SerializeField] Transform m_finalWithoutRoamingPos;

    //These parameters are for final rise : 
    private const int NUMBER_OF_WAYPOINTS = 5;
    [SerializeField] Transform[] m_waypointTransforms = new Transform[NUMBER_OF_WAYPOINTS];
    private Vector3[] m_waypoints = new Vector3[6];

    //For ui : 
    [SerializeField] Slider m_shuttleSlider;
    [SerializeField] Image m_fillSlider;
    [SerializeField] Gradient m_gradientColor;





    private void Start()
    {
        SetWaypoints();
        InitialiseUi();
        m_initialPos = this.transform.position;
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShuttlePart"))
        {
            RoamingInputHandler._instance.SetIsDragging(false);
            Destroy(other.gameObject);
            GetShuttlePart();
            ThirdRoamingGameManager._instance.GoNextState();
        }

        if (other.gameObject.CompareTag("RoamingBubble"))
        {
            Destroy(other.gameObject);
            m_spriteRenderer.sprite = m_finalRoamingSprite;
            m_shuttleSlider.DOValue(m_shuttleSlider.maxValue, 0.5f);
            RoamingInputHandler._instance.SetIsDragging(false);
            Invoke(nameof(StartWithRoaming),1f);
        }
    }

    private void GetShuttlePart()
    {
        m_spriteRenderer.sprite = m_shuttlePartSprites[m_shuttlePartIndex];
        m_shuttlePartIndex = (m_shuttlePartIndex + 1);
        UpdateUi(m_shuttlePartIndex);
    }

    public void StartWithoutRoaming()
    {
        
        m_spriteRenderer.sprite = m_finalSprite;
        Destroy(m_shuttleBasis.gameObject);
        Invoke(nameof(RiseWithoutRoaming), 0.5f);
        
    }

    private void RiseWithoutRoaming()
    {
        transform.DOMove(m_finalWithoutRoamingPos.position, 1f).OnComplete(()=>Invoke(nameof(FallWithoutRoaming),0.75f));
    }

    private void FallWithoutRoaming()
    {
        m_spriteRenderer.sprite = m_sadSprite;
        m_shuttleSlider.DOValue(0, 1f);
        transform.DOMove(m_initialPos, 0.5f).OnComplete(()=>GenerateRoamingBubble());
    }

    private void GenerateRoamingBubble()
    {
        ThirdRoamingGameManager._instance.GenerateRoamingBubble();
    }

    public void StartWithRoaming()
    {
        
        transform.DOPath(m_waypoints, m_translationCycleTime, PathType.CatmullRom, PathMode.TopDown2D, 5, Color.red);
        transform.DORotate(new Vector3(0, 0, 50), m_translationCycleTime * 2);
        transform.DOScale(m_finalScale, m_translationCycleTime).OnComplete(()=>ThirdRoamingGameManager._instance.ShowResultMenu());
    }

    public void UpdateUi(int numOfShuttleParts)
    {
        m_shuttleSlider.DOValue(m_shuttlePartIndex, 0.5f);
        m_fillSlider.color = m_gradientColor.Evaluate((float)m_shuttleSlider.value/m_shuttleSlider.maxValue);
        
    }

    private void InitialiseUi()
    {
        m_shuttleSlider.maxValue = 6;
    }

    private void SetWaypoints()
    {
        for (int i = 0; i<m_waypointTransforms.Length; i++)
        {
            m_waypoints[i] = m_waypointTransforms[i].position;
        }
    }
}
