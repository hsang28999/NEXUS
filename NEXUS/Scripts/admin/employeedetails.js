function EmployeedetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadEmployeedetailAP = function () {

        xhrService.get("GetListStore").then(function (data) {
            $scope.AllStore = data.data;
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });

        $scope.AllRole = [
            { value: 1, name: "Customer" },
            { value: 2, name: "Sales" },
            { value: 3, name: "Engineer" },
            { value: 4, name: "Manager" },
            { value: 5, name: "Admin" }
        ];

        $scope.AllGender = [
            { value: 1, name: "Boy" },
            { value: 2, name: "Girl" }
        ];

        if (id == undefined || id == null || id == "") {
            $scope.btnUpdate = true;
            $scope.inpAdr = true;
            $scope.inpGen = true;
            $scope.inpStore = true;
            $scope.inpPhone = false;
            myEl.html("New Employee");
        }
        else {
            myEl.html("Edit Employee");
            $scope.inpPhone = true;
            $scope.inpPass = true;
            $scope.btnAdd = true;
            xhrService.get("GetEmployeeDetail/" + id).then(function (data) {
                $scope.fullname = data.data.FullName;
                $scope.email = data.data.Email;
                $scope.phone = data.data.PhoneNumber;
                $scope.gender = data.data.Gender;
                $scope.address = data.data.Address;
                $scope.role = data.data.Role;
                $scope.store = data.data.Store.StoreId;
            }, function (error) {

            });
        }
    }

    $scope.Update = function () {
        var dataSend = {
            "Email": $scope.email,
            "FullName": $scope.fullname,
            "Gender": $scope.gender,
            "Address": $scope.address,
            "Birthday": "",
            "Role": $scope.role,
            "StoreId": $scope.store
        }
        xhrService.get("GetEmployeeDetail/" + id).then(function (data) {
            xhrService.post("SaveEmployeeDetail/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/employee";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.Add = function () {
        var dataSend = {
            "PhoneNumber": $scope.phone,
            "Email": $scope.email,
            "Password": $scope.password,
            "FullName": $scope.fullname,
            "Role": $scope.role
        };
        if ($scope.phone == undefined || $scope.email == undefined
            || $scope.password == undefined || $scope.fullname == undefined || $scope.role == undefined) {
            $scope.msgErr = true;
        }
        else {
            $scope.msgErr = false;
            xhrService.post("RegisterAdmin", dataSend).then(function (data) {
                toastr.success('Add new customer done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/employee";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn?", 'Loi');
    }
}

app.controller('EmployeedetailAPCtrl', EmployeedetailAPCtrl);
