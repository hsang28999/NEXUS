function StoreAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadStoreAP = function () {
        xhrService.get("GetListStore").then(function (data) {
            $scope.AllStoreAp = data.data;
        }, function (error) {

        });
    }
}

app.controller('StoreAPCtrl', StoreAPCtrl);
