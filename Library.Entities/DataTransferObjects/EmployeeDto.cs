﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
    }

}
