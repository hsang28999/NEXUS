function UserAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadUserAP = function () {
        xhrService.get("GetListUser/1").then(function (data) {
            $scope.AllUserAp = data.data.data;
            console.log(data.data.data.length);
        }, function (error) {
           
        });
    } 
}

app.controller('UserAPCtrl', UserAPCtrl);
