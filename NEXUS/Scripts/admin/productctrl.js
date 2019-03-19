function ProductAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadProductAP = function () {
        //$scope.AllStatus = [
        //    {
        //        value: 1, name: "Waiting for progressing"
        //    },
        //    { value: 2, name: "Processing" },
        //    { value: 3, name: "Processed" }
        //];

        $scope.AllType = [
            { value: 1, name: "Prepaid By Hour" },
            { value: 2, name: "Unlimited Prepaid" },
            { value: 3, name: "Postpaid" }
        ];

        xhrService.get("GetListConnectionGroup").then(function (data) {
            $scope.AllGroup = data.data;
        }, function (error) {

        });

        xhrService.get("GetListProductAdmin/1/").then(function (data) {
            $scope.AllProductAp = data.data.data;
        }, function (error) {

        });
    }
}

app.controller('ProductAPCtrl', ProductAPCtrl);
