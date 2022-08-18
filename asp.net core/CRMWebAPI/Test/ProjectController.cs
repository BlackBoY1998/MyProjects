using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMWebAPI.Repository;
//using CRMWebAPI.Models;
using System.Threading.Tasks;

namespace CRMWebAPI.Controllers
{
    public class ProjectController : ApiController
    {
    //    private readonly IProjectRepository _iprojectRepository = new ProjectRepository();

    //    [HttpGet]
    //    [Route("api/Project/Get")]
    //    public async Task<IEnumerable<ProjectMaster>> Get()
    //    {
    //        return await _iprojectRepository.GetProjects();
    //    }

    //    [HttpPost]
    //    [Route("api/Project/Create")]
    //    public async Task CreateAsync([FromBody] ProjectMaster projectDetails)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _iprojectRepository.Add(projectDetails);
    //        }
    //    }

    //    [HttpGet]
    //    [Route("api/Project/Details/{id}")]
    //    public async Task<ProjectMaster> Details(int id)
    //    {
    //        var result = await _iprojectRepository.GetProject(id);
    //        return result;
    //    }

    //    [HttpPut]
    //    [Route("api/Project/Edit")]
    //    public async Task EditAsync([FromBody] ProjectMaster projectDetails)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            await _iprojectRepository.Update(projectDetails);
    //        }
    //    }

    //    [HttpDelete]
    //    [Route("api/Project/Delete/{id}")]
    //    public async Task DeleteConfirmedAsync(int id)
    //    {
    //        await _iprojectRepository.Delete(id);
    //    }
    }
}
