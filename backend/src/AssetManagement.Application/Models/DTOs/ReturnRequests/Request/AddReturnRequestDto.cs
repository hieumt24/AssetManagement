﻿using AssetManagement.Domain.Common.Models;
using AssetManagement.Domain.Enums;

namespace AssetManagement.Application.Models.DTOs.ReturnRequests.Request
{
    public class AddReturnRequestDto
    {
        public Guid AssignmentId { get; set; }
        public Guid RequestedBy { get; set; }
        public Guid AcceptedBy { get; set; }
        public DateTime ReturnedDate { get; set; }
        public EnumReturnRequestState ReturnState { get; set; }
    }
}