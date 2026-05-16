$(document).ready(function () {

    // ── Create Modal: reset fields on open ──────────────────
    $('#createModal').on('show.bs.modal', function () {
        var today = new Date().toISOString().split('T')[0];
        $('#createDate').val(today);
        $('#createStudent').val('');
        $('#createStatus').val('');
        $('#createRemarks').val('');
        $('#createError').text('').hide();
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

    // ── Edit button: fetch attendance and populate modal ────
    $('.btn-edit').on('click', function () {
        var id = $(this).data('id');
        Attendance.getById(id, function (a) {
            $('#editAttendanceId').val(a.attendanceId);
            $('#editDate').val(a.date ? a.date.split('T')[0] : '');
            $('#editStudent').val(a.studentId);
            $('#editStatus').val(a.status);
            $('#editRemarks').val(a.remarks || '');
            $('#editError').text('').hide();
            $('#editModal').modal('show');
        });
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

    // ── Delete button: populate delete modal ────────────────
    $('.btn-delete').on('click', function () {
        $('#deleteAttendanceId').val($(this).data('id'));
        $('#deleteModal').modal('show');
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#deleteAttendanceBtn').on('click', function () {
        Attendance.remove($('#deleteAttendanceId').val(), function () {
            $('#deleteModal').modal('hide');
        });
    });

});