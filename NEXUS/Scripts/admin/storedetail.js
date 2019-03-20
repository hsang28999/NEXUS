function StoredetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadStoredetailAP = function () {
        $scope.AllStatus = [
            { value: 1, name: "Active" },
            { value: 2, name: "Close" }
        ];

        $scope.AllRole = [
            { value: 1, name: "Customer" },
            { value: 2, name: "Sales" },
            { value: 3, name: "Engineer" },
            { value: 4, name: "Manager" },
            { value: 5, name: "Admin" }
        ];

        if (id == undefined || id == null || id == "") {
            $scope.btnUpdate = true;
            myEl.html("New Store");
        }
        else {
            myEl.html("Edit Store");
            $scope.btnAdd = true;
            xhrService.get("GetStoreDetail/" + id).then(function (data) {
                $scope.name = data.data.Name;
                $scope.address = data.data.Address;
                $scope.status = data.data.Status;
                $scope.ListEmp = data.data.ListUser;
            }, function (error) {

            });
        }
    }

    $scope.Update = function () {
        var dataSend = {
            "Name": $scope.name,
            "Address": $scope.address,
            "Status": $scope.status
        }
        xhrService.get("GetStoreDetail/" + id).then(function (data) {
            xhrService.post("SaveStore/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/shop";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.Add = function () {
        var dataSend = {
            "Name": $scope.name,
            "Address": $scope.address
        };
        if ($scope.name == undefined || $scope.address == undefined) {
            $scope.msgErr = true;
        }
        else {
            $scope.msgErr = false;
            xhrService.post("CreateStore", dataSend).then(function (data) {
                toastr.success('Add new store done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/shop";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn?", 'Loi');
    }
}

app.controller('StoredetailAPCtrl', StoredetailAPCtrl);
