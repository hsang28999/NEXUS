function UserProfileCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadUserProfile = function () {
        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            $scope.userIF = data.data;
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });

        $scope.showTab = true;
        $scope.showForm = false;
        $scope.showForm2 = false;
    }

    $scope.displayUpdate = function () {
        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            $scope.fullname = data.data.FullName;
            $scope.email = data.data.Email;
            $scope.address = data.data.Address;
            $scope.gender = data.data.Gender;
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });

        $scope.showTab = false;
        $scope.showForm = true;
        $scope.showForm2 = false;
    }

    $scope.Update = function () {
        var gend = document.getElementById('gender');
        var dataSend = {
            "Email": $scope.email,
            "FullName": $scope.fullname,
            "Gender": gend.value,
            "Address": $scope.address
        }
        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            xhrService.post("SaveUserProfile/" + user.Id, dataSend).then(function (data) {
                var scope = angular.element('div[ng-controller="MainCtrl"]').scope();
                $timeout(function () {
                    scope.dispName = $scope.fullname;
                    scope.$apply();
                }, 0);
                user.FullName = $scope.fullname;
                localStorage.setItem('user', JSON.stringify(user));
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "/user-profile";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.displayInfo = function () {

        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            $scope.userIF = data.data;
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });

        $scope.showTab = true;
        $scope.showForm = false;
        $scope.showForm2 = false;
    }

    $scope.displayChangePass = function () {
        $scope.showTab = false;
        $scope.showForm = false;
        $scope.showForm2 = true;
    }
}
app.controller('UserProfileCtrl', UserProfileCtrl);
