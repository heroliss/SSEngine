using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting;

[assembly: Preserve]
enum IntEnum : int
{
    A,
    B,
}

public class RefTypes : MonoBehaviour
{

    void RefUnityEngine()
    {
        GameObject.Instantiate<GameObject>(null);
        Instantiate<GameObject>(null, null);
        Instantiate<GameObject>(null, null, false);
        Instantiate<GameObject>(null, new Vector3(), new Quaternion());
        Instantiate<GameObject>(null, new Vector3(), new Quaternion(), null);
        this.gameObject.AddComponent<RefTypes>();
        gameObject.AddComponent(typeof(RefTypes));
    }

    void RefNullable()
    {
        // nullable
        int? a = 5;
        object b = a;
    }

    void RefContainer()
    {
        new List<object>()
        {
            new Dictionary<int, int>(),
            new Dictionary<int, long>(),
            new Dictionary<int, object>(),
            new Dictionary<long, int>(),
            new Dictionary<long, long>(),
            new Dictionary<long, object>(),
            new Dictionary<object, long>(),
            new Dictionary<object, object>(),
            new SortedDictionary<int, long>(),
            new SortedDictionary<int, object>(),
            new SortedDictionary<long, int>(),
            new SortedDictionary<long, object>(),
            new HashSet<int>(),
            new HashSet<long>(),
            new HashSet<object>(),
            new List<int>(),
            new List<long>(),
            new List<float>(),
            new List<double>(),
            new List<object>(),
            new ValueTuple<int, int>(1, 1),
            new ValueTuple<long, long>(1, 1),
            new ValueTuple<object, object>(1, 1),
        };
    }

    class RefStateMachine : IAsyncStateMachine
    {
        public void MoveNext()
        {
            throw new NotImplementedException();
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            throw new NotImplementedException();
        }
    }

    void RefAsyncMethod()
    {
        var stateMachine = new RefStateMachine();

        TaskAwaiter aw = default;
        var c0 = new AsyncTaskMethodBuilder();
        c0.Start(ref stateMachine);
        c0.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c0.SetException(null);
        c0.SetResult();

        var c1 = new AsyncTaskMethodBuilder();
        c1.Start(ref stateMachine);
        c1.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c1.SetException(null);
        c1.SetResult();

        var c2 = new AsyncTaskMethodBuilder<bool>();
        c2.Start(ref stateMachine);
        c2.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c2.SetException(null);
        c2.SetResult(default);

        var c3 = new AsyncTaskMethodBuilder<int>();
        c3.Start(ref stateMachine);
        c3.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c3.SetException(null);
        c3.SetResult(default);

        var c4 = new AsyncTaskMethodBuilder<long>();
        c4.Start(ref stateMachine);
        c4.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c4.SetException(null);

        var c5 = new AsyncTaskMethodBuilder<float>();
        c5.Start(ref stateMachine);
        c5.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c5.SetException(null);
        c5.SetResult(default);

        var c6 = new AsyncTaskMethodBuilder<double>();
        c6.Start(ref stateMachine);
        c6.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c6.SetException(null);
        c6.SetResult(default);

        var c7 = new AsyncTaskMethodBuilder<object>();
        c7.Start(ref stateMachine);
        c7.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c7.SetException(null);
        c7.SetResult(default);

        var c8 = new AsyncTaskMethodBuilder<IntEnum>();
        c8.Start(ref stateMachine);
        c8.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c8.SetException(null);
        c8.SetResult(default);

        var c9 = new AsyncVoidMethodBuilder();
        var b = AsyncVoidMethodBuilder.Create();
        c9.Start(ref stateMachine);
        c9.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
        c9.SetException(null);
        c9.SetResult();
        Debug.Log(b);

        //TODO: 好像写的不对？
        //var a1 = new Cysharp.Threading.Tasks.CompilerServices.AsyncUniTaskVoidMethodBuilder();
        ////a1.Start(ref stateMachine);
        //var w = new TaskAwaiter<GameObject>();
        //IAsyncStateMachine m = default;
        //a1.AwaitUnsafeOnCompleted(ref w, ref m);
        //a1.SetException(null);
        //a1.SetResult();



        #region ASYNC
        TaskAwaiter wnull = default;
        TaskAwaiter<bool> wbool = default;
        TaskAwaiter<int> wint = default;
        TaskAwaiter<long> wlong = default;
        TaskAwaiter<object> wobject = default;
        TaskAwaiter<string> wstring = default;
        TaskAwaiter<double> wdouble = default;
        TaskAwaiter<float> wfloat = default;
        TaskAwaiter<IntEnum> wenum = default;

        var bnull = new AsyncTaskMethodBuilder();
        var bbool = new AsyncTaskMethodBuilder<bool>();
        var bint = new AsyncTaskMethodBuilder<int>();
        var bobject = new AsyncTaskMethodBuilder<object>();
        var bstring = new AsyncTaskMethodBuilder<string>();
        var bdouble = new AsyncTaskMethodBuilder<double>();
        var bfloat = new AsyncTaskMethodBuilder<float>();
        var benum = new AsyncTaskMethodBuilder<IntEnum>();
        var vbnull = new AsyncVoidMethodBuilder();

        vbnull.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        vbnull.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wnull, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wint, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wobject, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wstring, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wdouble, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wfloat, ref stateMachine);
        vbnull.AwaitOnCompleted(ref wenum, ref stateMachine);
        vbnull.SetException(null);
        vbnull.SetResult();


