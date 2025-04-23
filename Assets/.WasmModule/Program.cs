using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WasmModule;
public static class Program {
    private static readonly Dictionary<long, MonoBehaviour> Behaviours = new();
    private static readonly Dictionary<Type, Dictionary<string, MethodInfo>> Callbacks = new();
    private static readonly FieldInfo ObjectIdField = typeof(Object).GetField("ObjectId", BindingFlags.Instance | BindingFlags.NonPublic);
    
	[UnmanagedCallersOnly(EntryPoint = "scripting_create_instance")]
	public static void CreateInstance(int id, long objectId) {
        string name = Pop();
        try {
            Type type = Type.GetType(name);
            MonoBehaviour obj = RuntimeHelpers.GetUninitializedObject(type) as MonoBehaviour;
            ObjectIdField.SetValue(obj, objectId);
            Behaviours[id] = obj;
            if (Callbacks.ContainsKey(type)) return;
            Dictionary<string, MethodInfo> callbacks = new();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (MethodInfo method in methods) callbacks[method.Name] = method;
            Callbacks[type] = callbacks;
        } catch (Exception e) {
            Debug.LogError($"Error Creating WasmBehaviour `{name}`: {e}");
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "scripting_call")]
    public static void Call(int id) {
        string methodName = Pop();
        try {
            MonoBehaviour behaviour = Behaviours[id];
            if (Callbacks[behaviour.GetType()].TryGetValue(methodName, out MethodInfo method)) method.Invoke(behaviour, null);
        } catch (Exception e) {
            Debug.LogError($"Error Calling Method `{methodName}`: {e}");
        }
    }

    [UnmanagedCallersOnly(EntryPoint = "scripting_alloc")]
    public static IntPtr Alloc(int length) => Marshal.AllocHGlobal(length);

    [UnmanagedCallersOnly(EntryPoint = "scripting_free")]
    public static void Free(IntPtr address) => Marshal.FreeHGlobal(address);
}