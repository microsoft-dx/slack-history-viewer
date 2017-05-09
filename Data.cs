using RestoreFromLocal.Models;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace RestoreFromLocal
{
    class Data
    {

        public void truncateAllTables()
        {
            using (var dbContext = new SlackHistoryViewerContext())
            {
                dbContext.Database.ExecuteSqlCommand("DELETE FROM Messages");
                dbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Messages, RESEED, 0)");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM Users");
                dbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Users, RESEED, 0)");
                dbContext.Database.ExecuteSqlCommand("DELETE FROM Channels");
                dbContext.Database.ExecuteSqlCommand("DBCC CHECKIDENT (Channels, RESEED, 0)");

            }
        }

        public void insertUsers(string path)
        {
            string usersFile = path + "\\users.json";
            string text = File.ReadAllText(usersFile);
            var data = JsonConvert.DeserializeObject<List<Member>>(text);

            using (var dbContext = new SlackHistoryViewerContext())
            {
                foreach (Member member in data)
                {
                    Users user = new Users();
                    user.UserId = member.id;
                    user.Name = member.name;
                    dbContext.Users.Add(user);
                }
                dbContext.SaveChanges();
            }
        }

        public void insertChannels(string path)
        {
            string channelsFile = path + "\\channels.json";
            string text = File.ReadAllText(channelsFile);
            var data = JsonConvert.DeserializeObject<List<Channel>>(text);

            using (var dbContext = new SlackHistoryViewerContext())
            {
                foreach (Channel channel in data)
                {
                    Channels ch = new Channels();
                    ch.ChannelId = channel.id;
                    ch.Name = channel.name;
                    dbContext.Channels.Add(ch);
                }
                dbContext.SaveChanges();
            }
        }

        public void addMessage(Message message, string channelId)
        {
            using (var dbContext = new SlackHistoryViewerContext())
            {
                Messages m = new Messages();

                string key = message.user + message.ts;
                string hash = message.GetMd5Hash(key);
                m.MessageId = hash;

                var idUs = dbContext.Users.Where(u => u.UserId == message.user).Select(
                    u => u.Id).FirstOrDefault();
                if (idUs == null)
                {
                    int botId = dbContext.Users.Where(u => u.UserId == "USLACKBOT").Select(
                        u => u.Id).FirstOrDefault();
                    idUs = botId;
                }
                m.UserId = idUs;

                var idCh = dbContext.Channels.Where(c => c.ChannelId == channelId).Select(
                    c => c.Id).FirstOrDefault();
                m.ChannelId = idCh;

                m.JsonData = JsonConvert.SerializeObject(message);
            
                dbContext.Messages.Add(m);
                dbContext.SaveChanges();
            }

        }

        public void insertMessages(string path)
        {
            using (var dbContext = new SlackHistoryViewerContext())
            {

                string[] files = Directory.GetFiles(@path, "*.*", SearchOption.AllDirectories);
                foreach (string f in files)
                {
                    if (f.EndsWith("users.json") || f.EndsWith("channels.json"))
                    {
                        continue;
                    }

                    string[] words = f.Split('\\');
                    string channel = words[words.Length - 2];
                    var channelId = dbContext.Channels.Where(c => c.Name == channel).Select(
                        c => c.ChannelId).FirstOrDefault();
                    string text = File.ReadAllText(f);
                    var data = JsonConvert.DeserializeObject<List<Message>>(text);

                    foreach (Message message in data)
                    {
                        addMessage(message, channelId);
                    }
                }
            }

        }

    }
}
