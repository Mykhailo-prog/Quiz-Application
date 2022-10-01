﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizProject.Functions;
using QuizProject.Models;
using QuizProject.Models.DTO;

namespace QuizProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly TestLogic _testLogic;
        private readonly QuizContext _context;

        public TestsController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<Test>>> GetTests()
        {
            _context.Statistics.Load();
            _context.Questions.Include(q => q.Answers).Load();
            return  await _context.Tests.Include(t => t.UserCreatedTest).ToListAsync();
        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);

            if (!_testLogic.ElemExists<Test>(id))
            {
                return NotFound();
            }

            return test;
        }

        // PUT: api/Tests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest(int id, TestDTO testdto)
        {
            try
            {
                if (!_testLogic.ElemExists<Test>(id))
                {
                    return BadRequest();
                }

                var test = await _context.Tests.FindAsync(id);

                test.Name = testdto.Name;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_testLogic.ElemExists<Test>(id))
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

        // POST: api/Tests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Test>> PostTest(TestDTO testdto)
        {
            var test = new Test
            {
                Name = testdto.Name,
            };
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return Ok(test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestDTO>> DeleteTest(int id)
        {
            _context.Questions.Include(q => q.Answers).Load();
            var test = await _context.Tests.FirstAsync(t => t.TestId == id);
            if (!_testLogic.ElemExists<Test>(id))
            {
                return NotFound();
            }

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return ModelsToDto.TestToDTO(test);
        }
    }
}
