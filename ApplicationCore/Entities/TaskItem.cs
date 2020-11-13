﻿using System;
using ApplicationCore.Common;

namespace ApplicationCore.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}