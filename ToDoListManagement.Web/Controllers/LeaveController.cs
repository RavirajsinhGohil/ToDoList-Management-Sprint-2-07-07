using Microsoft.AspNetCore.Mvc;
using ToDoListManagement.Entity.Helper;
using ToDoListManagement.Entity.Models;
using ToDoListManagement.Entity.ViewModel;
using ToDoListManagement.Service.Interfaces;

namespace ToDoListManagement.Web.Controllers;

public class LeaveController : BaseController
{
    private readonly ILeaveService _leaveService;

    public LeaveController(IAuthService authService, ILeaveService leaveService)
        : base(authService)
    {
        _leaveService = leaveService;
    }

    public IActionResult Leave()
    {
        return View();
    }

    public async Task<JsonResult> GetLeaves(int draw, int start, int length, string searchValue, string sortColumn, string sortDirection)
    {
        int pageNumber = start / length + 1;
        int pageSize = length;

        Pagination<LeaveViewModel>? pagination = new()
        {
            SearchKeyword = searchValue,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            SortColumn = sortColumn,
            SortDirection = sortDirection
        };
        Pagination<LeaveViewModel>? data = await _leaveService.GetPaginatedLeavesAsync(pagination);

        return Json(new
        {
            draw,
            recordsTotal = data.TotalRecords,
            recordsFiltered = data.TotalRecords,
            data = data.Items
        });
    }

    [HttpGet]
    public IActionResult AddLeave()
    {
        if (SessionUser == null)
        {
            return RedirectToAction("Login", "Auth");
        }
        LeaveViewModel leaveViewModel = new()
        {
            RequestedUserId = SessionUser.UserId,
            RequestedName = SessionUser.Name
        };

        return PartialView("_AddLeaveModal", leaveViewModel);
    }
}