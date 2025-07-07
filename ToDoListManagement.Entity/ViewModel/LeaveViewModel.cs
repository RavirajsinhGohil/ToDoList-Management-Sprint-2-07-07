using System.ComponentModel.DataAnnotations;

namespace ToDoListManagement.Entity.ViewModel;

public class LeaveViewModel
{
    public int? LeaveId { get; set; }

    public int? RequestedUserId { get; set; }

    public int? ApprovalUserId { get; set; }

    [Required(ErrorMessage = Constants.Constants.NameRequiredError)]
    public string? RequestedName { get; set; }

    public string? ApprovalName { get; set; }

    [StringLength(1000)]
    public string? Reason { get; set; }

    [Required(ErrorMessage = Constants.Constants.StartDateRequiredError)]
    public DateTime? StartDate { get; set; }

    [Required(ErrorMessage = Constants.Constants.EndDateRequiredError)]
    public DateTime? EndDate { get; set; }

    public TimeSpan? Duration => EndDate - StartDate;

    public DateTime? ApprovedOn { get; set; }

    public string? Status { get; set; }

    [StringLength(15)]
    public string? PhoneNumber { get; set; }

    [StringLength(15)]
    public string? AlternatePhoneNumber { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsAvailableOnPhone { get; set; }
}
