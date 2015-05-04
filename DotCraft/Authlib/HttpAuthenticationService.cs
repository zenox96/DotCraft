using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Authlib
{
    public abstract class HttpAuthenticationService : BaseAuthenticationService
    {
        private static readonly ILog LOGGER = LogManager.GetLogger("Minecraft");
        private readonly Proxy proxy;

        protected HttpAuthenticationService(Proxy proxy)
        {
            if (proxy == null) throw new NullReferenceException();
            this.proxy = proxy;
        }

        public Proxy getProxy
        {
            return this.proxy;
        }

        protected WebClient createUrlConnection(Uri url)
        {
            if (url==null) throw new NullReferenceException();
            LOGGER.Debug("Opening connection to " + url);
            WebClient connection = new WebClient();
            connection.BaseAddress = url.AbsoluteUri;
            connection.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            HttpWebRequest.Create(url);

            //HttpURLConnection connection = (HttpURLConnection)url.openConnection(this.proxy);
            /*connection.setConnectTimeout(15000);
            connection.setReadTimeout(15000);
            connection.setUseCaches(false);*/
            return connection;
        }

        public String performPostRequest(Uri url, String post, String contentType)
        {
            if (url==null) throw new NullReferenceException();
            if (post==null) throw new NullReferenceException();
            if (contentType==null) throw new NullReferenceException();
            WebClient connection = this.createUrlConnection(url);
            byte[] postAsBytes = Encoding.UTF8.GetBytes(post);

            connection.Headers.Set("Content-Type", contentType + "; charset=utf-8");
            connection.Headers.Set("Content-Length", "" + postAsBytes.Length);

            connection.setRequestProperty("Content-Type", contentType + "; charset=utf-8");
            connection.setRequestProperty("Content-Length", "" + postAsBytes.Length);
            connection.setDoOutput(true);
            LOGGER.Debug("Writing POST data to " + url + ": " + post);
            Stream outputStream = null;

            try {
                outputStream = connection.OpenWrite(url);
                outputStream.Write(postAsBytes, 0, postAsBytes.Length);
                outputStream.Flush();
                /*outputStream = connection.getOutputStream();
                IOUtils.write(postAsBytes, outputStream);*/
            } finally {
                outputStream.Dispose();
                //IOUtils.closeQuietly(outputStream);
            }

            LOGGER.Debug("Reading data from " + url);
            Stream inputStream = null;

            String var10;
            try {
                String result;
                try {
                    inputStream = connection.OpenRead(url);
                    byte[] resultAsBytes = new byte[inputStream.Length];
                    inputStream.Read(resultAsBytes, 0, resultAsBytes.Length);
                    String e = Encoding.UTF8.GetString(resultAsBytes);
                    LOGGER.Debug("Successful read, server response was " + connection.getResponseCode());
                    LOGGER.Debug("Response: " + e);
                    result = e;
                    return result;
                } catch (IOException var19) {
                    IOUtils.closeQuietly(inputStream);
                    inputStream = connection.getErrorStream();
                    if(inputStream == null) {
                        LOGGER.Debug("Request failed", var19);
                        throw var19;
                    }

                    LOGGER.Debug("Reading error page from " + url);
                    result = IOUtils.toString(inputStream, Charsets.UTF_8);
                    LOGGER.Debug("Successful read, server response was " + connection.getResponseCode());
                    LOGGER.Debug("Response: " + result);
                    var10 = result;
                }
            } finally {
                IOUtils.closeQuietly(inputStream);
            }

            return var10;
        }

        public String performGetRequest(URL url)
        {
            if (url==null) throw new NullReferenceException();
            HttpURLConnection connection = this.createUrlConnection(url);
            LOGGER.Debug("Reading data from " + url);
            InputStream inputStream = null;

            String var6;
            try {
                String result;
                try {
                    inputStream = connection.getInputStream();
                    String e = IOUtils.toString(inputStream, Charsets.UTF_8);
                    LOGGER.Debug("Successful read, server response was " + connection.getResponseCode());
                    LOGGER.Debug("Response: " + e);
                    result = e;
                    return result;
                } catch (IOException var10) {
                    IOUtils.closeQuietly(inputStream);
                    inputStream = connection.getErrorStream();
                    if(inputStream == null) {
                        LOGGER.Debug("Request failed", var10);
                        throw var10;
                    }

                    LOGGER.Debug("Reading error page from " + url);
                    result = IOUtils.toString(inputStream, Charsets.UTF_8);
                    LOGGER.Debug("Successful read, server response was " + connection.getResponseCode());
                    LOGGER.Debug("Response: " + result);
                    var6 = result;
                }
            } finally {
                IOUtils.closeQuietly(inputStream);
            }

            return var6;
        }

        public static Uri constantURL(String url)
        {
            try {
                return new Uri(url);
            } catch (UriFormatException var2) {
                throw new Exception("Couldn\'t create constant for " + url, var2);
            }
        }

        public static String buildQuery(Dictionary<String, Object> query)
        {
            if(query == null)
            {
                return "";
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                Dictionary<String, Object>.Enumerator enumerator = query.GetEnumerator();

                while(enumerator.MoveNext()) {
                    KeyValuePair<String, Object> entry = enumerator.Current;
                    if (builder.Length > 0)
                    {
                        builder.Append('&');
                    }

                    try
                    {
                        builder.Append(URLEncoder.encode((String)entry.Key, "UTF-8"));
                    }
                    catch (FormatException var6)
                    {
                        LOGGER.Error("Unexpected exception building query", var6);
                    }

                    if(entry.Value != null)
                    {
                        builder.Append('=');

                        try 
                        {
                            builder.Append(URLEncoder.encode(entry.Value.ToString(), "UTF-8"));
                        }
                        catch (FormatException var5)
                        {
                            LOGGER.Error("Unexpected exception building query", var5);
                        }
                    }
                }

                return builder.ToString();
            }
        }

        public static Uri concatenateURL(Uri url, String query)
        {
            return url.Query != null && url.Query.Length > 0 ? new UriBuilder(url.Scheme, url.Host, url.Port, url.PathAndQuery + "&" + query).Uri : new UriBuilder(url.Scheme, url.Host, url.Port, url.PathAndQuery + "?" + query).Uri;;
        }
    }
}
