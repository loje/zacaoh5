using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
/// <summary>
///WebClientto 的摘要说明
/// </summary>
public class WebClientto : WebClient
{
    /// <summary>  
    /// 过期时间  
    /// </summary>  
    public int Timeout;

    public WebClientto(int timeout)
    {
        Timeout = timeout;
    }

    /// <summary>  
    /// 重写GetWebRequest,添加WebRequest对象超时时间  
    /// </summary>  
    /// <param name="address"></param>  
    /// <returns></returns>  
    protected override WebRequest GetWebRequest(Uri address)
    {
        HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
        request.Timeout = Timeout;
        request.ReadWriteTimeout = Timeout;
        return request;
    }
}