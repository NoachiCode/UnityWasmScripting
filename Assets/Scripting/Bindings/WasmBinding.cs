using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using Wasmtime;

namespace WasmScripting {
	public abstract class WasmBinding {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static StoreData GetData(Caller caller) => (StoreData)caller.Store.GetData()!;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static T IdTo<T>(StoreData data, long id) where T : class => data.AccessManager.ToWrapped(id).Target as T;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static long IdFrom(StoreData data, object obj) => data.AccessManager.ToWrapped(obj).Id;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static void Push<T>(StoreData data, ref T obj) where T : unmanaged => data.Stack.Push(ref obj);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static T Pop<T>(StoreData data) where T : unmanaged => data.Stack.Pop<T>();
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static void Push(StoreData data, string str) => data.Stack.Push(str);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static string Pop(StoreData data) => data.Stack.Pop();
	}
}