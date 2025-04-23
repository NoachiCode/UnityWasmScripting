global using static WasmModule.PassthroughStack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WasmModule;
public static unsafe class PassthroughStack {
	private static readonly int MaxStackSize = sizeof(IntPtr) * 64;
	private static IntPtr* _stackPointer;
	
	public static void Push<T>(ref T obj) where T : unmanaged {
		IntPtr address = Marshal.AllocHGlobal(sizeof(T));
		Unsafe.Write((void*)address, obj);
		*_stackPointer++ = address;
	}

	public static T Pop<T>() where T : unmanaged {
		T obj = Unsafe.Read<T>((void*)*--_stackPointer);
		Marshal.FreeHGlobal(*_stackPointer);
		return obj;
	}

	public static void Push(string str) {
		int size = str.Length * sizeof(char);
		IntPtr address = Marshal.AllocHGlobal(size);
		fixed (char* src = str) {
			Buffer.MemoryCopy(src, (void*)address, size, size);
		}
		*_stackPointer++ = address;
		*_stackPointer++ = size;
	}

	public static string Pop() {
		int size = (int)*--_stackPointer;
		IntPtr address = *--_stackPointer;
		string str = new string((char*)address, 0, size / sizeof(char));
		Marshal.FreeHGlobal(address);
		return str;
	}

	[UnmanagedCallersOnly(EntryPoint = "scripting_increment_stack_pointer")]
	public static long IncrementStackPointer() => (long)_stackPointer++;
	
	[UnmanagedCallersOnly(EntryPoint = "scripting_decrement_stack_pointer")]
	public static long DecrementStackPointer() => (long)--_stackPointer;
	
	[UnmanagedCallersOnly(EntryPoint = "scripting_alloc_passthrough_stack")]
	public static void AllocPassthroughStack() => _stackPointer = (IntPtr*)Marshal.AllocHGlobal(MaxStackSize);
}