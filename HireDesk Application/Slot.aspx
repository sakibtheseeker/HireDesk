<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Slot.aspx.cs" Inherits="HireDesk_Application.Slot" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>HireDesk - Slot Booking</title>
    <link rel="stylesheet"
          href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css"
          integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
          crossorigin="anonymous">
</head>
<body>

    <div class="container">
        <div class="row justify-content-center mt-5">
            <div class="col-md-6">

                <form id="form1" runat="server"
                      class="border border-success bg-light p-4 rounded shadow-sm">

                    <h2 class="text-center mb-4">HireDesk</h2>

                    <div class="form-group">
                        <label style="margin-left:25%">Day</label>
                        <asp:DropDownList ID="DropDownList1" runat="server"
                            class="form-control col-md-6 mx-auto">
                            <asp:ListItem>Monday</asp:ListItem>
                            <asp:ListItem>Tuesday</asp:ListItem>
                            <asp:ListItem>Wednesday</asp:ListItem>
                            <asp:ListItem>Thursday</asp:ListItem>
                            <asp:ListItem>Friday</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <div class="form-group">
                        <label style="margin-left:25%">Time</label>
                        <asp:DropDownList ID="DropDownList2" runat="server"
                            class="form-control col-md-6 mx-auto">
                            <asp:ListItem>10 - 11 AM</asp:ListItem>
                            <asp:ListItem>11 - 12 PM</asp:ListItem>
                            <asp:ListItem>1 - 2 PM</asp:ListItem>
                            <asp:ListItem>2 - 3 PM</asp:ListItem>
                            <asp:ListItem>3 - 4 PM</asp:ListItem>
                            <asp:ListItem>4 - 5 PM</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <br />
                    <div class="text-center">
                        <asp:Button ID="Button1" runat="server"
                            Text="Book Slot"
                            class="btn btn-success px-4" OnClick="Button1_Click1" />
                    </div>

                </form>

            </div>
        </div>
    </div>

</body>
</html>
