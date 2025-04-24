using UnityEngine;
using Wasmtime;

namespace WasmScripting.UnityEngine {
	public class GameObjectBindings : WasmBinding {
		public static void BindMethods(Linker linker) {
			linker.DefineFunction("unity", "gameObject_ctor_0", (Caller caller) => {
				StoreData data = GetData(caller);
				return IdFrom(data, new GameObject());
			});
			
			linker.DefineFunction("unity", "gameObject_ctor_1", (Caller caller) => {
				StoreData data = GetData(caller);
				return IdFrom(data, new GameObject(Pop(data)));
			});
			
			// linker.DefineFunction("unity", "gameObject_activeInHierarchy_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdTo<GameObject>(data, objectId).activeInHierarchy;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_activeSelf_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdTo<GameObject>(data, objectId).activeSelf;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_isStatic_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdTo<GameObject>(data, objectId).isStatic;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_isStatic_set", (Caller caller, long objectId, bool isStatic) => {
			// 	StoreData data = GetData(caller);
			// 	IdTo<GameObject>(data, objectId).isStatic = isStatic;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_layer_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdTo<GameObject>(data, objectId).layer;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_layer_set", (Caller caller, long objectId, int layer) => {
			// 	StoreData data = GetData(caller);
			// 	IdTo<GameObject>(data, objectId).layer = layer;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_scene_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdFrom(data, IdTo<GameObject>(data, objectId).scene);
			// });
			//
			// linker.DefineFunction("unity", "gameObject_sceneCullingMask_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	return IdTo<GameObject>(data, objectId).sceneCullingMask;
			// });
			//
			// linker.DefineFunction("unity", "gameObject_tag_get", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	GetData(caller).Stack.Push(IdTo<GameObject>(data, objectId).tag);
			// });
			//
			// linker.DefineFunction("unity", "gameObject_tag_set", (Caller caller, long objectId) => {
			// 	StoreData data = GetData(caller);
			// 	IdTo<GameObject>(data, objectId).tag = Pop(data);
			// });
			
			linker.DefineFunction("unity", "gameObject_transform_get", (Caller caller, long objectId) => {
				StoreData data = GetData(caller);
				return IdFrom(data, IdTo<GameObject>(data, objectId).transform);
			});
		}
	}
}