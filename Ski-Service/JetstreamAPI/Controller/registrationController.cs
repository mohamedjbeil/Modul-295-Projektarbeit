using System;
using System.Collections.Generic;
using System.Linq;
using JetstreamAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace JetstreamAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        // Simulierter In-Memory-Datastore
        private static readonly List<Order> _orders = new List<Order>();

        /// <summary>
        /// Gibt alle Registrierungen zurück.
        /// </summary>
        [HttpGet]
        public IActionResult GetRegistrations()
        {
            return Ok(_orders);
        }

        /// <summary>
        /// Gibt eine Registrierung anhand der ID zurück.
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetRegistrationById(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Fügt eine neue Registrierung hinzu.
        /// </summary>
        [HttpPost]
        public IActionResult AddRegistration([FromBody] Order newOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Simpler ID-Generator für In-Memory-Datenspeicher
            newOrder.OrderID = _orders.Any() ? _orders.Max(o => o.OrderID) + 1 : 1;
            _orders.Add(newOrder);
            return CreatedAtAction(nameof(GetRegistrationById), new { id = newOrder.OrderID }, newOrder);
        }

        /// <summary>
        /// Löscht eine Registrierung anhand der ID.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteRegistration(int id)
        {
            var order = _orders.FirstOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            _orders.Remove(order);
            return NoContent();
        }
    }
}