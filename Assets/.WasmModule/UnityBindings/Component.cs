using System.Runtime.InteropServices;

namespace UnityEngine;
public class Component(long id) : Object(id) {

    public GameObject gameObject => ComponentInterop.GetGameObject(ObjectId);
    public Transform transform => ComponentInterop.GetTransform(ObjectId);
    
    public string tag {
        get => ComponentInterop.GetTag(ObjectId);
        set => ComponentInterop.SetTag(ObjectId, value);
    }
}

internal static class ComponentInterop {
    public static string GetTag(long id) {
        component_tag_get(id);
        return Pop();
    }
    
    public static void SetTag(long id, string name) {
        Push(name);
        component_tag_set(id);
    }
    
    public static GameObject GetGameObject(long id) {
        return new(component_gameObject_get(id));
    }
    
    public static Transform GetTransform(long id) {
        return new(component_transform_get(id));
    }
    
    [WasmImportLinkage, DllImport("unity")]
    private static extern long component_gameObject_get(long id);
    
    [WasmImportLinkage, DllImport("unity")]
    private static extern long component_transform_get(long id);
    
    [WasmImportLinkage, DllImport("unity")]
    private static extern void component_tag_get(long id);

    [WasmImportLinkage, DllImport("unity")]
    private static extern void component_tag_set(long id);
}