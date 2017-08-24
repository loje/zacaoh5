using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


/// <summary>
/// login 的摘要说明
/// </summary>
public class login_member
{
    public static string GetUserIp()//获取当前登录用户的ip
    {
        string ip;
        string[] temp;
        bool isErr = false;
        if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
            ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        else
            ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
        if (ip.Length > 15)
            isErr = true;
        else
        {

            temp = ip.Split('.');
            if (temp.Length == 4)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Length > 3) isErr = true;
                }
            }
            else
                isErr = true;
        }

        if (isErr)
            return "1.1.1.1";
        else
            return ip;
    }

    public login_member()
    {
    }
    public static bool checkMemerLogin()
    {
        if (HttpContext.Current.Request.Cookies["mLoginCookies"] == null)
            return false;
        else return true;
    }
    //判断用户是否登陆
    public static void pd()
    {
        HttpCookie loginCookies = System.Web.HttpContext.Current.Request.Cookies["mLoginCookies"];
        if (loginCookies == null)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('对不起,你还没有登陆!'); top.location.href='login.aspx';</script>");
        }
    }
}
