using PlayFab.Internal;

namespace PlayFab.SharedModels
{
    public class HttpResponseObject
    {
        public int code;
        public string status;
        public object data;
    }

    public class PlayFabBaseModel
    {
        public string ToJson()
        {
            var json = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer);
            return json.SerializeObject(this);
        }
    }

    public interface IPlayFabInstanceApi { }

    public class PlayFabRequestCommon : PlayFabBaseModel
    {
        public PlayFabAuthenticationContext AuthenticationContext;
    }

    public class PlayFabResultCommon : PlayFabBaseModel
    {
        public PlayFabRequestCommon Request;
        public object CustomData;
    }

    public class PlayFabLoginResultCommon : PlayFabResultCommon
    {
        public PlayFabAuthenticationContext AuthenticationContext;
    }

    public class PlayFabResult<TResult> where TResult : PlayFabResultCommon
    {
        public TResult Result;
        public object CustomData;
    }

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
    public class WaitForPlayFabResponse<T> : UnityEngine.CustomYieldInstruction where T:PlayFabResultCommon
    {
        public T Result;
        public PlayFabError Error;

        bool complete = false;

        public override bool keepWaiting { get { return !complete; } }
        public bool IsError { get { return Error != null; } }

        public void Complete(T r) {
            Result = r;
            Error = null;
            complete = true;
        }

        public void Complete(PlayFabError e) {
            Result = null;
            Error = e;
            complete = true;
        }
    }
#endif // UNITY_5_3 || UNITY_5_3_OR_NEWER
}
