﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RP_Ovning15.Core.Entities;
using RP_Ovning15.Core.Repositories;
using RP_Ovning15.Data.Data;
using RP_Ovning15.Data.Repositories;

namespace RP_Ovning15.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        //private readonly LmsApiContext db;
        private readonly UnitofWork _unitOfWork;
        
        public CoursesController(LmsApiContext context)
        {
            //db = context;
            _unitOfWork = new UnitofWork(context);
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            var course = await _unitOfWork.CourseRepository.GetAllCourses();

            //return await db.Course.Include(e => e.Modules).ToListAsync();
            return Ok(course);
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourse(id);

            return Ok(course); 
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

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

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
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
