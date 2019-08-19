using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace HangoutBotDeploy {

  class Request {

    private HttpWebRequest _myReq;
    private ConfigSettings _config;

    private string _urlWebHook;
    private string _jsonBody;

    private string _responseWebHook;
    private HttpStatusCode _responseStatusCode;


    public Request() {
      this._config = new ConfigSettings();
      this._urlWebHook = _config.GetConfig()["chat_webhook"];

    }

    public bool SendMessage(string text) {

      this._myReq = WebRequest.CreateHttp(this._urlWebHook);

      this._jsonBody = JsonConvert.SerializeObject(new { text });

      this._myReq.Method = "POST";
      this._myReq.ContentType = "application/json; charset=UTF-8";
      this._myReq.ContentLength = Encoding.UTF8.GetBytes(this._jsonBody).Length;

      using (Stream requestStream = this._myReq.GetRequestStream()) {

        requestStream.Write(Encoding.UTF8.GetBytes(this._jsonBody), 0, Encoding.UTF8.GetBytes(this._jsonBody).Length);
        requestStream.Close();

      }

      using (WebResponse response = this._myReq.GetResponse()) {

        Stream responseStream = response.GetResponseStream();
        StreamReader responseReader = new StreamReader(responseStream);

        this._responseWebHook = responseReader.ReadToEnd();

        this._responseStatusCode = ((HttpWebResponse)response).StatusCode;


        responseStream.Close();
        response.Close();
      }

      if (this._responseStatusCode == HttpStatusCode.OK) return true;

      return false;

    }

    public string GetresponseWebHook() {

      return this._responseWebHook;

    }

  }

}
