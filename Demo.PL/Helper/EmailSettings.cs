﻿using Demo.DAL.Entities;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client =new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("rita.2000489@gmail.com", "wmkyujnjzwlaphly");
            client.Send("rita.2000489@gmail.com", email.To, email.Title, email.Body);
        }
    }
}
