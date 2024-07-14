﻿using AutoMapper;
using Bl.Interfaces;
using Dal;
using Dto.models;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using DTO.Classes;
namespace Service
{
    public class CustomerService : ICustomer
    {
        private DalManager dalManager;
        readonly IMapper mapper;
        public CustomerService(DalManager dalManager)
        {
            this.dalManager = dalManager;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
        }

        public async Task<List<Customers>> ReadAllAsync() => mapper.Map<List<Customers>>(await dalManager.customers.ReadAllAsync());

        public async Task<Customers> CreateAsync(Customers customer)
        {
            try
            {
                var newCustomer = mapper.Map<Dal.Models.Customer>(customer);
                return mapper.Map<Dto.models.Customers>(await dalManager.customers.CreateAsync(newCustomer));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> DeletByEmailAsync(string email)
        {
            try
            {
                List<Dal.Models.Customer> customers = await dalManager.customers.ReadAsync(o => o.Email == email);
                Dal.Models.Customer c = customers.First();
                if (c != null)
                    return await dalManager.customers.DeleteAsync(c.CustomerId);
                else
                    return false;

            }
            catch (Exception ex)
            {

                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                List<Dal.Models.Customer> customers = await dalManager.customers.ReadAsync(o => o.CustomerId == id);
                Dal.Models.Customer c = customers.First();
                if (c != null)
                    return await dalManager.customers.DeleteAsync(c.CustomerId);
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<Customers> GetByIdAsync(int customerId)
        {
            Dal.Models.Customer c = await dalManager.customers.GetByIdAsync(customerId);
            return mapper.Map<Customers>(c);
        }


        public async Task<bool> UpdateAsync(Customers customer)
        {
            var mappedCustomer = mapper.Map<Customers, Dal.Models.Customer>(customer);
            return await dalManager.customers.UpdateAsync(mappedCustomer);
        }
        public async Task<List<Customers>> ReadAsync(Predicate<Customers> filter)
        {
            throw new NotImplementedException();
        }

     
        public async Task<List<StatusCodeUser>> GetCustomerStatusAsync()
        {
            return mapper.Map<List<Dal.Models.StatusCodeUser>, List<StatusCodeUser>>(await dalManager.customers.GetCustomerStatusAsync());
        }

    }
}
