using System;
using System.Collections.Generic;
using System.Linq;
using NoodleEater.Utility;
using UnityEngine;

namespace NoodleEater.Canvas
{
    public class GameCanvasSystem : MonoBehaviour
    {
        private Dictionary<Type, object> _canvasUI = new();

        public void Awake()
        {
            IEnumerable<CanvasUI> result = ReflectionUtil.GetEnumerableOfType<CanvasUI>();

            foreach(CanvasUI ui in result) 
            {
                _canvasUI.Add(ui.GetType(), ui);
            }

            var fields = ReflectionUtil.GetFieldWithAttribute<BindUI>();
            
            fields.ToList().ForEach(item => 
            {
                var components = FindObjectsOfType(item.FieldType, true);
                foreach(var comp in components) {
                    if(comp.name == item.Name) {
                        var source = GetUI(item.DeclaringType);
                        item.SetValue(source, comp);
                    }
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