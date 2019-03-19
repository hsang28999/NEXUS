function FeedbackdetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadFeedbackdetailAP = function () {

        $scope.AllStatus = [
            {
                value: 1, name: "Waiting for progressing" },
                { value: 2, name: "Processing" },
                { value: 3, name: "Processed" }
            ];

        myEl.html("Feedback");
        $scope.inpPass = true;
        $scope.btnAdd = true;
        xhrService.get("GetFeedbackDetail/" + id).then(function (data) {
            $scope.name = data.data.CustomerName;
            $scope.email = data.data.CustomerEmail;
            $scope.status = data.data.Status;
        }, function (error) {

        });
    }

    $scope.Update = function () {
        var gend = document.getElementById('gender');
        var dataSend = {
            "Status": $scope.status
        }
        xhrService.get("GetFeedbackDetail/" + id).then(function (data) {
            xhrService.post("SaveFeedback/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/feedback";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn ?", 'Loi');
    }
}

app.controller('FeedbackdetailAPCtrl', FeedbackdetailAPCtrl);
