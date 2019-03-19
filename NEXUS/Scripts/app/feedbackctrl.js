function FeedbackCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }
    var user = JSON.parse(localStorage.getItem('user'));
    $scope.loadFeedbackAP = function () {
        $scope.name = user.FullName;
        $scope.email = user.Email;
    }

    $scope.sendFb = function () {
        var dataSend = {
            "CustomerName": $scope.name,
            "CustomerEmail": $scope.email,
            "Status": 1,
            "Note": $scope.note
        }
        xhrService.post("CreateFeedback", dataSend).then(function (data) {
            toastr.success("Thank you for your comments", 'Success');
            setTimeout(() => {
                    window.location.href = "/";
            }, 2000);
        }, function (error) {
          
        });
    }  
}

app.controller('FeedbackCtrl', FeedbackCtrl);
