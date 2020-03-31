using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace FindMyDinner.Contracts.Comman
{
  public  class SendOtp
    {
        public void SendMessage(int MobileNumber)
        {
            Random r = new Random();
            string OTP = r.Next(100000, 999999).ToString();

            //Send message        
            string APIKey = "A6epy9Hh3+E-cKyfJMgGXx2ghc81GNp9lG5iPdCwQ5";//This may vary api to api. like ite may be password, secrate key, hash etc
            string SenderName = "Test";
            string Number = MobileNumber.ToString();// "919148513322";
            string Message = "Your OTP code is - " + OTP + SenderName;
            String enmessage = HttpUtility.UrlEncode(Message);
            using (var wb = new WebClient())
            {
                byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , APIKey},
                {"numbers" , Number},
                {"message" , enmessage},
                {"sender" , "TXTLCL"}
                });
                string result = System.Text.Encoding.UTF8.GetString(response);
                
            }
        }
        //public bool ValidateOTP(int OTP)
        //{
        //    if (OTP == Session["OTP"])
        //        return true;
        //    else return false;
        //}
    }
}
