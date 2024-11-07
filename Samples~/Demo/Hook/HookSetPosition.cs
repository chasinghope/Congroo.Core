//使用 Unity 进行开发时经常遇到找不到谁修改了一个属性，比如 Transform.position 被其他脚本改了，导致位置不对。遇到这种情况，只能一步步地耐心调试，才能找到是谁把值改错了。如果能把断点打在 position 的 setter 里将绝杀，可惜打不上。
//https://zhuanlan.zhihu.com/p/5215265231

//DetourUtility 的添加方式：导入 DetourUtility.cs 到项目中，并设置 allow unsafe code (player settings -> other settings -> allow unsafe code)。
//如果不想在自己的项目中启用 allow unsafe code，可以新建一个项目，启用 allow unsafe code 并把 DetourUtility.cs 编译为 dll (创建程序集定义后，在 Library/ScriptAssemblies 目录下找到对应的 dll)，然后把 dll 导入自己的项目里。
using UnityEngine;
using System.Reflection;

public class HookSetPosition : MonoBehaviour
{
    public static void set_position(Transform tr, Vector3 val)
    {
        var type = typeof(Transform);
        var rawSetter = type.GetMethod("set_position_Injected", BindingFlags.NonPublic | BindingFlags.Instance);
        rawSetter!.Invoke(tr, new object[] { val });
        print($"set position, name: {tr.name}, val: {val}");
    }

    public void Awake()
    {
        var srcMethod = DetourUtility.MethodInfoForSetter(() => transform.position);
        var dstMethod = typeof(HookSetPosition).GetMethod(nameof(set_position), BindingFlags.Static | BindingFlags.Public);
  
        DetourUtility.TryDetourFromTo(srcMethod, dstMethod);
    }

    public void Start()
    {
        transform.position = Vector3.zero;
    }
}