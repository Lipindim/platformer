using System;


namespace Platformer
{
    public sealed class Quest : IQuest
    {
        #region Fields

        private readonly QuestObjectView _view;
        private readonly IQuestModel _model;

        private bool _active;

        #endregion


        #region Life Cycle

        public Quest(QuestObjectView view, IQuestModel model)
        {
            _view = view != null ? view : throw new ArgumentNullException(nameof(view));
            _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        }

        #endregion


        #region Methods

        private void OnContact(LevelObjectView arg1)
        {
            var completed = _model.TryComplete(arg1.gameObject);
            if (completed) Complete();
        }

        private void Complete()
        {
            if (!_active) return;
            _active = false;
            IsCompleted = true;
            _view.OnTriggerEnter -= OnContact;
            _view.ProcessComplete();
            OnCompleted();
        }

        private void OnCompleted()
        {
            Completed?.Invoke(this, this);
        }

        #endregion


        #region IQuest

        public event EventHandler<IQuest> Completed;
        public bool IsCompleted { get; private set; }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            IsCompleted = false;
            _view.OnTriggerEnter += OnContact;
            _view.ProcessActivate();
        }

        public void Dispose()
        {
            _view.OnTriggerEnter -= OnContact;
        }

        #endregion
    }

}
