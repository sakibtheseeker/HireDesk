<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="HireDesk_Application.ThankYou" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>HireDesk - Thank You</title>
    <link rel="stylesheet"
          href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css">
</head>
<body>

<div class="container">
    <div class="row justify-content-center mt-5">
        <div class="col-md-6 text-center">

            <div class="border border-success p-4 rounded bg-light shadow">
                <h2 class="text-success">Thank You!</h2>
                <p class="mt-3">
                    Your application and interview slot have been successfully submitted.
                </p>
                <p>
                    Our HR team will contact you shortly.
                </p>

                <a href="Resume.aspx" class="btn btn-success mt-3">
                    Back to Home
                </a>
            </div>

        </div>
    </div>
</div>

</body>
</html>
