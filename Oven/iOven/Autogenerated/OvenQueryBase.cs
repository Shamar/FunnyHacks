using System;
using Epic;
using Oven;

namespace iOven.Autogenerated
{
	[Serializable]
	public abstract class OvenQueryBase<TOven, TResult> : QueryBase<TOven, TResult>
		where TOven : IOven
	{
		private readonly Oven.OvenUri _address;
		public OvenQueryBase (OvenUri address)
		{
			_address = address;
		}
		

		#region implemented abstract members of Epic.QueryBase[IOven,OvenId]
		public override bool IsTarget (TOven entity)
		{
			if(null == entity)
				return false;
			return entity.Address.Equals(_address);
		}
		#endregion
	}
}

