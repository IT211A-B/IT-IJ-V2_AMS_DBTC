window.Teacher = (function () {

    const BASE_URL = 'https://localhost:7177/api/Teacher';

    // GET ALL
    function getAll(success, error) {
        $.ajax({
            url: BASE_URL,
            type: 'GET',
            success: success,
            error: error
        });
    }

    // GET BY ID
    function getById(id, success, error) {
        $.ajax({
            url: BASE_URL + '/' + id,
            type: 'GET',
            success: success,
            error: error
        });
    }

    // CREATE
    function create(payload, success, error) {
        $.ajax({
            url: BASE_URL,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: success,
            error: error
        });
    }

    // EDIT
    function edit(id, payload, success, error) {
        $.ajax({
            url: BASE_URL + '/' + id,
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(payload),
            success: success,
            error: error
        });
    }

    // DELETE
    function remove(id, success, error) {
        $.ajax({
            url: BASE_URL + '/' + id,
            type: 'DELETE',
            success: success,
            error: error
        });
    }

    return {
        getAll: getAll,
        getById: getById,
        create: create,
        edit: edit,
        remove: remove
    };

})();