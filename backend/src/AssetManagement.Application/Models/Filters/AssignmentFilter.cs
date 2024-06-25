﻿using AssetManagement.Application.Filter;
using AssetManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Application.Models.Filters
{
    public class AssignmentFilter
    {
        public PaginationFilter pagination { get; set; }
        public string? search { get; set; }
        public EnumAssignmentStatus? assignmentStatus { get; set; }

        [DataType(DataType.Date)]
        public DateTime? assignedDate { get; set; }

        public EnumLocation adminLocation { get; set; }
        public string? orderBy { get; set; }
        public bool? isDescending { get; set; } = false;
    }
}