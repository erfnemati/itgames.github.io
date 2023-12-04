using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class AstronutAutoMovement : MonoBehaviour
{
    //Movement parameters : 

    private Vector3 m_direction = Vector3.left;
    [SerializeField] float m_speed;
    [SerializeField] float m_speedChange;
    [SerializeField] float m_maxSpeed;

    [SerializeField] AstronutUiController m_UiController;

    //Tutorial parameters : 
    [SerializeField] Transform m_targetTransform;
    private bool m_isTutorialOver = false;
    private bool m_isLevelOver = false;

    public async Task ComeAstronut()
    {
        await this.transform.DOMove(m_targetTransform.position, 3f).AsyncWaitForCompletion();
        await m_UiController.PopAstronutDialogue();
    }

    public  void  TakeBackToMid()
    {
        TurnOffMovement();
         transform.DOMove(m_targetTransform.position, 2f).WaitForCompletion();
    }

    private void Update()
    {
        if(m_isTutorialOver && m_isLevelOver == false )
        {
            MoveAstronut();
        }
        
    }

    private void MoveAstronut()
    {
        Vector3 thisFrameMovement = m_speed * m_direction * Time.deltaTime;
        transform.position += thisFrameMovement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelBoundry"))
        {
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        m_direction = -m_direction;

        if (m_direction.x < 0)
        {
            m_UiController.GoingLeft();
        }
        else
        {
            m_UiController.GoingRight();
        }
    }

    public void EndTutorial()
    {
        m_isTutorialOver = true;
    }

    private void TurnOffMovement()
    {
        m_isLevelOver = true;
    }

    public void IncreaseSpeed()
    {
        m_speed += m_speedChange;
        if (m_speed >= m_maxSpeed)
        {
            m_speed = m_maxSpeed;
        }
    }
}
