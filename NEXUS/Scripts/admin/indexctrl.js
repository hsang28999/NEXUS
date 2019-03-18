function IndexCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadIndex = function () {
        $scope.userI = user.FullName; 

        //if (user.Role == 1) {
        //    window.location.href = "/";
        //} 
    }

    $scope.logOut = function () {
        if (confirm('Do you want to logout ?')) {
            localStorage.clear();
            location.reload();
            window.location.href = "/";
        }
    }
}

app.controller('IndexCtrl', IndexCtrl);
