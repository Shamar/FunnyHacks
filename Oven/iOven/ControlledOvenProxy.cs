using System;
using Oven;
using Epic;

namespace iOven
{
    [Serializable]
    internal sealed class ControlledOvenProxy : Oven.Control.IOven
    {
        private readonly OvenUri _uri;
        private readonly IServer _server;
        internal ControlledOvenProxy (OvenUri uri, IServer server)
        {
            _uri = uri;
            _server = server;
            
        }

        #region IOven implementation
        public OvenUri Address {
            get {
                return _uri;
            }
        }
        #endregion

        #region IOven implementation
        public event EventHandler<InfoEventArgs<TimerId>> TimerSyncronized;

        public event EventHandler<Oven.Control.CookingEventArgs> CookingAdjusted;

        public event EventHandler<InfoEventArgs<Time>> Cooked;

        public void CookFor (Minute duration, Temperature desiredTemperature)
        {
            throw new NotImplementedException ();
        }

        public void StopCooking ()
        {
            throw new NotImplementedException ();
        }

        public void Syncronize (ITimer timer)
        {
            throw new NotImplementedException ();
        }

        public void AlertThrough (Oven.Control.IAlarmBell alarm)
        {
            throw new NotImplementedException ();
        }

        public bool IsCooking {
            get {
                throw new NotImplementedException ();
            }
        }

        public Temperature CurrentTemperature {
            get {
                throw new NotImplementedException ();
            }
        }

        public Time PlannedStop {
            get {
                throw new NotImplementedException ();
            }
        }

        public System.Collections.Generic.IEnumerable<TimerId> SycronizedTimers {
            get {
                throw new NotImplementedException ();
            }
        }
        #endregion
    }
}