        bnull.Start(ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bnull.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bnull.AwaitOnCompleted(ref wnull, ref stateMachine);
        bnull.AwaitOnCompleted(ref wint, ref stateMachine);
        bnull.AwaitOnCompleted(ref wobject, ref stateMachine);
        bnull.AwaitOnCompleted(ref wstring, ref stateMachine);
        bnull.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bnull.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bnull.AwaitOnCompleted(ref wenum, ref stateMachine);
        bnull.SetException(null);
        bnull.SetResult();

        bbool.Start(ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bbool.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bbool.AwaitOnCompleted(ref wnull, ref stateMachine);
        bbool.AwaitOnCompleted(ref wint, ref stateMachine);
        bbool.AwaitOnCompleted(ref wobject, ref stateMachine);
        bbool.AwaitOnCompleted(ref wstring, ref stateMachine);
        bbool.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bbool.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bbool.AwaitOnCompleted(ref wenum, ref stateMachine);
        bbool.SetException(null);
        bbool.SetResult(default);

        bint.Start(ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bint.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bint.AwaitOnCompleted(ref wnull, ref stateMachine);
        bint.AwaitOnCompleted(ref wint, ref stateMachine);
        bint.AwaitOnCompleted(ref wobject, ref stateMachine);
        bint.AwaitOnCompleted(ref wstring, ref stateMachine);
        bint.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bint.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bint.AwaitOnCompleted(ref wenum, ref stateMachine);
        bint.SetException(null);
        bint.SetResult(default);

        bobject.Start(ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bobject.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bobject.AwaitOnCompleted(ref wnull, ref stateMachine);
        bobject.AwaitOnCompleted(ref wint, ref stateMachine);
        bobject.AwaitOnCompleted(ref wobject, ref stateMachine);
        bobject.AwaitOnCompleted(ref wstring, ref stateMachine);
        bobject.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bobject.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bobject.AwaitOnCompleted(ref wenum, ref stateMachine);
        bobject.SetException(null);
        bobject.SetResult(default);

        bstring.Start(ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bstring.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bstring.AwaitOnCompleted(ref wnull, ref stateMachine);
        bstring.AwaitOnCompleted(ref wint, ref stateMachine);
        bstring.AwaitOnCompleted(ref wobject, ref stateMachine);
        bstring.AwaitOnCompleted(ref wstring, ref stateMachine);
        bstring.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bstring.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bstring.AwaitOnCompleted(ref wenum, ref stateMachine);
        bstring.SetException(null);
        bstring.SetResult(default);

        bdouble.Start(ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bdouble.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wnull, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wint, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wobject, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wstring, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bdouble.AwaitOnCompleted(ref wenum, ref stateMachine);
        bdouble.SetException(null);
        bdouble.SetResult(default);

        bfloat.Start(ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        bfloat.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wnull, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wint, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wobject, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wstring, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wdouble, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wfloat, ref stateMachine);
        bfloat.AwaitOnCompleted(ref wenum, ref stateMachine);
        bfloat.SetException(null);
        bfloat.SetResult(default);

        benum.Start(ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wnull, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wbool, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wint, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wlong, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wobject, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wstring, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wdouble, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wfloat, ref stateMachine);
        benum.AwaitUnsafeOnCompleted(ref wenum, ref stateMachine);
        benum.AwaitOnCompleted(ref wnull, ref stateMachine);
        benum.AwaitOnCompleted(ref wint, ref stateMachine);
        benum.AwaitOnCompleted(ref wobject, ref stateMachine);
        benum.AwaitOnCompleted(ref wstring, ref stateMachine);
        benum.AwaitOnCompleted(ref wdouble, ref stateMachine);
        benum.AwaitOnCompleted(ref wfloat, ref stateMachine);
        benum.AwaitOnCompleted(ref wenum, ref stateMachine);
        benum.SetException(null);
        benum.SetResult(default);
        #endregion
    }
}
