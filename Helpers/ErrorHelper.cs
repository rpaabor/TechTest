using log4net;
using System;
using System.ServiceModel;

namespace Helpers
{
    public class ErrorHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ErrorHelper));


        public static  FaultException<Error> LogAndWrapInFaultException(Exception ex)
        {
            Log.Error(ex);
            var error = new Error(ex);
            return new FaultException<Error>(error, new FaultReason(error.Message));
        }
        public static Exception UnwrapFaultException(FaultException<Error> fex)
        {
            var error = fex.Detail;
            var ex = new Exception(error.Message);
            return ex;
        }

        public static void LogInfo(string info)
        {
            Log.Info(info);
        }
        public static void LogError(string error)
        {
            Log.Error(error);
        }
        public static void LogDebug(string debug)
        {
            Log.Debug(debug);
        }

    }
}