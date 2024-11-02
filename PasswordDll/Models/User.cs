using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDll.Models
{
    [Table("PassTbl")]
    public class User
    {
        [Key]
        public int UserId { get; set; } // Primary Key
        public string Username { get; set; } // Username for login
        public string Password { get; set; } // Hashed password
    }
    public class UserDbContext : DbContext
    {
        public UserDbContext() { }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }


    }
 }


