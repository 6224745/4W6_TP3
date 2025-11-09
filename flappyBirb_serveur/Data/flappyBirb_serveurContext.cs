using flappyBirb_serveur.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flappyBirb_serveur.Data
{
    public class flappyBirb_serveurContext : IdentityDbContext<User>
    {
        public flappyBirb_serveurContext (DbContextOptions<flappyBirb_serveurContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            PasswordHasher<User> hasher = new PasswordHasher<User>();

            User u1 = new User() { Id = "11111111-1111-1111-1111-111111111111", UserName = "Max", Email = "max1@gmail.com", NormalizedEmail = "MAX1@GMAIL.COM", NormalizedUserName = "MAX" };
            u1.PasswordHash = hasher.HashPassword(u1, "Max1!");
            builder.Entity<User>().HasData(u1);

            User u2 = new User() { Id = "11111111-1111-1111-1111-111111111112", UserName = "Eva", Email = "eva48@gmail.com", NormalizedEmail = "EVA48@GMAIL.COM", NormalizedUserName = "EVA" };
            u2.PasswordHash = hasher.HashPassword(u2, "eVa$48$");
            builder.Entity<User>().HasData(u2);

            builder.Entity<Score>().HasData(
            new Score { Id = 1, Pseudo = u1.UserName, ScoreValue = 95, TimeInSeconds = 59.26f, Date = "06-Apr-2025 22:08:06", IsPublic = true, UserId = u1.Id },
            new Score { Id = 2, Pseudo = u1.UserName, ScoreValue = 2, TimeInSeconds = 5.4f, Date = "15-Nov-2025 09:48:26", IsPublic = false, UserId = u1.Id },
            new Score { Id = 3, Pseudo = u2.UserName, ScoreValue = 89, TimeInSeconds = 45.39f, Date = "20-Feb-2025 15:37:00", IsPublic = true, UserId = u2.Id },
            new Score { Id = 4, Pseudo = u2.UserName, ScoreValue = 1, TimeInSeconds = 2.1f, Date = "31-Dec-2025 23:59:59", IsPublic = false, UserId = u2.Id });
        }

        public DbSet<flappyBirb_serveur.Models.Score> Score { get; set; } = default!;
    }
}
