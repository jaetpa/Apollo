using Discord.Commands;
using DiscordBot_Core.Attributes;
using DiscordBot_Core.Extensions;
using RedditNet;
using RedditNet.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Modules.Public.Reddit
{
    public partial class RedditModule
    {
        [RequireModeratorRole]
        [Command("ap", RunMode = RunMode.Async)]
        [Summary("Gets a random post from r/askreddit and posts to default channel")]
        [Remarks("!ap")]
        public async Task AskRedditInDefaultChannel([Remainder] string topic = null)
        {
            var mainChannel = Context.Guild.GetMainChannel();

            if (mainChannel == null)
            {
                await ReplyAsync("Main channel has not been set.");
                return;
            }
            try
            {
                var links = await SearchReddit(subreddit: "askreddit", topic: topic);
                var rand = new Random();
                await mainChannel.SendMessageAsync((links.ElementAt(rand.Next(links.Count())) as Link).Title);
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("There was a problem reaching Reddit servers. Please try again later.");
            }
        }


        [Command("askreddit", RunMode = RunMode.Async)]
        [Summary("Gets a random post from r/askreddit")]
        [Remarks("!askreddit")]
        public async Task AskReddit([Remainder] string topic = null)
        {
            var rand = new Random();
            try
            {
                var links = await SearchReddit(subreddit: "askreddit", topic: topic);
                await ReplyAsync((links.ElementAt(rand.Next(links.Count())) as Link).Title);
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("There was a problem reaching Reddit servers. Please try again later.");
                return;
            }
        }

        [RequireModeratorRole]
        [Command("askreddit", RunMode = RunMode.Async)]
        [Summary("Gets a random post from r/askreddit")]
        [Remarks("!askreddit")]
        public async Task AskReddit(int count = 1, [Remainder] string topic = null)
        {
            try
            {
                var links = await SearchReddit(subreddit: "askreddit", topic: topic);
                List<Link> randomLinks = new List<Link>();
                var rand = new Random();
                int attempts = 0;

                do
                {
                    randomLinks.Add(links.ElementAtOrDefault(rand.Next(links.Count())) as Link);
                    attempts++;
                } while (randomLinks.Count < count && (attempts < 50));


                StringBuilder stringBuilder = new StringBuilder();

                foreach (var link in randomLinks)
                {
                    stringBuilder.Append((link as Link).Title + "\n");
                }

                await ReplyAsync(stringBuilder.ToString());
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("There was a problem reaching Reddit servers. Please try again later.");
                return;
            }
        }

    }
}
