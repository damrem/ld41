using System;

namespace Helpers
{
	public class Delegates
	{
        public delegate int FloatFloatInt(float f, float g);
        public delegate bool TBool<T>(T t);
        public delegate T TT<T>(T t);
        public delegate T TIntT<T>(T t, int i);
        public delegate int TTInt<T>(T t, T u);
        public delegate void TVoid<T>(T t);
		public delegate void VoidVoid();
//		public static CallbackVoidVoid callbackVoidVoid
	}
}

