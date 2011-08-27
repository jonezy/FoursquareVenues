using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class FoursquareVenueClient {
    string GetJSON(string url, string postData, string method) {
        string returnValue = string.Empty;

        // create the request
        WebRequest webRequest = WebRequest.Create(url);
        webRequest.ContentType = "application/x-www-form-urlencoded";

        if (!string.IsNullOrEmpty(method)) {
            webRequest.Method = method;

            if (!string.IsNullOrEmpty(postData)) {
                // posting data to a url
                byte[] byteSend = Encoding.ASCII.GetBytes(postData);
                webRequest.ContentLength = byteSend.Length;

                Stream streamOut = webRequest.GetRequestStream();
                streamOut.Write(byteSend, 0, byteSend.Length);
                streamOut.Flush();
                streamOut.Close();
            }
        } else {
            // getting data
            webRequest.Method = "GET";
        }

        // deal with the response and return
        WebResponse webResponse = webRequest.GetResponse();
        StreamReader streamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);

        if (streamReader.Peek() > -1) {
            returnValue = streamReader.ReadToEnd();
        }
        streamReader.Close();
        streamReader.Dispose();

        return returnValue;
    }

    public dynamic Execute(string url) {
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(GetJSON(url, "", ""));
        var metaEl = JsonConvert.DeserializeObject<JObject>(dictionary["meta"].ToString());
        var responseEl = JsonConvert.DeserializeObject<JObject>(dictionary["response"].ToString());

        var result = new ExpandoObject();
        var d = result as IDictionary<string, object>; //work with the Expando as a Dictionary

        d.Add("meta", metaEl.Values().First());
        d.Add("response", responseEl.Values().Children());

        return result;
    }
}