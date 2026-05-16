$(document).ready(function () {

    // ── Create Modal: reset fields on open ──────────────────
    $('#createModal').on('show.bs.modal', function () {
        $('#createTeacherNumber').val('');
        $('#createFirstName').val('');
        $('#createLastName').val('');
        $('#createEmail').val('');
        $('#createError').text('').hide();
    });

    // ── Save new teacher ────────────────────────────────────
    $('#saveCreateBtn').on('click', function () {
        var firstName = $('#createFirstName').val().trim();
        var lastName = $('#createLastName').val().trim();
        var email = $('#createEmail').val().trim();

        if (!firstName || !lastName || !email) {
            $('#createError').text('Please fill in all required fields.').show();
            return;
        }
        $('#createError').hide();

        Teacher.create(
            { firstName: firstName, lastName: lastName, email: email },
            function () { $('#createModal').modal('hide'); },
            function (msg) { $('#createError').text(msg).show(); }
        );
    });

    // ── Edit button: fetch teacher and populate modal ───────
    $('.btn-edit').on('click', function () {
        var id = $(this).data('id');
        Teacher.getById(id, function (t) {
            $('#editId').val(t.teacherId);
            $('#editTeacherNumber').val(t.teacherId);
            $('#editFirstName').val(t.firstName);
            $('#editLastName').val(t.lastName);
            $('#editEmail').val(t.email);
            $('#editError').text('').hide();
            $('#editModal').modal('show');
        });
    });

    // ── Save edited teacher ─────────────────────────────────
    $('#saveEditBtn').on('click', function () {
        var teacherId = $('#editId').val();
        var firstName = $('#editFirstName').val().trim();
        var lastName = $('#editLastName').val().trim();
        var email = $('#editEmail').val().trim();

        if (!firstName || !lastName || !email) {
            $('#editError').text('Please fill in all required fields.').show();
            return;
        }
        $('#editError').hide();

        Teacher.edit(
            { teacherId: Number(teacherId), firstName: firstName, lastName: lastName, email: email },
            function () { $('#editModal').modal('hide'); },
            function (msg) { $('#editError').text(msg).show(); }
        );
    });

    // ── Delete button: populate delete modal ────────────────
    $('.btn-delete').on('click', function () {
        $('#deleteId').val($(this).data('id'));
        $('#deleteTeacherNumber').text($(this).data('number'));
        $('#deleteTeacherName').text($(this).data('name'));
        $('#deleteModal').modal('show');
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        Teacher.remove($('#deleteId').val(), function () {
            $('#deleteModal').modal('hide');
        });
    });

});