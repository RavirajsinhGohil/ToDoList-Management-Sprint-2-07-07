
$(document).ready(function () {
    viewModel = new LeaveViewModel();
    ko.applyBindings(viewModel);
    ko.validation.init({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        errorClass: 'text-danger',
         // Example CSS class for errors
    });
    let columns = [
        {
            name: "No.",
            data: "",
            title: "Name",
            orderable: true,
            searchable: true,
            type: 'num',
        },
        {
            name: "Start Date",
            data: "startDate",
            title: "Start Date",
            orderable: true,
            searchable: true,
            type: 'num',
        },
        {
            name: "End Date",
            data: "endDate",
            title: "End Date",
            orderable: true,
            searchable: false,
            type: 'num',
        },
        {
            name: "Duration",
            data: "duration",
            title: "Duration",
            orderable: false,
            searchable: false,
            type: 'num',
        },
        {
            name: "Return Date",
            data: "returnDate",
            title: "Return Date",
            orderable: false,
            searchable: false,
            type: 'num'
        },
        {
            name: "Available On Phone",
            data: "returnDate",
            title: "Return Date",
            orderable: false,
            searchable: false,
            type: 'num'
        },
        {
            name: "Approved Date",
            data: "returnDate",
            title: "Return Date",
            orderable: false,
            searchable: false,
            type: 'num'
        },
        {
            name: "Status",
            data: "returnDate",
            title: "Status",
            orderable: false,
            searchable: false,
            type: 'num'
        },
        {
            name: "Actions",
            data: "returnDate",
            title: "Return Date",
            orderable: false,
            searchable: false,
            render: function (data, type, row) {
                let actionButtons = '';

                actionButtons += `
                    <a class="btn btn-info btn-sm text-white" href="/Employee/GetEmployeeById?employeeId=${row.employeeId}">
                        <i class="fas fa-pencil-alt"></i> Edit
                    </a>
                    <a class="btn btn-danger btn-sm" onclick="showDeleteEmployeeModal('${row.employeeId}')">
                        <i class="fas fa-trash"></i> Delete
                    </a>
                `;

                return actionButtons || `<span class="text-muted">No Actions</span>`;
            }
        },
    ]
    // initializeDataTable("#leaveTable", "/Leave/GetLeaves", columns);

    $("#addLeaveModal").on('hidden.bs.modal', function () {
        $("#addLeaveForm").trigger("reset");
        $("#addLeaveForm").find(".text-danger").text("");
    });
});