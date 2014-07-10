using System;
using NDiagnostics.Metering.Extensions;

namespace NDiagnostics.Metering
{
    public abstract class DisposableObject : IDisposableObject
    {
        #region Constants and Fields

        private bool isDisposed;

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

        protected void ThrowIfDisposed()
        {
            if(this.IsDisposed)
            {
                throw new ObjectDisposedException(this.GetType().ToName());
            }
        }

        protected abstract void InternalDispose();

        protected virtual void InternalFinalize()
        {
        }

        private void Dispose(bool isDisposing)
        {
            if(this.isDisposed)
            {
                return;
            }

            if(isDisposing)
            {
                this.InternalDispose();
            }
            else
            {
                this.InternalFinalize();
            }

            this.isDisposed = true;
        }

        #endregion
    }
}
