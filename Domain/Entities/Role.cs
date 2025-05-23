﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public List<UserRole> UserRoles { get; set; } = [];
    }
}
