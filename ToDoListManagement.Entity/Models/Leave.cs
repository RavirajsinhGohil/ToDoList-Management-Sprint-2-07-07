using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListManagement.Entity.Models;

public class Leave
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LeaveId { get; set; }

    public int? RequestedUserId { get; set; }

    public int? ApprovalUserId { get; set; }

    [StringLength(1000)]
    public string? Reason { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

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

    [ForeignKey("RequestedUserId")]
    public virtual User? RequestedUser { get; set; }

    [ForeignKey("ApprovalUserId")]
    public virtual User? ApprovalUser { get; set; }
}