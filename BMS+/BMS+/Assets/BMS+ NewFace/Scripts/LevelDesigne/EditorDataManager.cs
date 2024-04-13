using GameEnums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace LevelDesign
{

    [ExecuteInEditMode]
    public class EditorDataManager : DataManager
    {
        public static EditorDataManager _instance;
        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                DestroyImmediate(gameObject);
        }
    }
}
