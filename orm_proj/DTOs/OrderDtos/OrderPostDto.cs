﻿using orm_proj.Enums;

namespace orm_proj.DTOs
{
    public class OrderPostDto
    {
        public int UserId { get; set; }
        public int TotalAmount { get; set; }  //Sifarişin ümumi məbləği
        public OrderStatus Status { get; set; }
    }
}
