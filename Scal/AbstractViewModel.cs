using System;
using Caliburn.Micro;
using MemBus.Subscribing;

namespace Scal
{
    public abstract class AbstractViewModel : PropertyChangedBase, IActivate, IDeactivate, IAcceptDisposeToken
    {
        private IDisposable _disposeToken;

        public void Activate()
        {
            IsActive = true;
            OnActivate();
            Activated.Raise(this, new ActivationEventArgs());
        }

        protected virtual void OnActivate() { }

        public bool IsActive { get; private set; }

        public event EventHandler<ActivationEventArgs> Activated;

        public void Deactivate(bool close)
        {
            AttemptingDeactivation.Raise(this, new DeactivationEventArgs { WasClosed = close });
            if (_disposeToken != null && close)
                _disposeToken.Dispose();
            IsActive = false;
            OnDeactivate(close);
            Deactivated.Raise(this, new DeactivationEventArgs { WasClosed = close });
        }

        protected virtual void OnDeactivate(bool close) { }

        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;
        public event EventHandler<DeactivationEventArgs> Deactivated;

        void IAcceptDisposeToken.Accept(IDisposable disposeToken)
        {
            _disposeToken = disposeToken;
        }
    }
}