using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelDesign
{

    [ExecuteInEditMode]
    public class EditorShapeManager :MonoBehaviour
    {
        public GameData.ShapeData shapeData;
        public GameData.EventData shapeEvent;
        public GameData.EventData event2Save;
        SpriteRenderer spriteRenderer;
        EditorShapesColorManager shapesColorManager;
        private void OnDestroy()
        {
            Debug.Log("bye bye");
            LevelDesignBoard._instance.shapeManagerList.Remove(this);
            LevelDesignBoard._instance.OnColorAdded -= AddColor;
            LevelDesignBoard._instance.OnColorRemoved -= RemoveColorRoutine;
        }
        private void Awake()
        {
            gameObject.AddComponent<BoxCollider2D>();
            spriteRenderer=gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
            shapesColorManager=new EditorShapesColorManager(); ;
            SetInitialData();
            shapeEvent = new GameData.EventData();
            LevelDesignBoard._instance.OnColorAdded += AddColor;
            LevelDesignBoard._instance.OnColorRemoved += RemoveColorRoutine;

        }
        public void SetInitialData()
        {
            shapeData=new GameData.ShapeData();
            shapeData.Position=transform.localPosition;
            shapeData.shapeAddedNumber=0;
            shapeData.ColorData = VectorInt.White;
        }
        public void SetPosition() => shapeData.Position = transform.localPosition;
        public void SetID(int id)=> shapeData.shapeId = id;
        public void SetAddedNumber(int number)=> shapeData.shapeAddedNumber = number;
        public int GetShapeId()=> shapeData.shapeId;
        public void SetColorFromEditorWindow(GameEnums.GameColorName colorName)
        {
            ConfigData.ShapeConfigData SelectedColorData = LevelDesignBoard._instance.GetData<ConfigData.ShapeConfigData>((int)colorName);
            shapeData.ColorData = SelectedColorData.color;
            spriteRenderer.sprite = SelectedColorData.sprite;
        }
        public void AddColor(int shapeEffected, VectorInt addedColor)
        {
            if (shapeEffected == shapeData.shapeId)
            {
                if(shapeData.ColorData != VectorInt.Jammed)
                    AddColor(addedColor);
                else if (addedColor == VectorInt.Jammed)
                    AddColor(addedColor);
                else
                    shapeData.shapeAddedNumber++;
            }
        }

        private void AddColor(VectorInt addedColor)
        {
            shapeData.shapeAddedNumber++;
            VectorInt color = shapesColorManager.GetCombinedColor(shapeData.ColorData, addedColor);
            shapeData.ColorData = color;
            Sprite addedColorSprite = shapesColorManager.GetSprite(color);
            UpdateNumOfAddedColorsText();
            UpdateSprite();
        }

        public void RemoveColorRoutine(int shapeEffected, VectorInt RemovedColor)
        {
            if (shapeEffected==shapeData.shapeId)
            {
                if(shapeData.ColorData != VectorInt.Jammed)
                {
                    RemoveColor(RemovedColor);
                }
                else if (RemovedColor == VectorInt.Jammed)
                {
                    RemoveColor(RemovedColor);
                }
                else
                    shapeData.shapeAddedNumber--;


            }
        }

        private void RemoveColor(VectorInt RemovedColor)
        {
            shapeData.shapeAddedNumber--;
            VectorInt color = shapesColorManager.GetSubtractedColor(shapeData.ColorData, RemovedColor);
            shapeData.ColorData = color;
            UpdateNumOfAddedColorsText();
            UpdateSprite();
        }

        private void UpdateNumOfAddedColorsText()
        {
            if (shapesColorManager.GetColorName(shapeData.ColorData) == GameColorName.White || shapesColorManager.GetColorName(shapeData.ColorData) == GameColorName.Jam)
                GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = "";
            else
                GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = shapeData.shapeAddedNumber.ToString();

        }       
        public void EventAddedNumber(int number)
        {
            Debug.Log(shapeData.ColorData);
            if (shapesColorManager.GetColorName(shapeEvent.changeToColor) == GameColorName.White || shapesColorManager.GetColorName(shapeEvent.changeToColor) == GameColorName.Jam)
                GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = "";
            else
                GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = number.ToString();

        }

        protected void UpdateSprite()
        {
            Sprite addedColorSprite = shapesColorManager.GetSprite(shapeData.ColorData);
            if (addedColorSprite != null)
                spriteRenderer.sprite = addedColorSprite;
        }

        public void SaveEventData()
        {
            event2Save = new GameData.EventData();
            event2Save.time = shapeEvent.time;
            event2Save.shapeId = shapeData.shapeId;
            event2Save.shapeAddedNumber = shapeEvent.shapeAddedNumber;
            event2Save.changeToColor = shapeEvent.changeToColor;
        }
    }
}
