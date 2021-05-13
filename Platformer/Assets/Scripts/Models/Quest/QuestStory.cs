using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer
{
    public sealed class QuestStory : IQuestStory
    {
        #region Fields

        private readonly List<IQuest> _questsCollection;

        #endregion

        #region Life Cycle

        public QuestStory(List<IQuest> questsCollection)
        {
            // квесты загружаются в цепочку извне
            _questsCollection = questsCollection ?? throw new ArgumentNullException(nameof(questsCollection));
            Subscribe();
            // старт первого квеста
            ResetQuest(0);
        }

        #endregion

        #region Methods

        private void Subscribe()
        {
            foreach (var quest in _questsCollection) quest.Completed += OnQuestCompleted;
        }

        private void Unsubscribe()
        {
            foreach (var quest in _questsCollection) quest.Completed -= OnQuestCompleted;
        }

        private void OnQuestCompleted(object sender, IQuest quest)
        {
            var index = _questsCollection.IndexOf(quest);
            if (IsDone)
            {
                Debug.Log("Story done!");
            }
            else
            {
                // если очередной квест выполнен, запускаем следующий квест
                ResetQuest(++index);
            }
        }

        private void ResetQuest(int index)
        {
            if (index < 0 || index >= _questsCollection.Count) return;
            var nextQuest = _questsCollection[index];
            if (nextQuest.IsCompleted) OnQuestCompleted(this, nextQuest);
            else _questsCollection[index].Reset();
        }

        #endregion

        #region IQuestStory

        public bool IsDone => _questsCollection.All(value => value.IsCompleted);

        public void Dispose()
        {
            Unsubscribe();
            foreach (var quest in _questsCollection) quest.Dispose();
        }

        #endregion
    }

}
