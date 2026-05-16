var Course = {

    // ── READ: Load all courses into the table ───────────────
    loadAll: function () {
        $.ajax({
            url: '/api/course/getall',
            type: 'GET',
            success: function (data) {
                var tbody = $('#courseTableBody');
                tbody.empty();

                var filtered = data;
                var search = $('#searchInput').val().toLowerCase();
                if (search) {
                    filtered = data.filter(function (c) {
                        return c.courseCode.toLowerCase().includes(search)
                            || c.description.toLowerCase().includes(search);
                    });
                }

                if (filtered.length === 0) {
                    tbody.append('<tr><td colspan="4" class="text-center">No courses found.</td></tr>');
                    return;
                }

                $.each(filtered, function (i, c) {
                    tbody.append(
                        '<tr>' +
                        '<td>' + c.courseId + '</td>' +
                        '<td>' + c.courseCode + '</td>' +
                        '<td>' + c.description + '</td>' +
                        '<td>' +
                        '<button class="btn btn-sm btn-warning btn-edit me-1" ' +
                        'data-id="' + c.courseId + '">Edit</button>' +
                        '<button class="btn btn-sm btn-danger btn-delete" ' +
                        'data-id="' + c.courseId + '" ' +
                        'data-code="' + c.courseCode + '" ' +
                        'data-description="' + c.description + '">Delete</button>' +
                        '</td>' +
                        '</tr>'
                    );
                });

                // Re-bind action buttons after table re-render
                Course.bindActionButtons();
            },
            error: function () {
                $('#courseTableBody').html(
                    '<tr><td colspan="4" class="text-center text-danger">Failed to load courses.</td></tr>'
                );
            }
        });
    },

    // ── READ ONE: Get course by ID ──────────────────────────
    getById: function (id, onSuccess) {
        $.ajax({
            url: '/api/course/get/' + id,
            type: 'GET',
            success: function (data) {
                onSuccess(data);
            },
            error: function () {
                alert('Failed to fetch course details.');
            }
        });
    },

    // ── CREATE ──────────────────────────────────────────────
    create: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/course/create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Course.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to create course.';
                onError(msg);
            }
        });
    },

    // ── EDIT / UPDATE ───────────────────────────────────────
    edit: function (payload, onSuccess, onError) {
        $.ajax({
            url: '/api/course/update/' + payload.courseId,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: function () {
                onSuccess();
                Course.loadAll();
            },
            error: function (xhr) {
                var msg = (xhr.responseJSON && xhr.responseJSON.message)
                    ? xhr.responseJSON.message
                    : 'Failed to update course.';
                onError(msg);
            }
        });
    },

    // ── DELETE ──────────────────────────────────────────────
    remove: function (id, onSuccess) {
        $.ajax({
            url: '/api/course/delete/' + id,
            type: 'DELETE',
            success: function () {
                onSuccess();
                Course.loadAll();
            },
            error: function () {
                alert('Failed to delete course.');
            }
        });
    },

    // ── Bind edit/delete buttons (called after table render) ─
    bindActionButtons: function () {
        $('.btn-edit').off('click').on('click', function () {
            var id = $(this).data('id');
            Course.getById(id, function (c) {
                $('#editId').val(c.courseId);
                $('#editCourseId').val(c.courseId);
                $('#editCourseName').val(c.courseCode);
                $('#editCourseDescription').val(c.description);
                $('#editModal').modal('show');
            });
        });

        $('.btn-delete').off('click').on('click', function () {
            $('#deleteId').val($(this).data('id'));
            $('#deleteCourseId').text($(this).data('id'));
            $('#deleteCourseName').text($(this).data('code'));
            $('#deleteCourseDescription').text($(this).data('description'));
            $('#deleteModal').modal('show');
        });
    }
};

// ============================================================
// DOM Ready — Wire up all UI events
// ============================================================
$(document).ready(function () {

    // Initial load
    Course.loadAll();

    // Live search
    $('#searchInput').on('input', function () {
        Course.loadAll();
    });

    // ── Create Modal: reset on open ─────────────────────────
    $('#createModal').on('show.bs.modal', function () {
        $('#createCourseId').val('');
        $('#createCourseName').val('');
        $('#createCourseDescription').val('');
        $('#createError').text('').hide();
    });

    // ── Save new course ─────────────────────────────────────
    $('#saveCreateBtn').on('click', function () {
        var courseCode = $('#createCourseName').val().trim();
        var description = $('#createCourseDescription').val().trim();

        if (!courseCode || !description) {
            $('#createError').text('Please fill in all required fields.').show();
            return;
        }
        $('#createError').hide();

        Course.create(
            { courseCode: courseCode, description: description },
            function () { $('#createModal').modal('hide'); },
            function (msg) { $('#createError').text(msg).show(); }
        );
    });

    // ── Save edited course ──────────────────────────────────
    $('#saveEditBtn').on('click', function () {
        var courseId = $('#editId').val();
        var courseCode = $('#editCourseName').val().trim();
        var description = $('#editCourseDescription').val().trim();

        if (!courseCode || !description) {
            $('#editError').text('Please fill in all required fields.').show();
            return;
        }
        $('#editError').hide();

        Course.edit(
            { courseId: Number(courseId), courseCode: courseCode, description: description },
            function () { $('#editModal').modal('hide'); },
            function (msg) { $('#editError').text(msg).show(); }
        );
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        var id = $('#deleteId').val();
        Course.remove(id, function () {
            $('#deleteModal').modal('hide');
        });
    });

});