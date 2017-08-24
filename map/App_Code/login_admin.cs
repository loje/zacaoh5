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
public class login_admin
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

    public login_admin()
	{
	}
    public static void lo(string usn, string pwd)
    {
        string sql = "select id,flag from hf_admin where username=@username and password=@password";
        SqlParameter[] sp = { new SqlParameter("@username", usn), new SqlParameter("@password", pwd) };
        DataTable dt = Utility.SQLHelper.ExecuteTable(sql,sp);
        if (dt.Rows.Count > 0)
        {
            HttpCookie cookie = new HttpCookie("LoginCookies");
            cookie.Expires = DateTime.Now.AddDays(1); //DateTime.Now.AddDays(1);
            cookie.HttpOnly = false;
            cookie.Values.Add("uid", dt.Rows[0]["id"].ToString());
            cookie.Values.Add("flag", dt.Rows[0]["flag"].ToString());
            System.Web.HttpContext.Current.Response.AppendCookie(cookie);


            Utility.SQLHelper.ExecuteNonQuery("update hf_admin set last_ip='" + GetUserIp() + "',login_num=login_num+1 where ID=" + dt.Rows[0]["id"]);
            System.Web.HttpContext.Current.Response.Write("<script>location.href='product_list.aspx';</script>");
        }
        else
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('用户名或密码错误!');</script>");
        }

    }

    //判断用户是否登陆
    public static bool pd()
    {
        bool lg = false;
        HttpCookie loginCookies = System.Web.HttpContext.Current.Request.Cookies["LoginCookies"];
        if (loginCookies == null)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('对不起,你还没有登陆!'); top.location.href='login.aspx';</script>");
        }
        else lg = true;
        return lg;
    }
}
