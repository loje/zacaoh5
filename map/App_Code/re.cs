using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// re 的摘要说明
/// </summary>
public class re
{
    public re()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    //防止注入
    public static string replace(string str)
    {
        str = str.Replace("&", "&amp;");
        str = str.Replace("'", "''");
        str = str.Replace("\"", "&quot;");
        str = str.Replace(" ", "&nbsp;");
        str = str.Replace("<", "&lt;");
        str = str.Replace(">", "&gt;");
        str = str.Replace("\n", "<br>");
        return str;
    }

    //文字缩略...
    public static string wlength(string str, int length)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        if (Encoding.UTF8.GetByteCount(str) > str.Length)
        {
            length = length / 2;
        }
        if (str.Length > length)
        {
            return str.Substring(0, length) + "…";
        }
        return str;
    }
    public static string wlength2(string str, int length)
    {
        if (string.IsNullOrEmpty(str)) return string.Empty;
        if (Encoding.UTF8.GetByteCount(str) > str.Length)
        {
            length = length / 2;
        }
        if (str.Length > length)
        {
            return str.Substring(0, length);
        }
        return str;
    }

    public static string getLength(string test, int len)
    {
        int length = 0;
        string title2 = "";
        System.Text.RegularExpressions.Regex regexp = new Regex(@"[A-Za-z0-9]");
        for (int i = 0; i < test.Length; i++)
        {
            if (length > len)
            {
                title2 = title2 + "…";
                break;
            }
            if (regexp.IsMatch(test[i].ToString()))
            {
                length += 1;
                title2 += test[i];
            }
            else
            {
                length += 2;
                title2 += test[i];
            }

        }
        return title2;
    }

    //过滤掉HTML字符
    public static string HTMLStr(string html)
    {
        System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" no[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        html = regex1.Replace(html, ""); //过滤<script></script>标记
        html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
        html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件
        html = regex4.Replace(html, ""); //过滤iframe
        html = regex5.Replace(html, ""); //过滤frameset
        html = regex6.Replace(html, ""); //过滤frameset
        html = regex7.Replace(html, ""); //过滤frameset
        html = regex8.Replace(html, ""); //过滤frameset
        html = regex9.Replace(html, "");
        html = html.Replace(" ", "");
        html = html.Replace("</strong>", "");
        html = html.Replace("<strong>", "");
        return html;
    }

    //后台信息反馈是否查看
    public static string Ztt(string str)
    {
        if (str == "0")
        {
            return "<font color='#0099CC'>未查看</font>";
        }
        else
        {
            return "<font color='#CCCCCC'>已查看</font>";
        }
    }
    public static bool checkMemerLogin()
    {
        if (HttpContext.Current.Request.Cookies["member"] == null)
            return false;
        else return true;
    }
    public static bool checkDailiLogin()
    {
        if (HttpContext.Current.Request.Cookies["daili"] == null)
            return false;
        else return true;
    }
    public static bool checkAdminLogin()
    {
        if (HttpContext.Current.Request.Cookies["admin"] == null)
            return false;
        else return true;
    }
    public static void pd()
    {
        HttpCookie loginCookies = System.Web.HttpContext.Current.Request.Cookies["member"];
        if (loginCookies == null)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('对不起,你还没有登陆!'); top.location.href='login.aspx';</script>");
        }
    }
    public static string getContent(object id, int len)
    {
        string content = "";
        DataTable dt = Utility.SQLHelper.ExecuteTable("select hf_news_content from hf_news where hf_news_id=" + int.Parse(id.ToString()));
        if (dt.Rows.Count > 0)
        {
            content = dt.Rows[0][0].ToString();
            content = HTMLStr(content);
            content = content.Substring(0, len) + "...";
        }
        return content;
    }
    public static string getContentImg(object id)
    {
        string img = "";
        int img_end = 0;
        string content = "";
        DataTable dt = Utility.SQLHelper.ExecuteTable("select hf_news_content from hf_news where hf_news_id=" + int.Parse(id.ToString()));
        if (dt.Rows.Count > 0)
        {
            content = dt.Rows[0][0].ToString();
        }
        img = content;
        int img_start = img.IndexOf("<img", StringComparison.OrdinalIgnoreCase);
        if (img_start != -1)
        {
            img = img.Substring(img_start);
            img_start = img.IndexOf("src", StringComparison.OrdinalIgnoreCase);
            img_start = img_start + 5;
            img_end = img.IndexOf("\"", img_start);
            img = img.Substring(img_start, img_end - img_start + 0);
        }
        else img = "";
        return img;
    }

    public static string getMessageState(object s)
    {
        string result = "未读";
        if (s.ToString() == "1")
            result = "已读";
        return result;
    }

    public static string getJobState(object date)
    {
        string result = "已经结束";
        DateTime dt = Convert.ToDateTime(date);
        if (dt > DateTime.Now)
        {
            result = "未结束";
        }
        return result;
    }

    public static string getJobApplyState(object i)
    {
        //0 1 2 已申请，未通过，可面试
        string result = "";
        string state = i.ToString();
        switch (state)
        {
            case "0":
                result = "已申请";
                break;
            case "1":
                result = "未通过";
                break;
            case "2":
                result = "可面试";
                break;
            default: break;
        }
        return result;
    }

    public static string getTaskState(object s)
    {
        string result = "未完成";//0 1 2 未完成 审核中 已完成
        if (s.ToString() == "1")
            result = "审核中";
        else if (s.ToString() == "2")
            result = "已完成";
        if (!login_member.checkMemerLogin())
        {
            result = "接任务";
        }
        return result;
    }
    public static string hasTask(object task_id)
    {
        string result = "";
        int id = int.Parse(task_id.ToString());
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(HttpContext.Current.Request.Cookies["mLoginCookies"].Values[0]);
            SqlParameter[] sp = { new SqlParameter("task_id", id), new SqlParameter("member_id", member_id) };
            DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id,task_state,b.task_type from task_join a,task b where a.task_id=@task_id and member_id=@member_id and a.task_id=b.task_id", sp);
            if (dt.Rows.Count == 0)
            {
                //还没参加,判断是否到期 
                dt = Utility.SQLHelper.ExecuteTable("select task_id from task where task_id=@task_id and task_enddate>='" + DateTime.Now.ToShortDateString() + "'", sp);
                if (dt.Rows.Count > 0)
                {
                    result = "<a class='btn' onclick='apply(" + id + ")'>接任务</a>";
                }
            }
            else
            {
                string state = dt.Rows[0][1].ToString();
                if (state == "0")
                {
                    result = "<a class='btn' href='javascript:taskSubmit(" + id + "," + dt.Rows[0][2].ToString() + ")'>开始任务</a>";
                }
                else result = "<a class='btn'>已完成</a>";
            }
        }
        else result = "<a class='btn' onclick='apply(" + id + ")'>接任务</a>";
        return result;
    }
    public static string tasking(object task_id, object task_type)
    {
        //判断任务类型，留言型显示提交按钮，探索型需先判断是否已经签到
        string result = "";
        int id = int.Parse(task_id.ToString());
        int member_id = int.Parse(HttpContext.Current.Request.Cookies["mLoginCookies"].Values[0]);
        int type1 = int.Parse(task_type.ToString());
        switch (type1)
        {
            case 1:
                result = "<div class='t_input' onclick='taskSubmit(" + id + "," + type1 + ")'>提交</div>";
                break;
            case 2:
                SqlParameter[] sp = { new SqlParameter("task_id", id), new SqlParameter("member_id", member_id) };
                DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from task_sign  where task_id=@task_id and member_id=@member_id", sp);
                if (dt.Rows.Count == 0)
                {
                    //还没签到,判断是否到期 
                    dt = Utility.SQLHelper.ExecuteTable("select task_id from task where task_id=@task_id and task_enddate>='" + DateTime.Now.ToShortDateString() + "'", sp);
                    if (dt.Rows.Count > 0)
                    {
                        //还没到期
                        result = "<div class='t_input' onclick='taskSign(" + id + "," + type1 + ")'>签到</div>";
                    }
                }
                else result = "<div class='t_input' onclick='taskSubmit(" + id + "," + type1 + ")'>提交</div>";
                break;
            default: break;
        }
        return result;
    }

    public string ToUnicodeString(string str)
    {
        StringBuilder strResult = new StringBuilder();
        if (!string.IsNullOrEmpty(str))
        {
            for (int i = 0; i < str.Length; i++)
            {
                strResult.Append("\\u");
                strResult.Append(((int)str[i]).ToString("x"));
            }
        }
        return strResult.ToString();
    }
    public static string getFileText(string strUrl)
    {
        string strResult = "";
        using (StreamReader sr = new StreamReader(strUrl))
        {
            strResult = sr.ReadToEnd();
            sr.Close();
        }
        return strResult;
    }

    public static string Get_Http(string strUrl, int timeout)
    {
        string strResult;
        try
        {
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            myReq.Timeout = timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            //StreamReader sr = new StreamReader(myStream, Encoding.Default);
            StreamReader sr = new StreamReader(myStream);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {
            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }

    public static string postData(string url, string data)
    {
        HttpWebRequest hreq = WebRequest.Create(url) as HttpWebRequest;
        hreq.Method = "POST";
        hreq.ContentType = "application/x-www-form-urlencoded";
        Encoding encoding = System.Text.Encoding.UTF8;
        string postdata = data;
        byte[] byte1 = encoding.GetBytes(postdata);
        hreq.ContentLength = byte1.Length;
        hreq.AllowAutoRedirect = true;
        HttpWebResponse hres = null;
        Stream poststream = hreq.GetRequestStream();
        poststream.Write(byte1, 0, byte1.Length);
        poststream.Close();
        StreamReader sr = null;
        Stream instream = null;
        hres = (HttpWebResponse)hreq.GetResponse();

        instream = hres.GetResponseStream();
        sr = new StreamReader(instream, encoding);
        string content = sr.ReadToEnd();
        return content;
    }
    public static DateTime StampToDateTime(string timeStamp)
    {
        DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        DateTime dt = dateTimeStart.Add(toNow);
        return dt;
    }
    public static string xqj(string num)
    {
        string xq = null;
        switch (num)
        {
            case "1":
                xq = "星期一";
                break;
            case "2":
                xq = "星期二";
                break;
            case "3":
                xq = "星期三";
                break;
            case "4":
                xq = "星期四";
                break;
            case "5":
                xq = "星期五";
                break;
            case "6":
                xq = "星期六";
                break;
            case "7":
                xq = "星期天";
                break;
            default: break;
        }
        return xq;
    }
    public static string Week(string weekName)
    {
        string week = null;
        switch (weekName)
        {
            case "Sunday":
                week = "7";
                break;
            case "Monday":
                week = "1";
                break;
            case "Tuesday":
                week = "2";
                break;
            case "Wednesday":
                week = "3";
                break;
            case "Thursday":
                week = "4";
                break;
            case "Friday":
                week = "5";
                break;
            case "Saturday":
                week = "6";
                break;
            default: week = "";
                break;
        }
        return week;
    }
    public static string Week2(string weekName, int day)
    {
        string week = null;
        switch (weekName)
        {
            case "Sunday":
                week = "<li class=\"cs1\"><p>S</p><span>" + day + "</span></li><li><p>M</p><span>" + (day + 1) + "</span></li><li><p>T</p><span>" + (day + 2) + "</span></li><li><p>W</p><span>" + (day + 3) + "</span></li><li><p>T</p><span>" + (day + 4) + "</span></li><li><p>F</p><span>" + (day + 5) + "</span></li><li><p>S</p><span>" + (day + 6) + "</span></li>";
                break;
            case "Monday":
                week = "<li><p>S</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>M</p><span>" + day + "</span></li><li><p>T</p><span>" + (day + 1) + "</span></li><li><p>W</p><span>" + (day + 2) + "</span></li><li><p>T</p><span>" + (day + 3) + "</span></li><li><p>F</p><span>" + (day + 4) + "</span></li><li><p>S</p><span>" + (day + 5) + "</span></li>";
                break;
            case "Tuesday":
                week = "<li><p>S</p><span>" + (day - 2) + "</span></li><li><p>M</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>T</p><span>" + day + "</span></li><li><p>W</p><span>" + (day + 1) + "</span></li><li><p>T</p><span>" + (day + 2) + "</span></li><li><p>F</p><span>" + (day + 3) + "</span></li><li><p>S</p><span>" + (day + 4) + "</span></li>";
                break;
            case "Wednesday":
                week = "<li><p>S</p><span>" + (day - 3) + "</span></li><li><p>M</p><span>" + (day - 2) + "</span></li><li><p>T</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>W</p><span>" + (day) + "</span></li><li><p>T</p><span>" + (day + 1) + "</span></li><li><p>F</p><span>" + (day + 2) + "</span></li><li><p>S</p><span>" + (day + 3) + "</span></li>";
                break;
            case "Thursday":
                week = "<li><p>S</p><span>" + (day - 4) + "</span></li><li><p>M</p><span>" + (day - 3) + "</span></li><li><p>T</p><span>" + (day - 2) + "</span></li><li><p>W</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>T</p><span>" + (day) + "</span></li><li><p>F</p><span>" + (day + 1) + "</span></li><li><p>S</p><span>" + (day + 2) + "</span></li>";
                break;
            case "Friday":
                week = "<li><p>S</p><span>" + (day - 5) + "</span></li><li><p>M</p><span>" + (day - 4) + "</span></li><li><p>T</p><span>" + (day - 3) + "</span></li><li><p>W</p><span>" + (day - 2) + "</span></li><li><p>T</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>F</p><span>" + (day) + "</span></li><li><p>S</p><span>" + (day + 1) + "</span></li>";
                break;
            case "Saturday":
                week = "<li><p>S</p><span>" + (day - 6) + "</span></li><li><p>M</p><span>" + (day - 5) + "</span></li><li><p>T</p><span>" + (day - 4) + "</span></li><li><p>W</p><span>" + (day - 3) + "</span></li><li><p>T</p><span>" + (day - 2) + "</span></li><li><p>F</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>S</p><span>" + (day) + "</span></li>";
                break;
            default: week = "";
                break;
        }
        return week;
    }
    public static string checkDate2(int day, int m, int y)
    {
        string result = "";
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        if (y != 0 && m != 0)
        {
            year = y;
            month = m;
        }
        int days = DateTime.DaysInMonth(year, month);
        //int days = day;
        if (day <= 0)
        {
            //判断当前月份是不是一月份
            if (month == 1)
            {
                year = year - 1;
                month = 12;
            }
            else month = month - 1;
            day = DateTime.DaysInMonth(year, month) + day;
        }
        else if (day > days)
        {
            day = day - days;
            month = month + 1;
            if (month > 12)
                year = year + 1;
        }
        result = year + "-" + month + "-" + day.ToString();
        return result;
    }
    public static string checkDate(int day, int m, int y)
    {
        string result = "";
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        if (y != 0 && m != 0)
        {
            year = y;
            month = m;
        }
        int days = DateTime.DaysInMonth(year, month);
        //int days = day;
        if (day <= 0)
        {
            //判断当前月份是不是一月份
            if (month == 1)
            {
                year = year - 1;
                month = 12;
            }
            else month = month - 1;
            day = DateTime.DaysInMonth(year, month) + day;
        }
        else if (day > days)
        {
            day = day - days;
        }
        result = day.ToString();
        return result;
    }
    public static string Week3(string weekName, int day, int m, int y)
    {
        string week = null;

        switch (weekName)
        {
            case "Sunday":
                week = "<li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>S</p><span>" + checkDate(day, m, y) + "</span></li><li  onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>M</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>T</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>W</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>T</p><span>" + checkDate(day + 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 5, m, y) + "\")'><p>F</p><span>" + checkDate(day + 5, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 6, m, y) + "\")'><p>S</p><span>" + checkDate(day + 6, m, y) + "</span></li>";
                break;
            case "Monday":
                week = "<li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>S</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>M</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>T</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>W</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>T</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>F</p><span>" + checkDate(day + 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 5, m, y) + "\")'><p>S</p><span>" + checkDate(day + 5, m, y) + "</span></li>";
                break;
            case "Tuesday":
                week = "<li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>S</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>M</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>T</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>W</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>T</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>F</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>S</p><span>" + checkDate(day + 4, m, y) + "</span></li>";
                break;
            case "Wednesday":
                week = "<li  onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>S</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>M</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>T</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>W</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>T</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>F</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>S</p><span>" + checkDate(day + 3, m, y) + "</span></li>";
                break;
            case "Thursday":
                week = "<li onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>S</p><span>" + checkDate(day - 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>M</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>T</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>W</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>T</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>F</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>S</p><span>" + checkDate(day + 2, m, y) + "</span></li>";
                break;
            case "Friday":
                week = "<li onclick='set(\"" + checkDate2(day - 5, m, y) + "\")'><p>S</p><span>" + checkDate(day - 5, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>M</p><span>" + checkDate(day - 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>T</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>W</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>T</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>F</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>S</p><span>" + checkDate(day + 1, m, y) + "</span></li>";
                break;
            case "Saturday":
                week = "<li onclick='set(\"" + checkDate2(day - 6, m, y) + "\")'><p>S</p><span>" + checkDate(day - 6, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 5, m, y) + "\")'><p>M</p><span>" + checkDate(day - 5, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>T</p><span>" + checkDate(day - 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>W</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>T</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>F</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>S</p><span>" + checkDate(day, m, y) + "</span></li>";
                break;
            default: week = "";
                break;
        }
        return week;
    }
    public static string Week5(string weekName, int day, int m, int y)
    {
        string week = null;
        switch (weekName)
        {
            //case "Sunday":
            //    week = "<li><p>S</p><span>" + checkDate(day) + "</span></li><li onclick='set(" + checkDate(day + 1) + ")' class=\"cs1\"><p>M</p><span>" + checkDate(day + 1) + "</span></li><li><p>T</p><span>" + checkDate(day + 2) + "</span></li><li><p>W</p><span>" + checkDate(day + 3) + "</span></li><li><p>T</p><span>" + checkDate(day + 4) + "</span></li><li><p>F</p><span>" + checkDate(day + 5) + "</span></li><li><p>S</p><span>" + checkDate(day + 6) + "</span></li>";
            //    break;
            //case "Monday":
            //    week = "<li><p>S</p><span>" + checkDate(day - 1) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day) + "</span></li><li><p>T</p><span>" + checkDate(day + 1) + "</span></li><li><p>W</p><span>" + checkDate(day + 2) + "</span></li><li><p>T</p><span>" + checkDate(day + 3) + "</span></li><li><p>F</p><span>" + checkDate(day + 4) + "</span></li><li><p>S</p><span>" + checkDate(day + 5) + "</span></li>";
            //    break;
            //case "Tuesday":
            //    week = "<li><p>S</p><span>" + checkDate(day - 2) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day - 1) + "</span></li><li><p>T</p><span>" + day + "</span></li><li><p>W</p><span>" + checkDate(day + 1) + "</span></li><li><p>T</p><span>" + checkDate(day + 2) + "</span></li><li><p>F</p><span>" + checkDate(day + 3) + "</span></li><li><p>S</p><span>" + checkDate(day + 4) + "</span></li>";
            //    break;
            //case "Wednesday":
            //    week = "<li onclick='set(" + checkDate(day - 3) + ")'><p>S</p><span>" + checkDate(day - 3) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day - 2) + "</span></li><li><p>T</p><span>" + checkDate(day - 1) + "</span></li><li><p>W</p><span>" + (day) + "</span></li><li><p>T</p><span>" + (day + 1) + "</span></li><li><p>F</p><span>" + checkDate(day + 2) + "</span></li><li><p>S</p><span>" + checkDate(day + 3) + "</span></li>";
            //    break;
            //case "Thursday":
            //    week = "<li><p>S</p><span>" + checkDate(day - 4) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day - 3) + "</span></li><li><p>T</p><span>" + checkDate(day - 2) + "</span></li><li><p>W</p><span>" + checkDate(day - 1) + "</span></li><li><p>T</p><span>" + (day) + "</span></li><li><p>F</p><span>" + (day + 1) + "</span></li><li><p>S</p><span>" + checkDate(day + 2) + "</span></li>";
            //    break;
            //case "Friday":
            //    week = "<li><p>S</p><span>" + checkDate(day - 5) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day - 4) + "</span></li><li><p>T</p><span>" + checkDate(day - 3) + "</span></li><li><p>W</p><span>" + checkDate(day - 2) + "</span></li><li><p>T</p><span>" + checkDate(day - 1) + "</span></li><li><p>F</p><span>" + (day) + "</span></li><li><p>S</p><span>" + checkDate(day + 1) + "</span></li>";
            //    break;
            //case "Saturday":
            //    week = "<li><p>S</p><span>" + checkDate(day - 6) + "</span></li><li class=\"cs1\"><p>M</p><span>" + checkDate(day - 5) + "</span></li><li><p>T</p><span>" + checkDate(day - 4) + "</span></li><li><p>W</p><span>" + checkDate(day - 3) + "</span></li><li><p>T</p><span>" + checkDate(day - 2) + "</span></li><li><p>F</p><span>" + checkDate(day - 1) + "</span></li><li><p>S</p><span>" + (day) + "</span></li>";
            //    break;
            case "Sunday":
                week = "<li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>S</p><span>" + checkDate(day, m, y) + "</span></li><li  class=\"cs1\"  onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>M</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>T</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>W</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>T</p><span>" + checkDate(day + 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 5, m, y) + "\")'><p>F</p><span>" + checkDate(day + 5, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 6, m, y) + "\")'><p>S</p><span>" + checkDate(day + 6, m, y) + "</span></li>";
                break;
            case "Monday":
                week = "<li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>S</p><span>" + checkDate(day - 1, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>M</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>T</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>W</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>T</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>F</p><span>" + checkDate(day + 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 5, m, y) + "\")'><p>S</p><span>" + checkDate(day + 5, m, y) + "</span></li>";
                break;
            case "Tuesday":
                week = "<li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>S</p><span>" + checkDate(day - 2, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>M</p><span>" + checkDate(day - 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>T</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>W</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>T</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>F</p><span>" + checkDate(day + 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 4, m, y) + "\")'><p>S</p><span>" + checkDate(day + 4, m, y) + "</span></li>";
                break;
            case "Wednesday":
                week = "<li  onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>S</p><span>" + checkDate(day - 3, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>M</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>T</p><span>" + checkDate(day - 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>W</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>T</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>F</p><span>" + checkDate(day + 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 3, m, y) + "\")'><p>S</p><span>" + checkDate(day + 3, m, y) + "</span></li>";
                break;
            case "Thursday":
                week = "<li onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>S</p><span>" + checkDate(day - 4, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>M</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>T</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>W</p><span>" + checkDate(day - 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>T</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>F</p><span>" + checkDate(day + 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 2, m, y) + "\")'><p>S</p><span>" + checkDate(day + 2, m, y) + "</span></li>";
                break;
            case "Friday":
                week = "<li onclick='set(\"" + checkDate2(day - 5, m, y) + "\")'><p>S</p><span>" + checkDate(day - 5, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>M</p><span>" + checkDate(day - 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>T</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>W</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>T</p><span>" + checkDate(day - 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>F</p><span>" + checkDate(day, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day + 1, m, y) + "\")'><p>S</p><span>" + checkDate(day + 1, m, y) + "</span></li>";
                break;
            case "Saturday":
                week = "<li onclick='set(\"" + checkDate2(day - 6, m, y) + "\")'><p>S</p><span>" + checkDate(day - 6, m, y) + "</span></li><li class=\"cs1\" onclick='set(\"" + checkDate2(day - 5, m, y) + "\")'><p>M</p><span>" + checkDate(day - 5, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 4, m, y) + "\")'><p>T</p><span>" + checkDate(day - 4, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 3, m, y) + "\")'><p>W</p><span>" + checkDate(day - 3, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 2, m, y) + "\")'><p>T</p><span>" + checkDate(day - 2, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day - 1, m, y) + "\")'><p>F</p><span>" + checkDate(day - 1, m, y) + "</span></li><li onclick='set(\"" + checkDate2(day, m, y) + "\")'><p>S</p><span>" + checkDate(day, m, y) + "</span></li>";
                break;
            default: week = "";
                break;
        }
        return week;
    }
    public static string Week4(string weekName, int day)
    {
        string week = null;
        switch (weekName)
        {
            case "Sunday":
                week = "<li class=\"cs1\"><p>S</p><span>" + day + "</span></li><li onclick='set(" + (day + 1) + ")'><p>M</p><span>" + (day + 1) + "</span></li><li><p>T</p><span>" + (day + 2) + "</span></li><li><p>W</p><span>" + (day + 3) + "</span></li><li><p>T</p><span>" + (day + 4) + "</span></li><li><p>F</p><span>" + (day + 5) + "</span></li><li><p>S</p><span>" + (day + 6) + "</span></li>";
                break;
            case "Monday":
                week = "<li><p>S</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>M</p><span>" + day + "</span></li><li><p>T</p><span>" + (day + 1) + "</span></li><li><p>W</p><span>" + (day + 2) + "</span></li><li><p>T</p><span>" + (day + 3) + "</span></li><li><p>F</p><span>" + (day + 4) + "</span></li><li><p>S</p><span>" + (day + 5) + "</span></li>";
                break;
            case "Tuesday":
                week = "<li><p>S</p><span>" + (day - 2) + "</span></li><li><p>M</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>T</p><span>" + day + "</span></li><li><p>W</p><span>" + (day + 1) + "</span></li><li><p>T</p><span>" + (day + 2) + "</span></li><li><p>F</p><span>" + (day + 3) + "</span></li><li><p>S</p><span>" + (day + 4) + "</span></li>";
                break;
            case "Wednesday":
                week = "<li onclick='set(" + (day + 1) + ")'><p>S</p><span>" + (day - 3) + "</span></li><li><p>M</p><span>" + (day - 2) + "</span></li><li><p>T</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>W</p><span>" + (day) + "</span></li><li><p>T</p><span>" + (day + 1) + "</span></li><li><p>F</p><span>" + (day + 2) + "</span></li><li><p>S</p><span>" + (day + 3) + "</span></li>";
                break;
            case "Thursday":
                week = "<li><p>S</p><span>" + (day - 4) + "</span></li><li><p>M</p><span>" + (day - 3) + "</span></li><li><p>T</p><span>" + (day - 2) + "</span></li><li><p>W</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>T</p><span>" + (day) + "</span></li><li><p>F</p><span>" + (day + 1) + "</span></li><li><p>S</p><span>" + (day + 2) + "</span></li>";
                break;
            case "Friday":
                week = "<li><p>S</p><span>" + (day - 5) + "</span></li><li><p>M</p><span>" + (day - 4) + "</span></li><li><p>T</p><span>" + (day - 3) + "</span></li><li><p>W</p><span>" + (day - 2) + "</span></li><li><p>T</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>F</p><span>" + (day) + "</span></li><li><p>S</p><span>" + (day + 1) + "</span></li>";
                break;
            case "Saturday":
                week = "<li><p>S</p><span>" + (day - 6) + "</span></li><li><p>M</p><span>" + (day - 5) + "</span></li><li><p>T</p><span>" + (day - 4) + "</span></li><li><p>W</p><span>" + (day - 3) + "</span></li><li><p>T</p><span>" + (day - 2) + "</span></li><li><p>F</p><span>" + (day - 1) + "</span></li><li class=\"cs1\"><p>S</p><span>" + (day) + "</span></li>";
                break;
            default: week = "";
                break;
        }
        return week;
    }

    public static string getProType(object type)
    {
        string result = "虚";
        if (type.ToString() == "2")
        {
            result = "实";
        }
        return result;
    }
    public static string getProType2(object type)
    {
        string result = "实物礼品";
        if (type.ToString().IndexOf("虚拟") != -1 || type.ToString().IndexOf("喇叭") != -1)
        {
            result = "虚拟礼品";
        }
        return result;
    }
    public static string getDate(object date)
    {
        string result = "";
        DateTime dt = Convert.ToDateTime(date);
        TimeSpan ts = DateTime.Now - dt;
        if (ts.Days > 0)
            result = dt.ToShortDateString();
        else
        {
            if (ts.Hours > 1)
                result = ts.Hours + "小时" + ts.Minutes + "分钟前";
            else
                result = (ts.Hours * 60 + ts.Minutes) + "分钟前";
        }
        return result;
    }

    public static int answerCount(object id)
    {
        int result = 0;
        int qid = Convert.ToInt32(id);
        result = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select count(lesson_question_id) from lesson_answer where lesson_question_id=" + qid));
        return result;
    }
    public static member getMember(object id)
    {
        string mid = "member" + id;
        member mb = (member)HiFi.Common.CacheClass.GetCache(mid);
        if (mb == null)
        {
            //获取不到缓存信息，统一用cache保存用户信息
            //先判断是否是微信登录
            string uname = null, pic = null, openid = "", member_number = null;
            DataTable dt = Utility.SQLHelper.ExecuteTable("select member_name,b.member_pic,member_openid,member_number from member a,member_info b where a.member_id=b.member_id and a.member_id=" + id);
            if (dt.Rows.Count > 0)
            {
                openid = dt.Rows[0][2].ToString();
                if (openid == "")
                {
                    //非微信登录
                    pic = dt.Rows[0][1].ToString();
                    uname = dt.Rows[0][0].ToString();
                    member_number = dt.Rows[0]["member_number"].ToString();
                }
                else
                {
                    string content = Get_Http("http://mp.qifanhui.cn/Api/Map/Fans/openid/" + dt.Rows[0][2].ToString() + ".html", 3000);
                    weixin_info wx = LitJson.JsonMapper.ToObject<weixin_info>(content);
                    pic = wx.headimgurl;
                    uname = wx.nickname;
                }
            }
            mb = new member();
            mb.memberName = uname;
            mb.nickName = uname;
            mb.headImgurl = pic;
            mb.openid = openid;
            mb.member_number = member_number;
            HiFi.Common.CacheClass.SetCache(mid, mb, DateTime.Now.AddDays(1), TimeSpan.Zero);
        }
        //if (string.IsNullOrEmpty(mb.openid))
        //{
        //    HttpContext.Current.Response.Redirect(gotoLoginUrl(),true);
        //}
        return mb;
    }
    public static zcUser getZCUser(string openid)
    {
        string content = Get_Http("http://mp.qifanhui.cn/Api/Map/UedInfo/openid/" + openid + ".html", 3000);
        zcUser zc = null;
        if (content != "{\"nickname\":\"\"}")
        {
            //用户在黑店有资料
            zc = LitJson.JsonMapper.ToObject<zcUser>(content);
        }
        return zc;
    }
    public static achievement getAVUser(string openid)
    {
        string content = Get_Http("http://mp.qifanhui.cn/Api/Map/achievement/openid/" + openid + ".html", 3000);
        achievement av = null;
        if (content.IndexOf("express") != -1)
        {
            av = new achievement();
            achievement av1 = LitJson.JsonMapper.ToObject<achievement>(content);
            av.express = av1.express;
            av.welfareday = av1.welfareday;
            if (content.IndexOf("timer") == -1)
                av.timer = "-1";
            else av.timer = av1.timer;
        }
        return av;
    }
    public static weixin_info getWXInfo(string openid)
    {
        string content = Get_Http("http://mp.qifanhui.cn/Api/Map/Fans/openid/" + openid + ".html", 3000);
        weixin_info wx = null;
        if (content != "{\"nickname\":\"\"}")
        {
            //用户在黑店有资料
            wx = LitJson.JsonMapper.ToObject<weixin_info>(content);
        }
        return wx;
    }
    public static string getMemberPic(object id)
    {
        string result = getMember(id).headImgurl;
        return result;
    }
    public static string getMemberPic2(object id)
    {
        string result = "";
        DataTable dt = Utility.SQLHelper.ExecuteTable("select member_pic from member_info where member_id=" + id);
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() != "")
            {
                //非微信登录
                result = dt.Rows[0][0].ToString();
            }
            else
            {
                result = "/images/u1.jpg";
            }
        }
        else result = "/images/u1.jpg";
        dt.Dispose();
        return result;
    }
    //获取当前用户的经验值，当前级别。如果经验值超过当前级别，级别加1
    public static void upDateEXP(int member_id, int exp)
    {
        int max = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select max(level_exp) from level"));
        DataTable dt = Utility.SQLHelper.ExecuteTable("select member_info_exp,level_exp from member_info a,level b where b.level_id=a.member_info_level and a.member_id=" + member_id);
        if (dt.Rows.Count > 0)
        {
            int mexp = Convert.ToInt32(dt.Rows[0][0]);
            int levelExp = Convert.ToInt32(dt.Rows[0][1]);
            mexp += exp;
            string sql = null;
            if (mexp > max)
            {

                //用户需要升级到最高级
                //获取最高的等级ID
                int lid = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select top 1 level_id from level order by level_exp desc"));
                //mexp = max;
                sql = "update member_info set member_info_exp=" + mexp + ",member_info_level=" + lid + " where member_id=" + member_id;
            }
            else if (mexp >= levelExp)
            {
                //经验值超过当前等级的经验值，需要升级
                //
                int lid = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select top 1 level_id from level where level_exp>" + mexp + " order by level_exp asc"));
                sql = "update member_info set member_info_exp=" + mexp + ",member_info_level=" + lid + " where member_id=" + member_id;
            }
            else
            {
                sql = "update member_info set member_info_exp=" + mexp + " where member_id=" + member_id;
            }
            Utility.SQLHelper.ExecuteNonQuery(sql);
        }
    }
    public static List<UedGoods> getGoods()
    {
        string cacheName = "goods";
        List<UedGoods> lu = (List<UedGoods>)HiFi.Common.CacheClass.GetCache(cacheName);
        if (lu == null)
        {
            string url = "http://mp.qifanhui.cn/Api/Map/UedGoods.html";
            string result = Get_Http(url, 3000);
            lu = LitJson.JsonMapper.ToObject<List<UedGoods>>(result);
            HiFi.Common.CacheClass.SetCache(cacheName, lu, DateTime.Now.AddDays(1), TimeSpan.Zero);
        }
        return lu;
    }
    public static List<orderCache> getOrders(string openid, int member_id)
    {
        string mid = "orders_" + member_id;
        List<orderCache> oc = (List<orderCache>)HiFi.Common.CacheClass.GetCache(mid);
        if (oc == null)
        {
            oc = new List<orderCache>();
            string url = "http://mp.qifanhui.cn/Api/Map/SeedRecord/openid/" + openid + ".html";
            string result = Get_Http(url, 3000);
            List<Order> lo = LitJson.JsonMapper.ToObject<List<Order>>(result);
            string ptype = null;
            string ptitle = null;
            foreach (Order o in lo)
            {
                if (o.remark.IndexOf("黑店") != -1)
                {
                    ptype = "实物礼品";//黑店订单：20160722jpcp0094,花费3
                    string num = o.remark;
                    int start = num.IndexOf("2");
                    int end = num.IndexOf(",");
                    num = num.Substring(start, end - start);
                    orderDetail od = re.getOrderState(num);
                    if (od != null)
                    {
                        if (od.shop_title == null)
                            ptitle = "订单已被删除";//订单已被删除
                        else ptitle = od.shop_title;
                    }
                    else ptitle = "订单已被删除";
                }
                else if (o.remark.IndexOf("签到") != -1 || o.remark.IndexOf("成就") != -1 || o.remark.IndexOf("任务") != -1 || o.remark.IndexOf("探索") != -1 || o.remark.IndexOf("报名") != -1)
                {
                    ptype = "其他";
                    ptitle = o.remark;
                }
                else
                {
                    ptype = "虚拟物品";
                    ptitle = o.remark;
                }
                orderCache oc1 = new orderCache();
                oc1.time = re.StampToDateTime(o.time).ToString();
                oc1.ptype = ptype;
                oc1.ptitle = ptitle;
                oc1.num = o.num;
                oc.Add(oc1);
            }
            HiFi.Common.CacheClass.SetCache(mid, oc, DateTime.Now.AddDays(1), TimeSpan.Zero);
        }

        return oc;
    }
    public static List<major> getMajor()
    {
        string text = re.Get_Http("http://zp.qifanhui.cn/Home/AppApi/MajorList.html", 20000);
        List<major> lm = (List<major>)HiFi.Common.CacheClass.GetCache("major");
        if (lm == null)
        {
            lm = LitJson.JsonMapper.ToObject<List<major>>(text);
            HiFi.Common.CacheClass.SetCache("major", lm, DateTime.Now.AddDays(7), TimeSpan.Zero);
        }
        return lm;
    }
    public static orderDetail getOrderState(string num)
    {
        string url = "http://mp.qifanhui.cn/Api/Map/ShopOrder.html";
        string result = postData(url, "order=" + num);
        orderDetail od = null;
        if (result.IndexOf("msg") == -1)
            od = LitJson.JsonMapper.ToObject<orderDetail>(result);
        return od;
    }
    public static string getOrderState2(string num)
    {
        string url = "http://mp.qifanhui.cn/Api/Map/ShopOrder.html";
        string result = postData(url, "order=" + num);

        return result;
    }
    public static string getSkillType(object t)
    {
        string re = null;
        switch (t.ToString())
        {
            case "1":
                re = "通用技能";
                break;
            case "2":
                re = "专业技能";
                break;
            case "3":
                re = "生存技能";
                break;
            default: break;
        }
        return re;
    }
    public static string getSkillApplyState(object t)
    {
        string re = null;
        switch (t.ToString())
        {
            case "0":
                re = "认证中";
                break;
            case "1":
                re = "通过认证";
                break;
            default: break;
        }
        return re;
    }
    public static bool isPic(string ext)
    {
        bool pic = false;
        ArrayList al = new ArrayList();
        al.Add(".jpg");
        al.Add(".jpeg");
        al.Add(".png");
        if (al.Contains(ext.ToLower()))
        {
            pic = true;
        }
        return pic;
    }
    public static string getAdvertPic(object a1, object a2)
    {
        string result = a1.ToString();
        if (a2.ToString() != "")
        {
            result = "/" + a2.ToString();
        }
        return result;
    }
    public static string getNum(object num)
    {
        string result = "";
        int num2 = Convert.ToInt32(num);
        for (int i = 0; i < num2; i++)
        {
            result += "<i></i>";

        }
        return result;
    }
    public static List<rank> getRank()
    {
        List<rank> lr = (List<rank>)HiFi.Common.CacheClass.GetCache("rank");
        if (lr == null)
        {
            lr = new List<rank>();
            //获取排行前10名的用户，同时判断当前的成就值，跟5小时前相比，大于就是上涨，小就是下跌
            //获取5个小时前的排名
            //DataTable dt = Utility.SQLHelper.ExecuteTable("select top 20 sum(success) as rank,a.member_id ,member_name from member_success a,member b where a.member_id=b.member_id and member_success_date<='"+DateTime.Now.AddHours(-5).ToString()+"'  group by a.member_id,b.member_name  order by rank desc");
            //获取5小时前的总排名 
            DataTable dt = Utility.SQLHelper.ExecuteTable("select sum(success) as rank,member_id from member_success where member_success_date<='" + DateTime.Now.AddHours(-5).ToString() + "'  group by member_id  order by rank desc");
            //获取当前时间前20名的排名
            DataTable dt2 = Utility.SQLHelper.ExecuteTable("select top 20 sum(success) as rank,a.member_id ,member_name from member_success a,member b where a.member_id=b.member_id and member_success_date<='" + DateTime.Now.ToString() + "'  group by a.member_id,b.member_name  order by rank desc");
            string member_id;
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                rank rk = new rank();
                member_id = dt2.Rows[i][1].ToString();
                rk.member_id = Convert.ToInt32(member_id);
                rk.member_name = dt2.Rows[i][2].ToString();
                rk.success = Convert.ToInt32(dt2.Rows[i][0]);
                rk.sort = 1;
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j][1].ToString() == member_id)
                    {
                        if (j > i) rk.sort = 0;
                        else if (j == i) rk.sort = -1;
                        break;
                    }
                }
                lr.Add(rk);
            }
            HiFi.Common.CacheClass.SetCache("rank", lr, DateTime.Now.AddHours(5), TimeSpan.Zero);//缓存5小时
        }
        return lr;
    }
    public static List<RegistRecord> getRegistRecord(string openid)
    {
        List<RegistRecord> li = null;
        string content = Get_Http("http://mp.qifanhui.cn/Api/Map/RegistRecord/openid/" + openid + ".html", 3000);
        if (content.Length > 10)
        {
            li = LitJson.JsonMapper.ToObject<List<RegistRecord>>(content);
        }
        return li;
    }
    public static LitJson.JsonData getUserResume(string openid)
    {
        LitJson.JsonData rs = null;    
        string content = Get_Http("http://jz.qifanhui.cn/Home/ZacaoApi/Resume/openid/" + openid + ".html", 3000);
        if (content.Length > 20)
        {
            rs = LitJson.JsonMapper.ToObject(content);
            //rs = LitJson.JsonMapper.ToObject<Resume>(content);
        }
        return rs;
    }
    //http://jz.qifanhui.cn/Home/ZacaoApi/History/openid/orLnoswoy3_acM1CKdLG4EjLAJoY.html
    public static bool baoming(string id, string openid)
    {
        bool has = false;
        LitJson.JsonData rs = (LitJson.JsonData)HiFi.Common.CacheClass.GetCache("jz" + openid);
        if (rs == null)
        {
            string content = Get_Http("http://jz.qifanhui.cn/Home/ZacaoApi/History/openid/" + openid + ".html", 3000);
            rs = LitJson.JsonMapper.ToObject(content);
        }
        if (rs != null)
        {
            foreach (LitJson.JsonData lj in rs)
            {
                if (lj["jid"].ToString() == id)
                {
                    has = true;
                    break;
                }
            }
        }
        return has;
    }
    public static string getCustomMarkCount(int id)
    {
        string result = "";
        int mcount = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select count(custom_mark_id) from custom_mark where custom_mark_school=" + id));
        if (mcount > 0)
        {
            result = "var ";
            for (int i = 1; i <= mcount; i++)
            {
                result += "cmark" + (i) + ",";
            }
            result = result.Substring(0, result.Length - 1) + ";";
        }
        return result;
    }
    public static string getCustomMark(int id)
    {
        string result = "";
        DataTable dt = Utility.SQLHelper.ExecuteTable("select * from custom_mark where custom_mark_school=" + id);
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            result += "if (cmark" + i + " != undefined){markersLayer.removeMarker(cmark" + i + ");}";
        }
        int j = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            j = i + 1;
            result += "iconUrl3='/" + dt.Rows[i]["custom_mark_img"] + "';size3 = new OpenLayers.Size(Math.floor(" + dt.Rows[i]["custom_mark_width"] + " / s), Math.floor(" + dt.Rows[i]["custom_mark_height"] + " / s));icons = new OpenLayers.Icon(iconUrl3, size3);cmark" + j + "=new OpenLayers.Marker(new OpenLayers.LonLat(" + dt.Rows[i]["custom_mark_x"] + ", " + dt.Rows[i]["custom_mark_y"] + "), icons);";
            if (dt.Rows[i]["custom_mark_web"].ToString() != "")
            {
                result += "cmark" + j + ".events.register('click', cmark1, function (evt) { markerEvent2(evt, '" + dt.Rows[i]["custom_mark_web"].ToString() + "') });";
            }
            result += "markersLayer.addMarker(cmark" + j + ");";
        }
        dt.Dispose();
        return result;
    }
    public static string gotoLoginUrl()
    {
        string result = "/login.aspx?gourl=" + HttpContext.Current.Request.Url.AbsoluteUri.Replace("&", "!");
        return result;
    }
    public static string taskContent(object obj)
    {
        string re = null;
        re = "未添加任务简述";
        return re;
    }
    public static string pingbi(string text)
    {
        string re = null;
        object obj = HiFi.Common.CacheClass.GetCache("pingbi");
        string content = null;
        if (obj == null)
        {
            content = Utility.SQLHelper.ExecuteScalar("select ziku_content from ziku").ToString();
            HiFi.Common.CacheClass.SetCache("pingbi", content, DateTime.Now.AddDays(10), TimeSpan.Zero);
        }
        else content = obj.ToString();
        if (!string.IsNullOrEmpty(content))
        {
            string[] pingbi = content.Split('|');
            foreach (string s in pingbi)
            {
                if (text.IndexOf(s) != -1)
                    text = text.Replace(s, "*");
            }
        }
        re = text;
        return re;
    }
    public static int getLaBa(string mid)
    {
        int laba = 0;
        //购买过的小喇叭
        object obj = Utility.SQLHelper.ExecuteScalar("select sum(product_count) from member_prop where product_id=1 and member_id=" + mid);
        if (obj != null && obj != DBNull.Value)
        {
            laba = Convert.ToInt32(obj);
            if (laba > 0)
            {
                //消费的喇叭数
                obj = Utility.SQLHelper.ExecuteScalar("select sum(member_prop_used_count) from member_prop_used a,member_prop b where a.member_prop_id=b.member_prop_id and b.product_id=1 and b.member_id=" + mid);
                if (obj != null && obj != DBNull.Value)
                    laba -= Convert.ToInt32(obj);
            }
        }
        return laba;
    }
    public static int getDaoju(object id)
    {

        int laba = 0;
        if (login_member.checkMemerLogin())
        {
            int did = Convert.ToInt32(id);
            int member_id = int.Parse(HttpContext.Current.Request.Cookies["mLoginCookies"].Values[0]);
            //购买过的小喇叭
            object obj = Utility.SQLHelper.ExecuteScalar("select sum(product_count) from member_prop where product_id=" + did + " and member_id=" + member_id);
            if (obj != null && obj != DBNull.Value)
            {
                laba = Convert.ToInt32(obj);
                if (laba > 0)
                {
                    //消费的喇叭数
                    obj = Utility.SQLHelper.ExecuteScalar("select sum(member_prop_used_count) from member_prop_used a,member_prop b where a.member_prop_id=b.member_prop_id and b.product_id=" + did + " and b.member_id=" + member_id);
                    if (obj != null && obj != DBNull.Value)
                        laba -= Convert.ToInt32(obj);
                }
            }
        }
        return laba;
    }
    //public static member getMember(string openid,string member_id,string mname,string nname,string img)
    //{
    //    string cName = "member" + member_id;
    //    member mb = (member)HiFi.Common.CacheClass.GetCache(cName);
    //    if (mb == null)
    //    {
    //        mb = new member();
    //        mb.headImgurl = img;
    //        mb.memberName = mname;
    //        mb.nickName = nname;
    //        mb.openid = openid;
    //        HiFi.Common.CacheClass.SetCache(cName, mb, DateTime.Now.AddDays(1), TimeSpan.Zero);
    //    }
    //    return mb;
    //}
}
