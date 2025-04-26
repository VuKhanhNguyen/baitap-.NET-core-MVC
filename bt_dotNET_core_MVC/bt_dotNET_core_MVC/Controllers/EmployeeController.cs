using bt_dotNET_core_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bt_dotNET_core_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly StaffManagementContext _context;

        public EmployeeController(StaffManagementContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Employee()
        {
            var employees118 = await _context.Employees
                .Include(e => e.Department) // Lấy thông tin phòng ban
                .ToListAsync(); // Lấy tất cả nhân viên
            var departments = await _context.Departments.ToListAsync();
            ViewBag.Departments = departments;
            return View(employees118); 
        }

        //Lấy id cuối cùng và tăng thêm 1 
        public async Task<string> GenerateNextEmployeeIdAsync()
        {
            var lastEmployee118 = await _context.Employees
                .OrderByDescending(e => e.EmployeesId)
                .FirstOrDefaultAsync();

            if (lastEmployee118 == null)
                return "NV001"; // Chưa có nhân viên nào

            string lastId118 = lastEmployee118.EmployeesId; //lấy nv cuối cùng
            int number118 = int.Parse(lastId118.Substring(2)); 
            number118++; 
            return "NV" + number118.ToString("D3"); 
        }

        //Tạo nhân viên
        [HttpPost]
        public async Task<IActionResult> Create118(Employee employee)
        {
            employee.EmployeesId = await GenerateNextEmployeeIdAsync();

            _context.Add(employee);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thêm nhân viên thành công!";
            //return RedirectToAction(nameof(Staff));
            return Ok();
        }

        //Lấy id nhân viên
        [HttpGet]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee118 = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeesId == id);

            if (employee118 == null)
            {
                return NotFound();
            }

            return Json(new
            {
                employee118.EmployeesName,
                employee118.Sdt,
                employee118.Age,
                employee118.Gender,
                employee118.Position,
                employee118.Salary,
                employee118.DepartmentId
            });
        }

        //Chỉnh sửa nhân viên
        [HttpPost]
        public async Task<IActionResult> Edit118(Employee employee118)
        {
            if (ModelState.IsValid)
            {
                _context.Update(employee118);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật nhân viên thành công!";
                return Ok();
            }

            return BadRequest(ModelState);
        }

        //Xem chi tiết nhân viên
        public async Task<IActionResult> Detail118(string id)
        {
            var employee118 = await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeesId == id);

            if (employee118 == null)
            {
                return NotFound();
            }

            return View("~/Views/EmployeeDetail/EmployeeDetail.cshtml", employee118);
        }

        //Xóa nhân viên
        [HttpPost]
        public async Task<IActionResult> Delete118(string id)
        {
            var employee118 = await _context.Employees.FindAsync(id);
            if (employee118 == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee118);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa nhân viên thành công!";
            return RedirectToAction(nameof(Employee));
        }


    }
}
