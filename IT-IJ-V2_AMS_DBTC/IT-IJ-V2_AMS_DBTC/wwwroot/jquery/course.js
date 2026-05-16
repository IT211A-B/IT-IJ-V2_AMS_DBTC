$(document).ready(function () {

    // ── Create Modal: reset fields on open ──────────────────
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

    // ── Edit button: fetch course and populate modal ────────
    $('.btn-edit').on('click', function () {
        var id = $(this).data('id');
        Course.getById(id, function (c) {
            $('#editId').val(c.courseId);
            $('#editCourseId').val(c.courseId);
            $('#editCourseName').val(c.courseCode);
            $('#editCourseDescription').val(c.description);
            $('#editError').text('').hide();
            $('#editModal').modal('show');
        });
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

    // ── Delete button: populate delete modal ────────────────
    $('.btn-delete').on('click', function () {
        $('#deleteId').val($(this).data('id'));
        $('#deleteCourseId').text($(this).data('id'));
        $('#deleteCourseName').text($(this).data('code'));
        $('#deleteCourseDescription').text($(this).data('description'));
        $('#deleteModal').modal('show');
    });

    // ── Confirm delete ──────────────────────────────────────
    $('#confirmDeleteBtn').on('click', function () {
        Course.remove($('#deleteId').val(), function () {
            $('#deleteModal').modal('hide');
        });
    });

});