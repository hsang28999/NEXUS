function UserAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.AllRole = [{ value: 1, name: "Customer" },
    { value: 2, name: "Sales" },
    { value: 3, name: "Engineer" },
    { value: 4, name: "Manager" },
    { value: 5, name: "Admin" }];
    $scope.loadUserAP = function () {
        xhrService.get("GetListUser/1").then(function (data) {
            $scope.AllUserAp = data.data.data;
        }, function (error) {
           
        });
    } 
}

app.controller('UserAPCtrl', UserAPCtrl);
