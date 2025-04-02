using System;
using System.Net;
using System.Net.Http;
using System.Threading;

namespace ISBNCaller_Lib
{
    internal class IRest_JSON
    {
        private static string mResponse = "";
        private struct CallStruct
        {
            public bool useHeader;
            public string userAgent;
            public string userName;
        }

        public string CallRest(string restCall, bool useHeader, string userAgent = "", string userName = "")
        {
            CallStruct callStruct = new CallStruct();
            callStruct.useHeader = useHeader;
            callStruct.userAgent = userAgent;
            callStruct.userName = userName;
            CallRest(restCall, callStruct);

            while(mResponse == "")
            {
                Thread.Sleep(5);
            }

            string response = mResponse;
            mResponse = "";
            return response;
        }

        private static /*async*/ void CallRest(string restCall, CallStruct callStruct)
        {
            try
            {
                WebClient client = new WebClient();
                //if (useHeader)
                //{
                //    client.Headers.Add(userAgent, userName);
                //} // if

                mResponse = client.DownloadString(restCall);
            } // try
            catch(Exception ex)
            {
                mResponse = $@"An error occured: { ex.ToString() }";
            } // catch
        }
    }
}
