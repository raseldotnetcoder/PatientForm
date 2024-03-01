
$Patient = {
    BindPatientEvents: function () {

        // NCD Mapping
        $(document).on('click', '.add-ncds-to-selected-list', function (e) {
            e.preventDefault();

            var RightNcdList = $('#RightNcdList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var leftNcdList = $('#LeftNcdList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var selectedList = $('#LeftNcdList option:selected').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var data = {
                SelectedNcdList: selectedList,
                RightNcdList: RightNcdList,
                LeftNcdList: leftNcdList
            };

            if (selectedList && selectedList.length > 0) {
                $.ajax({
                    url: '/Patient/Patients/MapNcdToNcdDetail',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        $('#ncd-listbox').html(data);
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            } else {
                alert('Please select at least one item.');
            }
        });

        $(document).on('click', '.remove-ncds-to-selected-list', function (e) {
            e.preventDefault();

            var RightNcdList = $('#RightNcdList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var leftNcdList = $('#LeftNcdList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var selectedList = $('#RightNcdList option:selected').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var data = {
                SelectedNcdList: selectedList,
                RightNcdList: RightNcdList,
                LeftNcdList: leftNcdList
            };

            if (selectedList && selectedList.length > 0) {
                $.ajax({
                    url: '/Patient/Patients/RemoveNcdToNcdDetail',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        console.log(data);
                        $('#ncd-listbox').html(data);
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            } else {
                alert('Please select at least one item.');
            }
        });

        // Allergie Mapping
        $(document).on('click', '.add-allergies-to-selected-list', function (e) {
            e.preventDefault();

            var RightAllergieList = $('#RightAllergieList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var leftAllergieList = $('#LeftAllergieList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var selectedList = $('#LeftAllergieList option:selected').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var data = {
                SelectedAllergieList: selectedList,
                RightAllergieList: RightAllergieList,
                LeftAllergieList: leftAllergieList
            };

            if (selectedList && selectedList.length > 0) {
                $.ajax({
                    url: '/Patient/Patients/MapAllergieToAllergieDetail',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        $('#allergie-listbox').html(data);
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            } else {
                alert('Please select at least one item.');
            }
        });

        $(document).on('click', '.remove-allergies-to-selected-list', function (e) {
            e.preventDefault();

            var RightAllergieList = $('#RightAllergieList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var leftAllergieList = $('#LeftAllergieList option').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var selectedList = $('#RightAllergieList option:selected').map(function () {
                return { Id: parseInt($(this).val(), 10), Name: $(this).text() };
            }).get();

            var data = {
                SelectedAllergieList: selectedList,
                RightAllergieList: RightAllergieList,
                LeftAllergieList: leftAllergieList
            };

            if (selectedList && selectedList.length > 0) {
                $.ajax({
                    url: '/Patient/Patients/RemoveAllergieToAllergieDetail',
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        console.log(data);
                        $('#allergie-listbox').html(data);
                    },
                    error: function (error) {
                        console.error(error);
                    }
                });
            } else {
                alert('Please select at least one item.');
            }
        });

        $(document).on('submit', '#patient-info-form', function (e) {
            e.preventDefault();

            var patient_name = $('#patient_name').val();
            var disease_id = $('#disease_id').val();
            var epilepsy = $('#epilepsy').val();

            var selectedNcdList = $('#RightNcdList option').map(function () {
                return { Value: $(this).val(), Text: $(this).text() };
            }).get();

            var selectedAllergieList = $('#RightAllergieList option').map(function () {
                return { Value: $(this).val(), Text: $(this).text() };
            }).get();

            var data = {
                PatientName: patient_name,
                DiseaseId: disease_id,
                Epilepsy: epilepsy,
                SelectedNcdList: selectedNcdList,
                SelectedAllergieList: selectedAllergieList
            }

            $.ajax({
                url: '/Patient/Patients/Create',
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response == "Success") {
                        window.location.href = "/Patient/Patients/Index";
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });

        $(document).on('submit', '#patient-info-form-edit', function (e) {
            e.preventDefault();

            var patient_id = $('#patient_id').val();
            var patient_name = $('#patient_name').val();
            var disease_id = $('#disease_id').val();
            var epilepsy = $('#epilepsy').val();

            var selectedNcdList = $('#RightNcdList option').map(function () {
                return { Value: $(this).val(), Text: $(this).text() };
            }).get();

            var selectedAllergieList = $('#RightAllergieList option').map(function () {
                return { Value: $(this).val(), Text: $(this).text() };
            }).get();

            var data = {
                PatientId: patient_id,
                PatientName: patient_name,
                DiseaseId: disease_id,
                Epilepsy: epilepsy,
                SelectedNcdList: selectedNcdList,
                SelectedAllergieList: selectedAllergieList
            }

            $.ajax({
                url: '/Patient/Patients/Edit',
                type: 'POST',
                data: data,
                success: function (response) {
                    if (response == "Success") {
                        window.location.href = "/Patient/Patients/Index";
                    }
                },
                error: function (xhr, status, error) {
                    console.error(error);
                }
            });
        });

        $(document).on('click', '#delete-confirmation', function (e) {
            $('#patient-id').val($(this).data('patient-id'));
            $('#delete-confirmation-modal').modal('show');
        });
    }
}