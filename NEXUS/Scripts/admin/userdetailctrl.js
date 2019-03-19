function UserdetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadUserdetailAP = function () {
        if (id == undefined || id == null || id == "") {
            $scope.btnUpdate = true;
            $scope.inpAdr = true;
            $scope.inpGen = true;
            myEl.html("New Customer");
        }
        else {
            myEl.html("Edit Customer");
            $scope.inpPass = true;
            $scope.btnAdd = true;
            xhrService.get("GetUserProfile/" + id).then(function (data) {
                $scope.fullname = data.data.FullName;
                $scope.email = data.data.Email;
                $scope.phone = data.data.PhoneNumber;
                $scope.gender = data.data.Gender;
                $scope.address = data.data.Address;
            }, function (error) {
  
            });
        }
    }

    $scope.Update = function () {
        var gend = document.getElementById('gender');
        var dataSend = {
            "Email": $scope.email,
            "FullName": $scope.fullname,
            "Gender": gend.value,
            "Address": $scope.address
        }
        xhrService.get("GetUserProfile/" + id).then(function (data) {
            xhrService.post("SaveUserProfile/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/user"; 
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
            "FullName": $scope.fullname
        };
        if ($scope.phone == undefined || $scope.email == undefined
            || $scope.password == undefined || $scope.fullname == undefined) {
            $scope.msgErr = true;
        }
        else {
            $scope.msgErr = false;
            xhrService.post("Register", dataSend).then(function (data) {
                toastr.success('Add new customer done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/user";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }   
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn ?", 'Loi');
    }
}

app.controller('UserdetailAPCtrl', UserdetailAPCtrl);
