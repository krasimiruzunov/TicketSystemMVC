﻿using TicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Data
{
    public interface IUowData
    {
        IRepository<Ticket> Tickets { get; }

        IRepository<Category> Categories { get; }

        IRepository<Comment> Comments { get; }

        IRepository<ApplicationUser> Users { get; }

        int SaveChanges();
    }
}
