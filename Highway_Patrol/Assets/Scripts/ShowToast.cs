using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowToast : MonoBehaviour
{
    public void ToastShow(string print)
    {

#if UNITY_ANDROID

        AndroidJavaClass toastClass =
                   new AndroidJavaClass("android.widget.Toast");

        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity =
          new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        toastParams[0] =
                     unityActivity.GetStatic<AndroidJavaObject>
                               ("currentActivity");
        toastParams[1] = print;
        toastParams[2] = toastClass.GetStatic<int>
                               ("LENGTH_LONG");

        AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                                      ("makeText", toastParams);
        toastObject.Call("show");

#endif
    }

}
