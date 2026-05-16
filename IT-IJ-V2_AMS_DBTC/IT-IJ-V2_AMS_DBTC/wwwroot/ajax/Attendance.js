var Attendance = {

    // ── READ: Load all attendance records into the table ────
    loadAll: function () {
        $.ajax({
            url: '/api/attendance/getall',
            type: 'GET',
            success: function (data) {
                var tbody = $('#attendanceTableBody');
                tbody.empty();

                var filtered = data;

                // Filter by search input
                var search = $('#searchInput').val().toLowerCase();
                if (search) {
                    filtered = filtered.filter(function (a) {
                        return (a.studentName && a.studentName.toLowerCase().includes(search))
                            || (a.courseCode && a.courseCode.toLowerCase().includes(search));
                    });
                }

                // Filter by status dropdown
                var statusFilter = $('#statusFilter').val();
                if (statusFilter) {
                    filtered = filtered.filter(function (a) {
                        return a.status === statusFilter;
                    });
                }

                if (filtered.length === 0) {
                    tbody.append('<tr><td colspan="7" class="text-center">No attendance records found.</td></tr>');
                    return;
                }

                $.each(filtered, function (i, a) {
                    var statusClass = '';
                    if (a.status === 'Present') statusClass = 'style="color:#2d8a5e;font-weight:600;"';
                    if (a.status === 'Absent') statusClass = 'style="color:#c0392b;font-weight:600;"';
                    if (a.status === 'Late') statusClass = 'style="color:#c97c1a;font-weight:600;"';

                    tbody.append(
                        '<tr>' +
                        '<td>' + a.attendanceId + '</td>' +
                        '<td>' + (a.date ? a.date.split('T')[0] : '') + '</td>' +
                        '<td>' + (a.studentName || '') + '</td>' +
                        '<td>' + (a.courseCode || '') + '</td>' +
                        '<td ' + statusClass + '>' + a.status + '</td>' +
                        '<td>' + (a.remarks || '') + '</td>' +
                        '<td>' +
                        '<button class="btn btn-sm btn-warning btn-edit me-1" ' +
                        'data-id="' + a.attendanceId + '">Edit</button>' +
                        '<button class="btn btn-sm btn-danger btn-delete" ' +
                        'data-id="' + a.attendanceId + '" ' +
                        'data-name="' + (a.studentName || '') + '">Delete</button>' +
                        '</td>' +
                        '</tr>'
                    );
                });

                // Re-bind action buttons after table re-render
                Attendance.bindActionButtons();
            },
            error: function () {
                $('#attendanceTableBody').html(
                    '<tr><td colspan="7" class="text-center text-danger">Failed to load attendance records.</td></tr>'
                );
            }
        });
    },

    // ── READ ONE: Get attendance record by ID ───────────────
    getById: function (id, onSuccess) {
        $.ajax({
            url: '/api/attendance/get/' + id,
            type: 'GET',
            success: function (data) {
                onSuccess(data);
            },
            error: function () {
                alert('Failed to fetch attendance details.');
            }
        });
    },

    // ── Load students into a <select> dropdown ──────────────
    loadStudentDropdown: function (selectId, selectedId) {
        $.ajax({
            url: '/api/student/getall',
            type: 'GET',
            success: function (students) {
                var select = $(selectId);
                select.empty().append('<option value="">Select Student</option>');
                $.each(students, function (i, s) {
                    var option = $('<option>')
                        .val(s.studentId)
                        .text(s.firstName + ' ' + s.lastName);
                    if (selectedId && s.studentId == selectedId) {
                        option.prop('selected', true);
                    }
                    select.append(option);
                });
            },
            error: function () {
                $(selectId).html('<option value="">Failed to load students</option>');
            }
        });
    },

    // ── CREATE ──────────────────────────────────────────────
    create: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/attendance/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Attendance.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to create attendance record.';
                onError(msg);
            }
        });
    },

    // ── EDIT / UPDATE ───────────────────────────────────────
    edit: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/attendance/update/' + payload.attendanceId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Attendance.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to update attendance record.';
                onError(msg);
            }
        });
    },

    // ── DELETE ──────────────────────────────────────────────
    remove: function (id, onSuccess) {
        $.ajax({
            url: '/api/attendance/delete/' + id,
            type: 'DELETE',
            success: function () {
                onSuccess();
                Attendance.loadAll();
            },
            error: function () {
                alert('Failed to delete attendance record.');
            }
        });
    },

    // ── Bind edit/delete buttons (called after table render) ─
    bindActionButtons: function () {
        $('.btn-edit').off('click').on('click', function () {
            var id = $(this).data('id');
            Attendance.getById(id, function (a) {
                $('#editAttendanceId').val(a.attendanceId);
                $('#editDate').val(a.date ? a.date.split('T')[0] : '');
                $('#editStatus').val(a.status);
                $('#editRemarks').val(a.remarks || '');
                // Populate student dropdown then pre-select
                Attendance.loadStudentDropdown('#editStudent', a.studentId);
                $('#editModal').modal('show');
            });
        });

        $('.btn-delete').off('click').on('click', function () {
            $('#deleteAttendanceId').val($(this).data('id'));
            $('#deleteModal').modal('show');
        });
    }
};

// ============================================================
// DOM Ready — Wire up all UI events
// ============================================================
$(document).ready(function () {

    // Initial load
    Attendance.loadAll();

    // Live search
    $('#searchInput').on('input', function () {
        Attendance.loadAll();
    });

    // Status filter
    $('#statusFilter').on('change', function () {
        Attendance.loadAll();
    });

    // ── Create Modal: reset & populate dropdowns on open ────
    $('#createModal').on('show.bs.modal', function () {
        var today = new Date().toISOString().split('T')[0];
        $('#createDate').val(today);
        $('#createStatus').val('');
        $('#createRemarks').val('');
        $('#createError').text('').hide();
        Attendance.loadStudentDropdown('#createStudent', null);
    });

    // ── Save new attendance record ──────────────────────────
    $('#saveAttendanceBtn').on('click', function () {
        var date = $('#createDate').val();
        var studentId = $('#createStudent').val();
        var status = $('#createStatus').val();
        var remarks = $('#createRemarks').val().trim();

        if (!date || !studentId || !status) {
            $('#createError').text('Please fill in all required fields.').show();
            return;
        }
        $('#createError').hide();

        Attendance.create(
            { date: date, studentId: Number(studentId), status: status, remarks: remarks },
            function () { $('#createModal').modal('hide'); },
            function (msg) { $('#createError').text(msg).show(); }
        );
    });

    // ── Save edited attendance record ───────────────────────
    $('#updateAttendanceBtn').on('click', function () {
        var attendanceId = $('#editAttendanceId').val();
        var date = $('#editDate').val();
        var studentId = $('#editStudent').val();
        var status = $('#editStatus').val();
        var remarks = $('#editRemarks').val().trim();

        if (!date || !studentId || !status) {
            $('#editError').text('Please fill in all required fields.').show();
            return;
        }
        $('#editError').hide();

        Attendance.edit(
            { attendanceId: Number(attendanceId), date: date, studentId: Number(studentId), status: status, remarks: remarks },
            function () { $('#editModal').modal('hide'); },
            function (msg) { $('#editError').text(msg).show(); }
        );
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#deleteAttendanceBtn').on('click', function () {
        var id = $('#deleteAttendanceId').val();
        Attendance.remove(id, function () {
            $('#deleteModal').modal('hide');
        });
    });

});