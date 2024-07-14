using AutoMapper;
using Bl.Interfaces;
using Dal;
using Dal.Models;
using Dto.models;
using DTO.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace Service
{
    public class ProjectService : IProject
    {
        public DalManager _dalManager;
        public Dal.Interfaces.IProject proj;
        readonly IMapper mapper;
        public ProjectService(  DalManager dal,Dal.Interfaces.IProject p)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CopyRightDProfile>();
            });
            mapper = config.CreateMapper();
            this.proj = p;
            this._dalManager = dal;
        }
        public async Task<Projects> CreateAsync(Projects item)
        {
            return mapper.Map<Dto.models.Projects>(await proj.CreateAsync(mapper.Map<Dal.Models.Project>(item)));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {

                List<Dal.Models.Project> u = await proj.ReadAsync(o => o.ProjectId == id);
                return await proj.DeleteAsync(u.First().ProjectId);
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<Projects>> ReadAsync(Predicate<Projects> filter)
        {
            List<Dto.models.Projects> u = await ReadAllAsync();
            return u.ToList().FindAll(o => filter(o));
        }


        public async Task<List<Dto.models.Projects>> ReadAllAsync() => mapper.Map<List<Dal.Models.Project>, List<Dto.models.Projects>>(await _dalManager.project.ReadAllAsync());

        
        public async Task<bool> UpdateAsync(Projects item) => await proj.UpdateAsync(mapper.Map<Projects, Dal.Models.Project>(item));


        public bool IsOnlyLetters(string input)
        {

            Regex regex = new Regex(@"^[a-zA-Zא-ת]+$");

            return regex.IsMatch(input);
        }
        public async Task<bool> IsOnTheDB(int? id)
        {
            List<Customer> m = await _dalManager.customers.ReadAsync(o => o.CustomerId == id);
            var k = m.FirstOrDefault();
            return m != null;
        }
        public bool IsCorrectDates(DateTime? start, DateTime? end)
        {
            return (start < end);
        }

    }
}

