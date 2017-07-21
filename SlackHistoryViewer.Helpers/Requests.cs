using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using SlackHistoryViewer.Configuration;

namespace RemoteMessages
{
    public class Requests
    {

        public static async Task<string> RequestChannelsList()
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string> {
                  { "token", AppSettings.Instance.SlackToken } };
                var encodedParameters = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(
                    AppSettings.Instance.ChannelsListUrl, 
                    encodedParameters);

                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> RequestUsersList()
        {
            using (var client = new HttpClient())
            {

                var parameters = new Dictionary<string, string> {
                  { "token", AppSettings.Instance.SlackToken } };
                var encodedParameters = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(
                    AppSettings.Instance.UsersListUrl, 
                    encodedParameters);

                return await response.Content.ReadAsStringAsync();
            }
        }


        public static async Task<string> RequestChannelsHistory(string channelId)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string> {
                  { "token", AppSettings.Instance.SlackToken },
                { "channel", channelId} };

                var encodedParameters = new FormUrlEncodedContent(parameters);
                var response = await client.PostAsync(
                    AppSettings.Instance.ChannelsHistoryUrl, 
                    encodedParameters);

                if (response.IsSuccessStatusCode == false)
                    throw new Exception();

                return await response.Content.ReadAsStringAsync();
            }
        }

    }
}
