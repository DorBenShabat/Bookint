﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models.ViewModels
{
	public class OrderNM
	{
		public OrderHeader OrderHeader { get; set; }

		public IEnumerable<OrderDetail> OrderDetail { get; set; }
	}
}
