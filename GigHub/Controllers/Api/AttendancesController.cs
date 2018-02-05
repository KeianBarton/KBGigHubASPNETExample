using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var attendanceAlreadyExists = _context.Attendances
                .Any(a => a.AttendeeId == userId
                        && a.GigId == dto.GigId);
            if (attendanceAlreadyExists)
                return BadRequest("The attendance already exists.");

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);
  
            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();

            return Ok(id);
        }
    }
}
