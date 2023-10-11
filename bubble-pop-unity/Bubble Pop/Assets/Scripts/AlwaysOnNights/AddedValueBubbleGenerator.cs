using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AddedValueBubbleGenerator : MonoBehaviour
{
    public static AddedValueBubbleGenerator _instance;

    [SerializeField] List<Transform> m_addedValueSpots = new List<Transform>();
    [SerializeField] Dictionary<int, int> m_savedIndecies = new Dictionary<int, int>();

    private int m_numOfActiveBubbles = 0;

    [SerializeField] GameObject m_addedValueGameObject;


    private void Start()
    {
        SetInstance();

        FillSavedIndices();
        GenerateValueBubbles();
    }
    
    private void SetInstance()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void FillSavedIndices()
    {
        m_savedIndecies.Clear();

        for(int i = 0; i < m_addedValueSpots.Count; i++)
        {
            m_savedIndecies.Add(i, i);
            
        }
    }

    private void GenerateValueBubbles()
    {
        GameObject tempGameObject = null;
        int numberOfToBegeneratedBubbles = Random.Range(1, 4);
        
        for(int i = 0; i < numberOfToBegeneratedBubbles;i++)
        {
            int randomKey = m_savedIndecies.Keys.ElementAt( Random.Range(0, m_savedIndecies.Keys.Count));
            int finalIndex = m_savedIndecies[randomKey];
            m_savedIndecies.Remove(randomKey);
            tempGameObject = Instantiate(m_addedValueGameObject, m_addedValueSpots[finalIndex].position, Quaternion.identity);
            m_numOfActiveBubbles++;
        }
        Debug.Log("Number of active generated bubbles are : " + m_numOfActiveBubbles);
    }

    public void ReduceNumberOfActiveValueBubbles()
    {
        //Every bubble when go out of scene need to call this function : 
        m_numOfActiveBubbles--;
        CheckGenerationTime();

    }

    private void CheckGenerationTime()
    {
        if(m_numOfActiveBubbles <= 0)
        {
            Debug.Log("Number of active bubbles are : " + m_numOfActiveBubbles);
            FillSavedIndices();
            GenerateValueBubbles();
        }
    }
}

