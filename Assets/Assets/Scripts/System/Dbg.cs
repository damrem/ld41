#define VERBOSE
#define VERBOSE_TIME
//#undef VERBOSE
using System.Collections.Generic;
using UnityEngine;
using Ext;

namespace System
{
	public class Dbg
	{
		static Dictionary<Type, bool> types = new Dictionary<Type, bool>();
		static public void AddType<T>()
		{
			Dbg.Log ("Dbg", "AddType", typeof(T));
			types [typeof(T)] = true;
		}

		static string ToString(object[] messages)
		{
			string joined = String.Join (",",
				messages.Map ((object message) => message!=null?message.ToString ():"null")
			);
			#if VERBOSE_TIME
			return FixedTime () + joined;
			#else
			return joined;
			#endif
		}

		static string ToString(object message)
		{
			#if VERBOSE_TIME
			return FixedTime () + message;
			#else
			return message;
			#endif
		}

		static string FixedTime()
		{
			return "[" + ((Time.fixedTime.ToString("0.000")).PadRight(5, '0')) + "s]\t";
		}

		static object[] UnshiftTime(object[] messages)
		{
			#if VERBOSE_TIME
			return messages.Unshifted (FixedTime ());
			#else
			return messages;
			#endif
		}

//		public static void Log(LogType logType, params object[] messages)
//		{
//			#if VERBOSE
//			Debug.logger.Log (logType, ToString (messages));
//			#endif
//		}

//		public static void Log(LogType logType, object message, UnityEngine.Object context)
//		{
//			#if VERBOSE
//			Debug.logger.Log (logType, (object)ToString(message), context);
//			#endif
//		}

//		public static void Log(LogType logType, string tag, params object[] messages)
//		{
//			#if (VERBOSE)
//			Debug.logger.Log (logType, tag, ToString (messages));
//			#endif
//		}

//		public static void Log(LogType logType, string tag, object message, UnityEngine.Object context)
//		{
//			#if VERBOSE
//			Debug.logger.Log (logType, tag, ToString(message), context);
//			#endif
//		}

		public static void Log(params object[] messages)
		{
			#if VERBOSE
			Debug.unityLogger.Log (ToString (messages));
			#endif
		}

//		public static void Log(string tag, params object[] messages)
//		{
//			#if VERBOSE
//			Debug.logger.Log (tag, ToString (messages));
//			#endif
//		}

//		public static void Log(string tag, object message, UnityEngine.Object context)
//		{
//			#if VERBOSE
//			Debug.logger.Log (tag, ToString(message), context);
//			#endif
//		}

//		public static void Log(Type holderType, params object[] messages)
//		{
//			#if VERBOSE
//			Debug.logger.Log (holderType.Name, ToString (messages));
//			#endif
//		}

		public static void Log(object holder, params object[] messages)
		{
//			Dbg.Log(holder.GetType ());

//			Debug.logger.Log (classes.Get (holder.GetType ()));
			#if VERBOSE
			Type holderType = holder.GetType ();
			if(types.Get(holderType))
				Debug.unityLogger.Log (holderType.Name, ToString (messages));
			#endif
		}


	}
}

