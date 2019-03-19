function ContractAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadContractAP = function () {

        $scope.AllStatus = [
            { value: 1, name: "Activated" },
            { value: 2, name: "Not Activated" }
        ];


        xhrService.get("GetListContract/").then(function (data) {
            $scope.AllContractAp = data.data;
        }, function (error) {

        });
    }
}

app.controller('ContractAPCtrl', ContractAPCtrl);
