﻿using MongoDB.Bson;
using RestSharp;
using System;
using System.Diagnostics;

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
                            Result(BsonDocument.Parse(response.Content.ToString()));
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
                        var client = new RestClient("https://localhost:44346/ChoiceStudioGame/ReciveMonetize");
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

                        if (response.StatusCode==System.Net.HttpStatusCode.OK)
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
                    public static async void ReciveListPlayer(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.ReciveListPlayer);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("Studio", SettingUser.CurentDetailStudio["Database"].AsString);
                        request.AddParameter("Token", SettingUser.Token);
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
                            ERR();
                        }

                    }

                    public static async void Creat(string NameLeaderboard, int reset, int Sort, Action<bool> Result)
                    {
                        var SeriliseDetail = new BsonDocument { { "Name", NameLeaderboard }, { "Reset", reset }, { "Sort", Sort } };

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
                            ERR();
                        }

                    }
                }

                public sealed class PageLog
                {
                    public static ModelLinks.DashboardGame.PageLog Links;
                    public static async void ReciveLog(Action<BsonDocument> Result, Action ERR)
                    {
                        var client = new RestClient(Links.LinkReciveLog);
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        client.ClearHandlers();
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
                            ERR();
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
                        var client = new RestClient("https://localhost:44346/PageDashboard/CheackStatusServer");
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
                }

            }
        }
    }


    public struct ModelLinks
    {
        public struct LinkAUT
        {
            public string Login => "https://localhost:44346/AUT/Login";
            public string Register => "https://localhost:44346/AUT/Register";
            public string CheackUsername => "https://localhost:44346/AUT/CheackUsername";

        }

        public struct DashboardGame
        {
            public struct PageStudios
            {
                public string ReciveStudio => "https://localhost:44346/ChoiceStudioGame/ReciveStudios";
                public string CreatStudio => "https://localhost:44346/ChoiceStudioGame/CreatNewStudio";
                public string Status => "https://localhost:44346/ChoiceStudiogame/Status";
                public string AddPayment => "https://localhost:44346/ChoiceStudioGame/AddPayment";
            }

            public struct PagePlayers
            {
                public string ReciveListPlayer => "https://localhost:44346/PagePlayer/ReciveDetailPagePlayer";
                public string CreatPlayer => "https://localhost:44346/PagePlayer/CreatPlayer";
                public string SearchUsername => "https://localhost:44346/PagePlayer/SearchUsername";
                public string SearchEmail => "https://localhost:44346/PagePlayer/SearchEmail";
                public string SearchToken => "https://localhost:44346/PagePlayer/SearchToken";
                public string Delete => "https://localhost:44346/PagePlayer/DeletePlayer";
                public string Save => "https://localhost:44346/PagePlayer/SavePlayer";
                public string Save_LeaderboardPlayer => "https://localhost:44346/PagePlayer/Save_LeaderboardPlayer";

            }

            public struct PageLeaderboard
            {
                public string ReciveLeaderboards => "https://localhost:44346/PageLeaderBoard/ReciveLeaderboards";
                public string Creat => "https://localhost:44346/PageLeaderBoard/CreatLeaderBoard";
                public string CheackName => "https://localhost:44346/PageLeaderBoard/CheackLeaderboardName";
                public string Editleaderboard => "https://localhost:44346/PageLeaderBoard/EditLeaderBoard";
                public string Add => "https://localhost:44346/PageLeaderBoard/Add";
                public string Remove => "https://localhost:44346/PageLeaderBoard/Remove";
                public string Reset => "https://localhost:44346/PageLeaderBoard/Reset";
                public string Backup => "https://localhost:44346/PageLeaderBoard/Backup";
                public string ReciveBackup => "https://localhost:44346/PageLeaderBoard/ReciveBackup";
                public string RemoveBackup => "https://localhost:44346/PageLeaderBoard/RemoveBackup";
                public string DownloadBackup => "https://localhost:44346/PageLeaderBoard/DownloadBackup";
                public string Leaderboard => "https://localhost:44346/PageLeaderBoard/Leaderboard";
            }

            public struct PageLog
            {
                public string LinkReciveLog => "https://localhost:44346/Log/ReciveLogs";
            }

            public struct PageDashboard
            {
                public string LinkDashboard => "https://localhost:44346/PageDashboard/ReciveDetail";
            }
        }
    }
}

