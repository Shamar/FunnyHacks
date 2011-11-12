using System;
using Oven;
using Epic;

namespace iOven
{
    [Serializable]
    internal sealed class ReportingOvenProxy : Oven.Reporting.IOven
    {
        private readonly OvenUri _uri;
        private readonly IServer _server;
        internal ReportingOvenProxy (OvenUri uri, IServer server)
        {
            _uri = uri;
            _server = server;
        }

        #region IOven implementation
        public Oven.Reporting.IState CurrentState {
            get 
            {
                return null;
            }
        }
        #endregion

        #region IOven implementation
        public OvenUri Address {
            get {
                return _uri;
            }
        }
        #endregion
    }
}

