﻿using Dal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Dal.Interfaces
{
    public interface ICustomer :IDalCrud<Customer>
    {

        Task<Customer> GetByIdAsync(int customerId);
        Task<List<StatusCodeUser>> GetCustomerStatusAsync();
    }
}
