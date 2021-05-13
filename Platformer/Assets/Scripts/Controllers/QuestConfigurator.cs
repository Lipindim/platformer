using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Platformer
{
    public class QuestsConfigurator
    {
        private readonly QuestStoryConfig[] _questStoryConfigs;
        private readonly QuestObjectView[] _questObjects;
        private readonly List<IQuestStory> _questStories;

        private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>
        { 
            { QuestType.Switch, () => new SwitchQuestModel() },
        };

        private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>
        {
            { QuestStoryType.Common, questCollection => new QuestStory(questCollection) },
        };
        

        public QuestsConfigurator(QuestStoryConfig[] questStoryConfigs)
        {
            _questObjects = GameObject.FindObjectsOfType<QuestObjectView>();
            _questStoryConfigs = questStoryConfigs;
            //_singleQuestView = singleQuestView;
            //_singleQuest = new Quest(_singleQuestView, new SwitchQuestModel());
            //_singleQuest.Reset();

            _questStories = new List<IQuestStory>();
            foreach (var questStoryConfig in _questStoryConfigs)
            {
                _questStories.Add(CreateQuestStory(questStoryConfig));
            }

        }


        private IQuestStory CreateQuestStory(QuestStoryConfig config)
        {
            var quests = new List<IQuest>();
            foreach (var questConfig in config.quests)
            {
                // создаём квест на основе данных из ScriptableObject
                var quest = CreateQuest(questConfig);
                if (quest == null) continue;
                quests.Add(quest);
            }
            // какая логика будет у цепочки определяем по типу QuestStoryType
            return _questStoryFactories[config.questStoryType].Invoke(quests);
        }

        private IQuest CreateQuest(QuestConfig config)
        {
            var questId = config.id;
            var questView = _questObjects.FirstOrDefault(value => value.Id == config.id);
            if (questView == null)
            {
                // пытаемся найти представление для квеста
                Debug.LogWarning($"QuestsConfigurator :: Start : Can't find view of quest {questId.ToString()}");
                return null;
            }

            if (_questFactories.TryGetValue(config.questType, out var factory))
            {
                // пытаемся создать модель для квеста по типу QuestType
                var questModel = factory.Invoke();
                return new Quest(questView, questModel);
            }

            Debug.LogWarning($"QuestsConfigurator :: Start : Can't create model for quest {questId.ToString()}");
            return null;
        }
    }
}
