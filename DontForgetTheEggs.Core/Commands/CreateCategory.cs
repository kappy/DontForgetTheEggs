﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShortBus;

namespace DontForgetTheEggs.Core.Commands
{
    public class CreateCategory : IAsyncRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
