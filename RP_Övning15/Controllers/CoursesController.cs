using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RP_Ovning15.Core.Dto;
using RP_Ovning15.Core.Entities;
using RP_Ovning15.Core.Repositories;
using RP_Ovning15.Data.Data;
using RP_Ovning15.Data.Repositories;
using AutoMapper;

namespace RP_Ovning15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        //private readonly LmsApiContext db;
        private readonly UnitofWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public CoursesController(LmsApiContext context, IMapper mapper)
        {
            //db = context;
            _unitOfWork = new UnitofWork(context);
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var courses = await _unitOfWork.CourseRepository.GetAllCourses();
            var dto = _mapper.Map<IEnumerable<CourseDto>>(courses);

            return Ok(dto);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourse(id);
            var dto = _mapper.Map<CourseDto>(course);
            return Ok(dto); 
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto dto)
        {

            var course = _mapper.Map<Course>(dto);
            course.Id = id;
            _unitOfWork.CourseRepository.Update(course);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(NoContent());
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseDto dto)
        {

            var course = _mapper.Map<Course>(dto);
            _unitOfWork.CourseRepository.Add(course);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            
           
            var course = await _unitOfWork.CourseRepository.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }

            _unitOfWork.CourseRepository.Remove(course);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        private bool CourseExists(int id)
        {
            return _unitOfWork.CourseRepository.AnyAsync(id).Result;
        }
    }
}
