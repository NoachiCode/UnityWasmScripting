using System.Runtime.InteropServices;

namespace UnityEngine;
public static class Debug {
	public static void Log(object obj) {
		Push(obj.ToString()!);
		debug_log();
	}
	
	public static void LogWarning(object obj) {
		Push(obj.ToString()!);
		debug_logWarning();
	}
	
	public static void LogError(object obj) {
		Push(obj.ToString()!);
		debug_logError();
	}
    
	[WasmImportLinkage, DllImport("unity")]
	private static extern void debug_log();
    
	[WasmImportLinkage, DllImport("unity")]
	private static extern void debug_logWarning();
    
	[WasmImportLinkage, DllImport("unity")]
	private static extern void debug_logError();
}