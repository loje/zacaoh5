using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using LitJson;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using System.Web.Security;
using HtmlAgilityPack;
using System.Net;

public partial class ajax : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.Form["action"]))
        {
            string action = Request.Form["action"];
            switch (action)
            {
                case "companyList":
                    companyList();
                    break;
                case "jobList":
                    jobList();
                    break;
                case "jobList2":
                    jobList2();
                    break;

                case "jianzhiList":
                    jianzhiList();
                    break;

                case "collect_jianzhiList":
                    collect_jianzhiList();
                    break;
                case "collect_jobList":
                    collect_jobList();
                    break;
                case "searchJianZhi":
                    searchJianZhi();
                    break;
                case "searchJob":
                    searchJob();
                    break;
                case "apply_job_list":
                    apply_job_list();
                    break;
                case "memberLogin":
                    memberLogin();
                    break;
                case "collectionJob":
                    collectionJob();
                    break;
                case "applyJob":
                    applyJob();
                    break;
                case "regMember":
                    regMember();
                    break;
                case "regMember2":
                    regMember2();
                    break;
                case "badge_apply":
                    badge_apply();
                    break;
                case "skill_apply":
                    skill_apply();
                    break;
                case "skill_apply2":
                    skill_apply2();
                    break;
                case "taskJoin":
                    taskJoin();
                    break;
                case "socail_new":
                    socail_new();
                    break;
                case "fayan":
                    fayan();
                    break;
                case "fayan2":
                    fayan2();
                    break;
                case "privateChat":
                    privateChat();
                    break;
                case "chatroom":
                    chatroom();
                    break;
                case "userList":
                    userList();
                    break;
                case "chatroomUser":
                    chatroomUser();
                    break;
                case "feedback":
                    feedback();
                    break;
                case "changepwd":
                    changepwd();
                    break;
                case "signIn":
                    signIn();
                    break;
                case "mallChange":
                    mallChange();
                    break;
                case "mallChange2":
                    mallChange2();
                    break;
                case "addPlan":
                    addPlan();
                    break;
                case "delPlan":
                    delPlan();
                    break;
                case "socail_pwd":
                    socail_pwd();
                    break;
                case "getOrder":
                    getOrder();
                    break;
                case "getOrders":
                    getOrders();
                    break;
                case "szdxBuilding":
                    szdxBuilding();
                    break;
                case "szxxBuilding":
                    szxxBuilding();
                    break;
                case "szzyBuilding":
                    szzyBuilding();
                    break;
                case "szzy_custom_mark":
                    szzy_custom_mark();
                    break;
                case "searchList":
                    searchList();
                    break;
                case "coinList":
                    coinList();
                    break;
                case "proList":
                    proList();
                    break;
                case "proList2":
                    proList2();
                    break;
                case "questionAdd":
                    questionAdd();
                    break;
                case "questionSearch":
                    questionSearch();
                    break;
                case "answerAdd":
                    answerAdd();
                    break;
                case "lessonInit":
                    lessonInit();
                    break;
                case "lessonInit2":
                    lessonInit2();
                    break;
                case "class_list":
                    class_list();
                    break;
                case "pjck":
                    pjck();
                    break;
                case "memberPic":
                    memberPic();
                    break;
                case "taskList":
                    taskList();
                    break;
                case "task1pulgs":
                    task1pulgs();
                    break;
                case "lectureList1":
                    lectureList1();
                    break;
                case "lectureList2":
                    lectureList2();
                    break;
                case "lectureList":
                    lectureList();
                    break;
                case "getbadgetype2":
                    getbadgetype2();
                    break;
                case "upUser":
                    upUser();
                    break;
                case "delanswer":
                    delanswer();
                    break;
                case "delquestion":
                    delquestion();
                    break;
                case "sumbit_search":
                    sumbit_search();
                    break;
                case "change_pack":
                    change_pack();
                    break;
                case "signed_lecture":
                    signed_lecture();
                    break;
                case "resume":
                    resume();
                    break;
                case "joy":
                    joy();
                    break;
                case "delfeedback":
                    delfeedback();
                    break;
                case "check_private_chat":
                    check_private_chat();
                    break;

                //表白
                case "confessionAdd":
                    confessionAdd();
                    break;

                //点赞提交
                case "like":
                    like();
                    break;

                //加载更多最新
                case "jiazaigengduo":
                    jiazaigengduo();
                    break;

                //加载更多最热
                case "zuire":
                    zuire();
                    break;

                 //表白记录删除
                case "confessionAdddelete":
                    confessionAdddelete();
                    break;

                  //杂活信息
                case "lecturellll":
                    lecturellll();
                    break;


                //深职 类型、学校、建筑
                case "Trigeminy3":
                    Trigeminy3();
                    break;

                //深大 类型、学校、建筑
                case "Trigeminy1":
                    Trigeminy1();
                    break;

                //深信 类型、学校、建筑
                case "Trigeminy2":
                    Trigeminy2();
                    break;

                //跨域要获取到的值
                case"Verdict":
                    Verdict();
                    break;
                default:break;
            }
        }
    }

    private void check_private_chat()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int laba = re.getLaBa(member_id.ToString());
            if (laba > 0)
            {
                int rid = int.Parse(Request.Form["id"]);
                DataTable dt = Utility.SQLHelper.ExecuteTable("select private_chat_id from private_chat where (send_id=" + member_id + " or receive_id=" + member_id + ") and  (send_id=" + rid + " or receive_id=" + rid + ")");
                if (dt.Rows.Count > 0)
                {
                    //存在私聊记录
                    Response.Write("private_chat.aspx?id=" + dt.Rows[0][0].ToString());
                }
                else Response.Write("private_chat.aspx?rid=" + rid);
            }

        }
        else
        {
            Response.Write("login");
        }
    }
    private void delfeedback()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int id = int.Parse(Request.Form["id"]);
            int i = Utility.SQLHelper.ExecuteNonQuery("delete from feedback where feedback_id=" + id + " and member_id=" + member_id);
            if (i > 0)
            {
                Utility.SQLHelper.ExecuteNonQuery("delete from feedback_reply where feedback_id=" + id);
                Response.Write("ok");
            }

        }
        else
        {
            Response.Write("login");
        }
    }
    //今日活动点赞
    private void joy()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            member mm = re.getMember(member_id);
            int id = int.Parse(Request.Form["id"]);
            string content = re.postData("http://mp.qifanhui.cn/Api/Map/enjoy.html", "lid=" + id + "&openid=" + mm.openid);
            JsonData js = JsonMapper.ToObject(content);
            if (js["code"].ToString() == "0")
            {
                HiFi.Common.CacheClass.RemoveOneCache("lecture2");
                HiFi.Common.CacheClass.RemoveOneCache("lecture1");
                Response.Write("ok");
            }
        }
        else
        {
            Response.Write("loginerr");
        }
    }
    private void szzy_custom_mark()
    {
        string result = "";
        DataTable dt = Utility.SQLHelper.ExecuteTable("select * from custom_mark where custom_mark_school=3");
        for (int i = 1; i <= dt.Rows.Count; i++)
        {
            result += "if (cmark" + i + " != undefined){markersLayer.removeMarker(cmark" + i + ");}";
        }
        int j = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            j = i + 1;
            result += "iconUrl3='" + dt.Rows[i]["custom_mark_img"] + "';var size3 = new OpenLayers.Size(Math.floor(" + dt.Rows[i]["custom_mark_width"] + " / s), Math.floor(" + dt.Rows[i]["custom_mark_height"] + " / s));var icons = new OpenLayers.Icon(iconUrl3, size3);cmark" + j + "=new OpenLayers.Marker(new OpenLayers.LonLat(" + dt.Rows[i]["custom_mark_x"] + ", " + dt.Rows[i]["custom_mark_y"] + "), icons);";
            if (dt.Rows[i]["custom_mark_web"].ToString() != "")
            {
                result += "cmark" + j + ".events.register('click', cmark1, function (evt) { markerEvent2(evt, '" + dt.Rows[i]["custom_mark_web"].ToString() + "') });";
            }
            result += "markersLayer.addMarker(cmark" + j + ");";
        }
        dt.Dispose();
        Response.Write(result);
    }
    private void resume()
    {
        if (login_member.checkMemerLogin())
        {
            int id = int.Parse(Request.Form["jid"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            member mm = re.getMember(member_id);
            string name = Server.UrlDecode(Request.Form["name"]);
            string openid = mm.openid;
            string sex = Request.Form["sex"];
            string birthday = Request.Form["birthday"];
            string education = Request.Form["education"];
            string phone = Request.Form["phone"];
            string alipay = Request.Form["alipay"];
            string health_card = Request.Form["health_card"];
            string height = Request.Form["height"];
            string bust = Request.Form["bust"];
            string waist = Request.Form["waist"];
            string hipline = Request.Form["hipline"];
            string code = re.postData("http://jz.qifanhui.cn/Home/ZacaoApi/SignUp.html", "jid=" + id + "&openid=" + openid);
            LitJson.JsonData jd = LitJson.JsonMapper.ToObject(code);
            if (jd["code"].ToString() == "0")
            {
                code = re.postData("http://jz.qifanhui.cn/Home/ZacaoApi/UpdateResume.html", "name=" + name + "&openid=" + openid + "&sex=" + sex + "&birthday=" + birthday + "&education=" + education + "&phone=" + phone + "&health_card=" + health_card + "&height=" + height + "&bust=" + bust + "&waist=" + waist + "&hipline=" + hipline + "&alipay=" + alipay);
                prove2 pr = LitJson.JsonMapper.ToObject<prove2>(code);
                if (pr.code == 0)
                {
                    Response.Write("ok");
                }
            }
            else if (jd["code"].ToString() == "1")
            {
                Response.Write("不需要重复报名");
            }
        }
    }

    //杂活报名
    private void signed_lecture()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            member mm = re.getMember(member_id);
            string school = Server.UrlDecode(Request.Form["school"]);
            string stn = Request.Form["stn"];
            string stname = Server.UrlDecode(Request.Form["stname"]);
            string tel = Request.Form["tel"];
            int id = int.Parse(Request.Form["id"]);
            string code = re.postData("http://mp.qifanhui.cn/Api/Map/signed.html", "lid=" + id + "&stuno=" + stn + "&school=" + school + "&name=" + stname + "&openid=" + mm.openid + "&contacts=" + tel);
            prove2 pr = LitJson.JsonMapper.ToObject<prove2>(code);
            if (pr.code == 0)
            {
                Response.Write("ok");
            }
        }
        else
        {
            Response.Write("loginerr");
        }
    }



    private void change_pack()
    {
        if (login_member.checkMemerLogin())
        {
            int t = int.Parse(Request.Form["t"]);
            int id = int.Parse(Request.Form["id"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string sql = "member_header";
            switch (t)
            {
                case 2:
                    sql = "member_upper_half";
                    break;
                case 3:
                    sql = "member_lower_half";
                    break;
                default: break;
            }
            //判断是否操作同件背包物品
            DataTable dt = Utility.SQLHelper.ExecuteTable("select search_pack_id from " + sql + " where search_pack_id=" + id + " and member_id=" + member_id);
            if (dt.Rows.Count > 0)
            {
                //是，删除
                Utility.SQLHelper.ExecuteNonQuery("delete from " + sql + " where search_pack_id=" + id + " and member_id=" + member_id);
                Response.Write("del");
            }
            else
            {
                int i = Utility.SQLHelper.ExecuteNonQuery("update " + sql + " set search_pack_id=" + id + " where member_id=" + member_id);
                if (i == 0)
                {
                    Utility.SQLHelper.ExecuteNonQuery("insert into " + sql + "(member_id,search_pack_id) values(" + member_id + "," + id + ")");
                }
            }

        }
        else
        {
            Response.Write("loginerr");
        }
    }
    private void sumbit_search()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int id = int.Parse(Request.Form["searchId"]);
            string dx1 = Server.UrlDecode(Request.Form["dx"]);
            string wz1 = Server.UrlDecode(Request.Form["content"]);
            if (wz1.Length > 400)
                wz1 = wz1.Substring(0, 400);
            if (dx1.Length > 50)
                dx1 = dx1.Substring(0, 50);
            string tp1 = Server.UrlDecode(Request.Form["pic"]);
            DataTable dt = Utility.SQLHelper.ExecuteTable("select id from search_task_sumbit where member_id=" + member_id + " and search_task_id=" + id);
            if (dt.Rows.Count == 0)
            {
                dt = Utility.SQLHelper.ExecuteTable("select search_task_content2,search_task_dx,search_task_wz,search_task_tp,money,success,search_pack_name from search_task a,search_pack b where a.search_task_id=" + id + " and b.search_pack_id=a.search_pack_type2_id");
                if (dt.Rows.Count > 0)
                {
                    //探索表：search_task_sumbit,sumbit_dx sumbit_wz,sumbit_tp
                    string dx = dt.Rows[0][1].ToString();
                    string wz = dt.Rows[0][2].ToString();
                    string tp = dt.Rows[0][3].ToString();
                    string msg = "<p>勇士，你完成了挑战，审核通过后，可以获得背包物品：" + dt.Rows[0]["search_pack_name"] + "，" + dt.Rows[0]["money"] + "种子币，" + dt.Rows[0]["success"] + "成就点。</p>";
                    dt = Utility.SQLHelper.ExecuteTable("insert into search_task_sumbit(member_id,search_task_id) values(" + member_id + "," + id + ");SELECT @@IDENTITY");
                    int id2 = int.Parse(dt.Rows[0][0].ToString());
                    if (dx != "")
                    {
                        SqlParameter[] sp = { new SqlParameter("@sumbit_id", id2), new SqlParameter("@content", dx1) };
                        Utility.SQLHelper.ExecuteNonQuery("insert into sumbit_dx(sumbit_id,content) values(@sumbit_id,@content)", sp);
                    }
                    if (wz == "1")
                    {
                        SqlParameter[] sp2 = { new SqlParameter("@sumbit_id", id2), new SqlParameter("@content", wz1) };
                        Utility.SQLHelper.ExecuteNonQuery("insert into sumbit_wz(sumbit_id,content) values(@sumbit_id,@content)", sp2);
                    }
                    if (tp == "1")
                    {
                        string[] picList = Request.Form["pic"].Split('?');
                        string newPath = Server.MapPath("~") + "/upPic/search/search_sumbit/";
                        string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                        Random ran = new Random();
                        if (!Directory.Exists(newPath + mulu))
                        {
                            Directory.CreateDirectory(newPath + mulu);
                        }
                        foreach (string s3 in picList)
                        {
                            if (s3 != "")
                            {
                                string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                                SqlParameter[] sp3 = { new SqlParameter("@sumbit_id", id2), new SqlParameter("@sumbit_img", "upPic/search/search_sumbit/" + fileName) };
                                Utility.SQLHelper.ExecuteNonQuery("insert into sumbit_tp(sumbit_id,sumbit_img) values(@sumbit_id,@sumbit_img)", sp3);
                                string oldPath = Server.MapPath("~") + "/" + s3;
                                if (File.Exists(oldPath))
                                {
                                    File.Move(oldPath, newPath + fileName);
                                }
                            }
                        }
                    }
                    Response.Write(msg);
                }
            }
        }
        else
        {
            Response.Write("loginerr");
        }
    }
    private void delquestion()
    {
        if (login_member.checkMemerLogin())
        {
            int id = int.Parse(Request.Form["id"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int i = Utility.SQLHelper.ExecuteNonQuery("delete from lesson_question where lesson_question_id=" + id + " and member_id=" + member_id);
            if (i > 0)
            {
                Utility.SQLHelper.ExecuteNonQuery("delete from lesson_answer where lesson_question_id=" + id);
            }
            Response.Write("ok");
        }
        else Response.Write("login");

    }
    private void delanswer()
    {
        if (login_member.checkMemerLogin())
        {
            int id = int.Parse(Request.Form["id"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            Utility.SQLHelper.ExecuteNonQuery("delete from lesson_answer where lesson_answer_id=" + id + " and member_id=" + member_id);
            Response.Write("ok");
        }
        else Response.Write("login");
    }
    private void userList()
    {
        int id = int.Parse(Request.Form["id"]);
        int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
        Utility.SQLHelper.ExecuteNonQuery("update online set online_time='" + DateTime.Now.ToString() + "' where charoom_id=" + id + " and member_id=" + member_id);
        object count = Utility.SQLHelper.ExecuteScalar("select count(online_id) from online where charoom_id=" + id + " and online_time>='" + DateTime.Now.AddMinutes(-15) + "'");
        int online = 0;
        if (count != null)
        {
            online = Convert.ToInt32(count);
        }
        DataTable dt = Utility.SQLHelper.ExecuteTable("select b.member_id,b.member_name,member_pic from online a,member b ,member_info c where charoom_id=" + id + " and a.member_id=b.member_id and a.member_id=c.member_id and a.online_time>='" + DateTime.Now.AddMinutes(-15) + "'");
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        int isuser = 0;
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["member_id"].ToString() == member_id.ToString())
                isuser = 1;//该用户为当前的用户
            sb.Append("{\"id\":" + dr["member_id"] + ",\"img\":\"" + dr["member_pic"] + "\",\"name\":\"" + dr["member_name"] + "\",\"online\":" + online + ",\"isuser\":" + isuser + "},");
        }
        if (dt.Rows.Count > 0)
            sb.Remove(sb.Length - 1, 1);
        sb.Append("]");
        Response.Write(sb.ToString());
    }
    private void proList()
    {
        List<UedGoods> lu = re.getGoods();
        int count = int.Parse(Request.Form["p"]);
        string st = "";
        int count2 = count + 8;
        if (count2 > lu.Count) count2 = lu.Count;
        for (int i = count; i < count2; i++)
        {
            //st += "";
            st += "<a class=\"media\" href=\"product_detail2.aspx?id=" + lu[i].id + "\"><div class=\"media-left media-middle\"><div class=\"goods-pic\" style=\"background-image:url(" + lu[i].imgurl + ")\"><span>实</span></div></div><div class=\"media-body\"><h5>" + lu[i].title + "</h5><span>" + lu[i].price + "种子币</span></div></a>";
        }
        Response.Write(st);
    }
    private void proList2()
    {
        int count = int.Parse(Request.Form["p"]);
        DataTable dt = Utility.SQLHelper.ExecuteTable("select top 8 product_id,product_name,product_pic,product_type,product_money from product where product_tj=1 and product_id not in (select top " + count + " product_id from product where product_tj=1 order by product_id desc)  order by product_id desc");
        string st = "";
        foreach (DataRow dr in dt.Rows)
        {
            st += "<a href=\"product_detail2.aspx?id=" + dr["product_id"] + "\"><li><div class=\"b_pic\"><div class=\"b_dote\" style=\"background-image:url(" + dr["product_pic"] + ")\"></div><div class=\"b_node\">实</div></div><div class=\"b_itx\"><p>" + dr["product_name"] + "</p><span>" + dr["product_money"] + "种子币</span></div></li></a>";
        }
        Response.Write(st);
    }
    private void upUser()
    {
        if (login_member.checkMemerLogin())
        {
            if (!string.IsNullOrEmpty(Request.Form["tel"]) && !string.IsNullOrEmpty(Request.Form["email"]) && !string.IsNullOrEmpty(Request.Form["sex"]))
            {
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                string tel = Request.Form["tel"];
                string email = Request.Form["email"];
                string sex = Request.Form["sex"];
                member mm = re.getMember(member_id);
                if (mm != null)
                {
                    zcUser zc = re.getZCUser(mm.openid);
                    if (zc != null)
                    {
                        string code = re.postData("http://mp.qifanhui.cn/Api/Map/prove.html", "openid=" + mm.openid + "&sex=" + sex + "&nickname=" + mm.nickName + "&school=" + zc.school + "&stuno=" + zc.stuno + "&name=" + zc.name + "&contacts=" + tel + "&email=" + email);
                        prove pr = LitJson.JsonMapper.ToObject<prove>(code);
                        if (pr.code == 0)
                        {
                            SqlParameter[] sp = { new SqlParameter("@member_mobile", tel), new SqlParameter("@member_email", email), new SqlParameter("@member_id", member_id) };
                            Utility.SQLHelper.ExecuteNonQuery("update member set member_mobile=@member_mobile,member_email=@member_email,member_upDate='" + DateTime.Now.ToString() + "' where member_id=@member_id", sp);
                            Response.Write("ok");
                        }
                    }
                }
            }
        }
        else
        {
            Response.Write("login");
        }

    }
    private void getbadgetype2()
    {
        int id = int.Parse(Request.Form["id"]);
        DataTable dt = Utility.SQLHelper.ExecuteTable("select badge_type_id,badge_type_name from badge_type where badge_type_fid=" + id);
        string s = "<option value='0'></option>";
        foreach (DataRow dr in dt.Rows)
        {
            s += "<option value=" + dr[0] + ">" + dr[1] + "</option>";
        }
        Response.Write(s);
    }

    //杂活
    private void lectureList()
    {
        if (login_member.checkMemerLogin())
        {
            member mm = re.getMember(Request.Cookies["mLoginCookies"].Values[0]);
            List<RegistRecord> lr = re.getRegistRecord(mm.openid);
            int t = int.Parse(Request.Form["type"]);
            object objList = HiFi.Common.CacheClass.GetCache("lecture" + t);
            List<lecture> lls = null;
            if (objList == null)
            {
                if (t == 1)
                    lls = getLecture1();
                else lls = getLecture2();
            }
            else lls = (List<lecture>)objList;
            StringBuilder sb = new StringBuilder();
            string state = null;
            int count = int.Parse(Request.Form["p"]);
            int count2 = count + 6;
            if (count2 > lls.Count) count2 = lls.Count;
            for (int i = count; i < count2; i++)
            {
                if (lls[i].type == t.ToString())
                {
                    //<a href="act-2.html"><li><p style="background-image:url(images/e_p1.jpg)"></p><div class="e_itex"><h3>创意创业 - T街创意集市（105）2016年4月2日~3日</h3><b>2016.04.02</b><b>香山东街华侨城文化创意园北区</b><span>已结束</span></div></li></a>
                    sb.Append("<a href='Lecture_detail.aspx?id=" + lls[i].id + "&t=" + lls[i].type + "'><li><p style='background-image:url(" + lls[i].img + ")'></p><div class=\"e_itex\"><h3>" + lls[i].title + "</h3><b>" + lls[i].start_time + "</b><b>" + lls[i].address + "</b>");
                    state = "<span>未结束</span>";
                    if (Convert.ToDateTime(lls[i].end_time) < DateTime.Now)
                    {
                        state = "<span>已结束</span>";
                    }
                    sb.Append(state + "</div>");
                    if (lr != null)
                    {
                        foreach (RegistRecord rr in lr)
                        {
                            if (rr.lid == lls[i].id)
                            {
                                sb.Append("<div class=\"e_regist\"></div>");
                                break;
                            }
                        }
                    }

                    sb.Append("</li></a>");
                }
            }

            Response.Write(sb.ToString());
        }

    }

    private string getLectureDate(string d1, string d2)
    {
        string result = "";
        DateTime dt1 = Convert.ToDateTime(d1);
        DateTime dt2 = Convert.ToDateTime(d2);
        if (dt1.Month == dt2.Month)
        {
            result = dt1.Year + "年" + dt1.Month + "月" + dt1.Day + "日~" + dt2.Day + "日";
        }
        else result = dt1.Year + "年" + dt1.Month + "月" + dt1.Day + "日~" + dt2.Month + "月" + dt2.Day + "日";
        return result;
    }

    //杂活
    private void lectureList1()
    {
        object objList = HiFi.Common.CacheClass.GetCache("lecture1");
        List<lecture> lls = null;
        if (objList == null)
        {
            lls = getLecture1();
        }
        else lls = (List<lecture>)objList;
        StringBuilder sb = new StringBuilder();
        int i = 0;
        string state = null;
        foreach (lecture ll in lls)
        {
            if (ll.type == "1")
            {
                //<a href="act-2.html"><li>                    	<p style="background-image:url(images/e_p1.jpg)"></p><h3>炫酷的深职瑜伽早课 & 航拍召集小伙伴啦! </h3>                        <b>2014-04-02</b><span>已结束</span>                    </li></a>
                sb.Append("<a href='Lecture_detail.aspx?id=" + ll.id + "&t=" + ll.type + "'><li><p style='background-image:url(" + ll.img + ")'></p><h3>" + ll.title + "</h3><b>" + Convert.ToDateTime(ll.start_time).ToShortDateString() + "</b>");
                state = "<span>未结束</span>";
                if (Convert.ToDateTime(ll.end_time) < DateTime.Now)
                {
                    state = "<span>已结束</span>";
                }
                sb.Append(state + "</li></a>");
                i++;
                if (i == 3)
                    break;
            }
        }
        Response.Write(sb.ToString());
        //<a><li><p><img src=\"images/e_p1.jpg\" /></p><h3>杂草创意集市深职院分会....</h3><span>2014-04-02</span></li></a>
    }
    //杂活
    private void lectureList2()
    {
        object objList = HiFi.Common.CacheClass.GetCache("lecture2");
        List<lecture> lls = null;
        if (objList == null)
        {
            lls = getLecture2();
        }
        else lls = (List<lecture>)objList;
        StringBuilder sb = new StringBuilder();
        int i = 0;
        string state = null;
        foreach (lecture ll in lls)
        {
            if (ll.type == "2")
            {
                sb.Append("<a href='Lecture_detail.aspx?id=" + ll.id + "&t=" + ll.type + "'><li><p style='background-image:url(" + ll.img + ")'></p><h3>" + ll.title + "</h3><b>" + Convert.ToDateTime(ll.start_time).ToShortDateString() + "</b>");
                state = "<span>未结束</span>";
                if (Convert.ToDateTime(ll.end_time) < DateTime.Now)
                {
                    state = "<span>已结束</span>";
                }
                sb.Append(state + "</li></a>");
                i++;
                if (i == 3)
                    break;
            }
        }
        Response.Write(sb.ToString());
        //<a><li><p><img src=\"images/e_p1.jpg\" /></p><h3>杂草创意集市深职院分会....</h3><span>2014-04-02</span></li></a>
    }
    //杂活
    private List<lecture> getLecture1()
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd HH").Replace("-", "").Replace(" ", "") + ".html";
        string text = re.Get_Http("http://mp.qifanhui.cn/Api/Map/LectureList/code/map" + date, 20000);
        int start = text.IndexOf("[");
        int end = text.LastIndexOf("]");
        text = text.Substring(start, end - start + 1);
        List<lecture> ll = JsonMapper.ToObject<List<lecture>>(text);
        List<lecture> l2 = new List<lecture>();
        foreach (lecture le in ll)
        {
            if (le.type == "1")
            {
                l2.Add(le);
            }
        }
        HiFi.Common.CacheClass.SetCache("lecture1", l2, DateTime.Now.AddDays(1), TimeSpan.Zero);
        return ll;
    }
    //杂活
    private List<lecture> getLecture2()
    {
        string date = DateTime.Now.ToString("yyyy-MM-dd HH").Replace("-", "").Replace(" ", "") + ".html";
        string text = re.Get_Http("http://mp.qifanhui.cn/Api/Map/LectureList/code/map" + date, 20000);
        int start = text.IndexOf("[");
        int end = text.LastIndexOf("]");
        text = text.Substring(start, end - start + 1);
        List<lecture> ll = JsonMapper.ToObject<List<lecture>>(text);
        List<lecture> l2 = new List<lecture>();
        foreach (lecture le in ll)
        {
            if (le.type == "2")
            {
                l2.Add(le);
            }
        }
        HiFi.Common.CacheClass.SetCache("lecture2", l2, DateTime.Now.AddDays(1), TimeSpan.Zero);
        return ll;
    }

    private void task1pulgs()
    {
        if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["taskId"]))
        {
            int id = int.Parse(Request.Form["taskId"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            if (!string.IsNullOrEmpty(Request.Form["pic"]))
            {
                string[] picList = Request.Form["pic"].Split('?');
                string newPath = Server.MapPath("~") + "/upPic/member/" + member_id + "/";
                string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                Random ran = new Random();
                if (!Directory.Exists(newPath + mulu))
                {
                    Directory.CreateDirectory(newPath + mulu);
                }
                SqlParameter[] sp = { new SqlParameter("task_id", id), new SqlParameter("member_id", member_id) };
                int tid = 0;
                if (picList.Length > 0)
                {
                    DataTable dt = Utility.SQLHelper.ExecuteTable("insert into task_submit(task_id,member_id) values(@task_id,@member_id);SELECT @@IDENTITY", sp);
                    if (dt.Rows.Count > 0)
                        tid = int.Parse(dt.Rows[0][0].ToString());
                }
                if (tid != 0)
                {
                    foreach (string s3 in picList)
                    {
                        if (s3 != "")
                        {
                            string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                            SqlParameter[] sp2 = { new SqlParameter("@task_submit_id", tid), new SqlParameter("@task1_pulgs_file", "/upPic/member/" + member_id + "/" + fileName) };
                            Utility.SQLHelper.ExecuteNonQuery("insert into task1_pulgs(task_submit_id,task1_pulgs_file) values(@task_submit_id,@task1_pulgs_file)", sp2);
                            string oldPath = Server.MapPath("~") + "/" + s3;
                            if (File.Exists(oldPath))
                            {
                                File.Move(oldPath, newPath + fileName);
                            }
                        }
                    }
                    Utility.SQLHelper.ExecuteNonQuery("update task_join set task_state=1 where task_id=@task_id and member_id=@member_id", sp);
                    Response.Write("ok");
                }

            }
        }
    }
    private void memberPic()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            if (!string.IsNullOrEmpty(Request.Form["pic"]))
            {
                string[] picList = Request.Form["pic"].Split('?');
                string newPath = Server.MapPath("~") + "/upPic/member/" + member_id + "/";
                string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                Random ran = new Random();
                if (!Directory.Exists(newPath + mulu))
                {
                    Directory.CreateDirectory(newPath + mulu);
                }
                foreach (string s3 in picList)
                {
                    if (s3 != "")
                    {
                        string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                        SqlParameter[] sp = { new SqlParameter("@member_pic", "/upPic/member/" + member_id + "/" + fileName), new SqlParameter("@member_id", member_id) };
                        Utility.SQLHelper.ExecuteNonQuery("update member_info set member_pic=@member_pic where member_id=@member_id", sp);
                        string oldPath = Server.MapPath("~") + "/" + s3;
                        if (File.Exists(oldPath))
                        {
                            File.Move(oldPath, newPath + fileName);
                        }
                        break;
                    }
                }
                Response.Write("ok");
            }
        }
        else Response.Write("login err");
    }
    private void pjck()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["content"]);
            if (content.Length > 1000)
                content = content.Substring(0, 1000);
            int hp = int.Parse(Request.Form["hp"]);
            int cp = int.Parse(Request.Form["cp"]);
            int kc = int.Parse(Request.Form["kc"]);
            SqlParameter[] sp = { new SqlParameter("@kc_id", kc), new SqlParameter("@member_id", member_id), new SqlParameter("@pj", content), new SqlParameter("@score", hp) };
            DataTable dt = Utility.SQLHelper.ExecuteTable("select kc_id from kcpj where member_id=@member_id and kc_id=@kc_id", sp);
            if (dt.Rows.Count == 0)
            {
                if (hp > 0) hp = 1;
                else hp = 0;
                content = re.getLength(content, 450);
                Utility.SQLHelper.ExecuteNonQuery("insert into kcpj(kc_id,member_id,pj,score) values(@kc_id,@member_id,@pj,@score)", sp);
                Response.Write("ok");
            }
            else Response.Write("has");

        }
        else Response.Write("login err");
    }
    private void class_list()
    {
        int zhou = 0;
        if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["zhou"]))
        {
            zhou = int.Parse(Request.Form["zhou"]);
        }
        string xq = null;
        if (DateTime.Now.Month < 7)
        {
            xq = (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year + "学年第二学期";
        }
        else
            xq = (DateTime.Now.Year) + "-" + (DateTime.Now.Year + 1) + "学年第一学期";
        int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
        DataTable dt = Utility.SQLHelper.ExecuteTable("select a.classes_id,classes_start from classes a,member_info b where a.classes_name=b.member_classname and a.classes_xq='" + xq + "' and b.member_id=" + member_id);
        if (dt.Rows.Count > 0)
        {
            //有数据
            int classes_id = int.Parse(dt.Rows[0][0].ToString());
            DateTime start = Convert.ToDateTime(dt.Rows[0][1].ToString());
            //获取开学时间，判断需要对应的时间
            string s = start.DayOfWeek.ToString();
            switch (s)
            {
                case "Monday":
                    start = start.AddDays(6);
                    break;
                case "Tuesday":
                    start = start.AddDays(5);
                    break;
                case "Wednesday":
                    start = start.AddDays(4);
                    break;
                case "Thursday":
                    start = start.AddDays(3);
                    break;
                case "Friday":
                    start = start.AddDays(2);
                    break;
                case "Saturday":
                    start = start.AddDays(1);
                    break;
                case "Sunday":
                    start = start.AddDays(0);
                    break;
                default: break;
            }
            start = start.AddDays(zhou * 7);

        }
    }
    //课程表
    private void lessonInit2()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);

            //Utility.SQLHelper.ExecuteNonQuery("update member_info set member_className='14040001' where member_id=" + member_id);
            //Response.Write("ok");
            //return;
            string num = Request.Form["num"];

            //string content = re.postData("http://mp.qifanhui.cn/Api/Map/SearchClass.html", "stuno="+num);
            //if (content.IndexOf("msg") == -1)
            //{
            //    content = re.postData("http://mp.qifanhui.cn/Api/Map/course.html", "clnum");
            //}
            string content = re.postData("http://mp.qifanhui.cn/Api/Map/course.html", "stuno=" + num);
            JsonData js = JsonMapper.ToObject(content);
            if (js.Count > 0)
            {
                DataTable classes = Utility.SQLHelper.ExecuteTable("select classes_id,classes_name from classes where classes_name='" + js[0]["class"].ToString() + "' and classes_xq='" + js[0]["year"].ToString() + "-" + js[0]["term"].ToString() + "'");
                if (classes.Rows.Count > 0)
                {
                    //已经存在对应的课程表
                    //记录学员对应的班级跟学号，同时获取课程表，同时跳转到当前日期的课程
                    //返回当前班级学期ID+开学时间
                    int sqli = Utility.SQLHelper.ExecuteNonQuery("update member_info set member_className='" + js[0]["class"].ToString() + "',member_num='" + num + "' where member_id=" + member_id);
                    //Response.Write(classes.Rows[0][0].ToString() + ";" + dt.ToShortDateString());
                    if (sqli == 0)
                        Utility.SQLHelper.ExecuteNonQuery("insert into member_info(member_id,member_className,member_num) values(" + member_id + ",'" + js[0]["class"].ToString() + "','" + num + "')");
                    Response.Write("ok");
                }
                else
                {
                    //导入班级学期表
                   //classes = Utility.SQLHelper.ExecuteTable("insert into classes(classes_name,classes_xq,classes_start,classes_zhou) values('" + js[0]["class"].ToString() + "','" + js[0]["year"].ToString() + "-" + js[0]["term"].ToString() + "','2016-02-25 0:00:00',1);SELECT @@IDENTITY");

                    //导入班级学期表
                    classes = Utility.SQLHelper.ExecuteTable("insert into classes(classes_name,classes_xq,classes_start,classes_zhou,year,term) values('" + js[0]["class"].ToString() + "','" + js[0]["year"].ToString() + "-" + js[0]["term"].ToString() + "','2016-02-25 0:00:00',1," + js[0]["year"].ToString() + "," + js[0]["term"].ToString() + ");SELECT @@IDENTITY");
                    int classes_id = int.Parse(classes.Rows[0][0].ToString());
                    int jieshu = 0;
                    for (int i = 0; i < js.Count; i++)
                    {
                        //weekly
                        string[] allzhou = js[i]["weekly"].ToString().Replace("第", "").Replace("周", "").Split('，');
                        foreach (string az in allzhou)
                        {
                            string[] zhou = az.Split('-');//第2周
                            string[] jie = js[i]["section"].ToString().Replace("第", "").Replace("节", "").Split(',');
                            int z1 = int.Parse(zhou[0]);
                            int zz1 = z1 + 1;
                            if (zhou.Length > 1)
                                zz1 = int.Parse(zhou[1]) + 1;
                            for (; z1 < zz1; z1++)
                            {
                                foreach (string z2 in jie)
                                {
                                    string ss = js[i]["assist"].ToString();
                                    if (z2 == "7+")
                                        jieshu = 8;
                                    else jieshu = int.Parse(z2);
                                    //周，节，课程内容 上课地点 主讲教师 辅讲教师 备注
                                    SqlParameter[] sp2 = { new SqlParameter("@classes_id", classes_id), new SqlParameter("@zhou", z1), new SqlParameter("@jie", jieshu), new SqlParameter("@kecheng", js[i]["course"].ToString()), new SqlParameter("@didian", js[i]["address"].ToString()), new SqlParameter("@t1", js[i]["teacher"].ToString()), new SqlParameter("@t2", js[i]["assist"].ToString()), new SqlParameter("@xq", js[i]["week"].ToString()), new SqlParameter("@beizhu", js[i]["remark"].ToString()) };
                                    Utility.SQLHelper.ExecuteNonQuery("insert into kc(classes_id,zhou,jie,kecheng,didian,t1,t2,xq,beizhu) values(@classes_id,@zhou,@jie,@kecheng,@didian,@t1,@t2,@xq,@beizhu)", sp2);
                                }
                            }
                        }
                        Response.Write("ok");
                    }
                }

            }

        }
        else Response.Write("login");
    }



    private void lessonInit()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string num = Request.Form["num"];
            WebClientto webClient = new WebClientto(3000);
            byte[] responseData = webClient.DownloadData("http://sic.szpt.edu.cn/timetable/kbcx.php");
            if (responseData == null || responseData.Length == 0)
            {
                Response.Write("net err");
                return;
            }
            string srcString = Encoding.UTF8.GetString(responseData);

            webClient = new WebClientto(3000);
            webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            string viewStateFlag = "id=\"__VIEWSTATE\" value=\"";
            int i = srcString.IndexOf(viewStateFlag) + viewStateFlag.Length;
            int j = srcString.IndexOf("\"", i);
            string viewState = srcString.Substring(i, j - i);

            string eventValidationFlag = "id=\"__EVENTVALIDATION\" value=\"";
            i = srcString.IndexOf(eventValidationFlag) + eventValidationFlag.Length;
            j = srcString.IndexOf("\"", i);
            string eventValidation = srcString.Substring(i, j - i);

            string tor = "id=\"__VIEWSTATEGENERATOR\" value=\"";
            i = srcString.IndexOf(tor) + tor.Length;
            j = srcString.IndexOf("\"", i);
            string VIEWSTATEGENERATOR = srcString.Substring(i, j - i);

            viewState = System.Web.HttpUtility.UrlEncode(viewState);
            eventValidation = System.Web.HttpUtility.UrlEncode(eventValidation);
            VIEWSTATEGENERATOR = System.Web.HttpUtility.UrlEncode(VIEWSTATEGENERATOR);
            string submitButton = System.Web.HttpUtility.UrlEncode("按学号查询");

            string postString = "txtStudntID=" + num + "&btnStuNo=" + submitButton + "&__VIEWSTATE=" + viewState + "&__EVENTVALIDATION=" + eventValidation + "&__VIEWSTATEGENERATOR=" + VIEWSTATEGENERATOR + "&__VIEWSTATEENCRYPTED=&ddlAcademy=&ddlW1_24=0&ddlClass=&ddlTeacher=&txtTeacher_No=&txtTeacher_Name=";

            byte[] postData = Encoding.ASCII.GetBytes(postString);

            responseData = webClient.UploadData("http://sic.szpt.edu.cn/timetable/kbcx.php", "POST", postData);

            srcString = Encoding.UTF8.GetString(responseData);


            string content = srcString;
            string html = srcString;


            int start = content.IndexOf("今天是");
            content = content.Substring(start + 3, 50);
            int end = content.IndexOf("</font>");
            content = content.Substring(0, end);//2016-04-29　第9周 星期五
            content = content.Replace("　", " ").Replace("第", "").Replace("周", "");
            string[] list = content.Split(' ');
            int s = int.Parse(list[1]);
            DateTime dt = DateTime.Now.AddDays(-(s * 7));//第一周的时间，即班级开学时间

            HtmlAgilityPack.HtmlDocument htc = new HtmlDocument();
            htc.LoadHtml(html);
            HtmlAgilityPack.HtmlNode ct = htc.DocumentNode.SelectSingleNode("//*[@id='ScheduleTitle']");
            //Response.Write(ct.InnerText);//2015-2016学年第二学期班级课表(15包装1)
            string xq = ct.InnerText;
            if (xq.IndexOf("无此学号") != -1)
            {
                Response.Write("num err");
            }
            else
            {
                string[] list2 = xq.Split('表');
                string bj = list2[1].Replace("(", "").Replace(")", "");//班级
                xq = list2[0].Substring(0, list2[0].Length - 3);//学期
                HtmlAgilityPack.HtmlNodeCollection hn = htc.DocumentNode.SelectNodes("//table[@id='gvSchedule']/tr");
                if (hn != null)
                {
                    if (hn.Count > 1)
                    {
                        HtmlAgilityPack.HtmlNodeCollection hn22 = hn[1].ChildNodes;
                        bj = hn22[2].InnerText;
                    }
                    SqlParameter[] sp = { new SqlParameter("@classes_name", bj), new SqlParameter("@classes_xq", xq), new SqlParameter("@classes_start", list[0]), new SqlParameter("@classes_zhou", list[1]) };
                    DataTable classes = Utility.SQLHelper.ExecuteTable("select classes_id,classes_name from classes where classes_name=@classes_name and classes_xq=@classes_xq", sp);
                    if (classes.Rows.Count > 0)
                    {
                        //已经存在对应的课程表
                        //记录学员对应的班级跟学号，同时获取课程表，同时跳转到当前日期的课程
                        //返回当前班级学期ID+开学时间
                        int sqli = Utility.SQLHelper.ExecuteNonQuery("update member_info set member_className='" + classes.Rows[0][1] + "',member_num='" + num + "' where member_id=" + member_id);
                        //Response.Write(classes.Rows[0][0].ToString() + ";" + dt.ToShortDateString());
                        if (sqli == 0)
                            Utility.SQLHelper.ExecuteNonQuery("insert into member_info(member_id,member_className,member_num) values(" + member_id + ",'" + classes.Rows[0][1] + "','" + num + "')");
                        Response.Write("ok");
                    }
                    else
                    {
                        //创建班级学期表
                        classes = Utility.SQLHelper.ExecuteTable("insert into classes(classes_name,classes_xq,classes_start,classes_zhou) values(@classes_name,@classes_xq,@classes_start,@classes_zhou);SELECT @@IDENTITY", sp);
                        int classes_id = int.Parse(classes.Rows[0][0].ToString());
                        ArrayList al = new ArrayList();
                        //int laseweek = 1;
                        bool has = false;
                        for (int ii = 1; ii < hn.Count; ii++)
                        {
                            HtmlAgilityPack.HtmlNodeCollection hn2 = hn[ii].ChildNodes;
                            string kc = hn2[3].InnerText;
                            if (hn2[1].InnerText.Trim() != "" && hn2[1].InnerText.Trim() != "&nbsp;")
                            {
                                //整周或者单元 gvScheduleAllWeek
                                //范围是当前课程的名称，到下一个课程名称之前
                                HtmlAgilityPack.HtmlNodeCollection allweek = htc.DocumentNode.SelectNodes("//table[@id='gvScheduleAllWeek']/tr");
                                for (int a1 = 1; a1 < allweek.Count; a1++)
                                {
                                    HtmlAgilityPack.HtmlNodeCollection allweek2 = allweek[a1].ChildNodes;
                                    if (allweek2[3].InnerText == kc)
                                    {
                                        //课程名称不为空，代表不是课程明细，表示开始读取;
                                        has = true;
                                        continue;
                                    }
                                    else if (allweek2[3].InnerText != "" && allweek2[3].InnerText != "&nbsp;")
                                    {
                                        has = false;
                                        continue;
                                        //break;
                                        //代表已经读取到下个课程明细的开始，停止读取;
                                    }
                                    if (has)
                                    {
                                        if (allweek2[6].InnerText != "" && allweek2[6].InnerText != "&nbsp;")
                                        {
                                            if (!al.Contains(a1))
                                            {
                                                al.Add(a1);//记录保存的行
                                                string[] allzhou = allweek2[6].InnerText.Replace("第", "").Replace("周", "").Split('，');
                                                foreach (string az in allzhou)
                                                {
                                                    string[] zhou = az.Split('-');//第2周
                                                    string[] jie = allweek2[9].InnerText.Replace("第", "").Replace("节", "").Split(',');
                                                    int z1 = int.Parse(zhou[0]);
                                                    int zz1 = z1 + 1;
                                                    if (zhou.Length > 1)
                                                        zz1 = int.Parse(zhou[1]) + 1;
                                                    for (; z1 < zz1; z1++)
                                                    {
                                                        foreach (string z2 in jie)
                                                        {
                                                            //周，节，课程内容 上课地点 主讲教师 辅讲教师 备注
                                                            SqlParameter[] sp2 = { new SqlParameter("@classes_id", classes_id), new SqlParameter("@zhou", z1), new SqlParameter("@jie", z2), new SqlParameter("@kecheng", kc), new SqlParameter("@didian", allweek2[7].InnerText), new SqlParameter("@t1", allweek2[4].InnerText), new SqlParameter("@t2", allweek2[5].InnerText), new SqlParameter("@xq", allweek2[8].InnerText), new SqlParameter("@beizhu", allweek2[10].InnerText) };
                                                            Utility.SQLHelper.ExecuteNonQuery("insert into kc(classes_id,zhou,jie,kecheng,didian,t1,t2,xq,beizhu) values(@classes_id,@zhou,@jie,@kecheng,@didian,@t1,@t2,@xq,@beizhu)", sp2);
                                                        }
                                                    }
                                                }
                                            }


                                        }
                                    }
                                }
                            }
                            else
                            {
                                //非整周或者单元
                                string[] allzhou = hn2[6].InnerText.Replace("第", "").Replace("周", "").Split('，');
                                foreach (string az in allzhou)
                                {
                                    string[] zhou = az.Split('-');//第2周
                                    string[] jie = hn2[9].InnerText.Replace("第", "").Replace("节", "").Split(',');
                                    int z1 = int.Parse(zhou[0]);
                                    int zz1 = z1 + 1;
                                    if (zhou.Length > 1)
                                        zz1 = int.Parse(zhou[1]) + 1;
                                    for (; z1 < zz1; z1++)
                                    {
                                        foreach (string z2 in jie)
                                        {
                                            //周，节，课程内容 上课地点 主讲教师 辅讲教师 备注
                                            SqlParameter[] sp2 = { new SqlParameter("@classes_id", classes_id), new SqlParameter("@zhou", z1), new SqlParameter("@jie", z2), new SqlParameter("@kecheng", kc), new SqlParameter("@didian", hn2[7].InnerText), new SqlParameter("@t1", hn2[4].InnerText), new SqlParameter("@t2", hn2[5].InnerText), new SqlParameter("@xq", hn2[8].InnerText), new SqlParameter("@beizhu", hn2[10].InnerText) };
                                            Utility.SQLHelper.ExecuteNonQuery("insert into kc(classes_id,zhou,jie,kecheng,didian,t1,t2,xq,beizhu) values(@classes_id,@zhou,@jie,@kecheng,@didian,@t1,@t2,@xq,@beizhu)", sp2);
                                        }
                                    }
                                }

                            }
                        }
                        Utility.SQLHelper.ExecuteNonQuery("update  member_info set member_className='" + bj + "',member_num='" + num + "' where member_id=" + member_id);
                        //Response.Write(classes_id + ";" + dt.ToShortDateString());
                        Response.Write("ok");
                    }
                }
                else Response.Write("err");
            }
        }
    }

    private void answerAdd()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["qc"]);
            if (content.Length > 400)
                content = content.Substring(0, 400);
            int qid = int.Parse(Request.Form["id"]);
            SqlParameter[] sp = { new SqlParameter("@lesson_question_id", qid), new SqlParameter("@member_id", member_id), new SqlParameter("@lesson_answer_content", content) };
            Utility.SQLHelper.ExecuteNonQuery("insert into lesson_answer(lesson_question_id,member_id,lesson_answer_content) values(@lesson_question_id,@member_id,@lesson_answer_content)", sp);
            Response.Write("ok");
        }
        else Response.Write("login");
    }

    //提问添加
    private void questionAdd()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["qc"]);
            string title = Server.UrlDecode(Request.Form["title"]);
            if (title.Length > 40)
                title = title.Substring(0, 40);
            if (content.Length > 400)
                content = content.Substring(0, 400);
            SqlParameter[] sp = { new SqlParameter("@member_id", member_id), new SqlParameter("@lesson_question_title", title), new SqlParameter("@lesson_question_content", content) };
            Utility.SQLHelper.ExecuteNonQuery("insert into lesson_question(member_id,lesson_question_title,lesson_question_content) values(@member_id,@lesson_question_title,@lesson_question_content)", sp);
            Response.Write("ok");
        }
        else Response.Write("login");
    }

    //获取杂活信息
    private void lecturellll()
    {

        object objList = HiFi.Common.CacheClass.GetCache("lecture1");
        List<lecture> lls = null;
        if (objList == null)
        {
            lls = getLecture1();
        }
        else {
            lls = (List<lecture>)objList;
        };
        StringBuilder sb = new StringBuilder();
        foreach (lecture ll in lls)
        {
            sb.Append("#"+ll.address);//地点

            sb.Append("-" + ll.title);//主题
        }
        Response.Write(sb.ToString());
    }


    //跨域要获取到值
    public void Verdict()
    {

       int school = int.Parse(Request.QueryString["school"]);

       // string jsoncallback = GameRequest.GetQueryString("jsoncallback");

      Response.ContentEncoding = System.Text.Encoding.UTF8;

       Response.ContentType = "text/html";

     //   Response.ContentType = "application/json";

       //Response.Write();

    //   Response.End();

        if (school == 1)
        {
            //深大
            DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < datatype.Rows.Count; i++)
            {
                DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=1 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

                sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

                sb.Append("\",");

                sb.Append("\"building\":[");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
                }

                if (dt.Rows.Count > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Remove(sb.Length - 0, 0);
                sb.Append("]}");
            }
            if (datatype.Rows.Count > 0)
            {
                sb.Remove(1, 1);
            }
            sb.Append("]");
            Response.Write(sb.ToString());
        }
        else if (school == 2)
        {
            //深信
            DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < datatype.Rows.Count; i++)
            {
                DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=2 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

                sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

                sb.Append("\",");

                sb.Append("\"building\":[");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
                }

                if (dt.Rows.Count > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Remove(sb.Length - 0, 0);
                sb.Append("]}");
            }
            if (datatype.Rows.Count > 0)
            {
                sb.Remove(1, 1);
            }
            sb.Append("]");
            Response.Write(sb.ToString());
        }
        else if (school == 3)
        {

            //深职
            DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < datatype.Rows.Count; i++)
            {
                DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=3 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

                sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

                sb.Append("\",");

                sb.Append("\"building\":[");

                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
                }

                if (dt.Rows.Count > 0)
                    sb.Remove(sb.Length - 1, 1);
                sb.Remove(sb.Length - 0, 0);
                sb.Append("]}");
            }
            if (datatype.Rows.Count > 0)
            {
                sb.Remove(1, 1);
            }
            sb.Append("]");
            Response.Write(sb.ToString());    
        }
    }

    //深职 类型、学校、建筑
    private void Trigeminy3()
    {
        DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < datatype.Rows.Count; i++)
        {
            DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=3 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

            sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

            sb.Append("\",");

            sb.Append("\"building\":[");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
            }

            if (dt.Rows.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Remove(sb.Length - 0, 0);
            sb.Append("]}");
        }
        if (datatype.Rows.Count > 0)
        {
            sb.Remove(1, 1);
        }
        sb.Append("]");
        Response.Write(sb.ToString());
    }

    //深大 类型、学校、建筑
    private void Trigeminy1()
    {
        DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < datatype.Rows.Count; i++)
        {
            DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=1 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

            sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

            sb.Append("\",");

            sb.Append("\"building\":[");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
            }

            if (dt.Rows.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Remove(sb.Length - 0, 0);
            sb.Append("]}");
        }
        if (datatype.Rows.Count > 0)
        {
            sb.Remove(1, 1);
        }
        sb.Append("]");
        Response.Write(sb.ToString());
    }

    //深信 类型、学校、建筑
    private void Trigeminy2()
    {

        DataTable datatype = Utility.SQLHelper.ExecuteTable("select building_type_name from building_type");//查询类型值 //类型值有 11个
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < datatype.Rows.Count; i++)
        {
            DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building_type byp,building b where byp.building_type_id=b.building_type_id and b.area_id=2 and building_type_name='" + datatype.Rows[i]["building_type_name"] + "'");//获取头部对应的信息name

            sb.Append(",{\"type\":\"" + datatype.Rows[i]["building_type_name"] + "");//获取头部type

            sb.Append("\",");

            sb.Append("\"building\":[");

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{\"name\":\"" + dr["building_name"] + "\"},");
            }

            if (dt.Rows.Count > 0)
                sb.Remove(sb.Length - 1, 1);
            sb.Remove(sb.Length - 0, 0);
            sb.Append("]}");
        }
        if (datatype.Rows.Count > 0)
        {
            sb.Remove(1, 1);
        }
        sb.Append("]");
        Response.Write(sb.ToString());
    }
 
    //表白提交
    private void confessionAdd()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string names = Server.UrlDecode(Request.Form["Addname"]);//表白对象
            string matters = Server.UrlDecode(Request.Form["Addmatter"]);//表白内容
            string floors = Server.UrlDecode(Request.Form["Addfloor"]);//表白楼层
            string signs = Server.UrlDecode(Request.Form["Addsign"]);//表白人
           
            if (matters.Length > 80)
                matters = matters.Substring(0, 80);
            SqlParameter[] sp = { new SqlParameter("@member_id", member_id), new SqlParameter("@Name", names), new SqlParameter("@Matter", matters), new SqlParameter("@Floor", floors), new SqlParameter("@Sign", signs) };
            Utility.SQLHelper.ExecuteNonQuery("insert into confession_add(Name,Matter,Floor,Sign,member_id) values(@Name,@Matter,@Floor,@Sign,@member_id)", sp);
            Response.Write("ok");
        }
    }

    //表白加载更多最新
    private void jiazaigengduo()
    {
        int count = int.Parse(Request.Form["p"]);
        DataTable dt = Utility.SQLHelper.ExecuteTable("select top 3 * from confession_add where ID not in (select top " + count + " ID from confession_add order by ID desc)  order by ID desc");
        string st = "";
        foreach (DataRow dr in dt.Rows)
        {
            st += "<div   class=\"panel panel-default position-relative\"><div class=\"panel-heading clearfix\"><span class=\"loveFrom\">From:&nbsp;" + dr["Name"] + "</span><span class=\"loveTime\">" + dr["Date"] + "</span></div><div class=\"panel-body\"><h4 class=\"loveTo\">To:&nbsp;" + dr["Sign"] + "</h4><p class=\"loveContent\">" + dr["Matter"] + "</p><span class=\"loveAddress\"><i class=\"glyphicon glyphicon-map-marker margin-right-5\"></i>" + dr["Floor"] + "</span></div><span class=\"loveLittle\"></span><div class=\"lovePraiseBox\"><span class=\"lovePraise active\" onclick=\"joyns(" + dr["ID"] + "," + dr["Praise"] + ")\"></span><p class=\"lovePraiseCount\" id='joysss" + dr["ID"] + "'>" + dr["Praise"] + "</p></div></div>";
        }
        Response.Write(st);
    }

    //表白加载更多最热
    private void zuire()
    {
        int count = int.Parse(Request.Form["p"]);
        DataTable dt = Utility.SQLHelper.ExecuteTable("select top 3 * from confession_add where ID not in (select top " + count + " ID from confession_add order by Praise desc)  order by Praise desc");
        string st = "";
        foreach (DataRow dr in dt.Rows)
        {
            st += "<div class=\"panel panel-default position-relative\"><div class=\"panel-heading clearfix\"><span class=\"loveFrom\">From:&nbsp;" + dr["Name"] + "</span><span class=\"loveTime\">" + dr["Date"] + "</span></div><div class=\"panel-body\"><h4 class=\"loveTo\">To:&nbsp;" + dr["Sign"] + "</h4><p class=\"loveContent\">" + dr["Matter"] + "</p><span class=\"loveAddress\"><i class=\"glyphicon glyphicon-map-marker margin-right-5\"></i>" + dr["Floor"] + "</span></div><span class=\"loveLittle\"></span><div class=\"lovePraiseBox\"><span class=\"lovePraise active\" onclick=\"joy(" + dr["ID"] + "," + dr["Praise"] + ")\"></span><p class=\"lovePraiseCount\" id='joys"+dr["ID"]+"'>" + dr["Praise"] + "</p></div></div>";
        }
        Response.Write(st);
    }

    //点赞提交
    private void like()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);

            string id = Server.UrlDecode(Request.Form["ID"]);

            string praise = Server.UrlDecode(Request.Form["PRAISE"]);

            SqlParameter[] sp = { new SqlParameter("@member_id", member_id), new SqlParameter("@Praise", praise), new SqlParameter("@ID", id) };

            //判断是否能重复点赞
            DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from confession_add_detail  where member_id=@member_id and confession_add_id=@ID", sp);
            if (dt.Rows.Count == 0)
            {
                Utility.SQLHelper.ExecuteNonQuery("update confession_add  set Praise=@Praise+1 where ID=@ID", sp);

                Utility.SQLHelper.ExecuteNonQuery("insert confession_add_detail(confession_add_id,member_id) values(@ID,@member_id)", sp);

                Response.Write("ok");

            }
            else Response.Write("has");



        }
    }

    //删除表白
    private void confessionAdddelete() {

        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int id = int.Parse(Request.Form["id"]);
            int i = Utility.SQLHelper.ExecuteNonQuery("delete from confession_add_detail where confession_add_id=" + id + " and member_id=" + member_id);
            if (i >= 0)
            {
                Utility.SQLHelper.ExecuteNonQuery("delete from confession_add where ID=" + id + " and member_id=" + member_id);
                Response.Write("ok");
            }

        }
        else
        {
            Response.Write("login");
        }

    }

    private void questionSearch()
    {
        string content = Server.UrlDecode(Request.Form["qc"]);
        content = re.replace(content);
        SqlParameter[] sp = { new SqlParameter("@lesson_question_title", content) };
        DataTable dt = Utility.SQLHelper.ExecuteTable("select a.*,b.member_id,b.member_name from lesson_question a,member b where a.member_id=b.member_id and charindex(@lesson_question_title,lesson_question_title)>0 order by lesson_question_id desc", sp);
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append("{\"id\":" + dr["lesson_question_id"] + ",\"img\":\"" + re.getMemberPic2(dr["member_id"]) + "\",\"name\":\"" + dr["member_name"] + "\",\"date\":\"" + re.getDate(dr["lesson_question_date"]) + "\",\"title\":\"" + re.getLength(dr["lesson_question_title"].ToString(), 50) + "\",\"answer\":\"" + re.answerCount(dr["lesson_question_id"]) + "\"},");
            //sb.Append("<a href='class_answer_detail.aspx?id=" + dr["lesson_question_id"] + "'><li><div class='c_uer'><p><img src='../images/u1.jpg'/></p><em>" + dr["member_name"] + "</em><i>" + re.getDate(dr["lesson_question_date"]) + "</i></div><p class='c_an'>" + re.getLength(dr["lesson_question_content"].ToString(), 50) + "</p><div class='c_sum'><p>" + re.answerCount(dr["lesson_question_id"]) + "个回答</p><span></span></div></li></a>");
        }
        if (dt.Rows.Count > 0)
            sb.Remove(sb.Length - 1, 1);
        sb.Append("]");
        Response.Write(sb.ToString());
    }

    private void taskList()
    {
        if (login_member.checkMemerLogin())
        {
            int count = int.Parse(Request.Form["p"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            DataTable dt = Utility.SQLHelper.ExecuteTable("select top 10 task_id,task_title,task_content,task_date,task_enddate,task_reward_money from task a,task_reward b where a.task_id not in (select top " + count + " task_id from task where task_enddate>'" + DateTime.Now + "' order by task_id desc) and a.task_reward_id=b.task_reward_id and a.task_enddate>'" + DateTime.Now + "' and task_id not in(select task_id from task_join where member_id=" + member_id + " and task_state>0) order by task_id desc");
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<div class='media'><div class='media-left'><div class='task-logo' style='background-image:url(../images/t_task3.png)'></div></div><a class='media-body' href='task_detail.aspx?id=" + dr["task_id"] + "'><h5>" + dr["task_title"] + "</h5><p>" + dr["task_content"] + "</p></a><div class='media-right'><div class='reward'><i class='iconfont'>&#xe62c;</i>" + dr["task_reward_money"] + "</div>" + re.hasTask(dr["task_id"]) + "</div></div>");
                //sb.Append("<a href='task_detail.aspx?id=" + dr["task_id"] + "'><li><p class=\"t_pic\"><img src=\"../images/t_task3.png\" /></p><div class=\"t_itex\"><span>" + dr["task_title"] + "</span></div><div class=\"t_ber\"><p class=\"t_jb\"><span></span><em>" + dr["task_reward_money"] + "</em></p>" + re.hasTask(dr["task_id"]) + "</div></li></a>");
            }
            Response.Write(sb.ToString());
        }
        else Response.Write("login");
    }
    private void taskList2()
    {
        if (login_member.checkMemerLogin())
        {
            int count = int.Parse(Request.Form["p"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            DataTable dt = Utility.SQLHelper.ExecuteTable("select top 10  a.task_id,a.task_title,a.task_content,a.task_date,a.task_enddate,a.task_type,b.task_joinDate,b.task_state,c.task_reward_money from task a,task_join b,task_reward c where a.task_id not in (select top " + count + " task_id from task_join where member_id=" + member_id + " and task_state>0 order by task_id desc) and b.member_id=" + member_id + " and a.task_id=b.task_id and b.task_state>0 and a.task_reward_id=c.task_reward_id order by a.task_id desc");
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<div class='media'><div class='media-left'><div class='task-logo' style='background-image:url(../images/t_task3.png)'></div></div><a class='media-body' href='task_detail.aspx?id=" + dr["task_id"] + "'><h5>" + dr["task_title"] + "</h5><p>" + dr["task_content"] + "</p></a><div class='media-right'><div class='reward'><i class='iconfont'>&#xe62c;</i>" + dr["task_reward_money"] + "</div>" + re.hasTask(dr["task_id"]) + "</div></div>");
                //sb.Append("<a href='task_detail.aspx?id=" + dr["task_id"] + "'><li><p class=\"t_pic\"><img src=\"../images/t_task3.png\" /></p><div class=\"t_itex\"><span>" + dr["task_title"] + "</span></div><div class=\"t_ber\"><p class=\"t_jb\"><span></span><em>" + dr["task_reward_money"] + "</em></p>" + re.hasTask(dr["task_id"]) + "</div></li></a>");
            }
            Response.Write(sb.ToString());
        }
        else Response.Write("login");
    }
    private void coinList()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int count = int.Parse(Request.Form["p"]);
            member mm = re.getMember(member_id);
            List<orderCache> lo = re.getOrders(mm.openid, member_id);
            int count2 = count + 3;
            if (count2 > lo.Count) count2 = lo.Count;
            StringBuilder sb = new StringBuilder();
            for (int i = count; i < count2; i++)
            {
                sb.Append("<li><span>" + lo[i].time + "</span><p><em>" + lo[i].ptitle + "</em><i>" + (int.Parse(lo[i].num) > 0 ? "+" + lo[i].num : lo[i].num) + "</i></p></li>");
            }
            //DataTable dt = Utility.SQLHelper.ExecuteTable("select top 3 member_money_date,member_money_title,money from member_money where member_money_id not in (select top " + count + " member_money_id from member_money where member_id=" + member_id + " order by member_money_id desc) and member_id=" + member_id + " order by member_money_id desc");

            //foreach (DataRow dr in dt.Rows)
            //{
            //    sb.Append("<li><span>" + dr["member_money_date"] + "</span><p><em>" + dr["member_money_title"] + "</em><i>" + dr["money"] + "</i></p></li>");
            //}
            Response.Write(sb.ToString());
        }
        else Response.Write("login");
    }
    private void searchList()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            DataTable dt = Utility.SQLHelper.ExecuteTable("select * from search where search_id not in(select search_id from search_task a,search_task_sumbit b where a.search_task_id=b.search_task_id and b.member_id=" + member_id + " )");
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{\"id\":" + dr[0] + ",\"x\":" + dr["building_x"] + ",\"y\":" + dr["building_y"] + ",\"t\":1},");
            }
            dt = Utility.SQLHelper.ExecuteTable("select * from search where search_id in(select search_id from search_task a,search_task_sumbit b where a.search_task_id=b.search_task_id and b.member_id=" + member_id + " )");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{\"id\":" + dr[0] + ",\"x\":" + dr["building_x"] + ",\"y\":" + dr["building_y"] + ",\"t\":2},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            Response.Write(sb.ToString());
        }
        else
        {
            Response.Write("loginerr");
        }

    }
    private void szzyBuilding()
    {
        DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building where area_id=3");
        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            //     sb.Append("var style = {" +
            //     "stroke: false," +
            //     "fill: false," +
            //     "label: '" + dr[1] + "'," +
            //     "fontSize: '13px'," +
            //     "fontFamily: '微软雅黑'," +
            //     "labelXOffset: 0," +
            //     "labelYOffset: -18," +
            //     "labelOutlineColor: '#ffffff'," +
            //     "labelOutlineWidth: 4" +
            //" };");
            sb.Append("var mark = new OpenLayers.Marker(new OpenLayers.LonLat(" + dr["building_x"] + ", " + dr["building_y"] + "), icon.clone());mark.events.register('touchstart', mark, function (evt) { markerEvent(evt," + dr[0] + ",\"" + dr["web"] + "\",\"" + dr[1] + "\"," + dr["building_x"] + "," + dr["building_y"] + "); });markersLayer.addMarker(mark);");
            //sb.Append("var feature = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(" + dr["building_x"] + ", " + dr["building_y"] + "), null, style);vectorLayer.addFeatures([feature], { silent: true });");
        } Response.Write(sb.ToString());
    }
    private void szxxBuilding()
    {
        DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building where area_id=2");
        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            //     sb.Append("var style = {" +
            //     "stroke: false," +
            //     "fill: false," +
            //     "label: '" + dr[1] + "'," +
            //     "fontSize: '13px'," +
            //     "fontFamily: '微软雅黑'," +
            //     "labelXOffset: 0," +
            //     "labelYOffset: -18," +
            //     "labelOutlineColor: '#ffffff'," +
            //     "labelOutlineWidth: 4" +
            //" };");
            sb.Append("var mark = new OpenLayers.Marker(new OpenLayers.LonLat(" + dr["building_x"] + ", " + dr["building_y"] + "), icon.clone());mark.events.register('touchstart', mark, function (evt) { markerEvent(evt," + dr[0] + ",\"" + dr["web"] + "\",\"" + dr[1] + "\"," + dr["building_x"] + "," + dr["building_y"] + "); });markersLayer.addMarker(mark);");
            //sb.Append("var feature = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(" + dr["building_x"] + ", " + dr["building_y"] + "), null, style);vectorLayer.addFeatures([feature], { silent: true });");
        }
        Response.Write(sb.ToString());
    }
    private void szdxBuilding()
    {
        //DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building where area_id=1");
        //StringBuilder sb = new StringBuilder("[");
        //foreach (DataRow dr in dt.Rows)
        //{
        //    sb.Append("{\"id\":"+dr[0]+",\"title\":\"" + dr["building_name"] + "\",\"x\":" + dr["building_x"] + ",\"y\":" + dr["building_y"] + ",\"web\":\""+dr["web"]+"\"},");
        //}
        //if (sb.Length > 2)
        //    sb.Remove(sb.Length - 1, 1);
        //sb.Append("]");
        DataTable dt = Utility.SQLHelper.ExecuteTable("select * from building where area_id=1");
        StringBuilder sb = new StringBuilder();
        foreach (DataRow dr in dt.Rows)
        {
            //     sb.Append("var style = {" +
            //     "stroke: false," +
            //     "fill: false," +
            //     "label: '" + dr[1] + "'," +
            //     "fontSize: '13px'," +
            //     "fontFamily: '微软雅黑'," +
            //     "labelXOffset: 0," +
            //     "labelYOffset: -18," +
            //     "labelOutlineColor: '#ffffff'," +
            //     "labelOutlineWidth: 4" +
            //" };");
            sb.Append("var mark = new OpenLayers.Marker(new OpenLayers.LonLat(" + dr["building_x"] + ", " + dr["building_y"] + "), icon.clone());mark.events.register('touchstart', mark, function (evt) { markerEvent(evt," + dr[0] + ",\"" + dr["web"] + "\",\"" + dr[1] + "\"," + dr["building_x"] + "," + dr["building_y"] + "); });markersLayer.addMarker(mark);");
            //sb.Append("var feature = new OpenLayers.Feature.Vector(new OpenLayers.Geometry.Point(" + dr["building_x"] + ", " + dr["building_y"] + "), null, style);vectorLayer.addFeatures([feature], { silent: true });");
        }
        Response.Write(sb.ToString());
    }
    private void getOrders()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            member mm = re.getMember(member_id);
            List<orderCache> lo = re.getOrders(mm.openid, member_id);
            string orderList = "";
            foreach (orderCache o in lo)
            {
                if (o.ptype != "其他")
                    orderList += "<li><h3>" + o.time + "<i>" + o.ptype + "</i></h3><p>" + o.ptitle + "</p><span>" + o.num + "种子币</span></li>";

            }
            Response.Write(orderList);
        }
        else Response.Write("login");
    }
    private void getOrder()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int t = int.Parse(Request.Form["t"]);
            string ptype = "虚拟物品";
            if (t == 2) ptype = "实物礼品";
            member mm = re.getMember(member_id);
            List<orderCache> lo = re.getOrders(mm.openid, member_id);
            string orderList = "";
            foreach (orderCache o in lo)
            {
                if (o.ptype == ptype)
                    orderList += "<li><h3>" + o.time + "<i>" + o.ptype + "</i></h3><p>" + o.ptitle + "</p><span>" + o.num + "种子币</span></li>";

            }
            Response.Write(orderList);
        }
        else Response.Write("login");
    }
    private void socail_pwd()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string pwd = Request.Form["pwd"];
            int sid = int.Parse(Request.Form["sid"]);
            DataTable dt = Utility.SQLHelper.ExecuteTable("select chatroom_pwd from chatroom where chatroom_id=" + sid);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == pwd)
                {
                    Utility.SQLHelper.ExecuteNonQuery("insert into chatroom_pwd(chatroom_id,member_id) values(" + sid + "," + member_id + ")");
                    Response.Write("ok");
                }
                else
                {
                    Response.Write("err");
                }

            }

        }
        else Response.Write("login");
    }
    private void delPlan()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int did = int.Parse(Request.Form["id"]);
            SqlParameter[] sp = { new SqlParameter("@lesson_plan_id", did),
                                    new SqlParameter("@member_id", member_id)
                                };
            Utility.SQLHelper.ExecuteNonQuery("delete from lesson_plan where lesson_plan_id=@lesson_plan_id and member_id=@member_id", sp);
            Response.Write("ok");
        }
        else Response.Write("login");
    }
    private void addPlan()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["c"]);
            string time = Server.UrlDecode(Request.Form["time"]);
            SqlParameter[] sp = { new SqlParameter("@member_id", member_id),
                                    new SqlParameter("@lesson_plan_content", content),
                                new SqlParameter("@lesson_plan_alertDate", time),
                                new SqlParameter("@buildding_id", 1)};
            Utility.SQLHelper.ExecuteNonQuery("insert into lesson_plan(member_id,lesson_plan_content,lesson_plan_alertDate,buildding_id) values(@member_id,@lesson_plan_content,@lesson_plan_alertDate,@buildding_id)", sp);
            Response.Write("ok");
        }
        else Response.Write("login");
    }
    private void mallChange()
    {
        //当前用户金币是否足够
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int changemoney = 0;
                string pname = null;
                int num = int.Parse(Request.Form["num"]);
                int store = 0;
                DataTable dt = Utility.SQLHelper.ExecuteTable("select pro_cuxiao_money,product_name,product_store from pro_cuxiao a,product b where a.product_id=b.product_id and pro_cuxiao_startDate<'" + DateTime.Now.ToString() + "' and pro_cuxiao_endDate>'" + DateTime.Now.ToString() + "' and a.product_id=" + id);
                if (dt.Rows.Count > 0)
                {
                    changemoney = Convert.ToInt32(dt.Rows[0][0]);
                    pname = dt.Rows[0][1].ToString();
                    store = Convert.ToInt32(dt.Rows[0][2]);
                }
                else
                {
                    dt = Utility.SQLHelper.ExecuteTable("select product_money,product_name,product_store from product where product_id=" + id);
                    if (dt.Rows.Count > 0)
                    {
                        changemoney = Convert.ToInt32(dt.Rows[0][0]);
                        pname = dt.Rows[0][1].ToString();
                        store = Convert.ToInt32(dt.Rows[0][2]);
                    }
                }
                if (store > 0)
                {
                    if (num > store)
                        num = store;
                    changemoney = changemoney * num;
                    if (changemoney > 0)
                    {
                        int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                        member mb = re.getMember(member_id);
                        if (mb != null)
                        {
                            int money = 0;
                            zcUser zc = re.getZCUser(mb.openid);
                            if (zc != null)
                            {
                                if (zc.seed != "")
                                {
                                    money = int.Parse(zc.seed);
                                }
                                if (money >= changemoney)
                                {
                                    //兑换礼品
                                    string postString = "stuno=" + zc.stuno + "&openid=" + mb.openid + "&num=" + changemoney + "&remark=" + pname;
                                    if (mb.openid != "" && postString != null)
                                    {
                                        //提交流水账到杂草
                                        string result = re.postData("http://mp.qifanhui.cn/Api/Map/NewRecord.html", postString);
                                        addSeed asd = JsonMapper.ToObject<addSeed>(result);
                                        if (asd.status == 0)
                                        {
                                            Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title,product_id) values(" + member_id + ",-" + changemoney + ",'" + pname + "'," + id + ");");
                                            //保存到道具表
                                            Utility.SQLHelper.ExecuteNonQuery("insert into member_prop(member_id,product_id,product_count) values(" + member_id + "," + id + "," + num + ")");
                                            //insert into product_ch(product_id,member_id) values(" + id + "," + member_id + ")
                                            HiFi.Common.CacheClass.RemoveOneCache("orders_" + member_id);
                                            Response.Write("ok");
                                        }
                                    }

                                }
                                else Response.Write("err");
                            }
                        }

                    }
                }
                else Response.Write("store");
            }
        }
        else Response.Write("login");
    }
    private void mallChange2()
    {

        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int num = int.Parse(Request.Form["num"]);
            int store = 0;
            member mm = re.getMember(member_id);
            if (mm != null)
            {
                if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
                {
                    string id = Request.Form["id"];
                    List<UedGoods> lu = re.getGoods();
                    int money = 0;
                    string proname = "";
                    string guige = "";
                    for (int i = 0; i < lu.Count; i++)
                    {
                        if (lu[i].id == id.ToString())
                        {
                            proname = lu[i].title;
                            money = int.Parse(lu[i].price);
                            size1[] s = lu[i].size;
                            guige = s[0].size;
                            store = int.Parse(s[0].store);
                            break;
                        }
                    }
                    if (store > 0)
                    {
                        if (num > store)
                            num = store;
                        money = money * num;
                        zcUser zc = re.getZCUser(mm.openid);
                        if (zc != null)
                        {
                            if (int.Parse(zc.seed) < money)
                            {
                                Response.Write("err");
                            }
                            else
                            {
                                string postString = "sid=" + id + "&openid=" + mm.openid + "&size=" + guige + "&num=" + num + "&cost=" + money;
                                string result = re.postData("http://mp.qifanhui.cn/Api/Map/BuyGood.html", postString);
                                addSeed asd = JsonMapper.ToObject<addSeed>(result);
                                if (asd.status == 0)
                                {
                                    Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title,product_id) values(" + member_id + ",-" + money + ",'购买" + proname + "'," + id + ");");
                                    HiFi.Common.CacheClass.RemoveOneCache("orders_" + member_id);
                                    Response.Write("ok");
                                }

                            }
                        }
                    }
                    else
                    {
                        Response.Write("store");
                    }

                }

            }
        }
        else Response.Write("login");
    }
    private void signIn()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            member mm = re.getMember(member_id);
            zcUser zc = re.getZCUser(mm.openid);
            if (zc != null)
            {
                //存在当前用户，同步种子币到黑店

                LitJson.JsonData js = LitJson.JsonMapper.ToObject(re.postData("http://mp.qifanhui.cn/Api/Map/UedSigned.html", "stuno=" + zc.stuno + "&openid=" + mm.openid));
                if (js["status"].ToString() == "1")
                {
                    //签到成功;
                    int sign = 0;
                    int exp = 0;
                    DataTable dt = Utility.SQLHelper.ExecuteTable("select signIn_jifen,signIn_exp from system");
                    if (dt.Rows.Count > 0)
                    {
                        sign = Convert.ToInt32(dt.Rows[0][0]);
                        exp = Convert.ToInt32(dt.Rows[0][1]);
                    }
                    string result = re.postData("http://mp.qifanhui.cn/Api/Map/AddSeed.html", "stuno=" + zc.stuno + "&openid=" + zc.openid + "&num=" + sign + "&remark=每日签到");
                    addSeed asd = LitJson.JsonMapper.ToObject<addSeed>(result);
                    if (asd.status == 0)
                    {
                        Utility.SQLHelper.ExecuteNonQuery("insert into member_signIn(member_id) values(" + member_id + ")");
                        Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title) values(" + member_id + "," + sign + ",'每日签到')");
                        //计算积分等级
                        re.upDateEXP(member_id, exp);
                        //Utility.SQLHelper.ExecuteNonQuery("update member_info set member_info_exp=member_info_exp+" + exp + " where member_id="+member_id);
                        //判断成就 是否有首次签到记录
                        //获取用户签到次数
                        //object count2 = Utility.SQLHelper.ExecuteScalar("select count(member_id) from member_signIn where member_id=" + member_id);
                        //获取签到的成就列表，同时排除掉用户已经获得的成就
                        object count2 = js["days"];
                        if (count2 != null)
                        {
                            int count3 = Convert.ToInt32(count2);
                            dt = Utility.SQLHelper.ExecuteTable("select success_id,success_count from success where success_content like '%签到%' and success_id not in (select success_id from success_list where member_id=" + member_id + ")");
                            foreach (DataRow dr in dt.Rows)
                            {
                                int count = Convert.ToInt32(dr[1]);
                                if (Convert.ToInt32(count3) >= count)
                                    Utility.SQLHelper.ExecuteNonQuery("insert into success_list(success_id,member_id) values(" + dr[0] + "," + member_id + ")");

                                //object obj = Utility.SQLHelper.ExecuteScalar("select * from success_list where success_id=" + dr[0] + " and member_id=" + member_id);
                                //if (obj == null)
                                //{
                                //    //当前没有获得对应的成就
                                //    if (Convert.ToInt32(count2) >= count)
                                //        Utility.SQLHelper.ExecuteNonQuery("insert into success_list(success_id,member_id) values(" + dr[0] + "," + member_id + ")");
                                //}

                            }
                        }


                        Response.Write(DateTime.Now.ToShortDateString() + "入账" + sign + "草币");
                    }

                }
                //DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from member_signIn where datediff(d,member_signIn_date,'" + DateTime.Now.ToShortDateString() + "')=0 and member_id=" + member_id);
                //if (dt.Rows.Count == 0)
                //{



                //}

            }

        }
        else Response.Write("login");
    }
    private void changepwd()
    {
        if (login_member.checkMemerLogin())
        {
            if (Request.Form["oldpwd"] != null && Request.Form["newpwd"] != null)
            {
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                string oldpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["oldpwd"], "MD5");
                string newpwd = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["newpwd"], "MD5");
                SqlParameter[] sp = { new SqlParameter("@newpwd", newpwd), new SqlParameter("@member_id", member_id), new SqlParameter("@oldpwd", oldpwd) };
                int i = Utility.SQLHelper.ExecuteNonQuery("update member set member_pwd=@newpwd where member_id=@member_id and member_pwd=@oldpwd", sp);
                if (i == 1)
                {
                    Response.Write("ok");
                }
                else Response.Write("err");
            }
        }
        else Response.Write("login");
    }
    private void feedback()
    {
        if (login_member.checkMemerLogin())
        {
            if (Request.Form["c"] != null)
            {
                string content = Server.UrlDecode(Request.Form["c"]);
                if (content.Length == 0 || content.Length > 500)
                {
                    Response.Write("err");
                }
                else
                {
                    int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                    SqlParameter[] sp = { new SqlParameter("@member_id", member_id), new SqlParameter("@feedback_content", content) };
                    Utility.SQLHelper.ExecuteNonQuery("insert into feedback(member_id,feedback_content) values(@member_id,@feedback_content)", sp);
                    Response.Write("ok");
                }
            }
        }
        else Response.Write("login");

    }
    private void chatroomUser()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                if (checkChatRoom(id))
                {
                    DataTable dt = Utility.SQLHelper.ExecuteTable("select member_mobile from member where member_id in (select distinct member_id from chatroom_detail where chatroom_id=" + id + ")");
                    string result = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        result += dr[0] + "<br/>";
                    }
                    Response.Write(result);
                }

            }
        }

    }
    private void chatroom()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int id2 = int.Parse(Request.Form["id2"]);
                string member_id = Request.Cookies["mLoginCookies"].Values[0];
                //if (checkChatRoom(id))
                //{
                //聊天室验证
                //}
                DataTable dt = Utility.SQLHelper.ExecuteTable("select top 10 a.member_id,a.chatroom_content,a.chatroom_detail_date,b.member_name,a.chatroom_detail_id from chatroom_detail a,member b where b.member_id=a.member_id and a.chatroom_id=" + id + " and chatroom_detail_id>" + id2 + " order by chatroom_detail_id asc");
                string result = "";
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i == 0)
                    {
                        i = 1;
                        result += "<script>id2=" + dt.Rows[dt.Rows.Count - 1]["chatroom_detail_id"] + "</script>";
                    }

                    result += "<div class='chat-time'>" + dr[2] + "</div>";
                    if (dr[0].ToString() == member_id)
                    {
                        result += "<div class='message right'><div class='chat-img'><img src='" + re.getMemberPic(dr[0]) + "'/></div><div class='bubble'>" + dr[1] + "</div></div>";
                    }
                    else
                    {
                        result += "<div class='message'><div class='chat-img'><img src='" + re.getMemberPic2(dr[0]) + "'/></div><div class='chat-name'>" + dr[3] + "</div><div class='bubble'>" + dr[1] + "</div></div>";
                    }
                    //result += "<div class='message'><div class='chat-img'><img src='../images/1_copy.jpg' /></div><div class='bubble'><div class='chat-name'><p>" + dr[2] + "</p><a>" + dr[1] + "</a></div><div class='chat-copy'>" + dr[0] + "</div><div class='corner'></div></div></div>";
                }
                Response.Write(result);
            }
        }
        else Response.Write("login");
    }
    private void privateChat()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int id2 = int.Parse(Request.Form["id2"]);
                string member_id = Request.Cookies["mLoginCookies"].Values[0];
                DataTable dt = Utility.SQLHelper.ExecuteTable("select top 10 a.private_message_id,a.send_content,a.send_date,b.member_name,c.member_pic from private_message a,member b,member_info c where b.member_id=a.member_id and c.member_id=a.member_id and a.private_chat_id=5 and private_message_id>" + id2 + " order by private_message_id asc");
                string result = "";
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    if (i == 0)
                    {
                        i = 1;
                        result += "<script>id2=" + dt.Rows[dt.Rows.Count - 1]["private_message_id"] + "</script>";
                    }

                    result += "<div class='chat-time'>" + dr[2] + "</div>";
                    if (dr[0].ToString() == member_id)
                    {
                        result += "<div class='message right'><div class='chat-img'><img src='" + dr["member_pic"] + "'/></div><div class='bubble'>" + dr[1] + "</div></div>";
                    }
                    else
                    {
                        result += "<div class='message'><div class='chat-img'><img src='" + dr["member_pic"] + "'/></div><div class='chat-name'>" + dr[3] + "</div><div class='bubble'>" + dr[1] + "</div></div>";
                    }
                    //result += "<div class='message'><div class='chat-img'><img src='../images/1_copy.jpg' /></div><div class='bubble'><div class='chat-name'><p>" + dr[2] + "</p><a>" + dr[1] + "</a></div><div class='chat-copy'>" + dr[0] + "</div><div class='corner'></div></div></div>";
                }
                Response.Write(result);
            }
        }
        else Response.Write("login");
    }
    private bool checkChatRoom(int id)
    {
        bool chok = false;
        int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
        DataTable dtch = Utility.SQLHelper.ExecuteTable("select chatroom_pwd,chatroom_bulid_id from chatroom where chatroom_id=" + id);
        if (dtch.Rows.Count > 0)
        {
            if (int.Parse(dtch.Rows[0][1].ToString()) != member_id)
            {
                string cpwd = dtch.Rows[0][0].ToString();
                if (cpwd != "")
                {
                    dtch = Utility.SQLHelper.ExecuteTable("select member_id from chatroom_pwd where member_id=" + member_id + " and chatroom_id=" + id);
                    if (dtch.Rows.Count > 0)
                        chok = true;
                    else chok = false;
                }
                else chok = true;
            }
            else chok = true;
        }
        return chok;
    }
    private void fayan()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                //if (checkChatRoom(id))
                //{

                //}
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                string content = Request.Form["content"];
                if (content.Length > 200)
                    content = re.wlength2(content, 200);
                content = re.pingbi(content);
                SqlParameter[] sp = { new SqlParameter("@chatroom_id", id), new SqlParameter("@member_id", member_id), new SqlParameter("@chatroom_content", content) };
                Utility.SQLHelper.ExecuteNonQuery("insert into chatroom_detail(chatroom_id,member_id,chatroom_content) values(@chatroom_id,@member_id,@chatroom_content)", sp);
            }
        }
    }
    private void fayan2()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int id1 = int.Parse(Request.Form["id1"]);
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                int laba = re.getLaBa(member_id.ToString());
                if (laba > 0)
                {
                    string content = Request.Form["content"];
                    if (content.Length > 200)
                        content = re.wlength2(content, 200);
                    content = re.pingbi(content);
                    string result = null;
                    DataTable dt;
                    if (id1 != 0)
                    {
                        result = id1.ToString();
                        dt = Utility.SQLHelper.ExecuteTable("select private_chat_id from private_chat where (send_id=" + member_id + " or receive_id=" + member_id + ") and private_chat_id=" + id1);
                        if (dt.Rows.Count == 0)
                        {
                            return;
                        }
                    }
                    else
                    {
                        dt = Utility.SQLHelper.ExecuteTable("select private_chat_id from private_chat where (send_id=" + member_id + " or receive_id=" + member_id + ") and  (send_id=" + id + " or receive_id=" + id + ")");
                        if (dt.Rows.Count > 0)
                        {
                            result = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            dt = Utility.SQLHelper.ExecuteTable("insert into private_chat(send_id,receive_id) values(" + member_id + "," + id + ");SELECT @@IDENTITY");
                            result = dt.Rows[0][0].ToString();
                        }

                    }
                    SqlParameter[] sp = { new SqlParameter("@private_chat_id", result), new SqlParameter("@member_id", member_id), new SqlParameter("@send_content", content) };
                    Utility.SQLHelper.ExecuteNonQuery("insert into private_message(private_chat_id,member_id,send_content) values(@private_chat_id,@member_id,@send_content)", sp);
                    string sid = Utility.SQLHelper.ExecuteScalar("select member_prop_id from member_prop where product_id=1 and member_id=" + member_id).ToString();
                    Utility.SQLHelper.ExecuteNonQuery("insert into member_prop_used(member_prop_id,member_prop_used_count) values(" + sid + ",1)");
                    Response.Write("id1=" + result + ";");
                }
                else Response.Write("0");
            }
        }
        else Response.Write("login");
    }
    private void socail_new()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int open = int.Parse(Request.Form["isopen"]);
            int build_id = int.Parse(Request.Form["bulid_id"]);
            string chatroom_title = Server.UrlDecode(Request.Form["chatroom_title"]);
            string pwd = Request.Form["pwd"];
            SqlParameter[] sp = { new SqlParameter("@chatroom_title", chatroom_title), new SqlParameter("@chatroom_bulid_id", build_id), new SqlParameter("@chatroom_open", open), new SqlParameter("@chatroom_pwd", pwd), new SqlParameter("@chatroom_member_id", member_id) };
            DataTable dt = Utility.SQLHelper.ExecuteTable("insert into chatroom(chatroom_title,chatroom_bulid_id,chatroom_open,chatroom_pwd,chatroom_member_id) values(@chatroom_title,@chatroom_bulid_id,@chatroom_open,@chatroom_pwd,@chatroom_member_id);SELECT @@IDENTITY", sp);
            if (dt.Rows.Count > 0)
            {
                Response.Write(dt.Rows[0][0].ToString());
            }
        }
        else
        {
            Response.Write("login");
        }
    }
    private void taskJoin()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                SqlParameter[] sp = { new SqlParameter("task_id", id), new SqlParameter("member_id", member_id) };
                DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from task_join where task_id=@task_id and member_id=@member_id", sp);
                if (dt.Rows.Count == 0)
                {
                    //还没参加,判断是否到期 
                    dt = Utility.SQLHelper.ExecuteTable("select task_id from task where task_id=@task_id and task_enddate>'" + DateTime.Now + "'", sp);
                    if (dt.Rows.Count > 0)
                    {
                        //还没到期
                        Utility.SQLHelper.ExecuteNonQuery("insert into task_join(task_id,member_id) values(@task_id,@member_id)", sp);
                        Response.Write("ok");
                    }
                }
                else Response.Write("has");
            }
        }
        else
        {
            Response.Write("login");
        }
    }
    private void skill_apply()
    {
        if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["skillId"]))
        {
            int id = int.Parse(Request.Form["skillId"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["content"]);
            if (content.Length > 400)
                content = content.Substring(0, 400);
            if (!string.IsNullOrEmpty(Request.Form["pic"]))
            {
                string[] picList = Request.Form["pic"].Split('?');
                string newPath = Server.MapPath("~") + "/upPic/skill/skill_apply/";
                string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                Random ran = new Random();
                if (!Directory.Exists(newPath + mulu))
                {
                    Directory.CreateDirectory(newPath + mulu);
                }
                SqlParameter[] sp = { new SqlParameter("member_id", member_id), new SqlParameter("skill_id", id), new SqlParameter("skill_apply_content", content) };
                int tid = 0;
                if (picList.Length > 0)
                {
                    DataTable dt = Utility.SQLHelper.ExecuteTable("insert into skill_apply(member_id,skill_id,skill_apply_content) values(@member_id,@skill_id,@skill_apply_content);SELECT @@IDENTITY", sp);
                    if (dt.Rows.Count > 0)
                        tid = int.Parse(dt.Rows[0][0].ToString());
                }
                if (tid != 0)
                {
                    foreach (string s3 in picList)
                    {
                        if (s3 != "")
                        {
                            string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                            SqlParameter[] sp2 = { new SqlParameter("@skill_apply_id", tid), new SqlParameter("@skill_apply_img", "upPic/skill/skill_apply/" + fileName) };
                            Utility.SQLHelper.ExecuteNonQuery("insert into skill_apply_pic(skill_apply_id,skill_apply_img) values(@skill_apply_id,@skill_apply_img)", sp2);
                            string oldPath = Server.MapPath("~") + "/" + s3;
                            if (File.Exists(oldPath))
                            {
                                File.Move(oldPath, newPath + fileName);
                            }
                        }
                    }
                    Response.Write("ok");
                }

            }
        }
    }
    private void skill_apply2()
    {
        if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["skillId"]))
        {
            int id = int.Parse(Request.Form["skillId"]);
            int t = int.Parse(Request.Form["t"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["content"]);
            if (content.Length > 400)
                content = content.Substring(0, 400);
            SqlParameter[] sp = { new SqlParameter("@member_id", member_id), new SqlParameter("@skill_apply_type", t), new SqlParameter("@skill_apply_content", content) };
            int has = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select count(skill_apply_id) from skill_apply2 where member_id=@member_id and skill_apply_content=@skill_apply_content", sp));
            if (has == 0)
            {
                if (!string.IsNullOrEmpty(Request.Form["pic"]))
                {
                    string[] picList = Request.Form["pic"].Split('?');
                    string newPath = Server.MapPath("~") + "/upPic/skill/skill_apply/";
                    string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                    Random ran = new Random();
                    if (!Directory.Exists(newPath + mulu))
                    {
                        Directory.CreateDirectory(newPath + mulu);
                    }
                    int tid = 0;
                    if (picList.Length > 0)
                    {
                        DataTable dt = Utility.SQLHelper.ExecuteTable("insert into skill_apply2(member_id,skill_apply_type,skill_apply_content) values(@member_id,@skill_apply_type,@skill_apply_content);SELECT @@IDENTITY", sp);
                        if (dt.Rows.Count > 0)
                            tid = int.Parse(dt.Rows[0][0].ToString());
                    }
                    if (tid != 0)
                    {
                        foreach (string s3 in picList)
                        {
                            if (s3 != "")
                            {
                                string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                                SqlParameter[] sp2 = { new SqlParameter("@skill_apply_id", tid), new SqlParameter("@skill_apply_img", "upPic/skill/skill_apply/" + fileName) };
                                Utility.SQLHelper.ExecuteNonQuery("insert into skill_apply_pic(skill_apply_id,skill_apply_img,skill_apply_type) values(@skill_apply_id,@skill_apply_img,2)", sp2);
                                string oldPath = Server.MapPath("~") + "/" + s3;
                                if (File.Exists(oldPath))
                                {
                                    File.Move(oldPath, newPath + fileName);
                                }
                            }
                        }
                        Response.Write("ok");
                    }

                }
            }
            else
            {
                Response.Write("has");
            }

        }
    }
    private void badge_apply()
    {
        if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["badgeId"]))
        {
            int id = int.Parse(Request.Form["badgeId"]);
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            string content = Server.UrlDecode(Request.Form["content"]);
            if (content.Length > 400)
                content = content.Substring(0, 400);
            if (!string.IsNullOrEmpty(Request.Form["pic"]))
            {
                string[] picList = Request.Form["pic"].Split('?');
                string newPath = Server.MapPath("~") + "/upPic/badge/badge_apply/";
                string mulu = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString();
                Random ran = new Random();
                if (!Directory.Exists(newPath + mulu))
                {
                    Directory.CreateDirectory(newPath + mulu);
                }
                SqlParameter[] sp = { new SqlParameter("member_id", member_id), new SqlParameter("badge_id", id), new SqlParameter("badge_apply_content", content) };
                int tid = 0;
                if (picList.Length > 0)
                {
                    DataTable dt = Utility.SQLHelper.ExecuteTable("insert into badge_apply(member_id,badge_id,badge_apply_content) values(@member_id,@badge_id,@badge_apply_content);SELECT @@IDENTITY", sp);
                    if (dt.Rows.Count > 0)
                        tid = int.Parse(dt.Rows[0][0].ToString());
                }
                if (tid != 0)
                {
                    foreach (string s3 in picList)
                    {
                        if (s3 != "")
                        {
                            string fileName = mulu + "/" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + "_" + ran.Next().ToString() + ".jpg";
                            SqlParameter[] sp2 = { new SqlParameter("@badge_apply_id", tid), new SqlParameter("@badge_apply_img", "upPic/badge/badge_apply/" + fileName) };
                            Utility.SQLHelper.ExecuteNonQuery("insert into badge_apply_pic(badge_apply_id,badge_apply_img) values(@badge_apply_id,@badge_apply_img)", sp2);
                            string oldPath = Server.MapPath("~") + "/" + s3;
                            if (File.Exists(oldPath))
                            {
                                File.Move(oldPath, newPath + fileName);
                            }
                        }
                    }
                    Response.Write("ok");
                }

            }
        }
    }
    private void regMember2()
    {
        if (!string.IsNullOrEmpty(Request.Form["pwd"]) && !string.IsNullOrEmpty(Request.Form["email"]) && !string.IsNullOrEmpty(Request.Form["uid"]) && !string.IsNullOrEmpty(Request.Form["pic"]))
        {
            string openid = Request.Form["uid"];
            string email = Request.Form["email"];
            int schoold_id = 1;
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["pwd"], "MD5");
            zcUser zc = re.getZCUser(openid);
            if (zc != null)
            {
                string tel = zc.contacts;
                string stnum = zc.stuno;
                string nkname = zc.nickname;
                string stname = zc.name;
                if (zc.email != "")
                {
                    email = zc.email;
                }
                else
                {
                    //同步到黑店,没有更新的功能?
                    string result = re.postData("http://mp.qifanhui.cn/Api/Map/prove.html", "openid=" + openid + "&nickname=" + nkname + "&school=" + zc.school + "&stuno" + stnum + "&name=" + stname + "&contacts=" + tel + "&email=" + email);
                }
                switch (zc.school)
                {
                    case "深圳职业技术学院":
                        schoold_id = 2;
                        break;
                    case "深圳信息职业技术学院":
                        schoold_id = 3;
                        break;
                    default: break;
                }
                weixin_info wx = re.getWXInfo(openid);
                if (wx != null)
                {
                    string member_name = wx.nickname;
                    string img = wx.headimgurl;
                    SqlParameter[] sp = { new SqlParameter("@member_name", member_name), 
                                       new SqlParameter("@member_realName", stname), 
                                        new SqlParameter("@member_mobile", tel), 
                                        new SqlParameter("@member_email", email), 
                                        new SqlParameter("@member_pwd", password), 
                                        new SqlParameter("@member_school_id", schoold_id), 
                                        new SqlParameter("@member_number", stnum), 
                                        new SqlParameter("@member_openid", openid) };
                    //DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from member where  member_name=@member_name or member_mobile=@member_mobile or member_email=@member_email", sp);
                    SqlParameter[] sp2 = { new SqlParameter("@member_realName", stname), new SqlParameter("@member_mobile", tel) };
                    DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from member where  member_realName=@member_realName and member_mobile=@member_mobile", sp2);
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write("has member");
                    }
                    else
                    {
                        dt = Utility.SQLHelper.ExecuteTable("insert into member(member_name,member_realName,member_mobile,member_email,member_pwd,member_school_id,member_number,member_openid) values(@member_name,@member_realName,@member_mobile,@member_email,@member_pwd,@member_school_id,@member_number,@member_openid);SELECT @@IDENTITY", sp);
                        //注册送积分跟经验值
                        if (dt.Rows.Count > 0)
                        {
                            string member_id = dt.Rows[0][0].ToString();
                            int lid = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select top 1 level_id from level order by level_exp asc"));
                            Utility.SQLHelper.ExecuteNonQuery("insert into member_info(member_id,member_info_exp,member_info_level,member_pic) values(" + member_id + ",30," + lid + ",'" + img + "')");
                            Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title) values(" + member_id + ",100,'新加入')");
                            HttpCookie user = new HttpCookie("mLoginCookies");
                            user.Values.Add("member_id", member_id);
                            string cName = "member" + member_id;
                            object member = HiFi.Common.CacheClass.GetCache(cName);
                            if (member == null)
                            {
                                member mm = new member();
                                mm.memberName = member_name;
                                mm.nickName = member_name;
                                mm.headImgurl = img;
                                mm.openid = openid;
                                HiFi.Common.CacheClass.SetCache(cName, mm, DateTime.Now.AddDays(1), TimeSpan.Zero);
                            }
                            Response.Cookies.Add(user);
                        }
                        Response.Write("ok");
                    }
                }
            }
        }
    }
    private void regMember()
    {
        if (HiFi.Common.ValidateHelper.IsMobilePhone(Request.Form["tel"]))
        {
            //if (!string.IsNullOrEmpty(Request.Form["pwd"]) && !string.IsNullOrEmpty(Request.Form["sch"]) && !string.IsNullOrEmpty(Request.Form["stn"]) && !string.IsNullOrEmpty(Request.Form["stname"]) && !string.IsNullOrEmpty(Request.Form["email"]) && !string.IsNullOrEmpty(Request.Form["uid"]) && !string.IsNullOrEmpty(Request.Form["pic"]) && !string.IsNullOrEmpty(Request.Form["wx"]))
            //{


            //}
            //需判断当前openid是否存在
            string openid = Request.Form["uid"];
            string tel = Request.Form["tel"];
            string email = Request.Form["email"];
            string schoold_id = Request.Form["sch"];
            string stnum = Request.Form["stn"];
            string stname = Server.UrlDecode(Request.Form["stname"]);
            string member_name = Server.UrlDecode(Request.Form["wx"]);
            string img = Request.Form["pic"];
            string password = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["pwd"], "MD5");
            string result = null;
            bool err = false;
            if (schoold_id == "2")
            {
                result = re.postData("http://mp.qifanhui.cn/Api/Map/StudentDataBase.html", "stuno=" + stnum);
                if (result == "" || result == null)
                {
                    err = true;
                }
            }
            if (err)
            {
                Response.Write("num err");
            }
            else
            {
                SqlParameter[] sp = { new SqlParameter("@member_name", member_name), 
                                       new SqlParameter("@member_realName", stname), 
                                        new SqlParameter("@member_mobile", tel), 
                                        new SqlParameter("@member_email", email), 
                                        new SqlParameter("@member_pwd", password), 
                                        new SqlParameter("@member_school_id", schoold_id), 
                                        new SqlParameter("@member_number", stnum), 
                                        new SqlParameter("@member_openid", openid) };
                //DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from member where  member_name=@member_name or member_mobile=@member_mobile or member_email=@member_email", sp);
                SqlParameter[] sp2 = { new SqlParameter("@member_realName", stname), new SqlParameter("@member_mobile", tel) };
                DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from member where  member_realName=@member_realName and member_mobile=@member_mobile", sp2);
                if (dt.Rows.Count > 0)
                {
                    Response.Write("has member");
                }
                else
                {
                    string school = "深圳大学";
                    switch (schoold_id)
                    {
                        case "2":
                            school = "深圳职业技术学院";
                            break;
                        case "3":
                            school = "深圳信息职业技术学院";
                            break;
                        default: break;
                    }
                    //nickname没有保存
                    string code = re.postData("http://mp.qifanhui.cn/Api/Map/prove.html", "openid=" + openid + "&nickname=" + member_name + "&school=" + school + "&stuno=" + stnum + "&name=" + stname + "&contacts=" + tel + "&email=" + email);
                    prove pr = LitJson.JsonMapper.ToObject<prove>(code);
                    if (pr.code == 0)
                    {
                        //成功同步
                        dt = Utility.SQLHelper.ExecuteTable("insert into member(member_name,member_realName,member_mobile,member_email,member_pwd,member_school_id,member_number,member_openid) values(@member_name,@member_realName,@member_mobile,@member_email,@member_pwd,@member_school_id,@member_number,@member_openid);SELECT @@IDENTITY", sp);
                        //注册送积分跟经验值
                        if (dt.Rows.Count > 0)
                        {
                            string member_id = dt.Rows[0][0].ToString();
                            int lid = Convert.ToInt32(Utility.SQLHelper.ExecuteScalar("select top 1 level_id from level order by level_exp asc"));
                            Utility.SQLHelper.ExecuteNonQuery("insert into member_info(member_id,member_info_exp,member_info_level,member_pic) values(" + member_id + ",30," + lid + ",'" + img + "')");
                            Utility.SQLHelper.ExecuteNonQuery("insert into member_money(member_id,money,member_money_title) values(" + member_id + ",100,'新加入')");
                            HttpCookie user = new HttpCookie("mLoginCookies");
                            user.Values.Add("member_id", member_id);
                            string cName = "member" + member_id;
                            object member = HiFi.Common.CacheClass.GetCache(cName);
                            if (member == null)
                            {
                                member mm = new member();
                                mm.memberName = member_name;
                                mm.nickName = member_name;
                                mm.headImgurl = img;
                                mm.openid = openid;
                                HiFi.Common.CacheClass.SetCache(cName, mm, DateTime.Now.AddDays(1), TimeSpan.Zero);
                            }
                            Response.Cookies.Add(user);
                        }
                        Response.Write("ok");
                    }
                    else
                    {
                        if (pr.code == 3)
                            Response.Write("has num");
                    }
                }
            }
        }
    }
    private void collectionJob()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int t = int.Parse(Request.Form["t"]);
                int jt = int.Parse(Request.Form["jt"]);
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                SqlParameter[] sp = { new SqlParameter("job_id", id), new SqlParameter("member_id", member_id), new SqlParameter("job_type", jt) };
                if (t == 1)
                {
                    Utility.SQLHelper.ExecuteNonQuery("insert into collection_job(job_id,member_id,job_type) values(@job_id,@member_id,@job_type)", sp);
                    Response.Write("ok");
                }
                else
                {
                    Utility.SQLHelper.ExecuteNonQuery("delete from collection_job where job_id=@job_id and member_id=@member_id and job_type=@job_type", sp);
                    Response.Write("ok");
                }

            }
        }
        else
        {
            Response.Write("login");
        }
    }
    private void applyJob()
    {
        if (login_member.checkMemerLogin())
        {
            if (HiFi.Common.ValidateHelper.IsNumber(Request.Form["id"]))
            {
                int id = int.Parse(Request.Form["id"]);
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                SqlParameter[] sp = { new SqlParameter("job_id", id), new SqlParameter("member_id", member_id) };
                DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id from apply_job where member_id=@member_id and job_id=@job_id", sp);
                if (dt.Rows.Count > 0)
                {
                    Response.Write("has");
                }
                else
                {
                    //dt = Utility.SQLHelper.ExecuteTable("select job_id from job where job_endDate>'" + DateTime.Now.ToString() + "' and job_id=" + id);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    //截止时间还没到,判断用户是否有写简历
                    //    dt = Utility.SQLHelper.ExecuteTable("select member_id from member_jianli where member_id=@member_id", sp);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        Utility.SQLHelper.ExecuteNonQuery("insert into apply_job(job_id,member_id) values(@job_id,@member_id)", sp);
                    //        Response.Write("ok");
                    //    }
                    //    else
                    //    {
                    //        Response.Write("jianli");
                    //    }

                    //}
                    Utility.SQLHelper.ExecuteNonQuery("insert into apply_job(job_id,member_id) values(@job_id,@member_id)", sp);
                    Response.Write("ok");
                }
            }
        }
        else
        {
            Response.Write("login");
        }

    }
    private void apply_job_list()
    {

    }
    private void searchJob()
    {
        if (!string.IsNullOrEmpty(Request.Form["scontent"]))
        {
            string content = Server.UrlDecode(Request.Form["scontent"]).Trim();
            string cacheName = "jobList";//招聘列表
            object jobList = HiFi.Common.CacheClass.GetCache(cacheName);
            StringBuilder sb = new StringBuilder();
            if (jobList == null)
            {
                string text = re.getFileText(Server.MapPath("~/tempfile/zhaopin.htm"));
                List<zhaopin> lt = JsonMapper.ToObject<List<zhaopin>>(text);
                HiFi.Common.CacheClass.SetCache(cacheName, lt, DateTime.Now.AddDays(7), TimeSpan.Zero);
            }
            List<zhaopin> lt2 = (List<zhaopin>)jobList;
            foreach (zhaopin zp in lt2)
            {
                if (zp.recruit_position.IndexOf(content) != -1)
                {
                    sb.Append("<div class=\"recruit-job\"><a href='job_detail.aspx?jid=" + zp.id + "' class=\"job-card\"><div class=\"media\"><div class=\"media-left\"><div class=\"img\" style=\"background-image:url(" + zp.company_logo + ");\"></div></div><div class=\"media-body\"><h4>" + zp.recruit_position + "</h4><p>薪资面议</p></div></div><p>深圳|" + zp.company_name + "</p></a></div>");
                    //sb.Append("<li class='r_list'><a href='job_detail.aspx?jid=" + zp.id + "'><p class='r_zw'>" + zp.recruit_position + "</p><div class='r_gz'><p>薪资面议</p></div><p class='r_dz'>" + zp.company_name + "</p></a></li>");
                }
            }
            Response.Write(sb.ToString());
        }
    }


    private void jobList()
    {
        //HiFi.Common.CacheClass.RemoveAllCache();
        string cacheName = "jobList";//招聘列表
        List<zhaopin> lt = null;
        object jobList = HiFi.Common.CacheClass.GetCache(cacheName);
        StringBuilder sb = new StringBuilder();
        if (jobList == null)
        {
            string text = re.getFileText(Server.MapPath("~/tempfile/zhaopin.htm"));
            //string text = re.Get_Http("http://zp.qifanhui.cn/Home/AppApi/index.html", 20000);
            lt = JsonMapper.ToObject<List<zhaopin>>(text);
            HiFi.Common.CacheClass.SetCache(cacheName, lt, DateTime.Now.AddDays(7), TimeSpan.Zero);
            cacheName = "company";
            object indexTable1 = HiFi.Common.CacheClass.GetCache(cacheName);
            if (indexTable1 == null)
            {
                int z = 0;
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("company_id");
                dt.Columns.Add("company_name");
                dt.Columns.Add("company_info");
                dt.Columns.Add("company_logo");
                DataColumn[] dcm = { dt.Columns[0] };
                dt.PrimaryKey = dcm;
                for (int j = 0; j < lt.Count; j++)
                {
                    int id = int.Parse(lt[j].company_id);
                    DataRow dr2 = dt.Rows.Find(new object[] { lt[j].company_id });
                    if (dr2 == null)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = id;
                        dr[1] = lt[j].company_name;
                        dr[2] = lt[j].company_info;
                        dr[3] = lt[j].company_logo;
                        dt.Rows.Add(dr);
                        z++;
                    }
                }
                HiFi.Common.CacheClass.SetCache(cacheName, dt, DateTime.Now.AddDays(7), TimeSpan.Zero);
            }
        }
        List<zhaopin> lt2 = null;
        if (jobList != null) lt2 = (List<zhaopin>)jobList;
        else if (lt != null) lt2 = lt;
        int count = int.Parse(Request.Form["p"]);
        int count2 = count + 3;
        if (count2 > lt2.Count) count2 = lt2.Count;
        for (int i = count; i < count2; i++)
        {
            //<li class="r_list"><a href="job-6-1.html"><p class="r_zw">文员</p><div class="r_gz"><p>薪资面议</p></div><p class="r_dz">深圳 | XXXX有限公司</p>					</a></li>
            //sb.Append("<li class='r_list'><a href='job_detail.aspx?jid=" + lt2[i].id + "'><p class='r_zw'>" + lt2[i].recruit_position + "</p><div class='r_gz'><p>薪资面议</p></div><p class='r_dz'>" + lt2[i].company_name + "</p></a></li>");
            sb.Append("<div class=\"recruit-job\"><a href='job_detail.aspx?jid=" + lt2[i].id + "' class=\"job-card\"><div class=\"media\"><div class=\"media-left\"><div class=\"img\" style=\"background-image:url(" + lt2[i].company_logo + ");\"></div></div><div class=\"media-body\"><h4>" + lt2[i].recruit_position + "</h4><p>薪资面议</p></div></div><p>" + lt2[i].company_address.Substring(0, 2) + " | " + lt2[i].company_name + "</p></a></div>");
        }
        Response.Write(sb.ToString());
    }








    private void jobList2()
    {
        string cacheName = "jobList";//招聘列表
        int num = int.Parse(Request.Form["num"]);
        List<zhaopin> lt = null;
        object jobList = HiFi.Common.CacheClass.GetCache(cacheName);
        StringBuilder sb = new StringBuilder();
        if (jobList == null)
        {
            string text = re.getFileText(Server.MapPath("~/tempfile/zhaopin.htm"));
            //string text = re.Get_Http("http://zp.qifanhui.cn/Home/AppApi/index.html", 20000);
            lt = JsonMapper.ToObject<List<zhaopin>>(text);
            HiFi.Common.CacheClass.SetCache(cacheName, lt, DateTime.Now.AddDays(7), TimeSpan.Zero);
        }
        List<zhaopin> lt2 = null;
        if (jobList != null) lt2 = (List<zhaopin>)jobList;
        else if (lt != null) lt2 = lt;
        for (int i = 0; i < num; i++)
        {
            sb.Append("<a href='job_detail.aspx?jid=" + lt2[i].id + "'><li class='r_list'><p class='r_zw'>" + lt2[i].recruit_position + "</p><p class='r_dz'>" + lt2[i].company_name + "</p><div class='r_gz'><p>月薪 面议</p><span>" + re.StampToDateTime(lt2[i].updatetime).ToShortDateString() + "</span></div></li></a>");
        }
        Response.Write(sb.ToString());
    }




    private void searchJianZhi()
    {
        if (!string.IsNullOrEmpty(Request.Form["scontent"]))
        {
            string content = Server.UrlDecode(Request.Form["scontent"]).Trim();
            object jzcache = getJianZhi();
            List<jianzhi> jzList = (List<jianzhi>)jzcache;
            StringBuilder sb2 = new StringBuilder("");
            foreach (jianzhi jz in jzList)
            {
                if (jz.jname.IndexOf(content) != -1)
                {
                    sb2.Append("<a class=\"media\" href='jianzhi_detail.aspx?jid=" + jz.id + "'><div class=\"media-body\"><h5 class=\"media-heading\">" + jz.jname + "</h5><p>" + re.StampToDateTime(jz.createtime).ToShortDateString() + "</p></div><div class=\"media-right\"><span>" + jz.reward + "</span><p>元/" + jz.unit + "</p></div></a>");
                    //sb2.Append("<a href='jianzhi_detail.aspx?jid=" + jz.id + "'><li><p class='j_gz'><span>" + jz.jname + "</span><span><i>" + re.StampToDateTime(jz.createtime).ToShortDateString() + "</i><em>" + jz.area + "</em></span><p><p class='j_jq'><span>" + jz.reward + "</span><i>元&nbsp;/&nbsp;" + jz.unit + "</i></p></li></a>");
                }
            }
            Response.Write(sb2.ToString());
        }
    }

    private object getJianZhi()
    {
        string cacheName = "jianzhi";
        object jzcache = HiFi.Common.CacheClass.GetCache(cacheName);
        if (jzcache == null)
        {
            string jzText = re.getFileText(Server.MapPath("~/tempfile/jianzhi.htm"));
            jzText = jzText.Replace("\"class\"", "\"className\"");
            List<jianzhi> jzList = JsonMapper.ToObject<List<jianzhi>>(jzText);
            HiFi.Common.CacheClass.SetCache(cacheName, jzList, DateTime.Now.AddDays(7), TimeSpan.Zero);
            jzcache = jzList;
        }
        return jzcache;
    }
    private void collect_jobList()
    {
        if (login_member.checkMemerLogin())
        {
            string cacheName = "jobList";//招聘列表

            List<zhaopin> lt = (List<zhaopin>)HiFi.Common.CacheClass.GetCache(cacheName);
            StringBuilder sb = new StringBuilder();
            if (lt == null)
            {
                string text = re.getFileText(Server.MapPath("~/tempfile/zhaopin.htm"));
                //string text = re.Get_Http("http://zp.qifanhui.cn/Home/AppApi/index.html", 20000);
                lt = JsonMapper.ToObject<List<zhaopin>>(text);
                HiFi.Common.CacheClass.SetCache(cacheName, lt, DateTime.Now.AddDays(7), TimeSpan.Zero);
            }
            if (lt != null)
            {
                int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
                int count = int.Parse(Request.Form["p"]);
                DataTable dt = Utility.SQLHelper.ExecuteTable("select top 5 collection_job_id,job_id,collection_job_date from collection_job  where collection_job_id not in (select top " + count + " collection_job_id from collection_job where member_id=" + member_id + " and job_type=1) and job_type=1 and member_id=" + member_id);
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (zhaopin zp in lt)
                    {
                        if (zp.id == dr[1].ToString())
                        {
                            sb.Append("<div class=\"recruit-job\"><a href='../job/job_detail.aspx?jid=" + zp.id + "' class=\"job-card\"><div class=\"media\"><div class=\"media-left\"><div class=\"img\" style=\"background-image:url(" + zp.company_logo + ");\"></div></div><div class=\"media-body\"><h4>" + zp.recruit_position + "</h4><p>薪资面议</p></div></div><p>" + zp.company_address.Substring(0, 2) + " | " + zp.company_name + "</p></a></div>");
                            break;
                        }
                    }
                }
                Response.Write(sb.ToString());
                //sb.Append("<div class=\"recruit-job\"><a href='job_detail.aspx?jid=" + lt2[i].id + "' class=\"job-card\"><div class=\"media\"><div class=\"media-left\"><div class=\"img\" style=\"background-image:url(" + lt2[i].company_logo + ");\"></div></div><div class=\"media-body\"><h4>" + lt2[i].recruit_position + "</h4><p>薪资面议</p></div></div><p>" + lt2[i].company_address.Substring(0,2) + " | " + lt2[i].company_name + "</p></a></div>");
            }
        }
        else
        {
            Response.Write("login");
        }

    }


    private void collect_jianzhiList()
    {
        if (login_member.checkMemerLogin())
        {
            int member_id = int.Parse(Request.Cookies["mLoginCookies"].Values[0]);
            int count = int.Parse(Request.Form["p"]);
            object jzcache = getJianZhi();
            if (jzcache != null)
            {
                List<jianzhi> jzList = (List<jianzhi>)jzcache;
                //select top 3 member_money_date,member_money_title,money from member_money where member_money_id not in (select top " + count + " member_money_id from member_money where member_id=" + member_id + " order by member_money_id desc)
                DataTable dt = Utility.SQLHelper.ExecuteTable("select top 5 collection_job_id,job_id,collection_job_date from collection_job  where collection_job_id not in (select top " + count + " collection_job_id from collection_job where member_id=" + member_id + " and job_type=2) and job_type=2 and member_id=" + member_id);
                StringBuilder sb2 = new StringBuilder("");
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (jianzhi jz in jzList)
                    {
                        if (jz.id == dr[1].ToString())
                        {
                            sb2.Append("<a class=\"media\" href='../job/jianzhi_detail.aspx?jid=" + jz.id + "'><div class=\"media-body\"><h5 class=\"media-heading\">" + jz.jname + "</h5><p>" + re.StampToDateTime(jz.createtime).ToShortDateString() + "</p></div><div class=\"media-right\"><span>" + jz.reward + "</span><p>元/" + jz.unit + "</p></div></a>");
                            break;
                        }
                    }
                }

                Response.Write(sb2.ToString());
            }
        }
        else
        {
            Response.Write("login");
        }

    }


    private void jianzhiList()
    {
        int count = int.Parse(Request.Form["p"]);
        int count2 = count + 4;
        object jzcache = getJianZhi();
        if (jzcache != null)
        {
            List<jianzhi> jzList = (List<jianzhi>)jzcache;
            StringBuilder sb2 = new StringBuilder("");
            if (count2 > jzList.Count) count2 = jzList.Count;
            for (int i = count; i < count2; i++)
            {
                sb2.Append("<a class=\"media\" href='jianzhi_detail.aspx?jid=" + jzList[i].id + "'><div class=\"media-body\"><h5 class=\"media-heading\">" + jzList[i].jname + "</h5><p>" + re.StampToDateTime(jzList[i].createtime).ToShortDateString() + "</p></div><div class=\"media-right\"><span>" + jzList[i].reward + "</span><p>元/" + jzList[i].unit + "</p></div></a>");
            }
            Response.Write(sb2.ToString());
        }
    }




    private void companyList()
    {
        string cacheName = "jobList";//招聘列表
        object jobList = HiFi.Common.CacheClass.GetCache(cacheName);
        StringBuilder sb = new StringBuilder();
        if (jobList == null)
        {
            string text = re.getFileText(Server.MapPath("~/tempfile/zhaopin.htm"));
            List<zhaopin> lt = JsonMapper.ToObject<List<zhaopin>>(text);
            HiFi.Common.CacheClass.SetCache(cacheName, lt, DateTime.Now.AddDays(7), TimeSpan.Zero);
            cacheName = "company";
            object indexTable1 = HiFi.Common.CacheClass.GetCache(cacheName);
            if (indexTable1 == null)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Columns.Add("company_id");
                dt.Columns.Add("company_name");
                dt.Columns.Add("company_info");
                dt.Columns.Add("company_logo");
                DataColumn[] dcm = { dt.Columns[0] };
                dt.PrimaryKey = dcm;
                for (int j = 0; j < lt.Count; j++)
                {
                    int id = int.Parse(lt[j].company_id);
                    DataRow dr2 = dt.Rows.Find(new object[] { lt[j].company_id });
                    if (dr2 == null)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = id;
                        dr[1] = lt[j].company_name;
                        dr[2] = lt[j].company_info;
                        dr[3] = lt[j].company_logo;
                        dt.Rows.Add(dr);
                        sb.Append("<li><a href='company_detail.aspx?cid=" + id + "'><p>" + dr[1] + "</p></a></li>");
                    }
                }
                HiFi.Common.CacheClass.SetCache(cacheName, dt, DateTime.Now.AddDays(7), TimeSpan.Zero);
                Response.Write(sb.ToString());
            }
        }
        else
        {
            cacheName = "company";
            object indexTable1 = HiFi.Common.CacheClass.GetCache(cacheName);
            if (indexTable1 != null)
            {
                System.Data.DataTable dt = (DataTable)indexTable1;
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<li><a href='company_detail.aspx?cid=" + dr[0] + "'><p>" + dr[1] + "</p></a></li>");
                }
                Response.Write(sb.ToString());
            }

        }
    }
    private void memberLogin()
    {
        //if (HiFi.Common.ValidateHelper.IsMobilePhone(Request.Form["tel"]) || HiFi.Common.ValidateHelper.IsEmail(Request.Form["tel"]))
        if (!string.IsNullOrEmpty(Request.Form["tel"]))
        {
            if (!string.IsNullOrEmpty(Request.Form["pwd"]))
            {
                string member_name = Request.Form["tel"];
                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(Request.Form["pwd"], "MD5");
                SqlParameter[] sp = { new SqlParameter("@member_email", member_name), new SqlParameter("@member_pwd", password) };
                DataTable dt = Utility.SQLHelper.ExecuteTable("select member_id,member_name,member_openid from member where (member_email=@member_email) and member_pwd=@member_pwd", sp);
                if (dt.Rows.Count == 0)
                {
                    Response.Write("err");
                }
                else
                {
                    //cache
                    string cacheName = "m" + dt.Rows[0][0].ToString();
                    object indexTable1 = HiFi.Common.CacheClass.GetCache(cacheName);
                    if (indexTable1 == null)
                    {
                        HiFi.Common.CacheClass.SetCache(cacheName, dt.Rows[0][1], DateTime.Now.AddDays(1), TimeSpan.Zero);
                    }
                    //cookie

                    HttpCookie cookie = new HttpCookie("mLoginCookies");
                    cookie.Expires = DateTime.Now.AddDays(1); //DateTime.Now.AddDays(1);
                    cookie.HttpOnly = true;
                    cookie.Values.Add("member_id", dt.Rows[0][0].ToString());
                    string cName = "member" + dt.Rows[0][0].ToString();
                    object member = HiFi.Common.CacheClass.GetCache(cName);
                    if (member == null)
                    {
                        member mm = new member();
                        mm.memberName = dt.Rows[0][1].ToString();
                        mm.nickName = dt.Rows[0][1].ToString();
                        mm.openid = dt.Rows[0][2].ToString();
                        string pic = "";
                        dt = Utility.SQLHelper.ExecuteTable("select member_pic from member_info where member_id=" + dt.Rows[0][0].ToString());
                        if (dt.Rows.Count > 0)
                            pic = dt.Rows[0][0].ToString();
                        mm.headImgurl = pic;

                        HiFi.Common.CacheClass.SetCache(cName, mm, DateTime.Now.AddDays(1), TimeSpan.Zero);
                    }
                    Response.AppendCookie(cookie);
                    Response.Write("ok");
                }
            }

        }
    }

}