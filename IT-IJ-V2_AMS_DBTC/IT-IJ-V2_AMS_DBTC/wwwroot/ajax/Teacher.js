var Teacher = {

    // ── READ: Load all teachers into the table ──────────────
    loadAll: function () {
        $.ajax({
            url: '/api/teacher/getall',
            type: 'GET',
            success: function (data) {
                var tbody = $('#teacherTableBody');
                tbody.empty();

                var filtered = data;
                var search = $('#searchInput').val().toLowerCase();
                if (search) {
                    filtered = data.filter(function (t) {
                        return (t.firstName + ' ' + t.lastName).toLowerCase().includes(search)
                            || t.email.toLowerCase().includes(search);
                    });
                }

                if (filtered.length === 0) {
                    tbody.append('<tr><td colspan="4" class="text-center">No teachers found.</td></tr>');
                    return;
                }

                $.each(filtered, function (i, t) {
                    tbody.append(
                        '<tr>' +
                        '<td>' + t.teacherId + '</td>' +
                        '<td>' + t.firstName + ' ' + t.lastName + '</td>' +
                        '<td>' + t.email + '</td>' +
                        '<td>' +
                        '<button class="btn btn-sm btn-warning btn-edit me-1" ' +
                        'data-id="' + t.teacherId + '">Edit</button>' +
                        '<button class="btn btn-sm btn-danger btn-delete" ' +
                        'data-id="' + t.teacherId + '" ' +
                        'data-number="' + t.teacherId + '" ' +
                        'data-name="' + t.firstName + ' ' + t.lastName + '">Delete</button>' +
                        '</td>' +
                        '</tr>'
                    );
                });

                // Re-bind action buttons after table re-render
                Teacher.bindActionButtons();
            },
            error: function () {
                $('#teacherTableBody').html(
                    '<tr><td colspan="4" class="text-center text-danger">Failed to load teachers.</td></tr>'
                );
            }
        });
    },

    // ── READ ONE: Get teacher by ID ─────────────────────────
    getById: function (id, onSuccess) {
        $.ajax({
            url: '/api/teacher/get/' + id,
            type: 'GET',
            success: function (data) {
                onSuccess(data);
            },
            error: function () {
                alert('Failed to fetch teacher details.');
            }
        });
    },

    // ── CREATE ──────────────────────────────────────────────
    create: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/teacher/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Teacher.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to create teacher.';
                onError(msg);
            }
        });
    },

    // ── EDIT / UPDATE ───────────────────────────────────────
    edit: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/teacher/update/' + payload.teacherId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Teacher.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to update teacher.';
                onError(msg);
            }
        });
    },

    // ── DELETE ──────────────────────────────────────────────
    remove: function (id, onSuccess) {
        $.ajax({
            url: '/api/teacher/delete/' + id,
            type: 'DELETE',
            success: function () {
                onSuccess();
                Teacher.loadAll();
            },
            error: function () {
                alert('Failed to delete teacher.');
            }
        });
    },

    // ── Bind edit/delete buttons (called after table render) ─
    bindActionButtons: function () {
        $('.btn-edit').off('click').on('click', function () {
            var id = $(this).data('id');
            Teacher.getById(id, function (t) {
                $('#editId').val(t.teacherId);
                $('#editTeacherNumber').val(t.teacherId);
                $('#editFirstName').val(t.firstName);
                $('#editLastName').val(t.lastName);
                $('#editEmail').val(t.email);
                $('#editModal').modal('show');
            });
        });

        $('.btn-delete').off('click').on('click', function () {
            $('#deleteId').val($(this).data('id'));
            $('#deleteTeacherNumber').text($(this).data('number'));
            $('#deleteTeacherName').text($(this).data('name'));
            $('#deleteModal').modal('show');
        });
    }
};

// ============================================================
// DOM Ready — Wire up all UI events
// ============================================================
$(document).ready(function () {

    // Initial load
    Teacher.loadAll();

    // Live search
    $('#searchInput').on('input', function () {
        Teacher.loadAll();
    });

    // ── Create Modal: reset on open ─────────────────────────
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

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        var id = $('#deleteId').val();
        Teacher.remove(id, function () {
            $('#deleteModal').modal('hide');
        });
    });

});