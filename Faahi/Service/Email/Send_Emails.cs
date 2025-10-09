namespace Faahi.Service.Email
{
    public class Send_Emails
    {
        public static string EmailBody_site_users(string fullname, string siteUserCode, string plainTextPassword)
        {
            return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Welcome to Faahi</title>
    <style>
        body {{
            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 0;
            color: #333;
        }}
        .email-container {{
            max-width: 650px;
            margin: 40px auto;
            background-color: #fff;
            border-radius: 12px;
            box-shadow: 0 6px 24px rgba(0, 0, 0, 0.08);
            overflow: hidden;
            border: 1px solid #eaeaea;
        }}
        .email-header {{
            background: linear-gradient(90deg, #007bff, #00c6ff);
            color: white;
            padding: 30px;
            text-align: center;
        }}
        .email-header h1 {{
            margin: 0;
            font-size: 26px;
            letter-spacing: 0.5px;
        }}
        .email-body {{
            padding: 35px 40px;
            font-size: 16px;
            line-height: 1.7;
            color: #444;
        }}
        .email-body p {{
            margin-bottom: 18px;
        }}
        .credentials {{
            background-color: #f9fafc;
            border: 1px solid #dfe3e8;
            border-radius: 8px;
            padding: 20px;
            margin: 25px 0;
        }}
        .credentials p {{
            margin: 8px 0;
            font-weight: 500;
        }}
        .button {{
            display: inline-block;
            background-color: #28a745;
            color: #fff;
            padding: 14px 28px;
            border-radius: 50px;
            text-decoration: none;
            font-weight: bold;
            font-size: 16px;
            margin-top: 20px;
        }}
        .email-footer {{
            background-color: #f7f7f7;
            padding: 25px;
            text-align: center;
            font-size: 13px;
            color: #888;
        }}
        .email-footer a {{
            color: #007bff;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='email-header'>
            <h1>Welcome to Faahi</h1>
        </div>
        <div class='email-body'>
            <p>Hi <strong>{fullname}</strong>,</p>

            <p>Welcome to <strong>Faahi</strong>! 🎉<br>
            We're thrilled to have you join our platform.</p>

            <p>Your site user account has been successfully created. Below are your login credentials:</p>

            <div class='credentials'>
                <p><strong>Username:</strong> {siteUserCode}</p>
                <p><strong>Password:</strong> {plainTextPassword}</p>
            </div>

            <p>🔒 For your security, we recommend that you change your password after your first login.</p>

            <p>If you have any questions or need support, our team is always here to help.</p>

            <p style='text-align: center;'>
                <a href='https://faahi.com/login' class='button'>Login to Your Account</a>
            </p>
        </div>
        <div class='email-footer'>
            <p>&copy; {DateTime.Now.Year} Faahi. All rights reserved.</p>
            <p>Need help? <a href='mailto:support@faahi.com'>Contact Support</a></p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
