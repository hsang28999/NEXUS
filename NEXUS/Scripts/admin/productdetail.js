function ProductdetailAPCtrl($scope, $rootScope, $stateParams, $location, $timeout, xhrService, $anchorScroll) {
    var id = $stateParams.id;
    myEl = angular.element(document.querySelector('.header2'));
    toastr.options = {
        "positionClass": "toast-bottom-right",
    }

    $scope.loadProductdetailAP = function () {

        $scope.AllType = [
            { value: 1, name: "Prepaid By Hour"},
            { value: 2, name: "Unlimited Prepaid" },
            { value: 3, name: "Postpaid" }
        ];

        xhrService.get("GetListConnectionGroup").then(function (data) {
            $scope.AllGroup = data.data;
        }, function (error) {

            });

        if (id == undefined || id == null || id == "") {
            $scope.start = new Date();
            $scope.btnUpdate = true;
            $scope.btnAdd = false;
            myEl.html("New Contract");
        }
        else {
            $scope.btnAdd = true;
            $scope.disuser = true;
            $scope.btnUpdate = false;
            myEl.html("Edit Product");
            xhrService.get("GetProductDetail/" + id).then(function (data) {
                console.log(data.data.ConnectionGroupId);
                $scope.name = data.data.ProductName;
                $scope.des = data.data.Description;
                $scope.price = data.data.Price;
                $scope.type = data.data.TimeType;
                $scope.time = data.data.TimeType;
                $scope.timeused = data.data.TimeUsed;
                $scope.ppmlocal = data.data.PpmLocal;
                $scope.ppmstd = data.data.PpmStd;
                $scope.ppmmobile = data.data.PpmMobile;
                $scope.status = data.data.Status;
                $scope.group = data.data.ConnectionGroupId;
                $scope.month = data.data.MonthAvailable;
            }, function (error) {

            });
        }
    }

    $scope.Update = function () {
        console.log($scope.type);
        var gend = document.getElementById('gender');
        var dataSend = {
            "ConnectionGroupId": $scope.group,
            "TimeType": $scope.type,
            "PpmMobile": $scope.ppmmobile,
            "PpmStd": $scope.ppmstd,
            "TimeUsed": $scope.timeused,
            "Type": 1,
            "Description": $scope.des,
            "MonthAvailable": $scope.month,
            "PpmLocal": $scope.ppmlocal,
            "Price": $scope.price,
            "ProductName": $scope.name,
            "Status": 1
        }
        xhrService.get("GetProductDetail/" + id).then(function (data) {
            xhrService.post("SaveProductDetail/" + id, dataSend).then(function (data) {
                toastr.success('Save done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/product";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }, function (error) {
            toastr.error(error.statusText, 'Error');
        });
    }

    $scope.Add = function () {
        var date = new Date($scope.start).getTime() / 1000;
        var dataSend = {
            "ConnectionGroupId": $scope.group,
            "TimeType": $scope.type,
            "PpmMobile": $scope.ppmmobile,
            "PpmStd": $scope.ppmstd,
            "TimeUsed": $scope.timeused,
            "Type": 1,
            "Description": $scope.des,
            "MonthAvailable": $scope.month,
            "PpmLocal": $scope.ppmlocal,
            "Price": $scope.price,
            "ProductName": $scope.name,
            "Status": 1
        };
        if ($scope.group == undefined || $scope.type == undefined
            || $scope.timeused == undefined || $scope.des == undefined || $scope.month == undefined
            || $scope.price == undefined || $scope.name == undefined) {
            $scope.msgErr = true;
        }
        else {
            $scope.msgErr = false;
            xhrService.post("CreateProductDetail", dataSend).then(function (data) {
                toastr.success('Add new product done !', 'Success');
                setTimeout(() => {
                    window.location.href = "admin/product";
                }, 2000);
            }, function (error) {
                toastr.error(error.statusText, 'Error');
            });
        }
    }

    $scope.Delete = function () {
        toastr.error("dAnG phAt tRiEn ?", 'Loi');
    }
}

app.controller('ProductdetailAPCtrl', ProductdetailAPCtrl);
