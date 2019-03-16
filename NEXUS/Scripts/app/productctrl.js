function ProductCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadProduct = function () {
        xhrService.get("GetListProduct").then(function (data) {
            $scope.dataProduct = data.data;
           
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }
}
app.controller('ProductCtrl', ProductCtrl);
