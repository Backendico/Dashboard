using RestSharp;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.GlobalElement
{
    public static class SettingUser
    {
        public static string Token;
        public static BsonDocument CurentDetailStudio;


        /// <summary>
        /// Cheack token player with search TOken in backend
        /// </summary>
        /// <param name="TokenPlayer"></param>
        /// <returns></returns>
        public static async Task<bool> CheackTokenPlayer(ObjectId TokenPlayer)
        {
            var client = new RestClient("https://localhost:44346/PagePlayer/SearchToken");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("Token", Token);
            request.AddParameter("TokenPlayer", TokenPlayer.ToString());
            request.AddParameter("Studio", CurentDetailStudio["Database"].ToString());
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
