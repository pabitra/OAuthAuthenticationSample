﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AAA.API.ViewModels
{
    public class UsersInRoleModel
    {

        public string Id { get; set; }
        public List<string> EnrolledUsers { get; set; }
        public List<string> RemovedUsers { get; set; }
    }
}