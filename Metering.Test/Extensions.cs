using System;

namespace NDiagnostics.Metering.Test
{
    public static class Extensions
    {
        #region Public Methods

        public static bool IsAlmostEqual(this float self, float other, float tolerance = float.Epsilon)
        {
            if(Math.Abs(self - other) < tolerance)
            {
                return true;
            }

            if(Math.Abs(other) > Math.Abs(self))
            {
                return Math.Abs((self - other) / other) < tolerance;
            }

            return Math.Abs((self - other) / self) < tolerance;
        }

        #endregion
    }
}
