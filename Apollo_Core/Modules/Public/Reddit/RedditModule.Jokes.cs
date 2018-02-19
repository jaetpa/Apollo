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
        [Command("jp", RunMode = RunMode.Async)]
        [Summary("Gets a random joke from r/jokes and posts to default channel")]
        [Remarks("!jp")]
        public async Task JokeInDefaultChannel([Remainder] string topic = null)
        {
            var mainChannel = Context.Guild.GetMainChannel();

            if (mainChannel == null)
            {
                await ReplyAsync("Main channel has not been set.");
                return;
            }
            try
            {
                await mainChannel.SendMessageAsync(await GetValidJoke(topic));
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("There was a problem reaching Reddit servers. Please try again later.");
            }
        }


        [Command("joke", RunMode = RunMode.Async)]
        [Summary("Gets a random post from r/jokes")]
        [Remarks("!joke")]
        public async Task GetJoke([Remainder] string topic = null)
        {
            var rand = new Random();
            try
            {
                var links = await SearchReddit(subreddit: "jokes", topic: topic);
                await ReplyAsync((links.ElementAt(rand.Next(links.Count())) as Link).Title);
            }
            catch (HttpRequestException)
            {
                await ReplyAsync("There was a problem reaching Reddit servers. Please try again later.");
                return;
            }
        }

        private async Task<string> GetValidJoke(string topic = null)
        {
            var links = await SearchReddit(subreddit: "jokes", topic: topic);

            string joke;

            var rand = new Random();
            Link jokeLink = (links.ElementAtOrDefault(rand.Next(links.Count())) as Link);

            if (jokeLink == null)
            {
                return "No jokes found.";
            }

            do
            {
                joke = jokeLink.Title + "\n\n" + jokeLink.SelfText;
            } while (joke.Length > 2000);

            return joke;
        }
    }
}
