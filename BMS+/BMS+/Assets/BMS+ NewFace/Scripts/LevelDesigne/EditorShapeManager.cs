using GameEnums;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace LevelDesign
{

    [ExecuteInEditMode]
    public class EditorShapeManager :MonoBehaviour
    {
        public GameData.ShapeData shapeData;
        SpriteRenderer spriteRenderer;
        private void OnDestroy()
        {
            Debug.Log("bye bye");
            LevelDesignBoard._instance.shapeManagerList.Remove(this);
            LevelDesignBoard._instance.OnColorAdded -= AddColor;
            LevelDesignBoard._instance.OnColorRemoved -= RemoveColor;
        }
        private void Awake()
        {
            gameObject.AddComponent<BoxCollider2D>();
            spriteRenderer=gameObject.GetComponentsInChildren<SpriteRenderer>()[1];
            SetInitialData();
            LevelDesignBoard._instance.OnColorAdded += AddColor;
            LevelDesignBoard._instance.OnColorRemoved += RemoveColor;

        }
        public void SetInitialData()
        {
            shapeData=new GameData.ShapeData();
            shapeData.Position=transform.position;
            shapeData.shapeAddedNumber=0;
            shapeData.ColorData = new Color(0, 0, 0, 0);
        }
        public void SetPosition(Vector3 position) => shapeData.Position = position;
        public void SetID(int id)=> shapeData.shapeId = id;
        public void SetAddedNumber(int number)=> shapeData.shapeAddedNumber = number;
        public int GetShapeId()=> shapeData.shapeId;
        public void SetColorFromEditorWindow(GameEnums.GameColorName colorName)
        {
            ShapeColorData SelectedColorData = EditorDataManager._instance.GetData<ShapeColorData>((int)colorName);
            shapeData.ColorData = SelectedColorData.color;
            spriteRenderer.sprite = SelectedColorData.sprite;
        }
        public void SetColor(Color color)
        {
            shapeData.ColorData=color;
        }
        public void AddColor(int shapeEffected, Color addedColor)
        {
            if (shapeEffected == shapeData.shapeId)
            {
                shapeData.shapeAddedNumber++;
                EditorShapesColorManager shapesColorManager = new EditorShapesColorManager();
                Color color = shapesColorManager.GetCombinedColor(shapeData.ColorData, addedColor);
                shapeData.ColorData = color;
                Sprite addedColorSprite = shapesColorManager.GetSprite(color);
                if (addedColorSprite != null)
                    spriteRenderer.sprite = addedColorSprite;
                if (color == Color.white || shapesColorManager.GetColorName(color) == GameColorName.White)
                {
                    GetComponentInChildren < RTLTMPro.RTLTextMeshPro>().text="";
                }
                else
                    GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = shapeData.shapeAddedNumber.ToString();
            }
        }
        public void RemoveColor(int shapeEffected,Color RemovedColor)
        {
            if (shapeEffected==shapeData.shapeId)
            {
                shapeData.shapeAddedNumber--;
                EditorShapesColorManager shapesColorManager = new EditorShapesColorManager();
                Color color = shapesColorManager.GetSubtractedColor(shapeData.ColorData,RemovedColor);
                shapeData.ColorData = color;
                Sprite addedColorSprite = shapesColorManager.GetSprite(color);
                if(addedColorSprite!= null)
                    spriteRenderer.sprite = addedColorSprite;
                if (color == Color.white || shapesColorManager.GetColorName(color) == GameColorName.White)
                    GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = "";
                else
                    GetComponentInChildren<RTLTMPro.RTLTextMeshPro>().text = shapeData.shapeAddedNumber.ToString();

            }
        }

    }
}
