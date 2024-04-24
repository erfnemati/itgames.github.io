using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using  UnityEngine.UI;

namespace LevelDesign
{

    [ExecuteInEditMode]
    public class EditorPinPoint : MonoBehaviour
    {
        public GameData.PinPointData pinPointData;
        public EditorPin stationedPin { get; set; }
        public Sprite initialSprite { get; set; }

        private void OnDestroy()
        {
            LevelDesignBoard._instance.pinPointList.Remove(this);
        }
        private void Awake()
        {
            SetInitialData();
            Image image = GetComponent<Image>();
            initialSprite = image.sprite;// khoob nist;
        }
        public void SetInitialData()
        {
            pinPointData = new GameData.PinPointData();
            pinPointData.position = transform.localPosition;
            pinPointData.InitialColor = Color.black;
            pinPointData.neighborShapes = new List<int>();
        }
        public void SetNeighborShapes()
        {
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(new Vector2(rect.position.x,rect.position.y), rect.rect.width);
            Debug.Log(hitColliders.Length);
            foreach(Collider2D hitCollider in hitColliders)
            {
                EditorShapeManager shapeManager = new EditorShapeManager();
                hitCollider.gameObject.TryGetComponent<EditorShapeManager>(out shapeManager);

                int shapeId = shapeManager.GetShapeId();
                //Debug.Log(shapeId);
                pinPointData.neighborShapes.Add(shapeId);
            }
        }
        public void InvokeAddColorEvent(VectorInt addedColor)
        {
            switch (stationedPin.pinColorData.name)
            {
                case GameEnums.PinName.Unkown:
                    List<int> orderedShapes=pinPointData.neighborShapes.OrderByDescending(n=>n).ToList();
                    List<VectorInt> addedColors = new List<VectorInt> { VectorInt.Red, VectorInt.Green, VectorInt.Blue };
                    for (int i =0; i<pinPointData.neighborShapes.Count; i++)
                    {
                        LevelDesignBoard._instance.OnColorAdded?.Invoke(orderedShapes[i], addedColors[i]);
                        Debug.Log(addedColors[i]);
                        Debug.Log(pinPointData.neighborShapes[i]);
                    }
                    break;
                default:
                    for (int i = 0; i < pinPointData.neighborShapes.Count; i++)
                        LevelDesignBoard._instance.OnColorAdded?.Invoke(pinPointData.neighborShapes[i], addedColor);
                    break;
            }
        }
        public void InvokeRemoveColorEvent(VectorInt removedColor)
        {
            switch (stationedPin.pinColorData.name)// should this be implemented here?
            {
                case GameEnums.PinName.Unkown:
                    List<int> orderedShapes = pinPointData.neighborShapes.OrderByDescending(n => n).ToList();
                    List<VectorInt> RemovedColors = new List<VectorInt> { VectorInt.Red, VectorInt.Green, VectorInt.Blue };
                    for (int i = 0; i < pinPointData.neighborShapes.Count; i++)
                    {
                        Debug.Log(RemovedColors[i]);
                        Debug.Log(pinPointData.neighborShapes[i]);
                        LevelDesignBoard._instance.OnColorRemoved?.Invoke(orderedShapes[i], RemovedColors[i]);
                    }
                    break;
                default:
                    for (int i = 0; i < pinPointData.neighborShapes.Count; i++)
                        LevelDesignBoard._instance.OnColorRemoved?.Invoke(pinPointData.neighborShapes[i], removedColor);
                    break;
            }
        }
    }


}
