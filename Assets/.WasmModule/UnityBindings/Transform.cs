using System.Runtime.InteropServices;

namespace UnityEngine;
public class Transform(long id) : Component(id) {
	public Vector3 position {
		get => TransformInterop.GetPosition(ObjectId);
		set => TransformInterop.SetPosition(ObjectId, value);
	}
}

internal static class TransformInterop {
	public static void SetPosition(long id, Vector3 position) {
		Push(ref position);
		transform_position_set(id);
	}
    
	public static Vector3 GetPosition(long id) {
		transform_position_get(id);
		return Pop<Vector3>();
	}
    
	[WasmImportLinkage, DllImport("unity")]
	private static extern void transform_position_get(long id);
    
	[WasmImportLinkage, DllImport("unity")]
	private static extern void transform_position_set(long id);
}