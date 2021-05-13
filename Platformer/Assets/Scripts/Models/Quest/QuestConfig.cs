using UnityEngine;


namespace Platformer
{
    [CreateAssetMenu(menuName = "configs/Quest config", fileName = "QuestConfig", order = 0)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType questType;
    }

    public enum QuestType
    {
        Switch,
    }

}
