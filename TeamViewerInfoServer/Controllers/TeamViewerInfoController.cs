using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using TeamViewerInfoServer.Controller;
using TeamViewerInfoServer.Models;

namespace TeamViewerInfoServer.Controllers
{
    public class TeamViewerInfoController : ApiController
    {
        // GET: api/TeamViewerInfo
        public IHttpActionResult GetAll()
        {
            LogController.Info("Load Info By <" + GetIPAddress + ">,Command Complete.");
            return Json(TeamViewerCommand.Load());
        }

        // POST: api/TeamViewerInfo
        [HttpPost]
        public IHttpActionResult Post([FromBody]AddMess entity)
        {
            string ip = GetIPAddress;
            if (string.IsNullOrEmpty(entity.id) && string.IsNullOrEmpty(entity.pwd))
                return Json(new { code = 0, result = "操作失败,检查参数" });
            LogController.Info(string.Format("accept TeamViewer Info From <{0}>,Info Mess:<ip={1}&pwd={2}&addr={3}>", ip, entity.id, entity.pwd, entity.addr));
            TeamViewerCommand.Add(entity.id, entity.pwd, entity.addr, ip);
            return Json(new { code = 1, result = "添加成功" });
        }
        
        public class AddMess
        {
            public string id { get; set; }
            public string pwd { get; set; }
            public string addr { get; set; }
        }

        ///  <summary>    
        ///  取得客户端真实IP。如果有代理则取第一个非内网地址    
        ///  </summary>    
        public static string GetIPAddress
        {
            get
            {
                var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(result))
                {
                    //可能有代理    
                    if (result.IndexOf(".") == -1)        //没有“.”肯定是非IPv4格式    
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。    
                            result = result.Replace("  ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                        && temparyip[i].Substring(0, 3) != "10."
                                        && temparyip[i].Substring(0, 7) != "192.168"
                                        && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];        //找到不是内网的地址    
                                }
                            }
                        }
                        else if (IsIPAddress(result))  //代理即是IP格式    
                            return result;
                        else
                            result = null;        //代理中的内容  非IP，取IP    
                    }

                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];

                if (string.IsNullOrEmpty(result))
                    result = HttpContext.Current.Request.UserHostAddress;
                result = result == "::1" ? "127.0.0.1" : result;
                return result;
            }
        }

        ///  <summary>  
        ///  判断是否是IP地址格式  0.0.0.0  
        ///  </summary>  
        ///  <param  name="str1">待判断的IP地址</param>  
        ///  <returns>true  or  false</returns>  
        public static bool IsIPAddress(string str1)
        {
            if (string.IsNullOrEmpty(str1) || str1.Length < 7 || str1.Length > 15) return false;

            const string regFormat = @"^d{1,3}[.]d{1,3}[.]d{1,3}[.]d{1,3}$";

            var regex = new Regex(regFormat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
    }
}
