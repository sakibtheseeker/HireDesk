using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.UI;

namespace HireDesk_Application
{
    public partial class Resume : Page
    {
       
        
        string connStr = ConfigurationManager.ConnectionStrings["dbconn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EnableForm();
            }
        }

        private void DisableForm()
        {
            formWrapper.Attributes["class"] = "locked-form";
            Button1.Enabled = false;

        }

        private void EnableForm()
        {
            formWrapper.Attributes["class"] = "";
            Button1.Enabled = true;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "Others")
            {
                DisableForm();
                ShowAlert("Currently we hire only IT background candidates.");

            }
            else
                EnableForm();
        }

    
        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (DropDownList2.SelectedValue == "Fresher")
            {
                
                salaryWrapper.Attributes["class"] = "locked-form";

                TextBox4.Text = "";
                TextBox5.Text = "";
                TextBox6.Text = "";
            }
            else
            {
                
                salaryWrapper.Attributes["class"] = "";
            }
        }

        private void ShowAlert(string msg)
        {
            Response.Write($"<script>alert('{msg}');</script>");
        }


        private bool IsDuplicateUser(string email, string contact)
        {
            string userEmail = email;
            string userContact = contact;
            string q = $"exec CheckDuplicateApplicant '{userEmail}' ,'{userContact}'";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(q,con);
                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }


        private int AddFresherApplicant()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string educationStream = DropDownList1.SelectedValue.Replace("'", "''");
                string experienceType = "Fresher";
                string aName = TextBox1.Text.Replace("'", "''");
                string aEmail = TextBox2.Text.Replace("'", "''");
                string aContact = TextBox3.Text;
                string q = $"exec AddFresherApplicant '{educationStream}', '{experienceType}', '{aName}','{aEmail}', '{aContact}' ";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int aid = Convert.ToInt32(cmd.ExecuteScalar());
                

                return aid;

            }
        }

        private void AddExperiencedApplicant()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {

                string educationStream = DropDownList1.SelectedValue.Replace("'", "''");
                string experienceType = "Experienced";
                string aName = TextBox1.Text.Replace("'", "''");
                string aEmail = TextBox2.Text.Replace("'", "''");
                string aContact = TextBox3.Text;
                decimal aCTC = string.IsNullOrWhiteSpace(TextBox4.Text) ? 0 : Convert.ToDecimal(TextBox4.Text);
                decimal aECTC = string.IsNullOrWhiteSpace(TextBox5.Text) ? 0 : Convert.ToDecimal(TextBox5.Text);
                string aNoticePeriod = TextBox6.Text.Replace("'", "''");
                string q = $"exec AddExperiencedApplicant '{educationStream}','{experienceType}','{aName}','{aEmail}','{aContact}','{aCTC}','{aECTC}','{aNoticePeriod}' ";

                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                cmd.ExecuteNonQuery();
           
            }
        }


        private void SendMailToHR(bool attachFromUpload)
        {
            string fromEmail = ConfigurationManager.AppSettings["FROM_EMAIL"];
            string password = ConfigurationManager.AppSettings["FROM_PASSWORD"];
            string hrEmail = ConfigurationManager.AppSettings["HR_EMAIL"];

            MailMessage mail = new MailMessage(fromEmail, hrEmail);
            mail.Subject = "New Application - HireDesk";

            mail.Body =
                "Name: " + TextBox1.Text + "\n" +
                "Email: " + TextBox2.Text + "\n" +
                "Contact: " + TextBox3.Text + "\n" +
                "Experience: " + DropDownList2.SelectedValue;


            if (attachFromUpload && FileUpload1.HasFile)
            {
                mail.Attachments.Add(
                    new Attachment(FileUpload1.PostedFile.InputStream,
                                   Path.GetFileName(FileUpload1.FileName)));
            }

            if (!attachFromUpload && Session["resumePath"] != null)
            {
                mail.Attachments.Add(
                    new Attachment(Session["resumePath"].ToString()));
            }

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }


        private void SendMailToApplicant()
        {
            string fromEmail = ConfigurationManager.AppSettings["FROM_EMAIL"];
            string password = ConfigurationManager.AppSettings["FROM_PASSWORD"];

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);


            mail.To.Add(TextBox2.Text);

            mail.Subject = "HireDesk - Application Received";

            mail.Body =
                "Dear " + TextBox1.Text + ",\n\n" +
                "Thank you for applying through HireDesk.\n\n" +
                "We have received your application with the following details:\n\n" +
                "Name: " + TextBox1.Text + "\n" +
                "Email: " + TextBox2.Text + "\n" +
                "Contact: " + TextBox3.Text + "\n" +
                "Stream: " + DropDownList1.SelectedValue + "\n" +
                "Experience: " + DropDownList2.SelectedValue + "\n\n" +
                "Our HR team will contact you shortly.\n\n" +
                "Regards,\nHireDesk Team";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(fromEmail, password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedValue == "Others")
            {
                ShowAlert("Non-IT background not eligible");
                return;
            }

            if (IsDuplicateUser(TextBox2.Text, TextBox3.Text))
            {
                ShowAlert("You have already applied");
                return;
            }


            if (DropDownList2.SelectedValue == "Fresher")
            {
                
                string resumePath = "";

                if (FileUpload1.HasFile)
                {
                    string folderPath = Server.MapPath("~/Resumes/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(FileUpload1.FileName);
                    resumePath = Path.Combine(folderPath, fileName);

                    FileUpload1.SaveAs(resumePath);
                }

                int aid = AddFresherApplicant();

                Session["aid"] = aid;
                Session["resumePath"] = resumePath;   
                Session["userEmail"] = TextBox2.Text;
                Session["userName"] = TextBox1.Text;

                Response.Redirect("Slot.aspx");
            }

            else
            {
                AddExperiencedApplicant();
                SendMailToHR(true);
                SendMailToApplicant();
                ShowAlert("Application submitted successfully");
            }
        }

        
    }
}
