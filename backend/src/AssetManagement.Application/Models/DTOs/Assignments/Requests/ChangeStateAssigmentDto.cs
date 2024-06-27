﻿using AssetManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Application.Models.DTOs.Assignments.Requests
{
    public class ChangeStateAssigmentDto
    {
        public Guid AssignmentId { get; set; }
        public EnumAssignmentState NewState { get; set; }
    }
}
