using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetTeamViewerInfo.Commands
{
    public class CookieWebClient: WebClient
    {
        // Cookie 容器
        private CookieContainer cookieContainer;

        /**/
        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        public CookieWebClient()
        {
            this.cookieContainer = new CookieContainer();
            Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
            Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        }

        /**/
        /// <summary>
        /// 创建一个新的 WebClient 实例。
        /// </summary>
        /// <param name="cookie">Cookie 容器</param>
        public CookieWebClient(CookieContainer cookies)
        {
            this.cookieContainer = cookies;
            Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
            Headers.Add("Content-Type", "application/x-www-form-urlencoded");
        }

        /**/
        /// <summary>
        /// Cookie 容器
        /// </summary>
        public CookieContainer Cookies
        {
            get { return this.cookieContainer; }
            set { this.cookieContainer = value; }
        }

        /**/
        /// <summary>
        /// 返回带有 Cookie 的 HttpWebRequest。
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = cookieContainer;
            }
            return request;
        }
    }
}
