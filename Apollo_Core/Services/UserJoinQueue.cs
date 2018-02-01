using Discord;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscordBot_Core.Services
{
    public class UserJoinQueue : IEnumerable<IUser>
    {
        private List<IUser> _users = new List<IUser>();
        private readonly int _maxSize = 50;

        public IEnumerable<IUser> Users => _users.ToList();

        public void Enqueue(IUser user)
        {
            var oldUser = _users.FirstOrDefault(x => x.Id == user.Id);
            if (oldUser != null)
            {
                _users.Remove(oldUser);
            }
            _users.Add(user);

            if (_users.Count > _maxSize)
            {
                var firstUser = _users.FirstOrDefault();
                _users.Remove(firstUser);
            }
        }

        public IUser Peek()
        {
            return _users.FirstOrDefault();
        }

        public IUser Dequeue()
        {
            var user = _users.FirstOrDefault();
            _users.Remove(user);
            return user;
        }

        public void Remove(IUser user)
        {
            if (_users.Contains(user))
            {
                _users.Remove(user);
            }
        }

        public IEnumerator<IUser> GetEnumerator()
        {
            return ((IEnumerable<IUser>)_users).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
