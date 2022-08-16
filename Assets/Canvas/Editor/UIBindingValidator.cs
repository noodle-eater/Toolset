using System.Linq;
using NoodleEater.Utility;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace NoodleEater.Canvas.Editor
{
    [InitializeOnLoad]
    public static class UIBindingValidator
    {
        static UIBindingValidator()
        {
            EditorApplication.hierarchyChanged += ValidateUIBinding;
        }
        
        [DidReloadScripts]
        public static void ValidateUIBinding()
        {
            var fields = ReflectionUtil.GetFieldWithAttribute<BindUI>();
            
            fields.ToList().ForEach(item => 
            {
                var components = Object.FindObjectsOfType(item.FieldType, true);
                var canFindRef = false;
                foreach(var comp in components) {
                    if(comp.name == item.Name)
                    {
                        canFindRef = true;
                        break;
                    } 
                }

                if (!canFindRef)
                {
                    Debug.LogError($"[Game Canvas] Failed to Bind {item.DeclaringType}.{item.Name}, cannot find reference.");
                }
            });
        }
    }
}