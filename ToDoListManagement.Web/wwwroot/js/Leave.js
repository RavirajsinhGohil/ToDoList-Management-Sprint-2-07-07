function LeaveViewModel() {
    var self = this;
    self.shouldShowAddBtn = ko.observable(window.LeavePermissions.canAddEdit);
    self.shouldShowEditBtn = ko.observable(window.LeavePermissions.canAddEdit);
    self.shouldShowDeleteBtn = ko.observable(window.LeavePermissions.canDelete);
    self.openAddLeaveModal = function () {
        $('#addLeaveModal').modal('show');
        $.validator.unobtrusive.parse("#addLeaveForm");
    };

    self.addLeaveData = {
        requestedName: ko.observable().extend({ required: true, minLength: 2, maxLength: 17 }),
        approvedName: ko.observable().extend({ required: true, minLength: 2, maxLength: 17 }),
        startDate: ko.observable(),
        endDate: ko.observable(),
        duration: ko.observable(),
        returnDate: ko.observable(),
        availableOnPhone: ko.observable(),
        approvedDate: ko.observable()
    };

    self.submitAddLeaveForm = function () {
        if (!$('#addLeaveForm').valid()) {
            return;
        }

        const data = {
            RequestedUserId: $('#addLeaveForm input[name="RequestedUserId"]').val(),
            RequestedName: self.addLeaveData.requestedName(),
            ApprovalName: self.addLeaveData.approvedName(),
            StartDate: self.addLeaveData.startDate(),
            EndDate: self.addLeaveData.endDate(),
        };

        $.post("/leave/add", data, function (returnedData) {
            $('#addLeaveModal').modal('hide');
        });
    };
}