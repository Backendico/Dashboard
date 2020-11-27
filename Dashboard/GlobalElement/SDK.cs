using Dashboard.Dashboards.Dashboard_Game.SubPages;
using Dashboard.Dashboards.Dashboard_Game.SubPages.Elements;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dashboard.GlobalElement
{
    public sealed class SDK
    {
        internal sealed class SDK_PageAUT
        {
            public static ModelLinks.LinkAUT Links;

            public async static void Login(string Username, string Password, Action<bool, string> Result)
            {
                var client = new RestClient(Links.Login);
                client.Timeout = -1;
                client.ClearHandlers();
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Username", Username);
                request.AddParameter("Password", Password);
                var ResultReqeust = await client.ExecuteAsync(request);

                if (ResultReqeust.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SettingUser.Token = ResultReqeust.Content;
                    Result(true, ResultReqeust.Content);
                }
                else
                {
                    Result(false, "");
                }

            }

            public async static void Register(string Username, string Password, string Email, string Phone, Action<string> Result, Action ERR)
            {
                var client = new RestClient(Links.Register);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                client.ClearHandlers();
                request.AddParameter("Username", Username);
                request.AddParameter("Password", Password);
                request.AddParameter("Email", Email);
                request.AddParameter("Phone", Phone);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    SettingUser.Token = response.Content;

                    Result(response.Content);
                }
                else
                {
                    ERR();
                }
            }

            public async static void CheackUsername(string Username, Action<bool> Result)
            {
                var client = new RestClient(Links.CheackUsername);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Username", Username);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && bool.Parse(response.Content))
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }

            public async static void Recovery1(MailAddress Email, Action<bool> Result)
            {
                var client = new RestClient(Links.Recovery1);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Email", Email.Address);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }

            public async static void Recovery2(MailAddress Email, int Code, Action<bool> Result)
            {
                var client = new RestClient(Links.Recovery2);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Email", Email.Address);
                request.AddParameter("Code", Code.ToString());
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }

            public async static void Recovery3(MailAddress Email, int Code, string NewPassword, Action<bool> Result)
            {
                var client = new RestClient(Links.Recovery3);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Email", Email.Address);
                request.AddParameter("Code", Code.ToString());
                request.AddParameter("NewPassword", NewPassword);
                var response = await client.ExecuteAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Result(true);
                }
                else
                {
                    Result(false);
                }
            }
        }


        internal sealed class SDK_PageDashboards
        {
            public sealed class DashboardGame
            {
                public sealed class PageStudios
                {
                    public static ModelLinks.DashboardGame.PageStudios Links;

                    public static async void ReciveStudios(Action<BsonDocument> Result, Action Err)
                    {
                        var client = new RestClient(Links.ReciveStudio);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        client.ClearHandlers();
                        request.AddParameter("Token", SettingUser.Token);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            var Deserilsedata = BsonDocument.Parse(response.Content);
                            Result(Deserilsedata);
                        }
                        else
                        {
                            Err();
                        }
                    }

                    public static async void CreatStudio(string NameStudio, Action<bool> Result)
                    {
                        var client = new RestClient(Links.CreatStudio);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("NameStudio", NameStudio);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }


                    }

                    public static async void Status(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.Status);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("NameStudio", SettingUser.CurentDetailStudio["Database"].ToString());
                        var response = await client.ExecuteAsync<BsonDocument>(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }


                    }

                    public static async void Delete(Action Result, Action ERR)
                    {
                        var client = new RestClient("https://localhost:44346/ChoiceStudiogame/Delete");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("NameStudio", SettingUser.CurentDetailStudio["Database"].ToString());
                        var response = await client.ExecuteAsync<BsonDocument>(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void ReciveMonetize(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.ReciveMonetiz);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("NameStudio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void RecivePaymentList(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.RecivePaymentList);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("NameStudio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {

                            ERR();
                        }

                    }

                    public static async void AddPayment(BsonDocument Details, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddPayment);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("DetailMonetize", Details.ToString());
                        request.AddParameter("NameStudio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void AddPaytoList(BsonDocument PayementDetail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.PaymentAddToList);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Detail", PayementDetail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void OpenGatePaye(BsonDocument PaymentRequest, Action<BsonDocument> Result)
                    {
                        var client = new RestClient(Links.LinkIDPAY);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("X-API-KEY", "a14190c5-c321-4a93-bdcc-e9f753608e00");
                        //request.AddHeader("X-SANDBOX", "1");
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("Cookie", "SSESS39ff69be91203b0b4d2039dd7a713620=7epaMgKagAqyX9SlMEc4j3MKve3PrWsPwYQQ5J0re20");
                        request.AddParameter("application/json", PaymentRequest.ToString(), ParameterType.RequestBody);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }
                    }

                    public static async Task<BsonDocument> CheackPay(BsonDocument PaymentDetail)
                    {
                        var client = new RestClient(Links.CheackPay);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("X-API-KEY", "a14190c5-c321-4a93-bdcc-e9f753608e00");
                        //request.AddHeader("X-SANDBOX", "1");
                        request.AddHeader("Cookie", "SSESS39ff69be91203b0b4d2039dd7a713620=7epaMgKagAqyX9SlMEc4j3MKve3PrWsPwYQQ5J0re20");
                        request.AddParameter("application/json", PaymentDetail.ToString(), ParameterType.RequestBody);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            return BsonDocument.Parse(response.Content);
                        }
                        else
                        {
                            return new BsonDocument();
                        }
                    }

                    public static async void VerifiePay(BsonDocument DetailPay, Action<bool> Result)
                    {
                        var client = new RestClient(Links.VerifiePay);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Detail", DetailPay.ToString());

                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void GenerateNewToken(Action<bool> Result)
                    {
                        var client = new RestClient(Links.GenerateNewToken);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                }

                public sealed class PagePlayers
                {
                    public static ModelLinks.DashboardGame.PagePlayers Links;
                    public static async void ReciveListPlayer(int Count, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.ReciveListPlayer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].AsString);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Count", Count.ToString());
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void CreatPlayer(string Username, string Password, Action Result, Action ERR)
                    {
                        var client = new RestClient(Links.CreatPlayer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Username", Username);
                        request.AddParameter("Password", Password);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            ERR();
                        }

                    }


                    /// <summary>
                    /// Return bool
                    /// </summary>
                    /// <param name="Username"></param>
                    /// <param name="Result">
                    /// <list type="number">
                    /// <item>
                    /// find true
                    /// </item>
                    /// <item>
                    /// not find false
                    /// </item>
                    /// </list>
                    /// </param>
                    public static async void SearchUsername(string Username, Action<bool> Result)
                    {
                        var client = new RestClient(Links.SearchUsername);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Username", Username);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    /// <summary>
                    /// </summary>
                    /// <param name="Username"></param>
                    /// <param name="Result">
                    /// <list type="number">
                    /// <item>
                    /// find detail player
                    /// </item>
                    /// <item>
                    /// fail
                    /// </item>
                    /// </list>
                    /// </param>
                    public static async void SearchUsername(string Username, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.SearchUsername);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Username", Username);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void SearchEmail(string Email, Action<BsonDocument> Result, Action ERR)
                    {
                        try
                        {
                            new System.Net.Mail.MailAddress(Email);

                            var client = new RestClient(Links.SearchEmail);
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AlwaysMultipartFormData = true;
                            client.ClearHandlers();
                            request.AddParameter("Token", SettingUser.Token);
                            request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                            request.AddParameter("Email", Email);
                            var response = await client.ExecuteAsync(request);


                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Result(BsonDocument.Parse(response.Content));
                            }
                            else
                            {
                                ERR();
                            }

                        }
                        catch (Exception)
                        {
                            ERR();
                        }
                    }

                    public static async void SearchToken(string Token, Action<BsonDocument> Result, Action ERR)
                    {
                        _ = new ObjectId();

                        if (ObjectId.TryParse(Token, out _))
                        {
                            var client = new RestClient(Links.SearchToken);
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AlwaysMultipartFormData = true;
                            client.ClearHandlers();
                            request.AddParameter("Token", SettingUser.Token);
                            request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                            request.AddParameter("TokenPlayer", Token);
                            var response = await client.ExecuteAsync(request);

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Result(BsonDocument.Parse(response.Content));
                            }
                            else
                            {
                                ERR();
                            }
                        }
                        else
                        {
                            ERR();
                        }


                    }

                    public static async void Delete(string TokenPlayer, Action<bool> Result)
                    {
                        var client = new RestClient(Links.Delete);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);

                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void Save(string TokenPlayer, BsonDocument PlayerDetail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.Save);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer);
                        request.AddParameter("AccountDetail", PlayerDetail["Account"].ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void Save_LeaderboardPlayer(string TokenPlayer, BsonDocument DetailLeaderboard, Action Result, Action Err)
                    {
                        var client = new RestClient(Links.Save_LeaderboardPlayer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer);
                        request.AddParameter("DetailLeaderboard", DetailLeaderboard.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            Err();
                        }

                    }

                    public static async void RecivePlayerlog(ObjectId TokenPlayer, int Count, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.RecivePlayerLogs);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Count", Count);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                            ERR();
                        }
                    }

                    public static async void AddLogPlayer(ObjectId TokenPlayer, string Header, string Description, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddPlayerlog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Header", Header);
                        request.AddParameter("Description", Description);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void ClearLog(ObjectId TokenPlayer, Action<bool> Result)
                    {
                        var client = new RestClient(Links.ClearLog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void Recovery1(ObjectId TokenPlayer, MailAddress Email, Action<int> CodeRecovery)
                    {

                        var client = new RestClient(Links.Recovery1);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Email", Email.Address);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            CodeRecovery(int.Parse(response.Content));
                        }
                        else
                        {
                            CodeRecovery(0);
                        }

                    }

                    public static async void Recovery2(ObjectId TokenPlayer, MailAddress Email, int Code, Action<bool> CodeRecovery)
                    {
                        var client = new RestClient(Links.Recovery2);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Email", Email.Address);
                        request.AddParameter("Code", Code);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            CodeRecovery(true);
                        }
                        else
                        {
                            CodeRecovery(false);
                        }

                    }

                    public static async void ChangePassword(ObjectId TokenPlayer, string Password, Action<bool> CodeRecovery)
                    {
                        var client = new RestClient(Links.ChangePassword);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Password", Password);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            CodeRecovery(true);
                        }
                        else
                        {
                            CodeRecovery(false);
                        }

                    }

                }

                public sealed class PageLeaderboard
                {
                    public static ModelLinks.DashboardGame.PageLeaderboard Links;


                    public static async void Reciveleaderboards(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.ReciveLeaderboards);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Token", SettingUser.Token);
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(BsonDocument.Parse(response.Content));
                            ERR();
                        }

                    }

                    public static async void Creat(string NameLeaderboard, int reset, int Amount, int Sort, int Min, int Max, Action<bool> Result)
                    {
                        var SeriliseDetail = new BsonDocument
                        {
                            { "Name", NameLeaderboard },
                            { "Reset", reset },
                            { "Sort", Sort } ,
                            {"Amount",Amount },
                            {"Min" ,Min},
                            {"Max",Max }
                        };

                        var client = new RestClient(Links.Creat);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("DetailLeaderboard", SeriliseDetail.ToString());
                        var response = await client.ExecuteAsync(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void Cheackname(string NameLeaderboard, Action<bool> Result)
                    {
                        var client = new RestClient(Links.CheackName);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderbaord", NameLeaderboard);
                        var response = await client.ExecuteAsync(request);

                        if (bool.Parse(response.Content) && response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void EditLeaderboard(BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.Editleaderboard);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Detailleaderboard", Detail.ToString());

                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void AddPlayer(string NameLeaderboard, string TokenPlayer, int Value, Action Result, Action ERR)
                    {
                        //cheack tokenplayer
                        if (ObjectId.TryParse(TokenPlayer, out _))
                        {
                            //send data
                            var client = new RestClient(Links.Add);
                            client.Timeout = -1;
                            var request = new RestRequest(Method.POST);
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("Token", SettingUser.Token);
                            request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                            request.AddParameter("TokenPlayer", TokenPlayer);
                            request.AddParameter("NameLeaderboard", NameLeaderboard);
                            request.AddParameter("Value", Value);
                            var response = await client.ExecuteAsync(request);

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Result();
                            }
                            else
                            {
                                ERR();
                            }

                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void Remove(string TokenPlayer, string NameLeaderboard, Action Result, Action ERR)
                    {

                        if (ObjectId.TryParse(TokenPlayer, out _))
                        {
                            //send data
                            var client = new RestClient(Links.Remove);
                            client.Timeout = -1;
                            var request = new RestRequest(Method.DELETE);
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("Token", SettingUser.Token);
                            request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                            request.AddParameter("TokenPlayer", TokenPlayer);
                            request.AddParameter("NameLeaderboard", NameLeaderboard);
                            var response = await client.ExecuteAsync(request);

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                Result();
                            }
                            else
                            {
                                ERR();
                            }

                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void Reset(string NameLeaderboard, Action Result, Action ERR)
                    {
                        //send data
                        var client = new RestClient(Links.Reset);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void Leaderboard(int Count, string NameLeaderboard, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.Leaderboard);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        request.AddParameter("Count", Count);
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void Backup(string NameLeaderboard, Action Result, Action ERR)
                    {
                        var client = new RestClient(Links.Backup);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void BackupRecive(string NameLeaderboard, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.ReciveBackup);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void RemoveBackup(string NameLeaderboard, string Version, Action Result, Action ERR)
                    {
                        var client = new RestClient(Links.RemoveBackup);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.DELETE);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        request.AddParameter("Version", Version);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result();
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public static async void DownloadBackup(string NameLeaderboard, string Version, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.DownloadBackup);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("NameLeaderboard", NameLeaderboard);
                        request.AddParameter("Version", Version);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(BsonDocument.Parse(response.Content));
                            ERR();
                        }

                    }
                }

                public sealed class PageAchievements
                {
                    public static ModelLinks.DashboardGame.PageAchievements Links = new ModelLinks.DashboardGame.PageAchievements();

                    public static async void AddAchievements(string Name, long Value, Action<bool> Result, Action ERR)
                    {
                        var serilese = new BsonDocument { { "Name", Name }, { "Value", Value } };

                        var client = new RestClient(Links.AddAchievements);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Detail", serilese.ToString());

                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            ERR();
                            Result(false);
                        }

                    }

                    public static async void CheackAchievements(string Textname, Action<bool> Result)
                    {
                        var client = new RestClient(Links.CheackNameAchievements);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("NameAchievements", Textname);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void ReciveAchievements(Action<BsonDocument> Result)
                    {
                        var client = new RestClient(Links.ReciveAchievements);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }
                    }

                    public static async void EditAchievements(ObjectId TokenAchievements, BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.EditAchievements);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenAchievements", TokenAchievements.ToString());
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void AddPlayerAchievements(ObjectId TokenPlayer, BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddPlayerAchievements);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.PUT);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void RecivePlayersAchivementsList(ObjectId TokenAchievements, int Count, Action<BsonDocument> Result)
                    {
                        var client = new RestClient(Links.RecivePlayersAchivementsList);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenAchievement", TokenAchievements.ToString());
                        request.AddParameter("Count", Count.ToString());
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }

                    }

                    public static async void RemoveAchievementsPlayer(ObjectId TokenPlayer, BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.RemoveAchievementsPlayer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void RemoveAchievements(BsonDocument DetailAchievements, Action<bool> Result)
                    {
                        var client = new RestClient(Links.RemoveAchievements);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Detail", DetailAchievements.ToString());
                        var response = await client.ExecuteAsync(request);

                        Debug.WriteLine(response.Content);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void PlayerAchievements(ObjectId TokenPlayer, Action<BsonDocument> Result)
                    {
                        var client = new RestClient(Links.PlayerAchievements);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AddParameter("Token", SettingUser.Token.ToString());
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("TokenPlayer", TokenPlayer.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }

                    }

                }

                public sealed class PageLog
                {
                    public static ModelLinks.DashboardGame.PageLog Links;
                    public static async void ReciveLog(int Count, Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.LinkReciveLog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Count", Count);
                        var response = await client.ExecuteAsync(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void AddLog(string header, string Description, BsonDocument Detail, bool IsNotifaction, Action<bool> Result)
                    {
                        var client = new RestClient(Links.LinkAddLog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Header", header);
                        request.AddParameter("Description", Description);
                        request.AddParameter("Detail", Detail.ToString());
                        request.AddParameter("IsNotifaction", IsNotifaction.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void DeleteLog(BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.LinkDeletLog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.DELETE);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);


                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void MarkNotifactionasRead(Action<bool> Result)
                    {
                        var client = new RestClient("http://193.141.64.203/Log/MarkReadNotifaction");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Token", SettingUser.Token);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }
                }

                public sealed class PageDashboard
                {
                    public static ModelLinks.DashboardGame.PageDashboard Links;
                    public async static void ReciveDetail(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.LinkDashboard);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }

                    }

                    public async static void CheackStatusServer(Action<bool> Result)
                    {
                        var client = new RestClient(Links.CheackStatusServer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.GET);
                        IRestResponse response = await client.ExecuteAsync(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public async static void Notifaction(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.Notifaction);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(BsonDocument.Parse(response.Content));
                            ERR();
                        }
                    }

                    public async static void CheackUpdate(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.CheackUpdate);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                            ERR();
                        }
                    }

                }

                public sealed class PageSupport
                {
                    public static ModelLinks.DashboardGame.PageSupport Links;

                    public static async void AddSupport(BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.LinkAddSupport);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void ReciveSupports(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.LinkReciveSupport);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            ERR();
                        }
                    }

                    public static async void AddMessage(ObjectId TokenSupport, BsonDocument DetailMessage, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddMessage);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.PUT);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenSupport", TokenSupport.ToString());
                        request.AddParameter("Detail", DetailMessage.ToString());
                        var response = await client.ExecuteAsync(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void CloseSupport(string TokenSupport, Action<bool> Result)
                    {
                        var client = new RestClient(Links.CloseSupport);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenSupport", TokenSupport);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }

                    public static async void AddReportBug(BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddReportBug);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }
                    }


                }

                public sealed class PageStore
                {
                    public static ModelLinks.DashboardGame.PageStore Links;

                    public static async void AddStore(BsonDocument Detail, Action<bool> Result)
                    {
                        var client = new RestClient(Links.AddStore);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("Detail", Detail.ToString());
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(true);
                        }
                        else
                        {
                            Result(false);
                        }

                    }

                    public static async void ReciveStore(Action<BsonDocument> Result)
                    {

                        var client = new RestClient(Links.ReciveStore);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        var response = await client.ExecuteAsync(request);

                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }
                    }
                    public static async void ReciveProduct(ObjectId TokenStore, Action<BsonDocument> Result)
                    {
                        var client = new RestClient(Links.ReciveProduct);
                        client.Timeout = -1;
                        client.ClearHandlers();
                        var request = new RestRequest(Method.POST);
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Token", SettingUser.Token);
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"]);
                        request.AddParameter("TokenStore", TokenStore.ToString());
                        IRestResponse response = await client.ExecuteAsync(request);
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            Result(BsonDocument.Parse(response.Content));
                        }
                        else
                        {
                            Result(new BsonDocument());
                        }
                    }

                }

            }
        }
    }


    public struct ModelLinks
    {

        public struct LinkAUT
        {
            public string BaseLink => "http://193.141.64.203/";

            public string Login => BaseLink + "AUT/Login";
            public string Register => BaseLink + "AUT/Register";
            public string CheackUsername => BaseLink + "AUT/CheackUsername";
            public string Recovery1 => BaseLink + "AUT/Recovery1";
            public string Recovery2 => BaseLink + "AUT/Recovery2";
            public string Recovery3 => BaseLink + "AUT/Recovery3";

        }

        public struct DashboardGame
        {

            public struct PageStudios
            {
                public string BaseLink => "http://193.141.64.203/";

                public string ReciveStudio => BaseLink + "ChoiceStudioGame/ReciveStudios";
                public string CreatStudio => BaseLink + "ChoiceStudioGame/CreatNewStudio";
                public string Status => BaseLink + "ChoiceStudiogame/Status";
                public string AddPayment => BaseLink + "ChoiceStudioGame/AddPayment";
                public string ReciveMonetiz => BaseLink + "ChoiceStudioGame/ReciveMonetize";
                public string RecivePaymentList => BaseLink + "ChoiceStudioGame/RecivePaymentList";

                public string LinkIDPAY => "https://api.idpay.ir/v1.1/payment";
                public string PaymentAddToList => BaseLink + "payments/AddPaymentToList";

                public string CheackPay => "https://api.idpay.ir/v1.1/payment/inquiry";

                public string VerifiePay => BaseLink + "Payments/VerifiePay";

                public string GenerateNewToken => BaseLink + "ChoiceStudioGame/GenerateToken";
            }

            public struct PagePlayers
            {
                public string BaseLink => "http://193.141.64.203/";

                public string ReciveListPlayer => BaseLink + "PagePlayer/ReciveDetailPagePlayer";
                public string CreatPlayer => BaseLink + "PagePlayer/CreatPlayer";
                public string SearchUsername => BaseLink + "PagePlayer/SearchUsername";
                public string SearchEmail => BaseLink + "PagePlayer/SearchEmail";
                public string SearchToken => BaseLink + "PagePlayer/SearchToken";
                public string Delete => BaseLink + "PagePlayer/DeletePlayer";
                public string Save => BaseLink + "PagePlayer/SavePlayer";
                public string Save_LeaderboardPlayer => BaseLink + "PagePlayer/Save_LeaderboardPlayer";
                public string RecivePlayerLogs => BaseLink + "PagePlayer/RecivePlayerLogs";
                public string AddPlayerlog => BaseLink + "PagePlayer/AddPlayerLog";
                public string ClearLog => BaseLink + "PagePlayer/ClearLogs";
                public string Recovery1 => BaseLink + "PagePlayer/RecoveryStep1";
                public string Recovery2 => BaseLink + "PagePlayer/RecoveryStep2";
                public string ChangePassword => BaseLink + "PagePlayer/ChangePassword";
            }

            public struct PageLeaderboard
            {
                public string BaseLink => "http://193.141.64.203/";

                public string ReciveLeaderboards => BaseLink + "PageLeaderBoard/ReciveLeaderboards";
                public string Creat => BaseLink + "PageLeaderBoard/CreatLeaderBoard";
                public string CheackName => BaseLink + "PageLeaderBoard/CheackLeaderboardName";
                public string Editleaderboard => BaseLink + "PageLeaderBoard/EditLeaderBoard";
                public string Add => BaseLink + "PageLeaderBoard/Add";
                public string Remove => BaseLink + "PageLeaderBoard/Remove";
                public string Reset => BaseLink + "PageLeaderBoard/Reset";
                public string Backup => BaseLink + "PageLeaderBoard/Backup";
                public string ReciveBackup => BaseLink + "PageLeaderBoard/ReciveBackup";
                public string RemoveBackup => BaseLink + "PageLeaderBoard/RemoveBackup";
                public string DownloadBackup => BaseLink + "PageLeaderBoard/DownloadBackup";
                public string Leaderboard => BaseLink + "PageLeaderBoard/Leaderboard";
            }

            public struct PageLog
            {
                public string BaseLink => "http://193.141.64.203/";

                public string LinkReciveLog => BaseLink + "Log/ReciveLogs";
                public string LinkAddLog => BaseLink + "Log/AddLog";
                public string LinkDeletLog => BaseLink + "Log/DeleteLog";
            }

            public struct PageAchievements
            {

                public string BaseLink => "http://193.141.64.203/";
                public string AddAchievements => BaseLink + "PageAchievements/AddAchievements";
                public string CheackNameAchievements => BaseLink + "PageAchievements/CheackNameAchievements";
                public string ReciveAchievements => BaseLink + "PageAchievements/ReciveAchievements";
                public string EditAchievements => BaseLink + "PageAchievements/EditAchievements";
                public string AddPlayerAchievements => BaseLink + "PageAchievements/AddPlayerAchievements";
                public string RecivePlayersAchivementsList => BaseLink + "PageAchievements/ReciveAchievementsPlayerList";
                public string RemoveAchievementsPlayer => BaseLink + "PageAchievements/RemoveAchievementPlayer";
                public string RemoveAchievements => BaseLink + "PageAchievements/RemoveAchievements";
                public string PlayerAchievements => BaseLink + "PageAchievements/PlayerAchievements";
            }

            public struct PageDashboard
            {
                public string BaseLink => "http://193.141.64.203/";

                public string LinkDashboard => BaseLink + "PageDashboard/ReciveDetail";
                public string CheackStatusServer => BaseLink + "PageDashboard/CheackStatusServer";
                public string Notifaction => BaseLink + "PageDashboard/Notifaction";
                public string CheackUpdate => BaseLink + "PageDashboard/CheackUpdate";

            }

            public struct PageSupport
            {
                public string BaseLink => "http://193.141.64.203/";

                public string LinkAddSupport => BaseLink + "PageSupport/AddSupport";
                public string LinkReciveSupport => BaseLink + "PageSupport/ReciveSupports";
                public string AddMessage => BaseLink + "PageSupport/AddMessage";
                public string CloseSupport => BaseLink + "PageSupport/CloseSupport";
                public string AddReportBug => BaseLink + "PageSupport/AddReportBug";
            }

            public struct PageStore
            {
                public string BaseLink => "http://193.141.64.203/";

                public string AddStore => BaseLink + "PageStore/AddStore";
                public string ReciveStore => BaseLink + "PageStore/ReciveStores";

                public string ReciveProduct => BaseLink + "PageStore/ReciveProduct";

            }
        }
    }
}

