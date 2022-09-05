using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RP_Ovning15.Core.Dto;
using RP_Ovning15.Core.Entities;
using RP_Ovning15.Data.Data;
using RP_Ovning15.Data.Repositories;

namespace RP_Ovning15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        //private readonly LmsApiContext db;
        private readonly UnitofWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModulesController(LmsApiContext context, IMapper mapper)
        {
            //db = context;
            _unitOfWork = new UnitofWork(context);
            _mapper = mapper;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var module = await _unitOfWork.ModuleRepository.GetAllModules();
            var dto = _mapper.Map<IEnumerable<ModuleDto>>(module);

            return Ok(dto);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModule(int id)
        {
            var module = await _unitOfWork.ModuleRepository.GetModule(id);
            var dto = _mapper.Map<ModuleDto>(module);
            return Ok(dto);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto dto)
        {
            var module = _mapper.Map<Module>(dto);
            module.Id = id;
            _unitOfWork.ModuleRepository.Update(module);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Module>> PostModule(ModuleDto dto)
        {
            var module = _mapper.Map<Module>(dto);
            _unitOfWork.ModuleRepository.Add(module);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetModule", new { id = module.Id }, module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            var module = await _unitOfWork.ModuleRepository.GetModule(id);
            if (module == null)
            {
                return NotFound();
            }

            _unitOfWork.ModuleRepository.Remove(module);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _unitOfWork.ModuleRepository.AnyAsync(id).Result;
        }
    }
}
