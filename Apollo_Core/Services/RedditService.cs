using Microsoft.Extensions.Configuration;
using RedditNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot_Core.Services
{
    public class RedditService
    {
        RedditApi _reddit;

        private int myVar;

        public RedditApi RedditApi
        {
            get { return _reddit; }
        }


        public RedditService(IConfiguration configuration)
        {
            _reddit = new RedditApi(new RedditNet.Auth.RedditPasswordAuth(
                configuration["Reddit:ClientId"],
                configuration["Reddit:ClientSecret"],
                configuration["Reddit:RedirectUri"],
                configuration["Reddit:Username"],
                configuration["Reddit:Password"]
            ));
        }
    }
}
