using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
/// <summary>
///weixin_login 的摘要说明
/// </summary>
public class weixin_login
{
	public weixin_login()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public static void login()
    {
        //登录，当前如果已经登录，返回openid，如果没有登录，返回登录链接，重新登录
        string gourl = "index.aspx";
        if(!string.IsNullOrEmpty(HttpContext.Current.Request.QueryString["gourl"]))
        {
            gourl = HttpContext.Current.Request.QueryString["gourl"];
            //if (HttpContext.Current.Request.Cookies["url"] == null)
            //{
            //    HttpCookie hc = new HttpCookie("url");
            //    hc.Value = gourl;
            //    hc.Expires = DateTime.Now.AddDays(1);
            //    HttpContext.Current.Response.Cookies.Add(hc);
            //}
            //else
            //{
            //    HttpCookie hc = HttpContext.Current.Request.Cookies["url"];
            //    hc.Value = gourl;
            //    hc.Expires = DateTime.Now.AddDays(1);
            //    HttpContext.Current.Response.Cookies.Add(hc);
            //}
        }
        if (HttpContext.Current.Request.Cookies["mLoginCookies"] == null)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request["openid"]))
            {
                //string url = "http://mp.qifanhui.cn/Api/Map/index.html?gourl=" + gourl;
                string url = "http://mp.qifanhui.cn/Api/Map/index.html";
                HttpContext.Current.Response.Redirect(url, true);
                
            }
            else
            {
                //微信授权登录返回，获取用户信息，记录到COOKIE
                string openid = HttpContext.Current.Request["openid"];
                //判断是否已经注册，未注册返回注册页面
                string result = re.Get_Http("http://mp.qifanhui.cn/Api/Map/Fans/openid/" + openid + ".html", 3000);
                weixin_info wx = LitJson.JsonMapper.ToObject<weixin_info>(result);
                System.Data.SqlClient.SqlParameter[] sp = { new System.Data.SqlClient.SqlParameter("@openid", openid) };
                System.Data.DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id,member_realName from member where member_openid=@openid", sp);
                string member_id = null;
                if (dt.Rows.Count > 0)
                {
                    //已经注册,获取用户信息
                    member_id=dt.Rows[0][0].ToString();
                    HttpCookie hc = new HttpCookie("mLoginCookies");
                    hc.Expires = DateTime.Now.AddDays(1);
                    hc.Values.Add("member_id", member_id);
                    HttpContext.Current.Response.AppendCookie(hc);

                    member mm = new member();
                    mm.memberName = wx.nickname;
                    mm.nickName = dt.Rows[0][1].ToString();
                    mm.headImgurl = wx.headimgurl;
                    HiFi.Common.CacheClass.SetCache("member_id" + member_id, mm, DateTime.Now.AddDays(1), TimeSpan.Zero);
                    //更新头像
                    dt = Utility.SQLHelper.ExecuteTable("select member_pic from member_info where member_id="+member_id);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() != wx.headimgurl)
                        {
                            Utility.SQLHelper.ExecuteNonQuery("update member_info set member_pic='"+wx.headimgurl+"' where member_id="+member_id);
                        }
                    }
                }
                else
                {
                    //HttpCookie hc2 = new HttpCookie("openid");
                    //hc2.Value = openid;
                    //HttpContext.Current.Response.Cookies.Add(hc2);
                    //HttpContext.Current.Response.Redirect("~/reg.aspx");
                    //新增注册用户
                    zcUser zc= re.getZCUser(openid);
                    if (zc != null)
                    {
                        //有在黑店绑定过资料,跳到完善界面2
                        HttpContext.Current.Response.Redirect("user_info2.aspx?uid=" + openid, true);
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect("user_info.aspx?uid=" + openid + "&pic=" + wx.headimgurl + "&name=" + HttpContext.Current.Server.UrlEncode(wx.nickname), true);
                    }

                    //SqlParameter[] sp2 = { new SqlParameter("@member_name", wx.nickname), new SqlParameter("@member_mobile", ""), new SqlParameter("@member_email", ""), new SqlParameter("@member_pwd", ""), new SqlParameter("@member_number", ""), new SqlParameter("@member_openid", openid) };
                    //dt = Utility.SQLHelper.ExecuteTable("insert into member(member_name,member_pwd,member_mobile,member_email,member_number,member_openid) values(@member_name,@member_pwd,@member_mobile,@member_email,@member_number,@member_openid);SELECT @@IDENTITY", sp2);
                    //member_id = dt.Rows[0][0].ToString();
                    //Utility.SQLHelper.ExecuteNonQuery("insert into member_info(member_id,member_info_exp,member_info_level,member_pic) values(" + member_id + ",30,1,'" + wx.headimgurl + "')");
                    //Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title) values(" + member_id + ",100,'新加入')");
                    //HttpCookie user = new HttpCookie("mLoginCookies");
                    //user.Values.Add("member_id", member_id);
                    //HttpContext.Current.Response.AppendCookie(user);
                }
                //if (HttpContext.Current.Request.Cookies["url"] != null)
                //{
                //    HttpCookie hc = new HttpCookie("url");
                //    if (hc.Value != null)
                //        gourl = hc.Value.Replace("!", "&");
                //}
                //if (HttpContext.Current.Request["gourl"] != null)
                //    HttpContext.Current.Response.Redirect(gourl, true);
                gourl = gourl.Replace("!", "&");
                HttpContext.Current.Response.Redirect(gourl, true);
                //string result = re.Get_Http("http://mp.qifanhui.cn/Api/Map/Fans/openid/" + openid + ".html", 3000);
                //weixin_info wx = LitJson.JsonMapper.ToObject<weixin_info>(result);
                //HttpCookie hc = new HttpCookie();
                //hc.Name = "mLoginCookies";
                //hc.Values.Add("openid", openid);
                //hc.Values.Add("nickname", wx.nickname);
                //hc.Values.Add("headimgurl", wx.headimgurl);
            }

        }
        else HttpContext.Current.Response.Redirect(gourl);//已经微信登录
    }
}