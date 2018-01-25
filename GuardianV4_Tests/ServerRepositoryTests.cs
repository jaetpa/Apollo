using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using GuardianV4_Data.Entities;
using GuardianV4_Repository;
using GuardianV4_Repository.Unit_of_Work;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using GuardianV4_Data.Contexts;

namespace GuardianV4_Tests
{

    [TestFixture]
    public class ServerRepositoryTests
    {
        IServiceProvider _services;

        [Test]
        public void Add_ValidServerEntity_AddedToDatabase()
        {
            //Arrange
            var server = new ServerEntity { Id = 3434145463624262 };
            _services.GetRequiredService<DiscordBotContext>();
            var uow = _services.GetRequiredService<UnitOfWork>();
            //Act
            uow.Servers.Add(server);
            uow.SaveChanges();

            //Assert
            Assert.Contains(server, uow.Context.Servers.ToList());
        }
    }



}
