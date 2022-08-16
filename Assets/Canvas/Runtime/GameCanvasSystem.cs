using System;
using System.Collections.Generic;
using System.Linq;
using NoodleEater.Utility;
using UnityEditor;
using UnityEngine;

namespace NoodleEater.Canvas
{
    public class GameCanvasSystem : MonoBehaviour
    {
        private Dictionary<Type, object> _canvasUI = new();

        public void Awake()
        {
            var result = ReflectionUtil.GetEnumerableOfType<CanvasUI>();

            foreach(CanvasUI ui in result) 
            {
                _canvasUI.Add(ui.GetType(), ui);
            }

            var fields = ReflectionUtil.GetFieldWithAttribute<BindUI>();
            
            fields.ToList().ForEach(item => 
            {
                var components = FindObjectsOfType(item.FieldType, true);
                var canFindRef = false;
                foreach(var comp in components) {
                    if(comp.name == item.Name) {
                        var source = GetUI(item.DeclaringType);
                        item.SetValue(source, comp);
                        canFindRef = true;
                    }
                }
                
                if (!canFindRef)
                {
                    Debug.LogError($"[Game Canvas] Failed to Bind {item.DeclaringType}.{item.Name}, cannot find reference.");
                }
            });

            foreach(CanvasUI ui in result)
            {
                ui.Initialize();
            }
        }

        private object GetUI(Type declaringType)
        {
            if(_canvasUI.ContainsKey(declaringType)) 
            {
                return _canvasUI[declaringType];
            }

            return default;
        }

        public T GetUI<T>() where T : CanvasUI
        {
            if(_canvasUI.ContainsKey(typeof(T))) 
            {
                return (T)_canvasUI[typeof(T)];
            }

            return default;
        }
    }
}