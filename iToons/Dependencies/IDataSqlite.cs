using System;
using iToons.Data;
using iToons.Library.Entity;
using iToons.Providers;
using iToons.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iToons.Dependencies
{
    public class IDataSqlite: IDataRepository
    {
        public void MigrateDatabase()
        {
            using var sql = new SqliteContext();
            // sql.Database.Migrate();
            sql.MetaDatas.RemoveRange(sql.MetaDatas);
            sql.Database.ExecuteSqlRaw("UPDATE SQLITE_SEQUENCE SET SEQ=0 WHERE NAME='MetaDatas'");
            // sql.Database.EnsureCreated();
        }

        public void AddMetaData(MetaData meta)
        {
            using var sql = new SqliteContext();
            sql.MetaDatas.Add(meta);
            sql.SaveChanges();
        }

        public void ClearMetaData()
        {
            using var sql = new SqliteContext();
            sql.MetaDatas.RemoveRange(sql.MetaDatas.ToList());
            sql.SaveChanges();
        }

        public MetaData GetMetaData(int id)
        {
            // random song selection
            using var sql = new SqliteContext();
            if (id == 0)
            {
                // random song selection
                int total = sql.MetaDatas.Count();
                Random random = new Random();
                int offset = random.Next(0, total);
                return sql.MetaDatas.FirstOrDefault(q => q.Id == offset);
            }
            return sql.MetaDatas.FirstOrDefault(q => q.Id == id);
        }

        public Task<List<User>> GetAllUsers()
        {
            using var sql = new SqliteContext();
            if (!sql.Users.Any())
            {
                CryptoServiceProvider csp = new CryptoServiceProvider();
                List<User> users = new List<User>
                {
                    new User { FirstName = "Michael", LastName = "Rinderle", Email = "michael@sofdigital.net", PasswordHash = csp.CreatePasswordHash("Password01")},
                };

                sql.Users.AddRange(users);
                sql.SaveChanges();
                return Task.FromResult(users);
            }
            else
            {
                return Task.FromResult(sql.Users.ToList());
            }
        }
    }
}