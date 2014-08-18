using System;

namespace NDiagnostics.Metering
{
    public abstract class DisposableObject : IDisposableObject
    {
        #region Constants and Fields

        private readonly object disposerLock = new object();

        private volatile bool isDisposed;

        #endregion

        #region Constructors and Destructors

        protected DisposableObject()
        {
            this.isDisposed = false;
        }

        ~DisposableObject()
        {
            this.Dispose(false);
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region IDisposableObject

        public bool IsDisposed
        {
            get { return this.isDisposed; }
        }

        #endregion

        #region Methods

        protected abstract void OnDisposing();

        private void Dispose(bool isDisposing)
        {
            lock(this.disposerLock)
            {
                if(this.isDisposed)
                {
                    return;
                }

                if(isDisposing)
                {
                    this.OnDisposing();
                }

                this.isDisposed = true;
            }
        }

        #endregion
    }
}
