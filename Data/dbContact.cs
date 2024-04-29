using Microsoft.EntityFrameworkCore;
using PR4_ToDoList_U.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR4_ToDoList_U.Data
{
    internal class dbContact : DbContext
    {
        public DbSet<NewTask> newTasks { get; set; }
        public DbSet<ReadyTask> ReadyTasks { get;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ToDoListdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
