using UnityEngine;


namespace Platformer
{
    [CreateAssetMenu(menuName = "configs/Quest story config", fileName = "QuestStoryConfig", order = 0)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType questStoryType;
    }
    public enum QuestStoryType
    {
        Common,
        Resettable
    }

}
