using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronutCollisionController : MonoBehaviour
{
    private AstronutValueController m_astronutValueController;
    private AstronutAutoMovement m_astronutAutoMovement;

    private void Start()
    {
        m_astronutValueController = GetComponent<AstronutValueController>();
        m_astronutAutoMovement = GetComponent<AstronutAutoMovement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if(collision.CompareTag("AddedValueBubble"))
        {
            m_astronutValueController.AddValue(collision.GetComponent<AddedValueBubble>().GetBubbleValue());
            AddedValueBubbleGenerator._instance.ReduceNumberOfActiveValueBubbles();
            m_astronutAutoMovement.IncreaseSpeed();
            m_astronutValueController.IncreaseDrainRate();
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("MoonBubble"))
        {
            AlwaysOnNigthsGameManager._instance.WinGame();
            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("TutorialAddedValueBubble"))
        {
            Destroy(collision.gameObject);
            AlwaysOnNigthsGameManager._instance.EndTutorial();
        }
    }
}
