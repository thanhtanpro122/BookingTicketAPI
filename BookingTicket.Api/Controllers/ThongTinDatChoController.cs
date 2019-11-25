using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookingTicket.Entities.Context;
using BookingTicket.Entities.Models;

namespace BookingTicket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongTinDatChoController : ControllerBase
    {
        private readonly BookingTicketContext _context;

        public ThongTinDatChoController(BookingTicketContext context)
        {
            _context = context;
        }

        // GET: api/ThongTinDatCho
        [HttpGet]
        public IEnumerable<ThongTinDatCho> GetThongTinDatChos()
        {
            return _context.ThongTinDatChos;
        }

        // GET: api/ThongTinDatCho/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThongTinDatCho([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var thongTinDatCho = await _context.ThongTinDatChos.FindAsync(id);

            if (thongTinDatCho == null)
            {
                return NotFound();
            }

            return Ok(thongTinDatCho);
        }

        // PUT: api/ThongTinDatCho/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThongTinDatCho([FromRoute] long id, [FromBody] ThongTinDatCho thongTinDatCho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thongTinDatCho.MaDatCho)
            {
                return BadRequest();
            }

            _context.Entry(thongTinDatCho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThongTinDatChoExists(id))
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

        // POST: api/ThongTinDatCho
        [HttpPost]
        public async Task<IActionResult> PostThongTinDatCho([FromBody] ThongTinDatCho thongTinDatCho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ThongTinDatChos.Add(thongTinDatCho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThongTinDatCho", new { id = thongTinDatCho.MaDatCho }, thongTinDatCho);
        }

        // DELETE: api/ThongTinDatCho/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteThongTinDatCho([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var thongTinDatCho = await _context.ThongTinDatChos.FindAsync(id);
            if (thongTinDatCho == null)
            {
                return NotFound();
            }

            _context.ThongTinDatChos.Remove(thongTinDatCho);
            await _context.SaveChangesAsync();

            return Ok(thongTinDatCho);
        }

        private bool ThongTinDatChoExists(long id)
        {
            return _context.ThongTinDatChos.Any(e => e.MaDatCho == id);
        }
    }
}