using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;

public class AstronutUiController : MonoBehaviour
{
    [SerializeField] Sprite m_goingRight;
    [SerializeField] Sprite m_goingLeft;

    [SerializeField] Canvas m_astronutCanvas;
    [SerializeField] float finalScale;

    private SpriteRenderer m_spriteRenderer;


    private void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GoingRight()
    {
        m_spriteRenderer.sprite = m_goingRight;
    }

    public void GoingLeft()
    {
        m_spriteRenderer.sprite = m_goingLeft;
    }

    public async Task  PopAstronutDialogue()
    {
        m_astronutCanvas.gameObject.SetActive(true);
        m_astronutCanvas.GetComponent<RectTransform>().localScale = Vector3.zero;
        await m_astronutCanvas.GetComponent<RectTransform>().DOScale(finalScale, 0.5f).AsyncWaitForCompletion();
    }
    
}
