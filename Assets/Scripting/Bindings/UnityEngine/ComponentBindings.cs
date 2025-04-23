using UnityEngine;
using Wasmtime;

namespace WasmScripting.UnityEngine {
	public class ComponentBindings : WasmBinding {
		public static void BindMethods(Linker linker) {
			linker.DefineFunction("unity", "component_tag_get", (Caller caller, long objectId) => {
				StoreData data = GetData(caller);
				data.Stack.Push(IdTo<Component>(data, objectId).tag);
			});
			
			linker.DefineFunction("unity", "component_tag_set", (Caller caller, long objectId) => {
				StoreData data = GetData(caller);
				IdTo<Component>(data, objectId).tag = Pop(data);
			});
			
			linker.DefineFunction("unity", "component_transform_get", (Caller caller, long objectId) => {
				StoreData data = GetData(caller);
				return IdFrom(data, IdTo<Component>(data, objectId).transform);
			});
			
			linker.DefineFunction("unity", "component_gameObject_get", (Caller caller, long objectId) => {
				StoreData data = GetData(caller);
				return IdFrom(data, IdTo<Component>(data, objectId).gameObject);
			});
		}
	}
}