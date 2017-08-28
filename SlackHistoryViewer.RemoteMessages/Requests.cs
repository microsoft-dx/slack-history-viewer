using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SlackHistoryViewer.Configuration;

namespace RemoteMessages
{
    internal class Requests
    {
        private Requests()
        {

        }

        public static async Task<string> RequestChannelsList()
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "token", AppSettings.Instance.SlackToken }
                };

                var response = await client.PostAsync(
                    AppSettings.Instance.ChannelsListUrl,
                    new FormUrlEncodedContent(parameters));

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error in getting channel lists !");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> RequestUsersList()
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "token", AppSettings.Instance.SlackToken }
                };

                var response = await client.PostAsync(
                    AppSettings.Instance.UsersListUrl,
                    new FormUrlEncodedContent(parameters));

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error in getting users history !");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }


        public static async Task<string> RequestChannelsHistory(string channelId)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
                {
                    { "token", AppSettings.Instance.SlackToken },
                    { "channel", channelId }
                };

                var response = await client.PostAsync(
                    AppSettings.Instance.ChannelsHistoryUrl,
                    new FormUrlEncodedContent(parameters));

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error in getting channels history !");
                }

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
