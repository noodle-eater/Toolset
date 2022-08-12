using UnityEngine;

namespace NoodleEater.Toolset.EnumGenerator
{
    [CreateAssetMenu(fileName = "New EnumGenerator", menuName = "Framework/New Enum Generator", order = 0)]
    public class EnumGenerator : ScriptableObject
    {
        [SerializeField] private string filePath;
        [SerializeField] private string nameSpace;
        [SerializeField] private string enumName;
        [SerializeField] private string[] values;

        [ContextMenu("Generate")]
        public void GenerateEnum()
        {
            EnumGenerationSystem enumGenerationSystem = new EnumGenerationSystem
            {
                Namespace = nameSpace,
                EnumName = enumName,
                FileDirectory = Application.dataPath + "/" + filePath,
                Values = values
            };
            
            enumGenerationSystem.GenerateEnum();
        }
    }
}