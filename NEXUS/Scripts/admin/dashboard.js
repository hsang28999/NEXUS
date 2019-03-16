function DasCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadDas = function () {
        xhrService.get("GetListUser/1").then(function (data) {
            console.log(user.FullName);
            $scope.userI = user.FullName;
            $scope.data = data.data.data;
            console.log(data.data.data);
            $scope.length = data.data.data.length;
        }, function (error) {

        });
    }
}

app.controller('DasCtrl', DasCtrl);
