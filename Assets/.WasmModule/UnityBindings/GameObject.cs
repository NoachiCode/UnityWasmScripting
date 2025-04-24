using System.Runtime.InteropServices;

namespace UnityEngine;

public class GameObject(long id) : Object(id)
{
    public GameObject() : this(gameObject_ctor_0())
    {
    }

    public GameObject(string str) : this(GameObject_ID(str))
    {
    }

    public static long GameObject_ID(string str)
    {
        Push(str);
        return gameObject_ctor_1();
    }

    public Transform transform => new Transform(gameObject_transform_get(ObjectId));


    [WasmImportLinkage, DllImport("unity")]
    private static extern long gameObject_ctor_0();

    [WasmImportLinkage, DllImport("unity")]
    private static extern long gameObject_ctor_1();

    [WasmImportLinkage, DllImport("unity")]
    public static extern long gameObject_transform_get(long id);
}