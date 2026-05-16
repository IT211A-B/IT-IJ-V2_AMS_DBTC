$(document).ready(function () {

    // ── Create Modal: reset fields on open ──────────────────
    $('#createModal').on('show.bs.modal', function () {
        $('#createStudentNumber').val('');
        $('#createFirstName').val('');
        $('#createLastName').val('');
        $('#createEmail').val('');
        $('#createError').text('').hide();
    });

    // ── Save new student ────────────────────────────────────
    $('#saveCreateBtn').on('click', function () {
        var firstName = $('#createFirstName').val().trim();
        var lastName = $('#createLastName').val().trim();
        var email = $('#createEmail').val().trim();

        if (!firstName || !lastName || !email) {
            $('#createError').text('Please fill in all required fields.').show();
            return;
        }
        $('#createError').hide();

        Student.create(
            { firstName: firstName, lastName: lastName, email: email },
            function () { $('#createModal').modal('hide'); },
            function (msg) { $('#createError').text(msg).show(); }
        );
    });

    // ── Edit button: fetch student and populate modal ───────
    $('.btn-edit').on('click', function () {
        var id = $(this).data('id');
        Student.getById(id, function (s) {
            $('#editId').val(s.studentId);
            $('#editStudentNumber').val(s.studentId);
            $('#editFirstName').val(s.firstName);
            $('#editLastName').val(s.lastName);
            $('#editEmail').val(s.email);
            $('#editError').text('').hide();
            $('#editModal').modal('show');
        });
    });

    // ── Save edited student ─────────────────────────────────
    $('#saveEditBtn').on('click', function () {
        var studentId = $('#editId').val();
        var firstName = $('#editFirstName').val().trim();
        var lastName = $('#editLastName').val().trim();
        var email = $('#editEmail').val().trim();

        if (!firstName || !lastName || !email) {
            $('#editError').text('Please fill in all required fields.').show();
            return;
        }
        $('#editError').hide();

        Student.edit(
            { studentId: Number(studentId), firstName: firstName, lastName: lastName, email: email },
            function () { $('#editModal').modal('hide'); },
            function (msg) { $('#editError').text(msg).show(); }
        );
    });

    // ── Delete button: populate delete modal ────────────────
    $('.btn-delete').on('click', function () {
        $('#deleteId').val($(this).data('id'));
        $('#deleteStudentNumber').text($(this).data('number'));
        $('#deleteStudentName').text($(this).data('name'));
        $('#deleteModal').modal('show');
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        Student.remove($('#deleteId').val(), function () {
            $('#deleteModal').modal('hide');
        });
    });

});