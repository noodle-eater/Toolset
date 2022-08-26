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
        private static string validationKey = "CanValidate";

        static UIBindingValidator()
        {
            EditorApplication.delayCall += DeferredBinding;
            EditorApplication.quitting += () => EditorPrefs.SetBool(validationKey, false);
        }

        private static void DeferredBinding()
        {
            EditorPrefs.SetBool(validationKey, true);
            EditorApplication.delayCall -= DeferredBinding;
            EditorApplication.hierarchyChanged += ValidateUIBinding;
        }

        [DidReloadScripts]
        public static void ValidateUIBinding()
        {
            if (Application.isPlaying)
            {
                return;
            }
            
            var canValidate = EditorPrefs.GetBool(validationKey);
            if(!canValidate) 
            {
                return;
            }

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
