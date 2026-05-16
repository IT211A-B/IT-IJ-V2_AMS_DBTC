var Student = {

    // ── READ: Load all students into the table ──────────────
    loadAll: function () {
        $.ajax({
            url: '/api/student/getall',
            type: 'GET',
            success: function (data) {
                var tbody = $('#studentTableBody');
                tbody.empty();

                var filtered = data;
                var search = $('#searchInput').val().toLowerCase();
                if (search) {
                    filtered = data.filter(function (s) {
                        return (s.firstName + ' ' + s.lastName).toLowerCase().includes(search)
                            || s.email.toLowerCase().includes(search);
                    });
                }

                if (filtered.length === 0) {
                    tbody.append('<tr><td colspan="4" class="text-center">No students found.</td></tr>');
                    return;
                }

                $.each(filtered, function (i, s) {
                    tbody.append(
                        '<tr>' +
                        '<td>' + s.studentId + '</td>' +
                        '<td>' + s.firstName + ' ' + s.lastName + '</td>' +
                        '<td>' + s.email + '</td>' +
                        '<td>' +
                        '<button class="btn btn-sm btn-warning btn-edit me-1" ' +
                        'data-id="' + s.studentId + '">Edit</button>' +
                        '<button class="btn btn-sm btn-danger btn-delete" ' +
                        'data-id="' + s.studentId + '" ' +
                        'data-number="' + s.studentId + '" ' +
                        'data-name="' + s.firstName + ' ' + s.lastName + '">Delete</button>' +
                        '</td>' +
                        '</tr>'
                    );
                });

                // Re-bind action buttons after table re-render
                Student.bindActionButtons();
            },
            error: function () {
                $('#studentTableBody').html(
                    '<tr><td colspan="4" class="text-center text-danger">Failed to load students.</td></tr>'
                );
            }
        });
    },

    // ── READ ONE: Get student by ID ─────────────────────────
    getById: function (id, onSuccess) {
        $.ajax({
            url: '/api/student/get/' + id,
            type: 'GET',
            success: function (data) {
                onSuccess(data);
            },
            error: function () {
                alert('Failed to fetch student details.');
            }
        });
    },

    // ── CREATE ──────────────────────────────────────────────
    create: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/student/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Student.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to create student.';
                onError(msg);
            }
        });
    },

    // ── EDIT / UPDATE ───────────────────────────────────────
    edit: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/student/update/' + payload.studentId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Student.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to update student.';
                onError(msg);
            }
        });
    },

    // ── DELETE ──────────────────────────────────────────────
    remove: function (id, onSuccess) {
        $.ajax({
            url: '/api/student/delete/' + id,
            type: 'DELETE',
            success: function () {
                onSuccess();
                Student.loadAll();
            },
            error: function () {
                alert('Failed to delete student.');
            }
        });
    },

    // ── Bind edit/delete buttons (called after table render) ─
    bindActionButtons: function () {
        $('.btn-edit').off('click').on('click', function () {
            var id = $(this).data('id');
            Student.getById(id, function (s) {
                $('#editId').val(s.studentId);
                $('#editStudentNumber').val(s.studentId);
                $('#editFirstName').val(s.firstName);
                $('#editLastName').val(s.lastName);
                $('#editEmail').val(s.email);
                $('#editModal').modal('show');
            });
        });

        $('.btn-delete').off('click').on('click', function () {
            $('#deleteId').val($(this).data('id'));
            $('#deleteStudentNumber').text($(this).data('number'));
            $('#deleteStudentName').text($(this).data('name'));
            $('#deleteModal').modal('show');
        });
    }
};

// ============================================================
// DOM Ready — Wire up all UI events
// ============================================================
$(document).ready(function () {

    // Initial load
    Student.loadAll();

    // Live search
    $('#searchInput').on('input', function () {
        Student.loadAll();
    });

    // ── Create Modal: reset on open ─────────────────────────
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

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        var id = $('#deleteId').val();
        Student.remove(id, function () {
            $('#deleteModal').modal('hide');
        });
    });

});