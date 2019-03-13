function UserProfileCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadUserProfile = function () {
        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            console.log(data);
            angular.element(document.querySelector('.liID')).html(data.data.Id + ".");
            angular.element(document.querySelector('.liFullName')).html(data.data.FullName + ".");
            angular.element(document.querySelector('.liPhoneNumber')).html(data.data.PhoneNumber + ".");
            angular.element(document.querySelector('.liMoney')).html(data.data.Money + " $.");
            angular.element(document.querySelector('.liEmail')).html(data.data.Email + ".");
            angular.element(document.querySelector('.liAddress')).html(data.data.Address + "­­.");
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });

        $scope.ul1 = true;
        $scope.ul2 = false;
        $scope.ul3 = false;
    }

    $scope.showul1 = function () {
        $scope.ul1 = true;
        $scope.ul2 = false;
        $scope.ul3 = false;
    }

    $scope.showul2 = function () {
        xhrService.get("GetUserProfile/" + user.Id).then(function (data) {
            $scope.ID = data.data.Id;
            $scope.FullName = data.data.FullName;
            $scope.PhoneNumber = data.data.PhoneNumber;
            $scope.Money = data.data.Money;
            $scope.Email = data.data.Email;
            $scope.Address = data.data.Address;
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
        $scope.ul1 = false;
        $scope.ul2 = true;
        $scope.ul3 = false;
    }

    $scope.showul3 = function () {
        $scope.ul1 = false;
        $scope.ul2 = false;
        $scope.ul3 = true;
    }
}
app.controller('UserProfileCtrl', UserProfileCtrl);
