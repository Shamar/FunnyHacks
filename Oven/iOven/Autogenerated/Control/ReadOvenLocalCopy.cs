using System;
using Oven.Control;

namespace iOven.Autogenerated
{
	public class ReadOvenLocalCopy : OvenQueryBase<IOven, IOven>
	{
		public ReadOvenLocalCopy (Oven.OvenUri address)
			: base(address)
		{
		}
		
		public override IOven Query (Oven.Control.IOven entity)
		{
			return entity;
		}
	}
}

