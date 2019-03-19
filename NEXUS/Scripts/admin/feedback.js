function FeedbackAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    $scope.loadFeedbackAP = function () {
        $scope.AllStatus = [
            {
                value: 1, name: "Waiting for progressing"
            },
            { value: 2, name: "Processing" },
            { value: 3, name: "Processed" }
        ];
        xhrService.get("GetListFeedback/").then(function (data) {
            $scope.AllFeedbackAp = data.data.data;
        }, function (error) {

        });
    }
}

app.controller('FeedbackAPCtrl', FeedbackAPCtrl);
