using bt_dotNET_core_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bt_dotNET_core_MVC.Controllers
{
    public class EmployeeDetailController : Controller
    {
        private readonly StaffManagementContext _context;

        public EmployeeDetailController(StaffManagementContext context118)
        {
            _context = context118;
        }

        public async Task<IActionResult> EmployeeDetail(string id118)
        {
            var employee118 = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeesId == id118);

            if (employee118 == null)
            {
                return NotFound();
            }

            return View(employee118);
        }
    }
}
