using Discord.Commands;
using DiscordBot_Core.Attributes;
using DiscordBot_Core.Services;
using DiscordBot_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot_Core.Modules.Public.Quotes
{
    public class QuoteModule : ModuleBase<SocketCommandContext>
    {

        readonly DatabaseService _db;
        static readonly Random _rand = new Random();

        public QuoteModule(DatabaseService db)
        {
            _db = db;
        }
        [RequireHelperRole]
        [Command("addquote")]
        public async Task AddQuote(string username, [Remainder] string quote)
        {
            if (quote.Length > 1000)
            {
                await ReplyAsync("You can only addquotes of up to 1000 characters.");
                return;
            }

            using (var uow = _db.UnitOfWork)
            {
                uow.Quotes.Add(new QuoteEntity { Name = username, Quote = quote });
                uow.SaveChanges();
            }

            await ReplyAsync($"Quote added for **{username}**: *{quote}*");
        }

        [Command("quote")]
        public async Task ShowQuote(string username)
        {
            using (var uow = _db.UnitOfWork)
            {
                var quotes = uow.Quotes.Find(x => x.Name.ToUpper() == username.ToUpper());
                if (quotes.Count() <= 0)
                {
                    await ReplyAsync($"No quotes found for name: **{username}**");
                    return;
                }

                var quote = quotes.ElementAt(_rand.Next(quotes.Count()));

                await ReplyAsync(":speech_balloon: " + quote.Quote);
            }

        }

        [Command("quoteall")]
        public async Task ShowAllQuotes(string username)
        {
            using (var uow = _db.UnitOfWork)
            {
                var quotes = uow.Quotes.Find(x => x.Name.ToUpper() == username.ToUpper());
                if (quotes.Count() <= 0)
                {
                    await ReplyAsync($"No quotes found for name: **{username}**");
                    return;
                }

                List<string> pages = new List<string>();

                pages = PaginateQuotes(quotes, username);

                foreach (var page in pages)
                {
                    await ReplyAsync(page);
                }
            }

        }

        private List<string> PaginateQuotes(IEnumerable<QuoteEntity> quotes, string username)
        {
            List<string> pages = new List<string>();
            string page = $"Showing **{quotes.Count()}** quotes for user **{username}**:\n\n";


            foreach (var quote in quotes)
            {
                if ((page + quote.Quote).Length < 2000)
                {
                    page += ":speech_balloon: " + quote.Quote + "\n";
                }
                else
                {
                    pages.Add(page);
                    page += ":speech_balloon: " + quote.Quote + "\n";
                }
            }
            pages.Add(page);

            return pages;
        }
    }
}