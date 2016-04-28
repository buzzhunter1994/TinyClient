﻿using FirstFloor.ModernUI.Windows.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TinyClient.Api
{
    class Common
    {
        public static PlayerWindow MusicPlayer = new PlayerWindow();
        public static MainWindow TinyMainWindow = new MainWindow();
        public static int[] PhotoSizes = { 0, 75, 130, 604, 807, 1280, 2560 };

        public static async Task<JToken> SendRequest(string method, string parameters = "", bool getRaw = false, string customToken = "", string ApiVersion = "5.37", string Lang ="")
        {
            string temp;
            string AccessToken = Properties.Settings.Default.AccessToken;

            Request:
            WebClient webClient1 = new WebClient();
            webClient1.Encoding = Encoding.UTF8;
            webClient1.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
            /*if (AccessToken.Length == 0)
                AccessToken = Properties.Settings.Default.AccessToken;*/
            string urls = "https://api.vk.com/method/" + method;
            try
            {
                temp = await webClient1.UploadStringTaskAsync(urls, String.Format("{0}&lang={1}&v={2}&access_token={3}", parameters, Lang, ApiVersion, customToken != "" ? customToken : AccessToken)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return null;
            }
            //TODO: Report method
            JObject a = await Task.Run(() => JObject.Parse(temp));
            //JObject a = await Task.Factory.StartNew(() => JObject.Parse(temp));
            if (getRaw)
                return a;
            if (a["error"] != null)
            {
                ModernDialog.ShowMessage(a["error"].ToString(), "TinyClient - VKAPI", MessageBoxButton.OK);
                //goto Request;
                //TODO: Implement errors 
                return null;
            }
            else
            {
                return a["response"];
            }

        }            
      /*
       If Not IsNothing(a("error")) Then
           Dim b = Await MyJsonConvert.DeserializeObjectAsync(Of types.VKError)(a("error").ToString)
            Select Case b.error_code
                Case 1
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError1 & " (" & method & ")")
                Case 2
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError2 & " (" & method & ")")
                Case 3
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError3 & " (" & method & ")")
                Case 4
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError4 & " (" & method & ")")
                Case 5
                    Forms.Application.Restart()
                    My.Settings.AccessToken = ""
                    My.Settings.Save()
                    My.Application.Shutdown()
                Case 6
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError6 & " (" & method & ")")
                Case 7
                    If(method = "messages.send") Then
                       ModernDialog.ShowMessage(My.Resources.ApiError_messageSend,
                                                 "MicroVK - VKAPI",
                                                MessageBoxButton.OK)
                    Else
                        If _
                            ModernDialog.ShowMessage(
                                My.Resources.ApiError7 & " (" & method & ")" & vbNewLine & My.Resources.ApiError7_1,
                                "MicroVK - VKAPI",
                                MessageBoxButton.YesNo) = MessageBoxResult.Yes Then
                            Forms.Application.Restart()
                            My.Settings.AccessToken = ""
                            My.Settings.Save()
                            My.Application.Shutdown()
                        End If
                    End If
                Case 8
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError8 & " (" & method & ")")
                Case 9
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError9 & " (" & method & ")")
                Case 10
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError10 & " (" & method & ")")
                Case 11
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError11 & " (" & method & ")")
                Case 14
                    If Not IsCaptchaShow Then
                        IsCaptchaShow = True
                        Dim stackPanel1 As New StackPanel
                        Dim textCaptcha1 As New TextBox
                        stackPanel1.Children.Add(New Image With {
                                                    .Source =
                                                    New BitmapImage(New Uri(a("error")("captcha_img").ToString)),
                                                    .Stretch = Stretch.Uniform,
                                                    .Width = 130,
                                                    .Height = 50,
                                                    .HorizontalAlignment = HorizontalAlignment.Left})
                        stackPanel1.Children.Add(textCaptcha1)
                        Call New ModernDialog() With {.Title = My.Resources.ApiError14,
                            .Content = stackPanel1
    }.ShowDialog()
                        IsCaptchaShow = False
                        parameters = parameters & "&captcha_sid=" & a("error")("captcha_sid").ToString & "&captcha_key=" &
                                     textCaptcha1.Text
                        GoTo Label1
                    End If
                Case 15
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError15 & " (" & method & ")")
                Case 16
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError16 & " (" & method & ")")
                Case 17
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError17 & " (" & method & ")")
                Case 20
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError20 & " (" & method & ")")
                Case 21
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError21 & " (" & method & ")")
                Case 23
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError23 & " (" & method & ")")
                Case 100
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError100 & " (" & method & ")")
                Case 101
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError101 & " (" & method & ")")
                Case 113
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError113 & " (" & method & ")")
                Case 150
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError150 & " (" & method & ")")
                Case 200
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError200 & " (" & method & ")")
                Case 201
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError201 & " (" & method & ")")
                Case 203
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError203 & " (" & method & ")")
                Case 300
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError300 & " (" & method & ")")
                Case 500
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError500 & " (" & method & ")")
                Case 600
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError600 & " (" & method & ")")
                Case 603
                    ShowMicroVkNot("MicroVK - VKAPI", My.Resources.ApiError603 & " (" & method & ")")
                Case Else
                    MsgMui(
                        "error_code: " & b.error_code & vbNewLine & "error_msg: " & b.error_msg & vbNewLine &
                        "request_params: " & Newtonsoft.Json.JsonConvert.SerializeObject(b.request_params))
            End Select
            Return Nothing
        Else
            Return a("response")
        End If
    End Function*/
        public static async Task<T> getResponse<T>(string method, string [,]args)
        {
            string p = "";
            string q = "";
            string path = "";
            if (args != null)
            {
                for (int i = 0; i < args.GetLength(0); i++)
                {
                 /*   if (!String.IsNullOrEmpty(args[i, 0]))
                    {*/
                        if (!String.IsNullOrEmpty(args[i, 1]))
                        {
                            p = String.Format("{0}={1}&", p + args[i, 0], args[i, 1]);
                        }
                        else
                        {
                            p = String.Format("{0}&", p + args[i, 0]);
                        }
                   // }
                }
            }
            Console.WriteLine(p);
            /*
            If args IsNot Nothing Then
               For i = 0 To args.GetLength(0) - 1
                    If Not args(i, 1).IsNullOrEmpty() Then
                        If Not String.IsNullOrEmpty(args(i, 0)) Then
                            p = String.Format("{0}={1}&", p + args(i, 0), args(i, 1))
                        Else
                            p = String.Format("{0}&", p + args(i, 1))
                        End If
                    End If
                Next
            End If
                
                */
            if (method.IndexOf('@') > 0)
            {
                q = method.Split('@')[0];
                path = method.Split('@')[1];
            }
            else
            {
                q = method;
            }
            JToken response = await SendRequest(q, p);
            if (response!= null && response.ToString() != "")
            {
                if (path == "")
                {
                    return await MyJsonConvert.DeserializeObjectAsync<T>(response.ToString());
                }
                else
                {
                    return await MyJsonConvert.DeserializeObjectAsync<T>(response[path].ToString());
                }
            }
            else
            {
                return default(T);
            }
        }
    }
}
