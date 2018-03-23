using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Modules.Public.WebApis
{
    public class WouldYouRather : ModuleBase<SocketCommandContext>
    {

        private class WouldYouRatherQuestion
        {
            public string choicea;
            public string choiceb;
            public string explanation;
            public string link;
            public bool nsfw;
            public string tags;
            public string title;
            public int votes;
        }

        static HttpClient _client = new HttpClient();

        [Command("rather")]
        [Alias("wyr", "wouldyourather")]
        public async Task PostWouldYouRather()
        {
            var response = await _client.GetStringAsync("http://www.rrrather.com/botapi");
            var question = JsonConvert.DeserializeObject<WouldYouRatherQuestion>(response);

            await ReplyAsync($"**{question.title}**:\n\n:regional_indicator_a: {question.choicea}\n:regional_indicator_b: {question.choiceb}");
        }
    }


}
