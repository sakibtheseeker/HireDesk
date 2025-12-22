<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume.aspx.cs" Inherits="HireDesk_Application.Resume" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
    .locked-form {
        pointer-events: none;  
        opacity: 0.6;           
    }
</style>

    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
</head>
<body>
    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-6">

                <form id="form1" runat="server"
                      class="border border-success bg-light p-4 rounded shadow-sm">
                    
                 <h2 class="text-center mb-4">HireDesk</h2>

                    <div class="form-group">
                        <label style="margin-left:17%">Stream</label>
                        <asp:DropDownList ID="DropDownList1" runat="server"
                            class="form-control col-md-8 mx-auto"  AutoPostBack="true"
    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem>BSC IT</asp:ListItem>
                            <asp:ListItem>BCA</asp:ListItem>
                            <asp:ListItem>B TECH</asp:ListItem>
                            <asp:ListItem>MCA</asp:ListItem>
                            <asp:ListItem>Others</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div id="formWrapper" runat="server">
                    <div class="form-group">
                        <label style="margin-left:17%">Are you?</label>
                        <asp:DropDownList ID="DropDownList2" runat="server"
                            class="form-control col-md-8 mx-auto" AutoPostBack="true"
OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"
>
                            <asp:ListItem>Fresher</asp:ListItem>
                            <asp:ListItem Selected="True">Experienced</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:17%">Name</label>
                        <asp:TextBox ID="TextBox1" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:17%">Email</label>
                        <asp:TextBox ID="TextBox2" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:17%">Contact</label>
                        <asp:TextBox ID="TextBox3" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>

                    <div id="salaryWrapper" runat="server">
                    <div class="form-group">
                        <label style="margin-left:17%">CTC</label>
                        <asp:TextBox ID="TextBox4" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:17%">ECTC</label>
                        <asp:TextBox ID="TextBox5" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:17%">Notice Period</label>
                        <asp:TextBox ID="TextBox6" runat="server"
                            class="form-control col-md-8 mx-auto"></asp:TextBox>
                    </div>
                    </div>
                    <div class="form-group">
                        <label style="margin-left:17%">Resume</label>
                        <asp:FileUpload ID="FileUpload1" runat="server"
                            class="form-control col-md-8 mx-auto" />
                    </div>

                    <div class="text-center">
                        <asp:Button ID="Button1" runat="server"
                            Text="Submit"
                            class="btn btn-success px-4" OnClick="Button1_Click" />
                    </div>
                          </div>
                </form>

            </div>
        </div>
    </div>
</body>
</html>
