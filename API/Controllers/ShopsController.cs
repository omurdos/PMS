using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core;
using Core.Entities;
using AutoMapper;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public ShopsController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Shops
        [HttpGet]
        public async Task<ActionResult<ICollection<ShopDTO>>> GetShops()
        {
          if (_context.Shops == null)
          {
              return NotFound();
          }
          var ShopsList = await _context.Shops.ToListAsync();
            return _mapper.Map<List<ShopDTO>>(ShopsList);
        }

        // GET: api/Shops/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShopDTO>> GetShop(string id)
        {
          if (_context.Shops == null)
          {
              return NotFound();
          }
            var shop = await _context.Shops.FindAsync(id);

            if (shop == null)
            {
                return NotFound();
            }

            return _mapper.Map<ShopDTO>(shop);
        }

        // PUT: api/Shops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShop(string id, ShopDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            var shop = _context.Shops.Find(id);
            _mapper.Map(dto, shop);
            _context.Entry(shop).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShopExists(id))
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

        // POST: api/Shops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shop>> PostShop(ShopDTO dto)
        {
          if (_context.Shops == null)
          {
              return Problem("Entity set 'DBContext.Shops'  is null.");
          }
            var shop = _mapper.Map<Shop>(dto);
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetShop", new { id = shop.Id }, _mapper.Map<ShopDTO>(shop));
        }

        // DELETE: api/Shops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop(string id)
        {
            if (_context.Shops == null)
            {
                return NotFound();
            }
            var shop = await _context.Shops.FindAsync(id);
            if (shop == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShopExists(string id)
        {
            return (_context.Shops?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
