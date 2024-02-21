using HRApplicationAPI.Core.Data.Entities.Common;
using HRApplicationAPI.Core.Data.RepositoryInterfaces;
using HRApplicationAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace HRApplicationAPI.Core.Data.Repositories
{
    public class ProjectDetailRepository : IProjectDetailRepository
    {
        private readonly HrDbContext context;

        public ProjectDetailRepository(HrDbContext context)
        {
            this.context = context;
        }
        public List<ProjectDetail> GetAll()
        {
            try
            {
                return context.ProjectDetails.Include(o => o.ProjectMembers).ThenInclude(o=>o.Employee).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ProjectDetail> GetById(string id)
        {
            try
            {
                return context.ProjectDetails.Where(o => o.ProjectId == Convert.ToInt32(id)).Include(o => o.ProjectMembers).ThenInclude(o => o.Employee).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
