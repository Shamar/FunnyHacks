using System;
using Oven;
using Oven.Control;

namespace iOven
{
	[Serializable]
	public sealed class Cook : ICook
	{
		private readonly string _name;
		private readonly IOven _oven;
		private readonly Uri _uri;
		
		public Cook (string publicIPv6, string name, IOven oven)
		{
			if(string.IsNullOrEmpty(publicIPv6))
				throw new ArgumentNullException("clientPublicIPv6");
			if(string.IsNullOrEmpty(name))
				throw new ArgumentNullException("name");
			if(null == oven)
				throw new ArgumentNullException("oven");
			_name = name;
			_oven = oven;
			UriBuilder builder = new UriBuilder("http", publicIPv6);
			builder.Fragment = name + "/";
			_uri = builder.Uri;
		}

		#region ICook implementation
		public string Name
		{
			get
			{
				return _name;
			}
		}

		public IOven Oven
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
				throw new NotImplementedException ();
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

