﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UserManagement_WebAPI.Models;

namespace UserManagement_WebAPI.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private SchoolDBContext context;

        public UserRepository(SchoolDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return context.Users.ToList();
        }

        public User GetUserByID(int id)
        {
            return context.Users.Find(id);
        }

        public User GetUserByAccount(string email, string password)
        {
            return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void InsertUser(User user)
        {
            context.Users.Add(user);
        }

        public void DeleteUser(int studentID)
        {
            User user = context.Users.Find(studentID);
            context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        public bool ExistUser(int userID)
        {
            return context.Users.Any(e => e.UserId == userID);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
