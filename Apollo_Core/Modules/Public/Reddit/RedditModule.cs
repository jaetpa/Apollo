using Discord.Commands;
using DiscordBot_Core.Attributes;
using DiscordBot_Core.Extensions;
using DiscordBot_Core.Services;
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
    public partial class RedditModule : ModuleBase<SocketCommandContext>
    {
        RedditService _redditService;
        RedditApi _reddit;
        DatabaseService _db;

        public RedditModule(RedditService redditService, DatabaseService db)
        {
            _redditService = redditService;
            _reddit = _redditService.RedditApi;
            _db = db;
        }

        private async Task<IEnumerable<Thing>> SearchReddit(string subreddit = null, int count = 25, string topic = null)
        {
            IEnumerable<Thing> results;
            if (subreddit != null)
            {
                Listing listing = await _reddit.SearchAsync(new RedditNet.Requests.SearchRequest { Query = $"subreddit:{subreddit} {topic}" });
                results = listing.Take(count);
            }
            else
            {
                Listing listing = await _reddit.SearchAsync(new RedditNet.Requests.SearchRequest { Query = $"" });
                results = listing.Take(count);
            }

            return results;
        }
    }
}
