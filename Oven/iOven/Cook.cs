using System;
using Oven;
using Epic;
using Oven.Control;

namespace iOven
{
	[Serializable]
	public sealed class Cook : ICook
	{
		private readonly string _name;
		private readonly Oven.Control.IOven _oven;
		private readonly Uri _uri;
        private readonly Oven.Control.AlarmBell _alarm;
        private readonly IReporter _reporter;
		
		public Cook (string publicIPv6, string name, OvenUri oven, IServer server)
		{
			if(string.IsNullOrEmpty(publicIPv6))
				throw new ArgumentNullException("clientPublicIPv6");
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if(null == oven)
				throw new ArgumentNullException("oven");
			_name = name;
			_oven = new ControlledOvenProxy(oven, server);
			UriBuilder builder = new UriBuilder("http", publicIPv6);
			builder.Fragment = name + "/";
			_uri = builder.Uri;
            _alarm = new AlarmBell(_uri);
            _reporter = new Reporter(_uri, new ReportingOvenProxy(oven, server));
		}

		#region ICook implementation
		public string Name
		{
			get
			{
				return _name;
			}
		}

		public Oven.Control.IOven Oven
		{
			get
			{
				return _oven;
			}
		}

		public IAlarmBell AlarmBell
		{
			get
			{
				return _alarm;
			}
		}

		public IReporter Reporter
		{
			get
			{
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

