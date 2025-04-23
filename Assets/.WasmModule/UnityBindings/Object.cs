using System.Runtime.InteropServices;

namespace UnityEngine;
public class Object(long id) {
    protected readonly long ObjectId = id;

    public string name {
        get => ObjectInterop.GetName(ObjectId);
        set => ObjectInterop.SetName(ObjectId, value);
    }

    public override string ToString() => ObjectInterop.ToString(ObjectId);

    public static void Destroy(Object obj) => ObjectInterop.object_destroy(obj.ObjectId);
    public static void Instantiate(Object obj) => ObjectInterop.object_instantiate(obj.ObjectId);
}

internal static class ObjectInterop {
    public static string GetName(long id) {
        object_name_get(id);
        return Pop();
    }
    
    public static void SetName(long id, string name) {
        Push(name);
        object_name_set(id);
    }
    
    public static string ToString(long id) {
        object_toString(id);
        return Pop();
    }
    
    [WasmImportLinkage, DllImport("unity")]
    private static extern void object_name_get(long id);

    [WasmImportLinkage, DllImport("unity")]
    private static extern void object_name_set(long id);

    [WasmImportLinkage, DllImport("unity")]
    private static extern void object_toString(long id);

    [WasmImportLinkage, DllImport("unity")]
    public static extern void object_destroy(long id);

    [WasmImportLinkage, DllImport("unity")]
    public static extern void object_instantiate(long id);
}