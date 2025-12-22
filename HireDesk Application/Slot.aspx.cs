using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using System.Net.Mail;

namespace HireDesk_Application
{
    public partial class Slot : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;

        
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Session["aid"] == null)
            {
                Response.Redirect("Resume.aspx");
            }
        }

   
        private int GetSlotId(string day, string time)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT sid FROM InterviewSlot WHERE sDay=@day AND sTime=@time",
                    con);

                cmd.Parameters.AddWithValue("@day", day);
                cmd.Parameters.AddWithValue("@time", time);

                con.Open();
                object result = cmd.ExecuteScalar();
                return result == null ? 0 : Convert.ToInt32(result);
            }
        }

        private void BookSlot(int aid, int sid)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string q = @"EXEC dbo.AddApplicantInterview @aid,@sid";

                SqlCommand cmd = new SqlCommand(q, con);

                cmd.Parameters.AddWithValue("@aid", aid);
                cmd.Parameters.AddWithValue("@sid", sid);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        private bool IsSlotAvailable(int sid)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM ApplicantInterview WHERE sid = @sid",
                    con);

                cmd.Parameters.AddWithValue("@sid", sid);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count < 2; 
            }
        }


       
        private void SendMailToHR(string day, string time)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(
                ConfigurationManager.AppSettings["FROM_EMAIL"]);

            mail.To.Add(
                ConfigurationManager.AppSettings["HR_EMAIL"]);

            mail.Subject = "Interview Slot Booked - HireDesk";

            mail.Body =
                "A fresher has booked an interview slot.\n\n" +
                "Name: " + Session["userName"] + "\n" +
                "Email: " + Session["userEmail"] + "\n" +
                "Slot: " + day + " " + time;

         
            if (Session["resumePath"] != null)
            {
                string path = Session["resumePath"].ToString();

                if (File.Exists(path))
                {
                    mail.Attachments.Add(new Attachment(path));
                }
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["FROM_EMAIL"],
                ConfigurationManager.AppSettings["FROM_PASSWORD"]);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }


        
        private void SendMailToApplicant(string day, string time)
        {
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(
                ConfigurationManager.AppSettings["FROM_EMAIL"]);

            mail.To.Add(Session["userEmail"].ToString());

            mail.Subject = "HireDesk - Interview Slot Confirmation";

            mail.Body =
                "Dear " + Session["userName"] + ",\n\n" +
                "Your interview slot has been successfully booked.\n\n" +
                "Slot: " + day + " " + time + "\n\n" +
                "Regards,\nHireDesk Team";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["FROM_EMAIL"],
                ConfigurationManager.AppSettings["FROM_PASSWORD"]);
            smtp.EnableSsl = true;

            smtp.Send(mail);
        }

       
        private void ShowAlert(string msg)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "alert",
                $"alert('{msg}');",
                true);
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int aid = Convert.ToInt32(Session["aid"]);
            string day = DropDownList1.SelectedValue;
            string time = DropDownList2.SelectedValue;

            int sid = GetSlotId(day, time);

            if (sid == 0)
            {
                ShowAlert("Invalid slot selected");
                return;
            }

         
            if (!IsSlotAvailable(sid))
            {
                ShowAlert("This slot is already fully booked. Please choose another slot.");
                return;
            }

            BookSlot(aid, sid);

          
            SendMailToHR(day, time);
            SendMailToApplicant(day, time);

            Session.Clear();
            ShowAlert("Slot booked successfully");
            Response.Redirect("ThankYou.aspx");

        }
    }
}
