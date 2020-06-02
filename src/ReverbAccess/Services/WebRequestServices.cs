using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ReverbAccess.Misc;
using ReverbAccess.Models;
using ReverbAccess.Models.Command;
using ReverbAccess.Models.Configuration;
using ServiceStack;

namespace ReverbAccess.Services
{
	internal class WebRequestServices
	{
		private readonly ReverbConfig _config;
		private string _host;

		public WebRequestServices(ReverbConfig config)
		{
			this._config = config;
			this._host = config.NativeHost;
		}

		public T GetResponse<T>(string url, string commandParams)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(string.Concat(url, commandParams));
			using (var response = request.GetResponse())
				result = ParseResponse<T>(response);

			return result;
		}

		public async Task<T> GetResponseAsync<T>(string url, string commandParams)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(string.Concat(url, commandParams));
			using (var response = await request.GetResponseAsync())
				result = ParseResponse<T>(response);

			return result;
		}

		public T GetResponse<T>(ReverbCommand command, string commandParams)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(string.Concat(this._host, command.Command, commandParams));
			using (var response = request.GetResponse())
				result = ParseResponse<T>(response);

			return result;
		}

		public async Task<T> GetResponseAsync<T>(ReverbCommand command, string commandParams)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(string.Concat(this._host, command.Command, commandParams));
			using (var response = await request.GetResponseAsync())
				result = ParseResponse<T>(response);

			return result;
		}

		public T GetResponse<T>(ReverbCommand command, string[] commandFormatParams, string commandParams)
		{
			T result;
			var request =
				this.CreateGetServiceGetRequest(string.Concat(this._host, String.Format(command.Command, commandFormatParams),
					commandParams));
			using (var response = request.GetResponse())
				result = ParseResponse<T>(response);

			return result;
		}

		public async Task<T> GetResponseAsync<T>(ReverbCommand command, string[] commandFormatParams, string commandParams)
		{
			T result;
			var request =
				this.CreateGetServiceGetRequest(string.Concat(this._host, String.Format(command.Command, commandFormatParams),
					commandParams));
			using (var response = await request.GetResponseAsync())
				result = ParseResponse<T>(response);

			return result;
		}

		public T GetResponse<T>(string url)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(url);
			using (var response = request.GetResponse())
				result = ParseResponse<T>(response);
			return result;
		}

		public async Task<T> GetResponseAsync<T>(string url)
		{
			T result;
			var request = this.CreateGetServiceGetRequest(url);
			using (var response = await request.GetResponseAsync())
				result = ParseResponse<T>(response);

			return result;
		}

		public void PutData(ReverbCommand command, string endpoint, string jsonContent)
		{
			var request = this.CreateServicePutRequest(command, endpoint, jsonContent);
			this.LogPutInfo(this._config.Token, endpoint, jsonContent);
			using (var response = (HttpWebResponse) request.GetResponse())
				this.LogUpdateInfo(this._config.Token, endpoint, response.StatusCode, jsonContent);
		}

		public async Task PutDataAsync(ReverbCommand command, string endpoint, string jsonContent)
		{
			var request = this.CreateServicePutRequest(command, endpoint, jsonContent);
			this.LogPutInfo(this._config.Token, endpoint, jsonContent);
			using (var response = await request.GetResponseAsync())
				this.LogUpdateInfo(this._config.Token, endpoint, ((HttpWebResponse) response).StatusCode, jsonContent);
		}

		public void PutJSONData(ReverbCommand command, string[] data, string jsonContent) {
			var request = this.CreateServicePutJSONRequest(command, data, jsonContent);
			this.LogPutInfo(this._config.Token, String.Join(",", data), jsonContent);
			using (var response = (HttpWebResponse)request.GetResponse())
				this.LogUpdateInfo(this._config.Token, String.Join(",", data), response.StatusCode, jsonContent);
		}

		public void PutFormatData(ReverbCommand command, string[] data, string jsonContent)
		{
			var request = this.CreateServicePutFormatRequest(command, data, jsonContent);
			this.LogPutInfo(this._config.Token, String.Join(",", data), jsonContent);
			using (var response = (HttpWebResponse) request.GetResponse())
				this.LogUpdateInfo(this._config.Token, String.Join(",", data), response.StatusCode, jsonContent);
		}

		public async Task PutFormatDataAsync(ReverbCommand command, string[] data, string jsonContent)
		{
			var request = this.CreateServicePutFormatRequest(command, data, jsonContent);
			this.LogPutInfo(this._config.Token, String.Join(",", data), jsonContent);
			using (var response = await request.GetResponseAsync())
				this.LogUpdateInfo(this._config.Token, String.Join(",", data), ((HttpWebResponse) response).StatusCode, jsonContent);
		}

		public string PostData(ReverbCommand command, string endpoint, string jsonContent) {
			var request = this.CreateServicePostRequest(command, endpoint, jsonContent);
			try {
				using (var response = (HttpWebResponse)request.GetResponse()) {
					StreamReader reader = new StreamReader(response.GetResponseStream());
					StringBuilder output = new StringBuilder();
					output.Append(reader.ReadToEnd());

					response.Close();

					return String.Empty;
				}
			} catch (WebException ex) {
				string exMessage = ex.Message;
				if (ex.Response != null) {
					using (var responseReader = new StreamReader(ex.Response.GetResponseStream())) {
						exMessage = responseReader.ReadToEnd();
					}
				}
				return exMessage;
			}
		}

		public T GetPostData<T>(ReverbCommand command, string endpoint, string jsonContent)
		{
			var request = this.CreateServicePostRequest(command, endpoint, jsonContent);
			using (var response = (HttpWebResponse) request.GetResponse())
			{
				StreamReader reader = new StreamReader(response.GetResponseStream());
				StringBuilder output = new StringBuilder();
				output.Append(reader.ReadToEnd());

				response.Close();

				return ParseJson<T>(output.ToString());
			}
		}

		public async Task<T> GetPostDataAsync<T>(ReverbCommand command, string endpoint, string jsonContent)
		{
			var request = this.CreateServicePostRequest(command, endpoint, jsonContent);
			using (var response = await (Task<WebResponse>) request.GetResponseAsync())
			{
				StreamReader reader = new StreamReader(response.GetResponseStream());
				StringBuilder output = new StringBuilder();
				output.Append(reader.ReadToEnd());

				response.Close();

				return ParseJson<T>(output.ToString());
			}
		}

		#region WebRequest configuration

		private HttpWebRequest CreateGetServiceGetRequest(string url)
		{
			this.AllowInvalidCertificate();
			this.InitSecurityProtocol();

			var uri = new Uri(url);
			var request = (HttpWebRequest) WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Get;
			request.Headers.Add( "Authorization", this.CreateAuthenticationHeader() );
			request.UserAgent = "SkuVault";
			request.ContentType = "application/hal+json";

			if (!String.IsNullOrEmpty(this.GetLogin()) && !String.IsNullOrEmpty(this.GetPassword()))
			{
				request.Credentials = new NetworkCredential(this.GetLogin(), this.GetPassword());
			}

			return request;
		}

		private HttpWebRequest CreateServicePostRequest(ReverbCommand command, string endpoint, string content)
		{
			this.AllowInvalidCertificate();
			this.InitSecurityProtocol();

			var uri = new Uri(string.Concat(this._host, command.Command, endpoint));
			var request = (HttpWebRequest) WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Post;
			request.UserAgent = "SkuVault";
			request.ContentType = "application/json";

			if (!String.IsNullOrEmpty(this.GetLogin()) && !String.IsNullOrEmpty(this.GetPassword()))
			{
				request.Credentials = new NetworkCredential(this.GetLogin(), this.GetPassword());
			}

			if (command != ReverbCommand.GetToken)
				request.Headers.Add("Authorization", this.CreateAuthenticationHeader());

			using (var writer = new StreamWriter(request.GetRequestStream()))
				writer.Write(content);

			return request;
		}

		private HttpWebRequest CreateServicePutRequest(ReverbCommand command, string endpoint, string content)
		{
			this.AllowInvalidCertificate();
			this.InitSecurityProtocol();

			var uri = new Uri(string.Concat(this._host, command.Command, endpoint));
			var request = (HttpWebRequest) WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Put;
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add("Authorization", CreateAuthenticationHeader());
			request.UserAgent = "SkuVault";

			if (!String.IsNullOrEmpty(this.GetLogin()) && !String.IsNullOrEmpty(this.GetPassword()))
			{
				request.Credentials = new NetworkCredential(this.GetLogin(), this.GetPassword());
			}

			using (var writer = new StreamWriter(request.GetRequestStream()))
				writer.Write(content);

			return request;
		}

		private HttpWebRequest CreateServicePutFormatRequest(ReverbCommand command, string[] data, string content)
		{
			this.AllowInvalidCertificate();
			this.InitSecurityProtocol();

			var uri = new Uri(string.Concat(this._host, String.Format(command.Command, data)));
			var request = (HttpWebRequest) WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Put;
			request.ContentType = "application/x-www-form-urlencoded";
			request.Headers.Add("Authorization", CreateAuthenticationHeader());
			request.UserAgent = "SkuVault";

			if (!String.IsNullOrEmpty(this.GetLogin()) && !String.IsNullOrEmpty(this.GetPassword()))
			{
				request.Credentials = new NetworkCredential(this.GetLogin(), this.GetPassword());
			}

			using (var writer = new StreamWriter(request.GetRequestStream()))
				writer.Write(content);

			return request;
		}

		private HttpWebRequest CreateServicePutJSONRequest(ReverbCommand command, string[] data, string content) {
			this.AllowInvalidCertificate();
			this.InitSecurityProtocol();

			var uri = new Uri(string.Concat(this._host, String.Format(command.Command, data)));
			var request = (HttpWebRequest)WebRequest.Create(uri);

			request.Method = WebRequestMethods.Http.Put;
			request.ContentType = "application/hal+json";
			request.Headers.Add("Authorization", CreateAuthenticationHeader());
			request.UserAgent = "SkuVault";

			if (!String.IsNullOrEmpty(this.GetLogin()) && !String.IsNullOrEmpty(this.GetPassword())) {
				request.Credentials = new NetworkCredential(this.GetLogin(), this.GetPassword());
			}

			using (var writer = new StreamWriter(request.GetRequestStream()))
				writer.Write(content);

			return request;
		}

		#endregion

		#region Misc

		private T ParseResponse<T>(WebResponse response)
		{
			var result = default(T);

			using (var stream = response.GetResponseStream())
			{
				var reader = new StreamReader(stream);
				var jsonResponse = reader.ReadToEnd();

				ReverbLogger.Log.Trace("[Reverb]\tResponse\t{0} - {1}", response.ResponseUri, jsonResponse);

				if (!String.IsNullOrEmpty(jsonResponse))
					result = jsonResponse.FromJson<T>();
			}

			return result;
		}

		private T ParseJson<T>(String jsonContent)
		{
			return jsonContent.FromJson<T>();
		}

		private string CreateAuthenticationHeader()
		{
			return "Bearer " + this._config.Token;
		}

		private string GetLogin()
		{
			return this._config.Login;
		}

		private string GetPassword()
		{
			return this._config.Password;
		}

		private void LogUpdateInfo(string shopName, string url, HttpStatusCode statusCode, string jsonContent)
		{
			ReverbLogger.Log.Trace(
				"[Reverb]\tPUT/POST call for shop '{0}' and url '{1}' has been completed with code '{2}'.\n{3}", shopName, url,
				statusCode, jsonContent);
		}

		private void LogPutInfo(string shopName, string url, string jsonContent)
		{
			ReverbLogger.Log.Trace("[Reverb]\tPUT data for shop '{0}' and url '{1}':\n{2}", shopName, url, jsonContent);
		}

		#endregion

		#region SSL certificate hack

		private void AllowInvalidCertificate()
		{
			ServicePointManager.ServerCertificateValidationCallback += AllowCert;
		}

		private void InitSecurityProtocol()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		private bool AllowCert(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
		{
			return true;
		}

		#endregion
	}
}